using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Database.Entities
{
    [Table("Books")]
    public class BookEntity
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public int YearOfIssue { get; set; }
        public bool IsIssued { get; set; }
        public bool InOrderWaiting { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        
        public int? VisitorFK { get; set; }
        public VisitorEntity? Visitor { get; set; }
    }
}