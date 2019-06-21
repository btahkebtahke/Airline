using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
 
    public class RaceTeamRepository : IDisposable, IAirlineRepository<RaceTeam>
    {
        private AirlineContext context;
        public RaceTeamRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<RaceTeam> GetList()
        {
            return context.RaceTeams.
                Include(r => r.Stuardesses).
                Include(r => r.RadioMan).
                Include(r => r.Pilot).
                Include(r => r.Navigator);
        }

        public RaceTeam GetByID(int? id)
        {
            return context.RaceTeams.
                Include(r => r.Stuardesses).
                Include(r => r.RadioMan).
                Include(r => r.Pilot).
                Include(r => r.Navigator).FirstOrDefault(r => r.ID == id);
        }

        public void Create(RaceTeam raceTeam)
        {
            context.RaceTeams.Add(raceTeam);
        }

        public void Delete(int? raceTeamID)
        {
            var r = context.Races.Where(x => x.RaceTeamID == raceTeamID);
            if (r != null)
            {
                foreach (Race a in r)
                {
                    a.RaceTeamID = null;
                }
            }
            context.SaveChanges();

            var s = context.Stuardesses.Where(x => x.TeamID == raceTeamID);
            if (s != null)
            {
                foreach (Stuardess a in s)
                {
                    a.TeamID = null;
                }
            }
            context.SaveChanges();
            RaceTeam raceTeam = context.RaceTeams.Find(raceTeamID);
            context.RaceTeams.Remove(raceTeam);
        }

        public void Update(RaceTeam raceTeam)
        {
            context.Entry(raceTeam).State = EntityState.Modified;
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
