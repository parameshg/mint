using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ISeedRepository : IRepository
    {
        Task<bool> Install();

        Task<bool> Populate(int user);

        Task<bool> Uninstall();
    }
}