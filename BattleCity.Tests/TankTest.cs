using BattleCity.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleCity.Tests
{
    public class TankTest
    {
        [Fact]
        public void Update_Move_Onto_Obstacle_Doesnt_Move()
        {
            #region [ Arrange ]
            var rightKey = 'f';

            var tank = new Tank(
                new Common.Position
                {
                    X = 5,
                    Y = 5
                },
                new Guid("45E23FB6-5FC1-4A20-804E-AAC93F5E7636"))
            {
                RightKey = rightKey
            };
            var brick = new Brick(new Common.Position
            {
                X = 9,
                Y = 6
            });
            var worldMock = new Mock<IWorld>();
            worldMock.Setup(w => w.Size).Returns(new Common.Size
            {
                Width = 50,
                Height = 50
            });
            worldMock.Setup(w => w.Entities).Returns(new System.Collections.ObjectModel.ObservableCollection<Entity>
            {
                brick
            });
            #endregion

            #region [ Act ]
            tank.OnKeyEvent(worldMock.Object, new Common.KeyEventArgs { Key = rightKey });
            tank.Update(worldMock.Object);
            var position = tank.Position;
            tank.OnKeyEvent(worldMock.Object, new Common.KeyEventArgs { Key = rightKey });
            tank.Update(worldMock.Object);
            tank.ProcessCollision(brick);
            #endregion

            #region [ Assert ]
            Assert.Equal(position, tank.Position);
            #endregion
        }

        [Fact]
        public void Update_Move_Out_Of_Boundaries_Doesnt_Move()
        {
            #region [ Arrange ]
            var rightKey = 'f';

            var tank = new Tank(
                new Common.Position
                {
                    X = 5,
                    Y = 5
                },
                new Guid("45E23FB6-5FC1-4A20-804E-AAC93F5E7636"))
            {
                RightKey = rightKey
            };
            
            var worldMock = new Mock<IWorld>();
            worldMock.Setup(w => w.Size).Returns(new Common.Size
            {
                Width = 8,
                Height = 50
            });

            #endregion

            #region [ Act ]
            tank.OnKeyEvent(worldMock.Object, new Common.KeyEventArgs { Key = rightKey });
            tank.Update(worldMock.Object);
            var position = tank.Position;
            tank.OnKeyEvent(worldMock.Object, new Common.KeyEventArgs { Key = rightKey });
            tank.Update(worldMock.Object);
            #endregion

            #region [ Assert ]
            Assert.Equal(position, tank.Position);
            #endregion
        }
    }
}
