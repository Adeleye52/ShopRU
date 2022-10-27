using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects
{
    public record CouponDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
    }
     public record CouponCreateDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
    }

}
