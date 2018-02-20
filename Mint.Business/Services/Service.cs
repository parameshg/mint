using Mint.Business.Services.Interfaces;

namespace Mint.Business.Services
{
    public class Service : IService
    {
        public IServiceContext Context { get; }

        public Service(IServiceContext context)
        {
            Context = context;
        }
    }
}