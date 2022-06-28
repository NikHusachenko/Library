namespace Library.Database.Entities
{
    public class VisitorEntity
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public bool InBlackList { get; set; } = false;
        public List<BookEntity> Books { get; set; }

        public VisitorEntity()
        {
            Books = new List<BookEntity>();
        }
    }
}