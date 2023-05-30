using BattleCity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity.Configuration
{
    public class UserConfig
    {
        public string Name { get; set; }

        public ConsoleColor Color { get; set; }

        public char UpKey { get; set; }
        public char RightKey { get; set; }
        public char DownKey { get; set; }
        public char LeftKey { get; set; }
        public char FireKey { get; set; }

        public Position FlagPosition { get; set; }
        public Position TankPosition { get; set; }
    }
}
