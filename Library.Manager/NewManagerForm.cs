using Library.Database.Entities;
using Library.Services.HistoryServices;
using Library.Services.ManagerServices;

namespace Library.Manager
{
    public partial class NewManagerForm : Form
    {
        private readonly IManagerService _managerService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;

        public NewManagerForm(IManagerService managerService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _managerService = managerService;
            _historyService = historyService;

            _managerEntity = managerEntity;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerEntity managerEntity = new ManagerEntity()
            {
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Login = textBox3.Text,
                Password = textBox4.Text,
            };

            if(_managerService.IsExists(managerEntity))
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;

                return;
            };

            _managerService.Create(managerEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"{_managerEntity.Name} {_managerEntity.Surname} added new manager {managerEntity.Name} {managerEntity.Surname}",
            };
            _historyService.Create(historyEntity);

            this.Close();
        }
    }
}