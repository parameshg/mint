
using Mint.Database.Repositories.Interfaces;
using AutoMapper;

namespace Mint.Database.Repositories
{
    public abstract class Repository : IRepository
    {
        protected DatabaseContext Context { get; }

        protected IMapper Mapper { get; }

        public Repository(DatabaseContext context)
        {
            Context = context;

            Mapper = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, Entity.User>();
                cfg.CreateMap<Entity.User, User>();
            }).CreateMapper();
        }
    }
}