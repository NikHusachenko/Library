using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;

namespace Library.Manager
{
    public partial class GiveBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;
        private readonly List<BookEntity> _bookEntities;

        public GiveBookForm(IBookService bookService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _bookService = bookService;
            _historyService = historyService;

            _managerEntity = managerEntity;
            _bookEntities = _bookService.Get();

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            comboBox1.Items.Clear();

            foreach (var book in _bookEntities)
            {
                if(book.InOrderWaiting)
                {
                    VisitorEntity visitor = book.Visitor;
                    richTextBox1.Text += $"Book {book.Title} {book.Author} {book.YearOfIssue} waiting order by {visitor.Name} {visitor.Surname}\n\n";
                    comboBox1.Items.Add($"{book.ID} {book.Title} {book.Author} {book.YearOfIssue}");
                }
            }

            if (richTextBox1.Text == String.Empty)
                richTextBox1.Text = "No one orders";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null)
                return;

            int id = int.Parse(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            BookEntity bookEntity = _bookService.GetById(id);

            bookEntity.InOrderWaiting = false;
            bookEntity.IsIssued = true;
            bookEntity.IssueDate = DateTime.Now;
            bookEntity.ReturnDate = DateTime.Now.AddDays(7);

            _bookService.Update(bookEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"Manager {_managerEntity.Name} {_managerEntity.Surname} gived book {bookEntity.Title} {bookEntity.Author} {bookEntity.YearOfIssue} to {bookEntity.Visitor.Name} {bookEntity.Visitor.Surname} visitor",
            };
            _historyService.Create(historyEntity);

            this.Close();
        }
    }
}