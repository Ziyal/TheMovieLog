using System.Collections.Generic;
using UserDashboard.Models;

namespace MovieLog.Models
{
    public class Image : BaseEntity
    {

        public int ImageId { get; set; }
        // public binary Name { get; set; }

        // public binary Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public int UserId { get; set; }



    }
}