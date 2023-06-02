using Back_End.Models.Books;
using Back_End.Models.Users;

namespace Back_End.Models.Reviews
{
    public class Review : BaseEntity.BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; } // 1-5
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
