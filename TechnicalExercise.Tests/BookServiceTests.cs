using NUnit.Framework;
using TechnicalExercise.Application;
using TechnicalExercise.Core.Interfaces;
using TechnicalExercise.Core.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercise.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IRepository<Book>> _mockBookRepository;
        private BookService _bookService;

        [SetUp]
        public void Setup()
        {
            _mockBookRepository = new Mock<IRepository<Book>>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Test]
        public async Task GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { BookId = 1, Title = "Book One", Author = "Author A", PublishedDate = DateTime.Now },
            new Book { BookId = 2, Title = "Book Two", Author = "Author B", PublishedDate = DateTime.Now }
        };
            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task AddBook_AddsBook()
        {
            // Arrange
            var book = new Book { Title = "New Book", Author = "New Author", PublishedDate = DateTime.Now };
            _mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Verifiable();

            // Act
            await _bookService.AddBookAsync(book);

            // Assert
            _mockBookRepository.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
        }
    }
}
