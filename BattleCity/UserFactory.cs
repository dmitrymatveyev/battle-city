using BattleCity.Configuration;
using BattleCity.Entities;
using BattleCity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCity
{
    public class UserFactory : IUserFactory, IUserViewFactory
    {
        private readonly Dictionary<Guid, UserConfig> _configs;

        public UserFactory(IEnumerable<UserConfig> configs)
        {
            _configs = configs.ToDictionary(c => Guid.NewGuid());
        }

        public List<Guid> UserIds => _configs.Keys.ToList();

        public Flag CreateFlag(Guid userId)
        {
            var config = _configs[userId];
            return new Flag(config.FlagPosition, userId, config.Name);
        }

        public Tank CreateTank(Guid userId)
        {
            var config = _configs[userId];
            return new Tank(config.TankPosition, userId)
            {
                UserName = config.Name,
                UpKey = config.UpKey,
                RightKey = config.RightKey,
                DownKey = config.DownKey,
                LeftKey = config.LeftKey,
                FireKey = config.FireKey
            };
        }

        RectangleView<Flag> IUserViewFactory.CreateFlag(Guid userId, Flag flag)
            => new RectangleView<Flag>(flag, 'F', _configs[userId].Color);

        TankView IUserViewFactory.CreateTank(Guid userId, Tank tank)
            => new TankView(tank, _configs[userId].Color);
    }
}
