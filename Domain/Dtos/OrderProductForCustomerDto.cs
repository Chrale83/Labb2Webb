using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Dtos
{
    public class OrderProductForCustomerDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public string? CategoryName { get; set; }

        public int Quantity { get; set; }
    }
}
