using BattleCity.Common;
using BattleCity.Entities;
using Moq;
using System;
using Xunit;

namespace BattleCity.Tests
{
    public class EntityTest
    {
        [Theory]
        [InlineData(10, 10, 3, 3, 12, 12, 3, 3)]
        [InlineData(10, 10, 9, 9, 12, 12, 3, 3)]
        public void Collide_Intersect_Returns_True(
            int leftPosX,
            int leftPosY,
            int leftWidth,
            int leftHeight,
            int rightPosX,
            int rightPosY,
            int rightWidth,
            int rightHeight)
        {
            #region [ Arrange ]
            var leftMock = new Mock<Entity>(new object[]
            {
                new Position { X = leftPosX, Y = leftPosY },
                new Size { Width = leftWidth, Height = leftHeight }
            })
            {
                CallBase = true
            };
            var rightMock = new Mock<Entity>(new object[]
            {
                new Position { X = rightPosX, Y = rightPosY },
                new Size { Width = rightWidth, Height = rightHeight }
            })
            {
                CallBase = true
            };
            #endregion

            #region [ Act ]
            var result = Entity.Collide(leftMock.Object, rightMock.Object, true);
            #endregion

            #region [ Assert ]
            Assert.True(result);
            #endregion
        }

        [Fact]
        public void Collide_Doesnt_Intersect_Returns_False()
        {
            #region [ Arrange ]
            var leftMock = new Mock<Entity>(new object[]
            {
                new Position { X = 9, Y = 15 },
                new Size { Width = 3, Height = 3 }
            })
            {
                CallBase = true
            };
            var rightMock = new Mock<Entity>(new object[]
            {
                new Position { X = 13, Y = 15 },
                new Size { Width = 3, Height = 3 }
            })
            {
                CallBase = true
            };
            #endregion

            #region [ Act ]
            var result = Entity.Collide(leftMock.Object, rightMock.Object, true);
            #endregion

            #region [ Assert ]
            Assert.False(result);
            #endregion
        }
    }
}
