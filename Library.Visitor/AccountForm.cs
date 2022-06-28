using Library.Database.Entities;
using Library.Services.BookServices;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    public partial class AccountForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        private readonly VisitorEntity _visitorEntity;

        public AccountForm(IBookService bookService,
            IVisitorService visitorService,
            IHistoryService historyService,
            VisitorEntity visitorEntity)
        {
            _bookService = bookService;
            _visitorService = visitorService;
            _historyService = historyService;

            _visitorEntity = visitorEntity;

            InitializeComponent();

            label1.Text = _visitorEntity.Name;
            label2.Text = _visitorEntity.Surname;

            if (_visitorEntity.InBlackList)
                label3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_visitorEntity.InBlackList)
            {
                MessageBox.Show("In blacklist. Not access");
                return;
            }

            AllBooksForm form = new AllBooksForm(_bookService);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_visitorEntity.InBlackList)
            {
                MessageBox.Show("In blacklist. Not access");
                return;
            }

            OrderBookForm form = new OrderBookForm(_bookService,
                _visitorService,
                _historyService,
                _visitorEntity);
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LibraryForm form = new LibraryForm(_visitorEntity);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnBookForm form = new ReturnBookForm(_bookService,
                _visitorService,
                _historyService,
                _visitorEntity);
            form.ShowDialog();
        }
    }
}