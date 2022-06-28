using Library.Database.Entities;
using Library.Database.GenericRepository;

namespace Library.Services.ManagerServices
{
    public class ManagerService : IManagerService
    {
        private readonly IGenericRepository<ManagerEntity> _managerRepository;

        public ManagerService(IGenericRepository<ManagerEntity> managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public void Create(ManagerEntity entity)
        {
            _managerRepository.Create(entity);
        }

        public void Delete(ManagerEntity entity)
        {
            _managerRepository.Remove(entity);
        }

        public List<ManagerEntity> GetAll()
        {
            return _managerRepository.Get();
        }

        public ManagerEntity Get(int id)
        {
            return _managerRepository.GetById(id);
        }

        public ManagerEntity Get(string login)
        {
            return _managerRepository.Get(manager => manager.Login == login).FirstOrDefault();
        }

        public ManagerEntity Get(string login, string password)
        {
            return _managerRepository.Get(manager => manager.Login == login &&
                manager.Password == password)
                .FirstOrDefault();
        }

        public ManagerEntity Get(string name, string surname, string login)
        {
            if (String.IsNullOrEmpty(login))
                return _managerRepository.Get(manager => manager.Name == name &&
                    manager.Surname == surname)
                    .FirstOrDefault();
            else
                return _managerRepository.Get(manager => manager.Name == name &&
                    manager.Surname == surname &&
                    manager.Login == login)
                    .FirstOrDefault();
        }

        public bool IsExists(ManagerEntity entity)
        {
            List<ManagerEntity> managerEntities = GetAll();
            foreach(var manager in managerEntities)
            {
                if (manager.Name == entity.Name &&
                    manager.Surname == entity.Surname)
                    return true;

                if (manager.Login == entity.Login)
                    return true;
            }

            return false;
        }

        public void Update(ManagerEntity entity)
        {
            _managerRepository.Update(entity);
        }
    }
}