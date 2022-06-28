using Library.Database.Entities;
using Library.Services.BookServices;

namespace Library.Visitor
{
    public partial class AllBooksForm : Form
    {
        private readonly IBookService _bookService;
        private readonly List<BookEntity> _books;

        public AllBooksForm(IBookService bookService)
        {
            _bookService = bookService;

            _books = _bookService.Get();

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            foreach (var book in _books)
                richTextBox1.Text += $"{book.Title} {book.Author} {book.YearOfIssue}\n\n";
        }
    }
}