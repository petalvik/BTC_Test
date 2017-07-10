using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using Ninject.Web.Common;
using DAL.Repositories;
using DAL.Models;

namespace BTC.Util
{
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
            kernel.Bind<IRepository<Hero>>().To<Repository<Hero>>().InRequestScope();
            kernel.Bind<IRepository<Ability>>().To<Repository<Ability>>().InRequestScope();
        }
    }
}