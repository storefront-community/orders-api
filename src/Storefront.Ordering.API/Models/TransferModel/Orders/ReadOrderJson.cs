using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storefront.Ordering.API.Models.DataModel.Orders;

namespace Storefront.Ordering.API.Models.TransferModel.Orders
{
    public sealed class ReadOrderJson : IActionResult
    {
        public ReadOrderJson() { }

        public ReadOrderJson(Order order)
        {
            Id = order.Id;
            Name = order.Name;
            Mobile = order.Mobile;
            Email = order.Email;
            Status = new OrderStatus(order).ToString().ToLower();
            OpenAt = order.OpenAt;
            CanceledAt = order.CanceledAt;
            StartedAt = order.StartedAt;
            CompletedAt = order.CompletedAt;
            ClosedAt = order.ClosedAt;
            Total = order.Total;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public decimal Total { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
