using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mint.Api.Middlewares;
using Mint.Business;
using Mint.Business.Services;
using Mint.Business.Services.Interfaces;
using Mint.Database;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;

namespace Mint.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["db:connection"]));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ISeedRepository, SeedRepository>();

            services.AddScoped<IServiceContext, ServiceContext>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ITransactionTypeService, TransactionTypeService>();
            services.AddTransient<ITransactionService, TransactionService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<UserContextMapper>();
            app.UseCors(i => i.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseMvc();
        }
    }
}