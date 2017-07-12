using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new ApplicationContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
            _clientManager = new ClientManager(_db);
        }

        public ApplicationContext Context
        {
            get { return _db; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public IClientManager ClientManager
        {
            get { return _clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _clientManager.Dispose();
                }
                disposed = true;
            }
        }
    }
}