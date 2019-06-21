using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    public class RadioManRepository : IDisposable, IAirlineRepository<RadioMan>
    {
        private AirlineContext context;

        public RadioManRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<RadioMan> GetList()
        {
            return context.RadioMen.ToList();
        }

        public RadioMan GetByID(int? id)
        {
            return context.RadioMen.Find(id);
        }

        public void Create(RadioMan radioMan)
        {
            context.RadioMen.Add(radioMan);
        }

        public void Delete(int? radioManID)
        {
            var r = context.RaceTeams.Where(x => x.RadioManID == radioManID);
            if (r != null)
            {
                foreach (RaceTeam a in r)
                {
                    a.RadioManID = null;
                }
            }
            context.SaveChanges();


            RadioMan radioMan = context.RadioMen.Find(radioManID);
            context.RadioMen.Remove(radioMan);
        }

        public void Update(RadioMan radioMan)
        {
            context.Entry(radioMan).State = EntityState.Modified;
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
