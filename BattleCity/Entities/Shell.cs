using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public class Shell : Entity
    {
        private readonly Direction _direction;
        private readonly int _speed;
        private DateTime _updateTime;
        private double _x;
        private double _y;

        public Shell(
            Direction direction,
            int speed,
            Position position)
            : base(
                position,
                new Size
                {
                    Width = 1,
                    Height = 1
                })
        {
            _direction = direction;
            _speed = speed;
        }

        public override void Update(IWorld world)
        {
            if (default(DateTime).Equals(_updateTime))
            {
                _updateTime = DateTime.UtcNow;
                var position = Position;
                _x = position.X;
                _y = position.Y;
                return;
            }
            var time = DateTime.UtcNow;
            var timeElapsed = (time - _updateTime).TotalSeconds;
            var path = timeElapsed * _speed;
            switch (_direction)
            {
                case Direction.Up:
                    _y -= path;
                    break;
                case Direction.Right:
                    _x += path;
                    break;
                case Direction.Down:
                    _y += path;
                    break;
                case Direction.Left:
                    _x -= path;
                    break;
            }

            Position = new Position { X = (int)_x, Y = (int)_y };
            if (!InBoundaries(world))
            {
                IsDestroyed = true;
            }
            _updateTime = time;
        }

        private bool InBoundaries(IWorld world)
        {
            var x = Position.X;
            var y = Position.Y;
            var xMax = world.Size.Width - 1;
            var yMax = world.Size.Height - 1;
            if (x < 0 || x > xMax || y < 0 || y > yMax)
            {
                return false;
            }
            return true;
        }

        public override void ProcessCollision(Entity entity)
        {
            switch (entity)
            {
                case Tank _:
                case Brick _:
                case Concrete _:
                    IsDestroyed = true;
                    break;
            }
        }
    }
}
