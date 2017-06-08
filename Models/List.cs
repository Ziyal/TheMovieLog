using UserDashboard.Models;

namespace MovieLog.Models
{
    public class List : BaseEntity
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        // public List<Movie> Movies { get; set; }
        // public List<Comment> Comments { get; set; }

    }
}