using BattleCity.Entities;
using BattleCity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Specialized = System.Collections.Specialized;

namespace BattleCity
{
    public class WorldView
    {
        private readonly World _world;

        private readonly List<IDrawer<Entity>> _views = new List<IDrawer<Entity>>();

        private readonly ViewFactory _viewFactory;

        private volatile bool _isDrawingInProgress;

        public Tuple<char, ConsoleColor>[,] Field { get; private set; }

        public WorldView(World world, ViewFactory viewFactory)
        {
            _world = world;
            _viewFactory = viewFactory;

            _world.Entities.ToList().ForEach(e => _views.Add(_viewFactory.Create(e)));
            _world.Entities.CollectionChanged += Entities_CollectionChanged;

            Field = CreateField();

            Console.SetWindowSize(_world.Size.Width, _world.Size.Height);
            Console.SetBufferSize(_world.Size.Width, _world.Size.Height);
            Console.CursorVisible = false;
            Console.CursorSize = 1;
        }

        private void Entities_CollectionChanged(object sender, Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _views.Add(_viewFactory.Create((Entity)item));
                    }
                    break;
                case Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _views.Remove(_views.First(v => v.Entity == item));
                    }
                    break;
            }
        }

        private Tuple<char, ConsoleColor>[,] CreateField()
        {
            var field = new Tuple<char, ConsoleColor>[_world.Size.Width, _world.Size.Height];
            return ClearField(field);
        }

        private Tuple<char, ConsoleColor>[,] ClearField(Tuple<char, ConsoleColor>[,] field)
        {
            for (var i = 0; i < field.GetLength(0); i++)
            {
                for (var j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new Tuple<char, ConsoleColor>(' ', ConsoleColor.Black);
                }
            }
            return field;
        }

        private Task Draw()
        {
            if (_isDrawingInProgress)
            {
                return Task.FromResult(0);
            }
            _isDrawingInProgress = true;

            return Task.Run(() =>
            {
                try
                {
                    var field = Field;

                    for (var i = 0; i < field.GetLength(0); i++)
                    {
                        for (var j = 0; j < field.GetLength(1); j++)
                        {
                            var (symbol, color) = field[i, j];
                            Console.ForegroundColor = color;
                            Console.SetCursorPosition(i, j);
                            Console.Write(symbol);
                        }
                    }
                }
                finally
                {
                    _isDrawingInProgress = false;
                }
            });
        }

        private void GameOver(Tuple<char, ConsoleColor>[,] field)
        {
            var message = string.IsNullOrWhiteSpace(_world.Winner)
                    ? "Draw!"
                    : $"{_world.Winner} won!";
            var color = ConsoleColor.Green;
            var shift = 5;
            for (var i = 0; i < message.Length; i++)
            {
                field[i + shift, shift] = new Tuple<char, ConsoleColor>(message[i], color);
            }
        }

        public void Update(object source, ElapsedEventArgs e)
        {
            var field = CreateField();
            if (_world.IsGameOver)
            {
                GameOver(field);
            }
            else
            {
                _views.ToList().ForEach(e => e.Draw(field));
            }
            Field = field;

            Draw();
        }
    }
}
