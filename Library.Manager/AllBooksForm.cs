using Library.Database.Entities;
using Library.Services.BookServices;

namespace Library.Manager
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

            richTextBox1.Text = string.Empty;
            foreach (BookEntity book in _books)
            {
                richTextBox1.Text += $"{book.Title} {book.Author} {book.YearOfIssue}\n";
                if (book.IsIssued)
                    richTextBox1.Text += $"Issued to {book.Visitor.Name} {book.Visitor.Surname} at {book.IssueDate}";
                else
                    richTextBox1.Text += "Not issued";
                richTextBox1.Text += "\n\n";
            }
        }
    }
}