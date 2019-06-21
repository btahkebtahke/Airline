using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
        public class RaceRepository : IDisposable, IAirlineRepository<Race>
        {
        private AirlineContext context;

        public RaceRepository(AirlineContext context)
        {
            this.context = context;
        }
      
        public IEnumerable<Race> GetList()
        {
            return context.Races.Include(r=>r.RaceTeam).ToList();
        }

        public Race GetByID(int? id)
        {
            return context.Races.Find(id);
        }

        public void Create(Race race)
        {
            context.Races.Add(race);
        }

        public void Delete(int? raceID)
        {
            Race race = context.Races.Find(raceID);
            context.Races.Remove(race);
        }

        public void Update(Race race)
        {
            context.Entry(race).State = EntityState.Modified;
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
