using Library.Database.Entities;
using Library.Database.GenericRepository;

namespace Library.Services.HistoryServices
{
    public class HistoryService : IHistoryService
    {
        private readonly IGenericRepository<HistoryEntity> _historyRepository;

        public HistoryService(IGenericRepository<HistoryEntity> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public void Create(HistoryEntity entity)
        {
            _historyRepository.Create(entity);
        }

        public void Delete(HistoryEntity entity)
        {
            _historyRepository.Remove(entity);
        }

        public void DeleteAll()
        {
            List<HistoryEntity> histories = Get();
            foreach (var history in histories)
                Delete(history);
        }

        public HistoryEntity Get(int id)
        {
            return _historyRepository.GetById(id);
        }

        public HistoryEntity Get(DateTime date)
        {
            var history = _historyRepository.Table
                .Where(history => history.CreatedDate == date)
                .FirstOrDefault();

            return history;
        }

        public List<HistoryEntity> Get(DateTime from, DateTime to)
        {
            var histories = _historyRepository.Table
                .Where(history => history.CreatedDate >= from &&
                history.CreatedDate <= to)
                .ToList();

            return histories;
        }

        public List<HistoryEntity> Get()
        {
            return _historyRepository.Get();
        }

        public void Update(HistoryEntity entity)
        {
            _historyRepository.Update(entity);
        }
    }
}