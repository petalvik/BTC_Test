using BTC.Interfaces;
using DAL.Repositories;

namespace BTC.Util
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}