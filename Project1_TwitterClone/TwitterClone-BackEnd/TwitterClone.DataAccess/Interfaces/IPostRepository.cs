using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Domain.Entities;

namespace TwitterClone.DataAccess.Interfaces
{
    public interface IPostRepository
    {
        Post GetById(int id);
        List<Post> GetAll();
        List<Post> GetByUserId(int userId);
        void Add(Post post);
        void Update(Post post);
    }
}
