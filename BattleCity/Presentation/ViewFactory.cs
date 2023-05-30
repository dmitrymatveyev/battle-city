using BattleCity.Common;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Presentation
{
    public class ViewFactory
    {
        private readonly IUserViewFactory _userViewFactory;

        public ViewFactory(IUserViewFactory userViewFactory)
        {
            _userViewFactory = userViewFactory;
        }

        public IDrawer<Entity> Create(Entity entity)
        {
            switch (entity)
            {
                case Armor a:
                    return new RectangleView<Armor>(a, 'B', ConsoleColor.Blue);
                case Attack a:
                    return new RectangleView<Attack>(a, 'B', ConsoleColor.Red);
                case Brick b:
                    return new RectangleView<Brick>(b, 'W', ConsoleColor.Red);
                case Concrete c:
                    return new RectangleView<Concrete>(c, 'W', ConsoleColor.Gray);
                case Flag f:
                    return _userViewFactory.CreateFlag(f.UserId, f);
                case River r:
                    return new RectangleView<River>(r, '~', ConsoleColor.DarkBlue);
                case Shell s:
                    return new RectangleView<Shell>(s, '#', ConsoleColor.Magenta);
                case Tank t:
                    return _userViewFactory.CreateTank(t.UserId, t);
            }
            throw new Exception($"Unexpected entity type: {entity.GetType().FullName}.");
        }
    }
}
