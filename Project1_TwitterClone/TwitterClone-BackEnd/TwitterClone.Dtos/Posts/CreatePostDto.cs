using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.DTOs.Posts
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public int? RetweetPostId { get; set; }


    }
}
