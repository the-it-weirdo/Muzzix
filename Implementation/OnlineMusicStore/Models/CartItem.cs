using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
	
//	[Index(IsUnique = false)]
        public int MusicId { get; set; }
        public Music Music { get; set; }

        public double Amount { get; set; }

        public string CartId { get; set; }
    }
}
