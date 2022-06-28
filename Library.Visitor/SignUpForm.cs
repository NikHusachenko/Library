using Library.Database.Entities;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Visitor
{
    public partial class SignUpForm : Form
    {
        private IVisitorService _visitorService;
        private IHistoryService _historyService;

        public SignUpForm(IVisitorService visitorService,
            IHistoryService historyService)
        {
            _visitorService = visitorService;
            _historyService = historyService;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VisitorEntity visitorEntity = new VisitorEntity()
            {
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Login = textBox3.Text,
                Password = textBox4.Text,
            };

            if(_visitorService.IsExists(visitorEntity))
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;

                return;
            }

            _visitorService.Create(visitorEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                 CreatedDate = DateTime.Now,
                 Description = $"Visitor {visitorEntity.Name} {visitorEntity.Surname} sign up in system",
            };
            _historyService.Create(historyEntity);

            this.Close();
        }
    }
}