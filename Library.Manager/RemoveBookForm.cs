using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;

namespace Library.Manager
{
    public partial class RemoveBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;

        private List<BookEntity> _books;
        private List<BookEntity> _selectedBooks;

        public RemoveBookForm(IBookService bookService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _bookService = bookService;
            _historyService = historyService;

            _managerEntity = managerEntity;

            _books = _bookService.Get();

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            // Get by year of issue
            int dateOfIssue = default;
            if(int.TryParse(textBox1.Text, out dateOfIssue))
            {
                _selectedBooks = _bookService.GetByDateOfIssue(dateOfIssue);
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by author
            _selectedBooks = _bookService.GetByAuthor(textBox1.Text);
            if(_selectedBooks.Count > 0)
            {
                foreach (var book in _selectedBooks)
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                return;
            }

            // Get by title
            _selectedBooks = _bookService.GetByTitle(textBox1.Text);
            if(_selectedBooks.Count > 0)
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

            _bookService.Delete(book);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"{_managerEntity.Name} {_managerEntity.Surname} removed {book.Title} {book.Author}",
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