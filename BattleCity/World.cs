using BattleCity.Common;
using BattleCity.Entities;
using BattleCity.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace BattleCity
{
    public class World : IWorld
    {
        private readonly IUserFactory _userFactory;

        public ObservableCollection<Entity> Entities { get; } = new ObservableCollection<Entity>();

        private event EventHandler<KeyEventArgs> _keyEvent;

        public ActionBlock<Entity> AddEntity { get; }

        public bool IsGameOver { get; private set; }

        public string Winner { get; private set; }

        public Size Size { get; }

        public World(Size size, List<Entity> entities, IUserFactory userFactory)
        {
            _userFactory = userFactory;

            AddEntity = new ActionBlock<Entity>(
                Entities.Add,
                new ExecutionDataflowBlockOptions
                {
                    BoundedCapacity = 10,
                    MaxDegreeOfParallelism = 1,
                    TaskScheduler = TaskScheduler.Current
                });

            Size = size;

            entities.ForEach(Entities.Add);

            InitUsers();
            ListenKeys();
            StartBonusPlacement();
        }

        private Task ListenKeys() => Task.Run(() =>
        {
            while (true)
            {
                Task.Delay(10).Wait();
                _keyEvent?.Invoke(this, new KeyEventArgs { Key = Console.ReadKey(true).KeyChar });
            }
        });

        private void InitUsers()
        {
            var userIds = _userFactory.UserIds;

            userIds
                .Select(_userFactory.CreateFlag)
                .ToList()
                .ForEach(Entities.Add);

            userIds.ForEach(id => SpawnTank(id));
        }

        private Task SpawnTank(Guid userId) => Task.Run(() =>
        {
            Task.Delay(Constants.TankSpawnTimeMs).Wait();
            var tank = _userFactory.CreateTank(userId);
            _keyEvent += tank.OnKeyEvent;

            AddEntity.Post(tank);
        });

        private Task StartBonusPlacement() => Task.Run(() =>
        {
            while (true)
            {
                Task.Delay(Constants.BonusPlacementMinTimeMs).Wait();
                var rnd = new Random((int)DateTime.UtcNow.Ticks);
                var position = new Position
                {
                    X = rnd.Next(Size.Width),
                    Y = rnd.Next(Size.Height)
                };

                var bonus = DateTime.UtcNow.Ticks % 2 == 0 ? (Entity)new Armor(position) : (Entity)new Attack(position);
                if (Entities.ToList().All(e => !Entity.Collide(e, bonus, true)))
                {
                    AddEntity.Post(bonus);
                }
            }
        });

        private void GameOver(List<Flag> flags)
        {
            IsGameOver = true;

            if(flags.Count == 1)
            {
                Winner = flags[0].UserName;
            }
        }

        private void ReSpawn()
        {
            foreach (var tank in Entities.ToList().Where(e => e is Tank && e.IsDestroyed).Cast<Tank>())
            {
                SpawnTank(tank.UserId);
            }
        }

        public void Update(object source, ElapsedEventArgs e)
        {
            if (IsGameOver)
            {
                return;
            }

            var entities = Entities.ToList();

            // Update entities
            entities.ForEach(entity => entity.Update(this));

            // Check collisions
            for (var i = 0; i < entities.Count; i++)
            {
                var entityA = entities[i];
                for (var j = i + 1; j < entities.Count; j++)
                {
                    Entity.Collide(entityA, entities[j]);
                }
            }

            ReSpawn();

            // Remove destroyed
            for (var i = entities.Count - 1; i >= 0; i--)
            {
                var entity = entities[i];
                if (entity.IsDestroyed)
                {
                    if (entity is Tank tank)
                    {
                        _keyEvent -= tank.OnKeyEvent;
                    }
                    Entities.Remove(entity);
                }
            }

            var flags = entities.Where(e => e is Flag).Cast<Flag>().ToList();
            if (flags.Count < 2)
            {
                GameOver(flags);
            }
        }
    }
}
