using Library.Database.Entities;
using Library.Services.HistoryServices;
using Library.Services.VisitorServices;

namespace Library.Manager
{
    public partial class RemoveFromBlacklistForm : Form
    {
        private readonly IVisitorService _visitorService;
        private readonly IHistoryService _historyService;

        private readonly ManagerEntity _managerEntity;

        private List<VisitorEntity> _visitorEntities;

        public RemoveFromBlacklistForm(IVisitorService visitorService,
            IHistoryService historyService,
            ManagerEntity managerEntity)
        {
            _visitorService = visitorService;
            _historyService = historyService;

            _managerEntity = managerEntity;

            _visitorEntities = _visitorService.Get(true);

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            comboBox1.Items.Clear();

            if(_visitorEntities.Count > 0)
            {
                foreach(var entity in _visitorEntities)
                {
                    richTextBox1.Text += $"{entity.Name} {entity.Surname}\n";
                    comboBox1.Items.Add($"{entity.ID} {entity.Name} {entity.Surname}");
                }
            }
            else
            {
                richTextBox1.Text = "Blacklist is empty";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null)
                return;

            int id = int.Parse(comboBox1.SelectedItem.ToString().Split(' ')[0]);

            VisitorEntity visitorEntity = _visitorService.Get(id);
            visitorEntity.InBlackList = false;
            _visitorService.Update(visitorEntity);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CreatedDate = DateTime.Now,
                Description = $"Manager {_managerEntity.Name} {_managerEntity.Surname} remove visitor {visitorEntity.Name} {visitorEntity.Surname} from blacklist",
            };
            _historyService.Create(historyEntity);

            MessageBox.Show($"Visitor {visitorEntity.Name} {visitorEntity.Surname} remove from blacklist");
            this.Close();
        }
    }
}