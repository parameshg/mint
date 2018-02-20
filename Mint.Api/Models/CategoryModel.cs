namespace Mint.Api.Models
{
    public class CreateCategoryModel
    {
        public int Parent { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UpdateCategoryModel
    {
        public int ID { get; set; }

        public int Parent { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}