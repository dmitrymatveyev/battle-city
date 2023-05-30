using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public class Brick : Entity
    {
        private int _health = 2;

        public Brick(Position position) : base(
            position,
            new Size
            {
                Width = 3,
                Height = 3
            })
        { }

        public override void Update(IWorld world) { }

        public override void ProcessCollision(Entity entity)
        {
            if(entity is Shell)
            {
                IsDestroyed = --_health < 1;
            }
        }
    }
}
