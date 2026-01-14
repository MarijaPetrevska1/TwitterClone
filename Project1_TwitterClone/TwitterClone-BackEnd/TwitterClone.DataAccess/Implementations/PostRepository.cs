using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.DataAccess.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly TwitterDbContext _context;

        public PostRepository(TwitterDbContext context)
        {
            _context = context;
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public List<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.RetweetPost)
                .ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.RetweetPost)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetByUserId(int userId)
        {
            return _context.Posts
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
        public void AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                throw new Exception(inner);
            }
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .OrderBy(c => c.CreatedAt)
                .ToList();
        }
    }
}
