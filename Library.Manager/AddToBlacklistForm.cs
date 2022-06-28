using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Manager
{
    public partial class AddToBlacklistForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;

        private List<BookEntity> _bookEntities;

        public AddToBlacklistForm(IBookService bookService,
            IVisitorService visitorService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _bookService = bookService;
            _visitorService = visitorService;
            _historyService = historyService;

            _managerEntity = managerEntity;

            _bookEntities = _bookService.Get();

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            comboBox1.Items.Clear();

            if(_bookEntities.Count > 0)
            {
                foreach (var bookEntity in _bookEntities)
                {
                    if(bookEntity.IsIssued)
                    {
                        if (bookEntity.ReturnDate < DateTime.Now)
                        {
                            VisitorEntity visitorEntity = bookEntity.Visitor;
                            richTextBox1.Text += $"{bookEntity.Title} {bookEntity.Author} {bookEntity.YearOfIssue} by {visitorEntity.Name} { visitorEntity.Surname}";
                            comboBox1.Items.Add($"{visitorEntity.ID} {visitorEntity.Name} {visitorEntity.Surname}");
                        }
                    }
                }
            }

            if(richTextBox1.Text == String.Empty)
            {
                richTextBox1.Text = "No overdue return";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null)
                return;

            int id = int.Parse(comboBox1.SelectedItem.ToString().Split(' ')[0]);

            VisitorEntity visitorEntity = _visitorService.Get(id);
            visitorEntity.InBlackList = true;
            _visitorService.Update(visitorEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"Manager {_managerEntity.Name} {_managerEntity.Surname} append visitor {visitorEntity.Name} {visitorEntity.Surname} to blacklist",
            };
            _historyService.Create(historyEntity);

            MessageBox.Show($"Visitor {visitorEntity.Name} {visitorEntity.Surname} appended to blacklist");
            this.Close();
        }
    }
}