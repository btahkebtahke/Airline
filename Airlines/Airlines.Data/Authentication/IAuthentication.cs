using Airlines.Data.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Authentication
{
     public interface IAuthentication : IDisposable
    {
        User CheckUser(string username, string password);
        void AddUser(string username,string email, string password);
        
    }
}
