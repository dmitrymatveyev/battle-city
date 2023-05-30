using BattleCity.Common;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Presentation
{
    public interface IDrawer<out T>
        where T : Entity
    {
        public T Entity { get; }

        Position Draw(Tuple<char, ConsoleColor>[,] field);
    }
}
