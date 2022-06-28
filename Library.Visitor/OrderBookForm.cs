using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    public partial class OrderBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        private readonly VisitorEntity _visitorEntity;

        private List<BookEntity> _books;
        private List<BookEntity> _selectedBooks;

        public OrderBookForm(IBookService bookService,
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

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedString = comboBox1.SelectedItem.ToString();

            int id = int.Parse(selectedString.Split(' ')[0]);
            var book = _bookService.GetById(id);

            if (book.InOrderWaiting)
            {
                MessageBox.Show($"This book on waiting confirmation");
                return;
            }
            if (book.IsIssued)
            {
                MessageBox.Show($"This book in ordering. Must be returned at {book.ReturnDate}");
                return;
            }

            book.InOrderWaiting = true;
            book.Visitor = _visitorEntity;
            _bookService.Update(book);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"Visitor {_visitorEntity.Name} {_visitorEntity.Surname} ordered the book {book.Title} {book.Author} {book.YearOfIssue}",
            };
            _historyService.Create(historyEntity);

            MessageBox.Show("After confirmation, the book will automatically appear in your library");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            // Get by year of issue
            int dateOfIssue = default;
            if (int.TryParse(textBox1.Text, out dateOfIssue))
            {
                _selectedBooks = _bookService.GetByDateOfIssue(dateOfIssue);
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by author
            _selectedBooks = _bookService.GetByAuthor(textBox1.Text);
            if (_selectedBooks.Count > 0)
            {
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by title
            _selectedBooks = _bookService.GetByTitle(textBox1.Text);
            if (_selectedBooks.Count > 0)
            {
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }
        }
    }
}