namespace Mint.Database.Test
{
    public class RepositoryTest
    {
        protected DatabaseContext Context { get; private set; }

        public RepositoryTest()
        {
            Context = new DatabaseContext(@"Server=(localdb)\mssqllocaldb;Database=mint;Trusted_Connection=True;");
        }
    }
}