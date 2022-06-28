using Library.Database.Entities;

namespace Library.Services.ManagerServices
{
    public interface IManagerService
    {
        public void Create(ManagerEntity entity);

        public void Delete(ManagerEntity entity);

        public void Update(ManagerEntity entity);

        public List<ManagerEntity> GetAll();

        public ManagerEntity Get(int id);

        public ManagerEntity Get(string login);

        public ManagerEntity Get(string login, string password);

        public ManagerEntity Get(string name, string surname, string login);

        public bool IsExists(ManagerEntity entity);
    }
}