using System;
using System.Collections.Generic;

namespace SE1632_PRN211_CaoThanhHai_Project.Models
{
    public partial class Payment
    {
        public Payment()
        {
            OrderItems = new HashSet<OrderItem>();
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
