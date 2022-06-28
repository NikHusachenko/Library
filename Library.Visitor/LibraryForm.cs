using Library.Database.Entities;

namespace Library.Visitor
{
    public partial class LibraryForm : Form
    {
        private readonly VisitorEntity _visitorEntity;

        public LibraryForm(VisitorEntity visitorEntity)
        {
            _visitorEntity = visitorEntity;

            InitializeComponent();

            richTextBox1.Text = String.Empty;
            if (_visitorEntity.Books.Count > 0)
            {
                foreach (var book in _visitorEntity.Books)
                {
                    if (book.IsIssued)
                    {
                        richTextBox1.Text += $"{book.Title} {book.Author} {book.YearOfIssue} return at {book.ReturnDate}\n\n";
                    }
                }
            }
            
            if(richTextBox1.Text == String.Empty)
            {
                richTextBox1.Text = "No one books is ordeing";
            }
        }
    }
}