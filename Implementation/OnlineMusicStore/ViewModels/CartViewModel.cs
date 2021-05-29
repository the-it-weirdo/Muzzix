using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public Order Order { get; set; }
        public double CartTotal { get; set; }
        public Music Music { get; set; }
    }
}
