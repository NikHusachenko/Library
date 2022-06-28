using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    public partial class SignInForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        public SignInForm(IBookService bookService,
            IVisitorService visitorService,
            IHistoryService historyService)
        {
            _bookService = bookService;
            _visitorService = visitorService;
            _historyService = historyService;

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUpForm form = new SignUpForm(_visitorService,
                _historyService);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            VisitorEntity visitorEntity = _visitorService.Get(login, password);

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;

            if (visitorEntity is null)
                return;

            AccountForm form = new AccountForm(_bookService,
                _visitorService,
                _historyService,
                visitorEntity);
            form.ShowDialog();
        }
    }
}