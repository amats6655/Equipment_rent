using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model
{
    public interface IUserRpository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(User auth);
        void Edit(User auth);
        void Remove(string id);
        User GetById(string id);
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
    }
}
