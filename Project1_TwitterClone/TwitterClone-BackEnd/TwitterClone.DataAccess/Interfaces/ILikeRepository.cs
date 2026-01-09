using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Domain.Entities;

namespace TwitterClone.DataAccess.Interfaces
{
    public interface ILikeRepository
    {
        Like GetByUserAndPost(int userId, int postId);
        void Add(Like like);
        void Remove(Like like);
        List<Like> GetByPostId(int postId);
    }
}
