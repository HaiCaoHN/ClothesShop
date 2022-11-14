using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Total { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
