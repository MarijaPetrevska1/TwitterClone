using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.DTOs.Posts
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; } 
        public int LikesCount { get; set; }   
        public int? RetweetId { get; set; }   
        public DateTime CreatedAt { get; set; }
    }
}
