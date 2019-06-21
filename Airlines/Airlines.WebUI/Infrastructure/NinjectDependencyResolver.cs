using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airlines.Data.Repository;
using Airlines.Data.Entities;
using Airlines.Data.Authentication;

namespace Airlines.WebUI.Infrastructure
{
    //CLass for DI bindings
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAirlineRepository<RaceTeam>>().To<RaceTeamRepository>();
            kernel.Bind<IAirlineRepository<Race>>().To<RaceRepository>();
            kernel.Bind<IAirlineRepository<Pilot>>().To<PilotsRepository>();
            kernel.Bind<IAirlineRepository<Navigator>>().To<NavigatorRepository>();
            kernel.Bind<IAirlineRepository<RadioMan>>().To<RadioManRepository>();
            kernel.Bind<IAirlineRepository<Stuardess>>().To<StuardessRepository>();
            kernel.Bind<IAirlineRepository<Query>>().To<QueryRepository>();
            kernel.Bind<IAuthentication>().To<Authentication>(); 
        }
    }
}