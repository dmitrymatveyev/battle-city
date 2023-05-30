using BattleCity.Common;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Presentation
{
    public class TankView : RectangleView<Tank>
    {
        public TankView(Tank tank, ConsoleColor color) : base(tank, '#', color)
        { }

        public override Position Draw(Tuple<char, ConsoleColor>[,] field)
        {
            var position = base.Draw(field);
            var halfWidth = Entity.Dimensions.Width / 2;
            var halfHeight = Entity.Dimensions.Height / 2;
            switch (Entity.Direction)
            {
                case Direction.Up:
                    DrawSymbol(field, position.X, position.Y - halfHeight - 1);
                    break;
                case Direction.Right:
                    DrawSymbol(field, position.X + halfWidth + 1, position.Y);
                    break;
                case Direction.Down:
                    DrawSymbol(field, position.X, position.Y + halfHeight + 1);
                    break;
                case Direction.Left:
                    DrawSymbol(field, position.X - halfWidth - 1, position.Y);
                    break;
            }
            return position;
        }
    }
}
