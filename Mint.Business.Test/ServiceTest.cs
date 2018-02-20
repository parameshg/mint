using Moq;

namespace Mint.Business.Test
{
    public class ServiceTest
    {
        protected Mock<IServiceContext> Context { get; set; }

        public ServiceTest()
        {
            Context = new Mock<IServiceContext>();
            Context.SetupGet(i => i.User).Returns(1024);
            Context.SetupGet(i => i.Account).Returns(2);
        }
    }
}