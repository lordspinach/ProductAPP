using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPP.BLLayer.DTO
{
    public class SizeDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public float RFSize { get; set; }
        public float BrandSize { get; set; }
    }
}
