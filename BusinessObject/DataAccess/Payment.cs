using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess;

public partial class Payment
{
    public int PaymentId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
