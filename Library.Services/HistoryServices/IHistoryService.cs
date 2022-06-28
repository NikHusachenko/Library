using Library.Database.Entities;

namespace Library.Services.HistoryServices
{
    public interface IHistoryService
    {
        public void Create(HistoryEntity entity);

        public void Update(HistoryEntity entity);

        public void Delete(HistoryEntity entity);

        public void DeleteAll();

        public HistoryEntity Get(int id);

        public HistoryEntity Get(DateTime date);

        public List<HistoryEntity> Get(DateTime from, DateTime to);

        public List<HistoryEntity> Get();
    }
}