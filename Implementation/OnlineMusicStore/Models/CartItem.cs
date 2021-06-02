using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMusicStore.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int MusicId { get; set; }
        public Music Music { get; set; }

        public double Amount { get; set; }

        public string CartId { get; set; }
    }
}
