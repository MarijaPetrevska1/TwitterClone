using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.DataAccess.Implementations
{
    public class LikeRepository : ILikeRepository
    {
        private readonly TwitterDbContext _context;

        public LikeRepository(TwitterDbContext context)
        {
            _context = context;
        }

        public void Add(Like like)
        {
            _context.Likes.Add(like);
            _context.SaveChanges();
        }

        public Like GetByUserAndPost(int userId, int postId)
        {
            return _context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == postId);
        }

        public void Remove(Like like)
        {
            _context.Likes.Remove(like);
            _context.SaveChanges();
        }

        public List<Like> GetByPostId(int postId)
        {
            return _context.Likes.Where(l => l.PostId == postId).ToList();
        }
    }
}
