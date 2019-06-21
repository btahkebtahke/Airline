using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    public class PilotsRepository : IDisposable, IAirlineRepository<Pilot>
    {
        private AirlineContext context;

        public PilotsRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<Pilot> GetList()
        {
            return context.Pilots.ToList();
        }

        public Pilot GetByID(int? id)
        {
            return context.Pilots.Find(id);
        }

        public void Create(Pilot pilot)
        {
            context.Pilots.Add(pilot);
        }

        public void Delete(int? pilotID)
        {
            var r = context.RaceTeams.Where(x => x.PilotID == pilotID);
            if (r != null)
            {
                foreach (RaceTeam a in r)
                {
                    a.Pilot = null;
                }
            }
            context.SaveChanges();


            Pilot pilot = context.Pilots.Find(pilotID);
            context.Pilots.Remove(pilot);
        }

        public void Update(Pilot pilot)
        {
            context.Entry(pilot).State = EntityState.Modified;
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
