using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects
{
    public record InvoiceDto
    {
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal DiscoutPercentage { get; set; }

    }
    public record GetInvoiceDto
    {
        public Guid CustomerId { get; set; }
        public string ShoppingType { get; set; }
        public decimal TotalBill { get; set; }

    }
}
