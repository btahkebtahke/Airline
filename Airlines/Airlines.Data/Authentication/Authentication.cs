using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airlines.Data.Entities;
using Airlines.Data.AuthEntities;
using Airlines.Data.Repository;

namespace Airlines.Data.Authentication
{
    public class Authentication : IDisposable, IAuthentication
    {
        private AirlineContext context;
        public Authentication(AirlineContext context)
        {
            this.context = context;
        }
        public User CheckUser(string username, string password)
        {
            User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
        public void AddUser(string username,string email, string password)
        {
            context.Users.Add(new User { Username = username, Email = email, Password = password, RoleId = 3 });
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
