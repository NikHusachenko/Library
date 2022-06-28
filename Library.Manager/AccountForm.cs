using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.ManagerServices;
using Library.Services.VisitorServices;

namespace Library.Manager
{
    public partial class AccountForm : Form
    {
        private readonly IManagerService _managerService;
        private readonly IHistoryService _historyService;
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;

        private readonly ManagerEntity _currentEntity;
        private List<HistoryEntity> _histories;

        public AccountForm(IManagerService managerService,
            IHistoryService historyService,
            IBookService bookService,
            IVisitorService visitorService,
            ManagerEntity managerEntity)
        {
            _managerService = managerService;
            _historyService = historyService;
            _bookService = bookService;
            _visitorService = visitorService;

            _currentEntity = managerEntity;
            _histories = _historyService.Get();

            InitializeComponent();

            label1.Text = _currentEntity.Name;
            label2.Text = _currentEntity.Surname;

            ShowHistory();
        }

        private void ShowHistory()
        {
            richTextBox1.Text = String.Empty;
            _histories = _historyService.Get();

            for (int i = _histories.Count - 1; i >= 0; i--)
                richTextBox1.Text += $"{_histories[i].Description}\n{_histories[i].CreatedDate}\n\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm(_bookService,
                _historyService,
                _currentEntity);
            form.ShowDialog();

            ShowHistory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RemoveBookForm form = new RemoveBookForm(_bookService,
                _historyService,
                _currentEntity);
            form.ShowDialog();

            ShowHistory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllBooksForm form = new AllBooksForm(_bookService);
            form.ShowDialog();
            ShowHistory();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NewManagerForm form = new NewManagerForm(_managerService,
                _historyService,
                _currentEntity);
            form.ShowDialog();

            ShowHistory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GiveBookForm form = new GiveBookForm(_bookService,
                _historyService,
                _currentEntity);
            form.ShowDialog();

            ShowHistory();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OrderedBooksForm form = new OrderedBooksForm(_bookService);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddToBlacklistForm form = new AddToBlacklistForm(_bookService,
                _visitorService,
                _historyService,
                _currentEntity);

            form.ShowDialog();
            ShowHistory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RemoveFromBlacklistForm form = new RemoveFromBlacklistForm(_visitorService,
                _historyService,
                _currentEntity);
            form.ShowDialog();
            ShowHistory();
        }
    }
}