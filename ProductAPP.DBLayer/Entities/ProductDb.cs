
namespace ProductAPP.DBLayer.Entities
{
    public class ProductDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public float RFSize { get; set; }
        public bool IsDeleted { get; set; }
    }
}
