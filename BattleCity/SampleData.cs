using BattleCity.Common;
using BattleCity.Configuration;
using BattleCity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCity
{
    public static class SampleData
    {
        public static List<Entity> TestMap = new List<Entity>
        {
            new River(new Position
            {
                X = WORLD_HEIGHT,
                Y = 14
            }),
            new Brick(new Position
            {
                X = 56,
                Y = 26
            }),
            new Brick(new Position
            {
                X = 53,
                Y = 26
            }),
            new Brick(new Position
            {
                X = 53,
                Y = 23
            }),
            new Brick(new Position
            {
                X = 53,
                Y = 29
            }),
            new Concrete(new Position
            {
                X = 11,
                Y = 1
            }),
            new Concrete(new Position
            {
                X = 11,
                Y = 5
            }),
            new Brick(new Position
            {
                X = 17,
                Y = 3
            }),
            new Concrete(new Position
            {
                X = 45,
                Y = 15
            }),
            new Concrete(new Position
            {
                X = 45,
                Y = 12
            }),
            new Brick(new Position
            {
                X = 10,
                Y = 20
            }),
            new Brick(new Position
            {
                X = 10,
                Y = 23
            })
        };

        public static List<UserConfig> UserConfigs = new List<UserConfig>
        {
            new UserConfig
                {
                    Name = "User A",
                    Color = ConsoleColor.Red,
                    UpKey = 'e',
                    RightKey = 'f',
                    DownKey = 'd',
                    LeftKey = 's',
                    FireKey = 'a',
                    FlagPosition = new Position
                    {
                        X = 3,
                        Y = 3
                    },
                    TankPosition = new Position
                    {
                        X = 3,
                        Y = WORLD_HEIGHT / 2
                    }
                },
                new UserConfig
                {
                    Name = "User B",
                    Color = ConsoleColor.Blue,
                    UpKey = 'o',
                    RightKey = ';',
                    DownKey = 'l',
                    LeftKey = 'k',
                    FireKey = 'j',
                    FlagPosition = new Position
                    {
                        X = WORLD_WIDTH - 1 - 3,
                        Y = WORLD_HEIGHT - 1 - 3
                    },
                    TankPosition = new Position
                    {
                        X = WORLD_WIDTH - 1 - 3,
                        Y = WORLD_HEIGHT / 2
                    }
                }
        };

        public const int WORLD_WIDTH = 60;

        public const int WORLD_HEIGHT = 30;

        public static Size WorldDimensions = new Size
        {
            Width = WORLD_WIDTH,
            Height = WORLD_HEIGHT
        };
    }
}
