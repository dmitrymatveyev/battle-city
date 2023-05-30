using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public class Armor : Entity
    {
        public Armor(Position position) : base(
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
            IsDestroyed = entity is Tank;
        }
    }
}
