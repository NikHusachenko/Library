using Library.Database.Entities;

namespace Library.Services.VisitorServices
{
    public interface IVisitorService
    {
        public void Create(VisitorEntity entity);

        public void Update(VisitorEntity entity);

        public void Delete(VisitorEntity entity);

        public List<VisitorEntity> GetAll();

        public VisitorEntity Get(int id);

        public VisitorEntity Get(string login);

        public VisitorEntity Get(string login, string password);

        public VisitorEntity Get(string name, string surname, string login);

        public VisitorEntity Get(BookEntity entity);

        public List<VisitorEntity> Get(bool InBlackList);

        public bool IsExists(VisitorEntity entity);
    }
}