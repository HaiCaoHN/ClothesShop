using System;
using System.Collections.Generic;

namespace SE1632_PRN211_CaoThanhHai_Project.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
