using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Data;
using OnlineMusicStore.Models;
using OnlineMusicStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineMusicStore.Network;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace OnlineMusicStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly ApplicationDbContext _dbContext;

        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _dbContext = context;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public IActionResult Index()
        {
            return View(_dbContext.AllUsers.ToList());
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _dbContext.AllUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            _dbContext.AllUsers.Remove(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> RegistrationConfirm(string returnUrl = null)
        {
            var countries = await new CountryApiRepository().GetAllCountries();
            return View("AddressForm", new AddressFormViewModel(countries) { ReturnUrl = returnUrl });
        }

        [Authorize]
        public async Task<IActionResult> SaveAddress(AddressFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("AddressForm", viewModel);

            var address = new Address()
            {
                Id = viewModel.Id ?? 0,
                Street = viewModel.Street,
                ZIP = viewModel.ZIP,
                City = viewModel.City,
                Country = viewModel.Country,
            };
            var user = await _userManager.GetUserAsync(User);
            address.IdentityUserId = user?.Id;


            _dbContext.Addresses.Add(address);

            await _dbContext.SaveChangesAsync();

            if (viewModel.ReturnUrl != null && viewModel.ReturnUrl != "")
                return LocalRedirect(viewModel.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
