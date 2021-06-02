using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMusicStore.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string ZIP { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
