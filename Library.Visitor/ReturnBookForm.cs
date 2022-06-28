using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    public partial class ReturnBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        private readonly VisitorEntity _visitorEntity;

        private List<BookEntity> _books;
        private List<BookEntity> _selectedBooks;

        public ReturnBookForm(IBookService bookService,
            IVisitorService visitorService,
            IHistoryService historyService,
            VisitorEntity visitorEntity)
        {
            _bookService = bookService;
            _visitorService = visitorService;
            _historyService = historyService;

            _visitorEntity = visitorEntity;

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            // Get by year of issue
            int dateOfIssue = default;
            if (int.TryParse(textBox1.Text, out dateOfIssue))
            {
                _selectedBooks = _visitorEntity.Books;
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by author
            _selectedBooks = _visitorEntity.Books
                .Where(book => book.Author == textBox1.Text)
                .ToList();
            if (_selectedBooks.Count > 0)
            {
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by title
            _selectedBooks = _visitorEntity.Books
                .Where(book => book.Title == textBox1.Text)
                .ToList();
            if (_selectedBooks.Count > 0)
            {
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedString = comboBox1.SelectedItem.ToString();

            int id = int.Parse(selectedString.Split(' ')[0]);
            var book = _bookService.GetById(id);

            book.Visitor = null;
            _bookService.Update(book);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"{_visitorEntity.Name} {_visitorEntity.Surname} returned the book {book.Title} {book.Author} {book.YearOfIssue}",
            };
            _historyService.Create(historyEntity);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}