namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void TheConstructor_IsWorking()
        //{
        //    string expectedTitle = "Lord of the ring";
        //    string expectedAutor = "Tokin";
        //    string expectedCategory = "Fantasy";
        //
        //    TextBook textBook = new TextBook(expectedTitle, expectedAutor, expectedCategory);
        //
        //    Assert.AreEqual(expectedTitle, textBook.Title);
        //    Assert.AreEqual(expectedAutor, textBook.Author);
        //    Assert.AreEqual(expectedCategory, textBook.Category);
        //} // може и без него

        [Test]
        public void AddTextBookToLibrary_IsWorking()
        {
            TextBook textBook = new TextBook("Lord of the ring", "Tokin", "Fantasy");

            UniversityLibrary library = new UniversityLibrary();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Book: Lord of the ring - 1");
            sb.AppendLine($"Category: Fantasy");
            sb.AppendLine($"Author: Tokin");

            string actualTextbook = library.AddTextBookToLibrary(textBook);

            Assert.AreEqual(sb.ToString().TrimEnd(), actualTextbook);
        }

        [Test]
        public void AddTextBookToLibrary_bookInventoryNumberCheckIfItsWorking()
        {
            TextBook textBookOne = new TextBook("Lord of the ring", "Tokin", "Fantasy");
            TextBook textBookTwo = new TextBook("Moonlitght", "Alex", "Fantasy");
            TextBook textBookThree = new TextBook("Men and women", "Peter", "Filosophy");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBookOne);
            library.AddTextBookToLibrary(textBookTwo);
            library.AddTextBookToLibrary(textBookThree);

            int expectedBookInventoryNumber = 2;
            var actualBookInventoryNumber = textBookTwo.InventoryNumber;

            Assert.AreEqual(expectedBookInventoryNumber, actualBookInventoryNumber);
        }

        [Test]
        public void CheckCatalogCount()
        {
            TextBook textBookOne = new TextBook("Lord of the ring", "Tokin", "Fantasy");
            TextBook textBookTwo = new TextBook("Moonlitght", "Alex", "Fantasy");
            TextBook textBookThree = new TextBook("Men and women", "Peter", "Filosophy");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBookOne);
            library.AddTextBookToLibrary(textBookTwo);
            library.AddTextBookToLibrary(textBookThree);

            int expectedBooksInTheCatalog = 3;
            int actualBooksInTheCatalog = library.Catalogue.Count;

            Assert.AreEqual(expectedBooksInTheCatalog, actualBooksInTheCatalog);
        }

        [Test]
        public void LoanTextBook_WhenBookIsNotLoaned()
        {
            TextBook textBookOne = new TextBook("Lord of the ring", "Tokin", "Fantasy");
            TextBook textBookTwo = new TextBook("Moonlitght", "Alex", "Fantasy");
            TextBook textBookThree = new TextBook("Men and women", "Peter", "Filosophy");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBookOne);
            library.AddTextBookToLibrary(textBookTwo);
            library.AddTextBookToLibrary(textBookThree);

            var expected = "Moonlitght loaned to Aleksandar Paytalov.";
            var actual = library.LoanTextBook(2, "Aleksandar Paytalov");

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Aleksandar Paytalov", textBookTwo.Holder);
        }

        [Test]
        public void LoanTextBook_WhenBookIsLoaned()
        {
            TextBook textBookOne = new TextBook("Lord of the ring", "Tokin", "Fantasy");
            TextBook textBookTwo = new TextBook("Moonlitght", "Alex", "Fantasy");
            TextBook textBookThree = new TextBook("Men and women", "Peter", "Filosophy");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBookOne);
            library.AddTextBookToLibrary(textBookTwo);
            library.AddTextBookToLibrary(textBookThree);
            library.LoanTextBook(2, "Aleksandar Paytalov");

            var expected = $"Aleksandar Paytalov still hasn't returned Moonlitght!";
            var actual = library.LoanTextBook(2, "Aleksandar Paytalov");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReturnTextBook_WhenBookIsLoaned()
        {
            TextBook textBookOne = new TextBook("Lord of the ring", "Tokin", "Fantasy");
            TextBook textBookTwo = new TextBook("Moonlitght", "Alex", "Fantasy");
            TextBook textBookThree = new TextBook("Men and women", "Peter", "Filosophy");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBookOne);
            library.AddTextBookToLibrary(textBookTwo);
            library.AddTextBookToLibrary(textBookThree);
            library.LoanTextBook(2, "Aleksandar Paytalov");

            var expected = "Moonlitght is returned to the library.";
            var actual = library.ReturnTextBook(2);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(string.Empty, textBookTwo.Holder);
        }
    }
}