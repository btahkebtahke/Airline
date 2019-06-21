using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Repository
{
    public interface IAirlineRepository<Tentity> : IDisposable where Tentity:class
    {
        IEnumerable<Tentity> GetList();
        Tentity GetByID(int? entityId);
        void Create(Tentity entity);
        void Delete(int? entityID);
        void Update(Tentity entity);
        void Save();
    }
}
