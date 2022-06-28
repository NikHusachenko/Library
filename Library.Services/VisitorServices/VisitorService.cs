using Microsoft.EntityFrameworkCore;
using Library.Database.Entities;
using Library.Database.GenericRepository;

namespace Library.Services.VisitorServices
{
    public class VisitorService : IVisitorService
    {
        private readonly IGenericRepository<VisitorEntity> _visitorRepository;

        public VisitorService(IGenericRepository<VisitorEntity> visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public void Create(VisitorEntity entity)
        {
            _visitorRepository.Create(entity);
        }

        public void Delete(VisitorEntity entity)
        {
            _visitorRepository.Remove(entity);
        }

        public VisitorEntity Get(int id)
        {
            var visitor = _visitorRepository.Table
                .Where(visitor => visitor.ID == id)
                .Include(visitor => visitor.Books)
                .FirstOrDefault();

            return visitor;
        }

        public VisitorEntity Get(string login)
        {
            var visitor = _visitorRepository.Table
                .Where(visitor => visitor.Login == login)
                .Include(visitor => visitor.Books)
                .FirstOrDefault();

            return visitor;
        }

        public VisitorEntity Get(string login, string password)
        {
            var visitor = _visitorRepository.Table
                .Where(visitor => visitor.Login == login &&
                visitor.Password == password)
                .Include(visitor => visitor.Books)
                .FirstOrDefault();

            return visitor;
        }

        public VisitorEntity Get(string name, string surname, string login)
        {
            var visitor = _visitorRepository.Table
                .Where(visitor => visitor.Name == name &&
                visitor.Surname == surname &&
                visitor.Login == login)
                .Include(visitor => visitor.Books)
                .FirstOrDefault();

            return visitor;
        }

        public VisitorEntity Get(BookEntity entity)
        {
            var visitors = _visitorRepository.Table
                .Include(visitor => visitor.Books)
                .ToList();

            var visitor = visitors.First(visitor => visitor.Books.Contains(entity));

            return visitor;
        }

        public List<VisitorEntity> Get(bool InBlackList)
        {
            var visitors = _visitorRepository.Table
                .Where(visitor => visitor.InBlackList == InBlackList)
                .Include(visitor => visitor.Books)
                .ToList();

            return visitors;
        }

        public List<VisitorEntity> GetAll()
        {
            return _visitorRepository.Get();
        }

        public bool IsExists(VisitorEntity entity)
        {
            List<VisitorEntity> visitorEntities = GetAll();
            foreach(var visitor in visitorEntities)
            {
                if(visitor.Name == entity.Name &&
                    visitor.Surname == entity.Surname)
                    return true;

                if (visitor.Login == entity.Login)
                    return true;
            }

            return false;
        }

        public void Update(VisitorEntity entity)
        {
            _visitorRepository.Update(entity);
        }
    }
}