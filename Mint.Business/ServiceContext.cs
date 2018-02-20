namespace Mint.Business
{
    public interface IServiceContext
    {
        int User { get; set; }

        int Account { get; set; }
    }

    public class ServiceContext : IServiceContext
    {
        public int User { get; set; }

        public int Account { get; set; }
    }
}