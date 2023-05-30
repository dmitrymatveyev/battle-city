using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Entities
{
    public class Tank : Entity
    {
        public Guid UserId { get; }

        public string UserName { get; set; }

        public Direction Direction { get; private set; }

        private Position _prevPosition;

        private bool _isArmored;
        private bool _attack;
        private bool _isGodMode = true;

        public override Position Position
        {
            get => base.Position;
            protected set
            {
                _prevPosition = Position;
                base.Position = value;
            }
         }

        public char UpKey { get; set; }
        public char RightKey { get; set; }
        public char DownKey { get; set; }
        public char LeftKey { get; set; }
        public char FireKey { get; set; }

        private char? _keyPressed;

        private bool _fireKeyPressed;

        public Tank(Position position, Guid userId) : base(
            position,
            new Size
            {
                Width = 3,
                Height = 3
            })
        {
            UserId = userId;
            Task.Run(() =>
            {
                Task.Delay(Constants.TankImmortalityTimeMs).Wait();
                _isGodMode = false;
            });
        }

        public override void Update(IWorld world)
        {
            var position = Position;
            Fire(world);
            Move(position, world);
        }

        public void OnKeyEvent(object s, KeyEventArgs e)
        {
            if (_keyPressed == null)
            {
                if (e.Key == UpKey || e.Key == RightKey || e.Key == DownKey || e.Key == LeftKey)
                {
                    _keyPressed = e.Key;
                }
                else
                {
                    _fireKeyPressed = e.Key == FireKey;
                }
            }
        }

        private void Fire(IWorld world)
        {
            if (_fireKeyPressed)
            {
                var speed = _attack ? Constants.ShellSpeed * 2 : Constants.ShellSpeed;
                var shell = new Shell(Direction, speed, GetStartingShellPosition());
                world.Entities.Add(shell);
            }
            _fireKeyPressed = false;
        }

        private void Move(Position startingPos, IWorld world)
        {
            var newPosition = startingPos;
            if (!_keyPressed.HasValue)
            {
                return;
            }
            var key = _keyPressed.Value;
            _keyPressed = null;
            if (key == UpKey)
            {
                newPosition.Y -= 1;
                Direction = Direction.Up;
            }
            else if (key == RightKey)
            {
                newPosition.X += 1;
                Direction = Direction.Right;
            }
            else if (key == DownKey)
            {
                newPosition.Y += 1;
                Direction = Direction.Down;
            }
            else if (key == LeftKey)
            {
                newPosition.X -= 1;
                Direction = Direction.Left;
            }
            else
            {
                return;
            }

            if (!InBoundaries(newPosition, world))
            {
                return;
            }

            Position = newPosition;
        }

        private bool InBoundaries(Position newPosition, IWorld world)
        {
            var x = newPosition.X;
            var y = newPosition.Y;
            var halfWidth = Dimensions.Width / 2;
            var halfHeight = Dimensions.Height / 2;
            var xMax = world.Size.Width - 1;
            var yMax = world.Size.Height - 1;

            if (x - halfWidth < 0
                || x + halfWidth > xMax
                || y - halfHeight < 0
                || y + halfHeight > yMax)
            {
                return false;
            }
            return true;
        }

        private Position GetStartingShellPosition()
        {
            var position = Position;
            switch (Direction)
            {
                case Direction.Up:
                    position.Y -= 2;
                    break;
                case Direction.Right:
                    position.X += 2;
                    break;
                case Direction.Down:
                    position.Y += 2;
                    break;
                case Direction.Left:
                    position.X -= 2;
                    break;
            }
            return position;
        }

        public override void ProcessCollision(Entity entity)
        {
            switch (entity)
            {
                case Shell _:
                    if (_isGodMode)
                    {
                        break;
                    }
                    if (_isArmored)
                    {
                        _isArmored = false;
                        break;
                    }
                    IsDestroyed = true;
                    break;
                case Brick _:
                case Concrete _:
                case River _:
                    base.Position = _prevPosition;
                    _prevPosition = default;
                    break;
                case Armor _:
                    _isArmored = true;
                    break;
                case Attack _:
                    _attack = true;
                    Task.Run(() =>
                    {
                        Task.Delay(Constants.AttackBonusTimeMs).Wait();
                        _attack = false;
                    });
                    break;
            }
        }
    }
}
