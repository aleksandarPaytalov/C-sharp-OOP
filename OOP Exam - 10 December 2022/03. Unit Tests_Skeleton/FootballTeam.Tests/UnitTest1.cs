using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FootballTeam.Tests
{
    public class Tests
    {

        [Test]
        public void CheckIfConstructorIsWorkingCorrect()
        {
            string expectedName = "Liverpool";
            int expectedCapacity = 20;

            FootballTeam footballTeam = new FootballTeam("Liverpool", 20);


            Assert.AreEqual(expectedName, footballTeam.Name);
            Assert.AreEqual(expectedCapacity, footballTeam.Capacity);

            //Type t = footballTeam.Players.GetType();
            //Type expectedType = typeof(List<FootballPlayer>);
            //
            //Assert.AreEqual(t, expectedType);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CheckIfTeamCanBeCreatedWhenInvalidParametersForNameIsGiven(string name)
        {                  
            var expectedException = "Name cannot be null or empty!";
            var actualNameException = Assert.Throws<ArgumentException>(() => new FootballTeam (name, 20));
        
            Assert.AreEqual(expectedException, actualNameException.Message);
        }

         [Test]
        public void CheckIfTeamCanBeCreatedIfCapacityIsLessThanFifteen()
        {
            FootballTeam team;
            //ArgumentException actualPlayersCount = Assert.Throws<ArgumentException>(() => new FootballTeam("Arsenal", 5));
            Assert.Throws<ArgumentException>(() => team = new FootballTeam("Arsenal", 5));
            //Assert.AreEqual("Capacity min value = 15", actualPlayersCount.Message);
        }

        
        [Test]

        public void CheckWhenTryingToAddNewPlayerWhenCapacityIsFull()
        {
            FootballTeam footballTeam = new FootballTeam("Liverpool", 15);

            FootballPlayer one = new FootballPlayer("Ronaldo", 2, "Forward");
            FootballPlayer two = new FootballPlayer("Gigs", 3, "Midfielder");
            FootballPlayer three = new FootballPlayer("Ronaldo", 4, "Forward");
            FootballPlayer four = new FootballPlayer("Gigs", 5, "Midfielder");
            FootballPlayer five = new FootballPlayer("Ronaldo", 6, "Forward");
            FootballPlayer six = new FootballPlayer("Gigs", 7, "Midfielder");
            FootballPlayer seven = new FootballPlayer("Ronaldo", 8, "Forward");
            FootballPlayer eight = new FootballPlayer("Gigs", 9, "Midfielder");
            FootballPlayer nine = new FootballPlayer("Ronaldo", 10, "Forward");
            FootballPlayer ten = new FootballPlayer("Gigs", 11, "Midfielder");
            FootballPlayer eleven = new FootballPlayer("Ronaldo", 12, "Forward");
            FootballPlayer twelve = new FootballPlayer("Gigs", 13, "Midfielder");
            FootballPlayer thirtheen = new FootballPlayer("Ronaldo", 14, "Forward");
            FootballPlayer fourtheen = new FootballPlayer("Gigs", 15, "Midfielder");
            FootballPlayer fivetheen = new FootballPlayer("Ronaldo", 16, "Forward");
            FootballPlayer sixtheen = new FootballPlayer("Gigs", 17, "Midfielder");

            footballTeam.AddNewPlayer(one);
            footballTeam.AddNewPlayer(two);
            footballTeam.AddNewPlayer(three);
            footballTeam.AddNewPlayer(four);
            footballTeam.AddNewPlayer(five);
            footballTeam.AddNewPlayer(six);
            footballTeam.AddNewPlayer(seven);
            footballTeam.AddNewPlayer(eight);
            footballTeam.AddNewPlayer(nine);
            footballTeam.AddNewPlayer(ten);
            footballTeam.AddNewPlayer(eleven);
            footballTeam.AddNewPlayer(twelve);
            footballTeam.AddNewPlayer(thirtheen);
            footballTeam.AddNewPlayer(fourtheen);
            footballTeam.AddNewPlayer(fivetheen);

            string expectedMessage = "No more positions available!";
            var actualMessage = footballTeam.AddNewPlayer(sixtheen);

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        //[Test]
        //public void CheckIfListReturnCorrectNumberOfPlayersWithValidParametersAndIfCounterOfThePlayersInTheTeamIsWorking() //
        //{
        //    FootballTeam footballTeam = new FootballTeam("Liverpool", 20);
        //
        //    FootballPlayer playerOne = new FootballPlayer("Ronaldo", 8, "Forward");
        //    FootballPlayer playerTwo = new FootballPlayer("Gigs", 9, "Midfielder");
        //    FootballPlayer playerThree = new FootballPlayer("Runi", 10, "Forward");
        //    FootballPlayer playerFour = new FootballPlayer("Drogba", 11, "Midfielder");
        //
        //    footballTeam.AddNewPlayer(playerOne);
        //    footballTeam.AddNewPlayer(playerTwo);
        //    footballTeam.AddNewPlayer(playerThree);
        //    footballTeam.AddNewPlayer(playerFour);
        //
        //    int expectedPlayersCount = 4;
        //    int actualPlayersCount = footballTeam.Players.Count;
        //
        //    Assert.AreEqual(expectedPlayersCount, actualPlayersCount);
        //}
       

       //[Test]
       //public void CheckIfCapacityRetunRightNumberOfPlayers()
       //{
       //    FootballTeam footballTeam = new FootballTeam("Liverpool", 20);
       //
       //    FootballPlayer playerOne = new FootballPlayer("Ronaldo", 8, "Forward");
       //    FootballPlayer playerTwo = new FootballPlayer("Gigs", 9, "Midfielder");
       //
       //    footballTeam.AddNewPlayer(playerOne);
       //    footballTeam.AddNewPlayer(playerTwo);
       //
       //    int expectedCapacityLeft = 18;
       //    int actualCapacityLeft = footballTeam.Capacity - 2;
       //
       //    Assert.AreEqual(expectedCapacityLeft, actualCapacityLeft);
       //}

        //[Test]
        //public void CheckIfListReturnCorrectNumberOfPlayersWithInvalidParametersForPosition()
        //{            
        //    ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer("Ronaldo", 8, "Attacher"));
        //
        //    Assert.AreEqual("Invalid Position", ex.Message);
        //}
        //
        //[TestCase("")]
        //[TestCase(null)]
        //public void CheckIfListReturnCorrectNumberOfPlayersWithInvalidParametersForName(string name)
        //{
        //    ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer(name, 8, "Attacher"));
        //
        //    Assert.AreEqual("Name cannot be null or empty!", ex.Message);
        //}
        //
        //[TestCase(0)]
        //[TestCase(22)]
        //[TestCase(-1)]
        //[TestCase(33)]
        //public void CheckIfListReturnCorrectNumberOfPlayersWithInvalidParametersForPlayerNumber(int playerNumber)
        //{
        //    ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer("Gigs", playerNumber, "Attacher"));
        //
        //    Assert.AreEqual("Player number must be in range [1,21]", ex.Message);
        //}
        
        [Test]
        public void CheckTheReturnMessageIsItCorrectWhenAddingNewPlayer()
        {
            FootballTeam footballTeam = new FootballTeam("Liverpool", 15);

            FootballPlayer one = new FootballPlayer("Ronaldo", 8, "Forward");
            
            var actualResult = footballTeam.AddNewPlayer(one);
            string expectenMessage = "Added player Ronaldo in position Forward with number 8";
            
            Assert.AreEqual(expectenMessage, actualResult);
        }

        [Test]
        public void CheckThePickUpOfTheRightPlayerByGivenNameIfNameExistInTheTeam()
        {            
            FootballTeam footballTeam = new FootballTeam("Liverpool", 15);

            FootballPlayer one = new FootballPlayer("Ronaldo", 8, "Forward");
            FootballPlayer two = new FootballPlayer("Scous", 9, "Forward");
            FootballPlayer four = new FootballPlayer("Gigs", 11, "Midfielder");
            FootballPlayer three = new FootballPlayer("Gigs", 10, "Forward");

            string name = "Scous";

            footballTeam.AddNewPlayer(one);
            footballTeam.AddNewPlayer(two);
            footballTeam.AddNewPlayer(four);
            footballTeam.AddNewPlayer(three);
            
            var expected = footballTeam.PickPlayer(name);            

            Assert.AreEqual(expected, two);
        }
       
        [TestCase(10)]
        public void CheckTheReturnOfTheScoredGoalsForTheGivenPlayerWithGivenNumber(int playerNumber)
        {
            FootballTeam footballTeam = new FootballTeam("Liverpool", 15);

            FootballPlayer one = new FootballPlayer("Ronaldo", 8, "Forward");
            FootballPlayer two = new FootballPlayer("Scous", 9, "Forward");
            FootballPlayer three = new FootballPlayer("Gigs", 10, "Forward");

            footballTeam.AddNewPlayer(one);
            footballTeam.AddNewPlayer(two);
            footballTeam.AddNewPlayer(three);

            three.Score();
            three.Score();
            three.Score();

            var expected = "Gigs scored and now has 4 for this season!";
            var actual = footballTeam.PlayerScore(playerNumber);      

            Assert.AreEqual(expected, actual);
        }
    }
}