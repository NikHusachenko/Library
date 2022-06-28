using Library.Database;
using Library.Database.Entities;
using Library.Database.GenericRepository;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.ManagerServices;
using Library.Services.VisitorServices;

namespace Library.Manager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            IGenericRepository<ManagerEntity> managerRepository = new GenericRepository<ManagerEntity>(applicationDbContext);
            IGenericRepository<VisitorEntity> visitorRepository = new GenericRepository<VisitorEntity>(applicationDbContext);
            IGenericRepository<BookEntity> bookRepository = new GenericRepository<BookEntity>(applicationDbContext);
            IGenericRepository<HistoryEntity> historyRepository = new GenericRepository<HistoryEntity>(applicationDbContext);

            IManagerService managerService = new ManagerService(managerRepository);
            IVisitorService visitorService = new VisitorService(visitorRepository);
            IBookService bookService = new BookService(bookRepository);
            IHistoryService historyService = new HistoryService(historyRepository);
            
            ApplicationConfiguration.Initialize();
            Application.Run(new SignInForm(managerService,
                historyService,
                bookService,
                visitorService));
        }
    }
}