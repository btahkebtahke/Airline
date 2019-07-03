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
            return context.Queries.Include(q => q.Race).Include(q => q.RaceTeam).FirstOrDefault(r => r.ID == id);
        }

        public void Create(Query query)
        {
            context.Queries.Add(query);  
        }

        public void Delete(int? queryID)
        {
            Query query = context.Queries.Find(queryID);
            query.IsAccepted = false;
        }

        public void Update(Query query)
        {
            query.IsAccepted = true;
            query.Race.RaceTeamID = query.RaceTeamID;
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

