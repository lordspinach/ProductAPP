namespace ProductAPP.API.Models.Requests
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public float RFSize { get; set; }
    }
}
