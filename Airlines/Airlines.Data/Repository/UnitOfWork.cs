using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    //The class that is created to use the same context for the different repositories
    public class UnitOfWork : IDisposable
    {
        AirlineContext db = new AirlineContext();

        private QueryRepository queryRepository;
        private RaceRepository raceRepository;
        private StuardessRepository strdRepository;
        private NavigatorRepository naviRepository;
        private PilotsRepository pilotRepository;
        private RadioManRepository radioRepository;


        public QueryRepository Queries
        {
            get
            {
                if (queryRepository == null)
                    queryRepository = new QueryRepository(db);
                return queryRepository;
            }
        }

        public StuardessRepository Stuardesses
        {
            get
            {
                if (strdRepository == null)
                    strdRepository = new StuardessRepository(db);
                return strdRepository;
            }
        }


        public NavigatorRepository Navigators
        {
            get
            {
                if (naviRepository == null)
                    naviRepository = new NavigatorRepository(db);
                return naviRepository;
            }
        }


        public PilotsRepository Pilots
        {
            get
            {
                if (pilotRepository == null)
                    pilotRepository = new PilotsRepository(db);
                return pilotRepository;
            }
        }

        public RadioManRepository RadioMen
        {
            get
            {
                if (radioRepository == null)
                    radioRepository = new RadioManRepository(db);
                return radioRepository;
            }
        }

        public RaceRepository Races
        {
            get
            {
                if (raceRepository == null)
                    raceRepository = new RaceRepository(db);
                return raceRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
