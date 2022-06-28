using Library.Database.Entities;

namespace Library.Services.BookServices
{
    public interface IBookService
    {
        public void Create(BookEntity entity);

        public void Update(BookEntity entity);

        public void Delete(BookEntity entity);

        public List<BookEntity> Get();

        public BookEntity GetById(int id);

        public List<BookEntity> GetByTitle(string title);

        public List<BookEntity> GetByAuthor(string author);

        public List<BookEntity> GetByDateOfIssue(int dateOfIssue);

        public List<BookEntity> Get(DateTime issueDate, DateTime returDate);
    }
}