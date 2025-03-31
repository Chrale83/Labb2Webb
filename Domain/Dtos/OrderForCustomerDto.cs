using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace Domain.Dtos
{
    public class OrderForCustomerDto
    {
        public int OrderId { get; set; }

        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderProductForCustomerDto> Products { get; set; } = new List<OrderProductForCustomerDto>();
    }
}
