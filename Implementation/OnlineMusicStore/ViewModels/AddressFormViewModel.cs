using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineMusicStore.Models;
using OnlineMusicStore.Models.NetworkModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMusicStore.ViewModels
{
    public class AddressFormViewModel
    {
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Address" : "Add an Address";
            }
        }

        public int? Id { get; set; }

        public string ReturnUrl { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string ZIP { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public SelectList Countries { get; }

        public AddressFormViewModel()
        {
            Id = 0;
        }

        public AddressFormViewModel(List<Country> countries)
        {
            Id = 0;
            this.Countries = new SelectList(countries, "Name", "Name");
        }

        public AddressFormViewModel(Address address, List<Country> countries)
        {
            Id = address.Id;
            Street = address.Street;
            ZIP = address.ZIP;
            City = address.City;
            State = address.State;
            Country = address.Country;

            Countries = new SelectList(countries, "Name", "Name");
        }
    }
}
