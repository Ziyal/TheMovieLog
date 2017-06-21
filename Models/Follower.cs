using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserDashboard.Models;

namespace MovieLog.Models
{
    public class Follower : BaseEntity
    {

        [Key]
        public int FollowersId { get; set; }
        public int UserId { get; set; }
        public int FollowingId { get; set; }

    }
}