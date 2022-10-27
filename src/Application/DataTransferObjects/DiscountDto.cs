using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects
{
    public record DiscountDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Percentage { get; set; }
    }
     public record DiscountCreateDto
    {
        [Required]
        [EnumDataType(typeof(EDiscountType))]
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Percentage { get; set; }
    }

}
