using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface ITagService : IService
    {
        Task<List<Tag>> GetTags();

        Task<Tag> GetTagById(int id);

        Task<int> CreateTag(string name, string description);

        Task<bool> UpdateTag(int id, string name, string description);

        Task<bool> DeleteTag(int id);
    }
}