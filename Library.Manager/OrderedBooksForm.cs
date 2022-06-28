using Library.Database.Entities;
using Library.Services.BookServices;

namespace Library.Manager
{
    public partial class OrderedBooksForm : Form
    {
        private readonly IBookService _bookService;

        private readonly List<BookEntity> _bookEntities;

        public OrderedBooksForm(IBookService bookService)
        {
            _bookService = bookService;

            _bookEntities = _bookService.Get();

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            if (_bookEntities.Count > 0)
            {
                foreach (var book in _bookEntities)
                {
                    if (book.IsIssued)
                    {
                        VisitorEntity visitorEntity = book.Visitor;
                        richTextBox1.Text += $"{book.Title} {book.Author} {book.YearOfIssue} issued to {visitorEntity.Name} {visitorEntity.Surname}\n\n";
                    }
                }
            }
            else
            {
                richTextBox1.Text = "No one books issued";
            }
        }
    }
}