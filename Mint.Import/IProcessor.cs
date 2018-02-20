using System.Threading.Tasks;

namespace Mint.Import
{
    public interface IProcessor
    {
        Task Execute(string filename);
    }
}