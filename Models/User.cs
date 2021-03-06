using System.Collections.Generic;
using UserDashboard.Models;

namespace MovieLog.Models
{
    public class User : BaseEntity
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string About { get; set; }

        public string FavoriteMovie { get; set; }

        public string ProfilePicture { get; set; }

        public List<List> Lists { get; set; }

        public List<Follower> Followers { get; set; }

        public User () {
            Lists = new List<List>();
            Followers = new List<Follower>();
        }

    }
}