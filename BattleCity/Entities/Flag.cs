using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public class Flag : Entity
    {
        public Guid UserId { get; }

        public string UserName { get; }

        public Flag(Position position, Guid userId, string userName) : base(
            position,
            new Size
            {
                Width = 1,
                Height = 1
            })
        {
            UserId = userId;
            UserName = userName;
        }

        public override void Update(IWorld world) { }

        public override void ProcessCollision(Entity entity)
        {
            if (entity is Shell)
            {
                IsDestroyed = true;
            }
        }
    }
}
