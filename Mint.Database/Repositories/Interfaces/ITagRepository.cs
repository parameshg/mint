
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ITagRepository : IRepository
    {
        Task<List<Tag>> GetTags(int user);

        Task<Tag> GetTagById(int id);

        Task<int> CreateTag(int user, string name, string description);

        Task<bool> UpdateTag(int id, string name, string description);

        Task<bool> DeleteTag(int id);
    }
}