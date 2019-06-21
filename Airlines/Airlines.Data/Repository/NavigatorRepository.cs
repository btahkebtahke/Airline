using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    public class NavigatorRepository : IDisposable, IAirlineRepository<Navigator>
    {
        private AirlineContext context;

        public NavigatorRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<Navigator> GetList()
        {
            return context.Navigators.ToList();
        }

        public Navigator GetByID(int? id)
        {
            return context.Navigators.Find(id);
        }

        public void Create(Navigator navigator)
        {
            context.Navigators.Add(navigator);
        }

        public void Delete(int? navigatorID)
        {
            var r = context.RaceTeams.Where(x => x.NavigatorID == navigatorID);
            if (r != null)
            {
                foreach (RaceTeam a in r)
                {
                    a.NavigatorID = null;
                }
            }
            context.SaveChanges();


            Navigator navigator = context.Navigators.Find(navigatorID);
            context.Navigators.Remove(navigator);
        }

        public void Update(Navigator navigator)
        {
            context.Entry(navigator).State = EntityState.Modified;
        }

        public void Save()
        {
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
