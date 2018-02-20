namespace Mint.Api.Models
{
    public class CreateAccountModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UpdateAccountModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}