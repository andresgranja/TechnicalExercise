using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalExercise.Core.Entities;
using TechnicalExercise.Core.Interfaces;

namespace TechnicalExercise.Application
{
    public class BookService
    {
        private readonly IRepository<Book> bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await bookRepository.GetByIdAsync(bookId);
        }

        public async Task AddBookAsync(Book book)
        {
            await bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await bookRepository.DeleteAsync(bookId);
        }
    }
}
