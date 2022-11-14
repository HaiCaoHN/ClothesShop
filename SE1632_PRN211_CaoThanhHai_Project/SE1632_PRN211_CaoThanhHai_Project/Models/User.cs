using System;
using System.Collections.Generic;

namespace SE1632_PRN211_CaoThanhHai_Project.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
