using BattleCity.Common;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Presentation
{
    public class RectangleView<T> : IDrawer<T>
        where T : Entity
    {
        protected char _symbol;
        protected ConsoleColor _color { get; set; }

        public RectangleView(T entity, char symbol, ConsoleColor color)
        {
            Entity = entity;
            _symbol = symbol;
            _color = color;
        }

        public T Entity { get; }

        public virtual Position Draw(Tuple<char, ConsoleColor>[,] field)
        {
            var halfWidth = Entity.Dimensions.Width / 2;
            var halfHeight = Entity.Dimensions.Height / 2;
            var position = Entity.Position;
            for (var i = position.X - halfWidth; i <= position.X + halfWidth; i++)
            {
                for (var j = position.Y - halfHeight; j <= position.Y + halfHeight; j++)
                {
                    DrawSymbol(field, i, j);
                }
            }
            return position;
        }

        protected void DrawSymbol(Tuple<char, ConsoleColor>[,] field, int x, int y)
        {
            var xMax = field.GetLength(0) - 1;
            var yMax = field.GetLength(1) - 1;

            if (x < 0 || y < 0 || x > xMax || y > yMax)
            {
                return;
            }
            field[x, y] = new Tuple<char, ConsoleColor>(_symbol, _color);
        }
    }
}
