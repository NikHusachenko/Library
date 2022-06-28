using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.ManagerServices;
using Library.Services.VisitorServices;

namespace Library.Manager
{
    public partial class SignInForm : Form
    {
        private readonly IManagerService _managerService;
        private readonly IHistoryService _historyService;
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;

        public SignInForm(IManagerService managerService,
            IHistoryService historyService,
            IBookService bookService,
            IVisitorService visitorService)
        {
            _managerService = managerService;
            _historyService = historyService;
            _bookService = bookService;
            _visitorService = visitorService;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            ManagerEntity managerEntity = _managerService.Get(login, password);

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            if (managerEntity is null)
                return;

            AccountForm form = new AccountForm(_managerService,
                _historyService,
                _bookService,
                _visitorService,
                managerEntity);
            form.ShowDialog();
        }
    }
}