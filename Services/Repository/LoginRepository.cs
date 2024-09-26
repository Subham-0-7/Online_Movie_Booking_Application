using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly OnlineMovieBookingApplicationContext userLoginDbContext;
        public LoginRepository(OnlineMovieBookingApplicationContext _userLoginDbContext)
        {
            userLoginDbContext = _userLoginDbContext;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return userLoginDbContext.Users.ToList();
        }
    }
}
