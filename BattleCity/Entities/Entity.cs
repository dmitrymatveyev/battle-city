using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public abstract class Entity
    {
        protected Entity(Position position, Size dimensions)
        {
            Position = position;
            Dimensions = dimensions;
        }

        public virtual Position Position { get; protected set; }

        public Size Dimensions { get; }

        public abstract void Update(IWorld world);

        public abstract void ProcessCollision(Entity entity);

        public bool IsDestroyed { get; protected set; }

        public static bool Collide(Entity left, Entity right, bool dontProcess = false)
        {
            var position1 = left.Position;
            var body1 = left.Dimensions;
            var position2 = right.Position;
            var body2 = right.Dimensions;
            var halfWidth1 = body1.Width / 2;
            var halfHeight1 = body1.Height / 2;
            var halfWidth2 = body2.Width / 2;
            var halfHeight2 = body2.Height / 2;

            var l1 = new Position
            {
                X = position1.X - halfWidth1,
                Y = position1.Y - halfHeight1
            };
            var r1 = new Position
            {
                X = position1.X + halfWidth1,
                Y = position1.Y + halfHeight1
            };

            var l2 = new Position
            {
                X = position2.X - halfWidth2,
                Y = position2.Y - halfHeight2
            };
            var r2 = new Position
            {
                X = position2.X + halfWidth2,
                Y = position2.Y + halfHeight2
            };

            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            if (l1.Y > r2.Y || l2.Y > r1.Y)
            {
                return false;
            }

            if (!dontProcess)
            {
                left.ProcessCollision(right);
                right.ProcessCollision(left);
            }

            return true;
        }
    }
}
