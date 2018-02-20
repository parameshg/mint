namespace Mint.Api.Models
{
    public class CreateTagModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UpdateTagModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}