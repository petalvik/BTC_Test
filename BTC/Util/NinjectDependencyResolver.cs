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
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IRepository<Hero>>().To<Repository<Hero>>().InRequestScope();
            _kernel.Bind<IRepository<Ability>>().To<Repository<Ability>>().InRequestScope();
        }
    }
}