using Library.Database;
using Library.Database.Entities;
using Library.Database.GenericRepository;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            IGenericRepository<BookEntity> bookRepository = new GenericRepository<BookEntity>(applicationDbContext);
            IGenericRepository<VisitorEntity> visitorRepository = new GenericRepository<VisitorEntity>(applicationDbContext);
            IGenericRepository<HistoryEntity> historyRepository = new GenericRepository<HistoryEntity>(applicationDbContext);

            IBookService bookService = new BookService(bookRepository);
            IVisitorService visitorService = new VisitorService(visitorRepository);
            IHistoryService historyService = new HistoryService(historyRepository);

            ApplicationConfiguration.Initialize();
            Application.Run(new SignInForm(bookService,
                visitorService,
                historyService));
        }
    }
}