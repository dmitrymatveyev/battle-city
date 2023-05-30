using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public class River : Entity
    {
        public River(Position position) : base(
            position,
            new Size
            {
                Width = 9,
                Height = 30
            })
        { }

        public override void Update(IWorld world) { }

        public override void ProcessCollision(Entity entity) { }
    }
}
