using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineMovieBookingApplicationContext userDbContext;
        public UserRepository(OnlineMovieBookingApplicationContext _userDbContext)
        {
            userDbContext = _userDbContext;
        }

        public int AddUser(User user)
        {
            userDbContext.Users.Add(user);
            return userDbContext.SaveChanges();
        }

        public bool DeleteUser(int id)
        {
            var filterData = userDbContext.Users.SingleOrDefault(u => u.UserId == id);
            var result = userDbContext.Users.Remove(filterData);
            userDbContext.SaveChanges();
            return result != null ? true : false;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userDbContext.Users.ToList();
        }

        public User GetUser(int id)
        {
            return userDbContext.Users.SingleOrDefault(u => u.UserId == id);
        }

        public int UpdateUser(User user)
        {
            userDbContext.Users.Update(user);
            return userDbContext.SaveChanges();
        }

        public bool UserExists(int userId)
        {
            return userDbContext.Users.Any(u => u.UserId == userId);
        }

        public int SetAdmin(int userId)
        {
            var user = userDbContext.Users.SingleOrDefault(u => u.UserId == userId);
            user.IsAdmin = 1;
            userDbContext.Users.Update(user);
            return userDbContext.SaveChanges();
        }
    }
}
