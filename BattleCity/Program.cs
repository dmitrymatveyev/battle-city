using BattleCity.Common;
using BattleCity.Configuration;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace BattleCity
{
    class Program
    {
        private static Timer _timer = new Timer(1);

        static void Main(string[] args)
        {
            var userFactory = new UserFactory(SampleData.UserConfigs);

            var world = new World(SampleData.WorldDimensions, SampleData.TestMap, userFactory);
            _timer.Elapsed += world.Update;
            var view = new WorldView(world, new Presentation.ViewFactory(userFactory));
            _timer.Elapsed += view.Update;

            _timer.AutoReset = true;
            _timer.Enabled = true;

            Task.Delay(-1).Wait();
        }
    }
}
