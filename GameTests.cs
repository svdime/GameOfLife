using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Cycle()
        {
            var field = new bool[5, 5];
            field[1, 2] = true;
            field[2, 2] = true;
            field[3, 2] = true;
            var nextGen = Game.NextStep(field);
            var expectedResult = new bool[5, 5];
            expectedResult[1, 2] = false;
            expectedResult[2, 2] = true;
            expectedResult[3, 2] = false;
            expectedResult[2, 3] = true;
            expectedResult[2, 1] = true;

            Assert.AreEqual(expectedResult, nextGen);
            Assert.AreEqual(field, Game.NextStep(nextGen));
        }

        [Test]
        public void Block()
        {
            var X = 4;
            var Y = 5;

            var field = new bool[10, 10];
            field[X, Y] = true;
            field[X, Y + 1] = true;
            field[X + 1, Y] = true;
            field[X + 1, Y + 1] = true;

            var nextGen = Game.NextStep(field);
            Assert.AreEqual(field, nextGen);
        }

        [Test]
        public void SomeCreature1()
        {
            var field = new bool[5, 5];
            field[0, 0] = true;
            field[1, 1] = true;
            field[0, 2] = true;

            var nextGen = Game.NextStep(field);
            var expectedRes = new bool[5, 5];
            expectedRes[0, 1] = true;
            expectedRes[1, 1] = true;
            Assert.AreEqual(expectedRes, nextGen);

            expectedRes = new bool[5, 5];
            Assert.AreEqual(expectedRes, Game.NextStep(nextGen));
        }

        [Test]
        public void SomeCreature2()
        {
            var field = new bool[5, 5];
            field[0, 0] = true;
            field[1, 1] = true;
            field[2, 2] = true;

            var expectedRes = new bool[5, 5];
            expectedRes[1, 1] = true;
            var nextGen = Game.NextStep(field);
            Assert.AreEqual(expectedRes, nextGen);

            expectedRes = new bool[5, 5];
            Assert.AreEqual(expectedRes, Game.NextStep(nextGen));
        }

        [Test]
        public void SomeCreature3()
        {
            var field = new bool[3, 3];
            field[0, 0] = true;
            field[2, 0] = true;
            field[2, 1] = true;

            var expectedRes = new bool[3, 3];
            expectedRes[1, 0] = true;
            expectedRes[1, 1] = true;

            var nextGen = Game.NextStep(field);

            Assert.AreEqual(expectedRes, nextGen);

            expectedRes = new bool[3, 3];
            Assert.AreEqual(expectedRes, Game.NextStep(nextGen));
        }
    }
}