using UserDashboard.Models;
using System.Collections.Generic;
using System;

namespace MovieLog.Models
{
    public class List : BaseEntity
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Movie> Movies { get; set; }
        public User User { get; set; }
        // public List<User> Users { get; set; }

        public List () {
            Movies = new List<Movie>();
            // Users = new List<User>();
        }
    }
}