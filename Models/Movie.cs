using UserDashboard.Models;

namespace MovieLog.Models
{
    public class Movie : BaseEntity
    {
        public int MovieId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Release { get; set; }
        public int ListId { get; set; }

    }
}