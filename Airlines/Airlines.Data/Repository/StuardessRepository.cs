using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Airlines.Data.Repository
{
    public class StuardessRepository : IDisposable, IAirlineRepository<Stuardess>
    {
        private AirlineContext context;

        public StuardessRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<Stuardess> GetList()
        {
            return context.Stuardesses.Include(r => r.Team).ToList();
        }

        public Stuardess GetByID(int? id)
        {
            return context.Stuardesses.Include(r => r.Team).FirstOrDefault(r => r.ID == id);
        }

        public void Create(Stuardess stuardess)
        {
            context.Stuardesses.Add(stuardess);
        }

        public void Delete(int? stuardessID)
        {
            Stuardess stuardess = context.Stuardesses.Find(stuardessID);
            context.Stuardesses.Remove(stuardess);
        }

        public void Update(Stuardess stuardess)
        {
            context.Entry(stuardess).State = EntityState.Modified;
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
