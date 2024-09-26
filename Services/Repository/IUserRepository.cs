using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(User user);
        bool DeleteUser(int id);
        bool UserExists(int userId);
        int SetAdmin(int userId);
    }
}
