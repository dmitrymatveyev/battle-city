using BattleCity.Common;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BattleCity
{
    public interface IWorld
    {
        public ObservableCollection<Entity> Entities { get; }

        public Size Size { get; }
    }
}
