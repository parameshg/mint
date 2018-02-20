using Microsoft.Extensions.Configuration;
using Mint.Business;
using Mint.Business.Services;
using Mint.Business.Services.Interfaces;
using Mint.Database;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using SimpleInjector;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Import
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var container = new Container();

            container.RegisterSingleton(new DatabaseContext(configuration["db:connection"]));
            container.Register<IUserRepository, UserRepository>();
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<ICategoryRepository, CategoryRepository>();
            container.Register<ISubCategoryRepository, SubCategoryRepository>();
            container.Register<ITagRepository, TagRepository>();
            container.Register<ITransactionTypeRepository, TransactionTypeRepository>();
            container.Register<ITransactionRepository, TransactionRepository>();
            container.Register<ISeedRepository, SeedRepository>();

            container.RegisterSingleton<IServiceContext, ServiceContext>();
            container.Register<IUserService, UserService>();
            container.Register<IAccountService, AccountService>();
            container.Register<ICategoryService, CategoryService>();
            container.Register<ISubCategoryService, SubCategoryService>();
            container.Register<ITagService, TagService>();
            container.Register<ITransactionTypeService, TransactionTypeService>();
            container.Register<ITransactionService, TransactionService>();

            container.Register<IProcessor, Importer>();

            var users = await container.GetInstance<IUserService>().GetUsers();

            var user = users.FirstOrDefault(i => i.Email.Equals(args[0]));

            if (user != null)
            {
                Console.WriteLine($"UserId: {user.ID}");
                container.GetInstance<IServiceContext>().User = user.ID;

                var accounts = await container.GetInstance<IAccountService>().GetAccounts();

                var account = accounts.FirstOrDefault(i => i.Name.Equals(args[1]));

                if (account != null)
                {
                    Console.WriteLine($"AccountId: {account.ID}");
                    container.GetInstance<IServiceContext>().Account = account.ID;
                    await container.GetInstance<IProcessor>().Execute(args[2]);
                }
            }
        }
    }
}