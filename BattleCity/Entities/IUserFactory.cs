using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Entities
{
    public interface IUserFactory
    {
        List<Guid> UserIds { get; }

        Flag CreateFlag(Guid userId);

        Tank CreateTank(Guid userId);
    }
}
