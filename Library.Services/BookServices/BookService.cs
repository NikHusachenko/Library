using Microsoft.EntityFrameworkCore;
using Library.Database.Entities;
using Library.Database.GenericRepository;

namespace Library.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<BookEntity> _bookRepository;

        public BookService(IGenericRepository<BookEntity> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void Create(BookEntity entity)
        {
            _bookRepository.Create(entity);
        }

        public void Delete(BookEntity entity)
        {
            _bookRepository.Remove(entity);
        }

        public BookEntity GetById(int id)
        {
            var book = _bookRepository.Table
                .Where(book => book.ID == id)
                .Include(book => book.Visitor)
                .FirstOrDefault();

            return book;
        }

        public List<BookEntity> GetByTitle(string title)
        {
            var books = _bookRepository.Table
                .Where(book => book.Title == title)
                .Include(book => book.Visitor)
                .ToList();

            return books;
        }

        public List<BookEntity> GetByAuthor(string author)
        {
            var books = _bookRepository.Table
                    .Where(book => book.Author == author)
                    .Include(book => book.Visitor)
                    .ToList();

            return books;
        }

        public List<BookEntity> GetByDateOfIssue(int dateOfIssue)
        {
            var books = _bookRepository.Table
                .Where(book => book.YearOfIssue == dateOfIssue)
                .Include(book => book.Visitor)
                .ToList();

            return books;
        }

        public List<BookEntity> Get(DateTime issueDate, DateTime returDate)
        {
            var books = _bookRepository.Table
                .Where(book => book.IssueDate == issueDate &&
                book.ReturnDate == returDate)
                .Include(book => book.Visitor)
                .ToList();

            return books;
        }

        public List<BookEntity> Get()
        {
            return _bookRepository.Table
                .Include(book => book.Visitor)
                .ToList();
        }

        public void Update(BookEntity entity)
        {
            _bookRepository.Update(entity);
        }
    }
}