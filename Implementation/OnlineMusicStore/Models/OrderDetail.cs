using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMusicStore.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int MusicId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public virtual Music music { get; set; }
        public virtual Order Order { get; set; }

    }
}
