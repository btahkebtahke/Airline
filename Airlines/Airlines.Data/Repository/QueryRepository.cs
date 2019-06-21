using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    public class QueryRepository : IDisposable, IAirlineRepository<Query>
    {
        private AirlineContext context;

        public QueryRepository(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<Query> GetList()
        {
            return context.Queries.Include(r=>r.Race).Include(r=>r.RaceTeam).ToList();
        }

        public Query GetByID(int? id)
        {
            return context.Queries.Find(id);
        }

        public void Create(Query query)
        {
            query = GetList().FirstOrDefault(r=>r.RaceTeam.ID== query.RaceTeamID);

            Race race = context.Races.Include(r => r.RaceTeam).FirstOrDefault(r => r.ID == query.ID);

            race.RaceTeamID = query.RaceTeamID;
            context.SaveChanges();
        }

        public void Delete(int? queryID)
        {
            Pilot pilot = context.Pilots.Find(queryID);
            context.Pilots.Remove(pilot);
        }

        public void Update(Query query)
        {
            context.Entry(query).State = EntityState.Modified;
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

