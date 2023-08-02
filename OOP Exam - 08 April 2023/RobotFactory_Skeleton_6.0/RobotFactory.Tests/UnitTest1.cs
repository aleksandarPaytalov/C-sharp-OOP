using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ProduceRobotWithValidParamsInput()
        {
            Factory factory = new Factory("Unity", 5);

            string expectedResult = factory.ProduceRobot("IRobot", 1500.1, 5000);
            string actualResult = "Produced --> Robot model: IRobot IS: 5000, Price: 1500.10";

            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void CheckCounterWhenProducingNewRobot()
        {
            Factory factory = new Factory("Space Jam", 5);

            int expectedCountBeforeCreatingNewRobot = 0;
            int actualCountBeforeRobotIsCreated = factory.Robots.Count;

            //Robot robot = new Robot("Terminator", 1999.9, 500); в Robot не се създава робота. Той се създава във Factory, затова тряба да се добави през него
            factory.ProduceRobot("Terminator", 1999.9, 500);

            int expectedCountAfterCreatingNewRobot = 1;
            int actualCountAfterRobotIsCreated = factory.Robots.Count;

            Assert.AreEqual(expectedCountBeforeCreatingNewRobot, actualCountBeforeRobotIsCreated);
            Assert.AreEqual(expectedCountAfterCreatingNewRobot, actualCountAfterRobotIsCreated);

        }

        [Test]
        public void CheckingIfTheCapacityIsFullWorksWhenTriedToMakeNewRobot()
        {
            Factory factory = new Factory("SpaceX", 5);

            factory.ProduceRobot("Robo-1", 2500, 22);
            factory.ProduceRobot("Robo-2", 2500, 22);
            factory.ProduceRobot("Robo-3", 2500, 22);
            factory.ProduceRobot("Robo-4", 2500, 22);
            factory.ProduceRobot("Robo-5", 2500, 22);
                       
            string expectedResult = "The factory is unable to produce more robots for this production day!"; // трябва да преверим със съобщението, а не чрез инт каутър
            string actualResult = factory.ProduceRobot("Robo-6", 2500, 22);

            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void CheckingIfTheSupplementIsProducedWithValidParameters()
        {
            Factory factory = new Factory("Space Troopers", 2); // създаваме фактори, защото съплемента се прави там а не в класа Supplement                

            string expecterResult = factory.ProduceSupplement("F-Arm", 500);
            string actualResult = "Supplement: F-Arm IS: 500";

            Assert.AreEqual(expecterResult, actualResult);
            
        }

        [Test]
        public void CheckIfTheCurrentSupplementIsAddedToTheCollectionSupplements()
        {
            Factory factory = new Factory("Space Jam", 5);

            int expectedCountBeforeCreatingNewRobot = 0;
            int actualCountBeforeRobotIsCreated = factory.Supplements.Count;

            factory.ProduceSupplement("F-Arm", 500);

            int expectedCountAfterCreatingNewRobot = 1;
            int actualCountAfterRobotIsCreated = factory.Supplements.Count;

            Assert.AreEqual(expectedCountBeforeCreatingNewRobot, actualCountBeforeRobotIsCreated);
            Assert.AreEqual(expectedCountAfterCreatingNewRobot, actualCountAfterRobotIsCreated);

        }
        
        [Test]
        public void CheckIfRobotGetUpgradedWhenInputValidParameters()
        {
            Factory factory = new Factory("Paradox", 5);

            factory.ProduceRobot("Alien", 5000.5534534, 10);
            factory.ProduceSupplement("Powerful Armor", 10);

            bool actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());

            Assert.IsTrue(actualResult);

        }

        [Test]
        public void CheckIfRobotIsAlreadyUpgradedWithGivenSupplement()
        {
            Factory factory = new Factory("Paradox", 5);

            factory.ProduceRobot("Alien", 5000.5534534, 10);
            factory.ProduceSupplement("Powerful Armor", 10);

            factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());

            bool actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());

            Assert.IsFalse(actualResult);

        }

        [Test]
        public void CheckIfRobotCanBeUpgradedWhenStandartsAreDifferent()
        {
            Factory factory = new Factory("Paradox", 5);

            factory.ProduceRobot("Alien", 5000.5534534, 10);
            factory.ProduceSupplement("Powerful Armor", 9);

            bool actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());

            Assert.IsFalse(actualResult);

        }

        [Test]
        public void CheckIfTheSellingMethodWorksCorrectly()
        {
            Factory factory = new Factory("SpaceX", 10);

            factory.ProduceRobot("Robo-3", 2000, 22);
            factory.ProduceRobot("Robo-3", 5500, 32);
            factory.ProduceRobot("Robo-3", 8800, 5);

            Robot expectedResult = factory.Robots.FirstOrDefault(p => p.Price <= 2999);

            Robot actualResult = factory.SellRobot(3500);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}