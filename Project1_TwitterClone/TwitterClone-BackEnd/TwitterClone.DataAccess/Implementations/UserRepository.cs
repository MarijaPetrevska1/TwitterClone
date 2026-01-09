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
    public class UserRepository : IUserRepository
    {
        private readonly TwitterDbContext _context;

        public UserRepository(TwitterDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetById(int id)
        {
            return _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Username == username);
        }
    }
}
