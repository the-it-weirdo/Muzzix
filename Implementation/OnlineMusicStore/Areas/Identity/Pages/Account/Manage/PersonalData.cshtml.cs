using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineMusicStore.Data;
using Microsoft.EntityFrameworkCore;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        private readonly ApplicationDbContext _dbcontext;

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _dbcontext = context;
        }

        public Address UserAddress {get;set;}

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            UserAddress = await _dbcontext.Addresses.FirstOrDefaultAsync(ad => ad.IdentityUserId == user.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}