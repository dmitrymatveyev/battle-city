using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Presentation
{
    public interface IUserViewFactory
    {
        RectangleView<Flag> CreateFlag(Guid userId, Flag flag);

        TankView CreateTank(Guid userId, Tank tank);
    }
}
