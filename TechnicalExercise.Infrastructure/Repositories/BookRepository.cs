using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalExercise.Core.Entities;
using TechnicalExercise.Core.Interfaces;

namespace TechnicalExercise.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly string? connectionString;

        public BookRepository(string? connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = new List<Book>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT BookId, Title, Author, PublishedDate FROM Books", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Author = reader.GetString(reader.GetOrdinal("Author")),
                                PublishedDate = reader.GetDateTime(reader.GetOrdinal("PublishedDate"))
                            });
                        }
                    }
                }
            }
            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT BookId, Title, Author, PublishedDate FROM Books WHERE BookId = @BookId", connection))
                {
                    command.Parameters.AddWithValue("@BookId", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Book
                            {
                                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Author = reader.GetString(reader.GetOrdinal("Author")),
                                PublishedDate = reader.GetDateTime(reader.GetOrdinal("PublishedDate"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task AddAsync(Book book)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("INSERT INTO Books (Title, Author, PublishedDate) VALUES (@Title, @Author, @PublishedDate)", connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Book book)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE Books SET Title = @Title, Author = @Author, PublishedDate = @PublishedDate WHERE BookId = @BookId", connection))
                {
                    command.Parameters.AddWithValue("@BookId", book.BookId);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DELETE FROM Books WHERE BookId = @BookId", connection))
                {
                    command.Parameters.AddWithValue("@BookId", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
