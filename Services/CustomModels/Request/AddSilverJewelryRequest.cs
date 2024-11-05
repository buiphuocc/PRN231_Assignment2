using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomModels.Request
{
    public class AddSilverJewelryRequest
    {
        public string SilverJewelryId { get; set; } = null!;

        public string SilverJewelryName { get; set; } = null!;

        public string? SilverJewelryDescription { get; set; }

        public decimal? MetalWeight { get; set; }

        public decimal? Price { get; set; }

        public int? ProductionYear { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CategoryId { get; set; }
    }
}
