using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;

namespace Library.Manager
{
    public partial class AddBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;

        public AddBookForm(IBookService bookService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _bookService = bookService;
            _historyService = historyService;

            _managerEntity = managerEntity;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookEntity bookEntity = new BookEntity()
            {
                Author = textBox2.Text,
                YearOfIssue = (int)numericUpDown2.Value,
                IsIssued = false,
                Pages = (int)numericUpDown1.Value,
                Title = textBox1.Text,
            };

            _bookService.Create(bookEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"{_managerEntity.Name} {_managerEntity.Surname} append {bookEntity.Title} of {bookEntity.Author} {bookEntity.YearOfIssue} years",
            };

            _historyService.Create(historyEntity);

            this.Close();
        }
    }
}