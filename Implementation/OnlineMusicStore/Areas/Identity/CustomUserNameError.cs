using Microsoft.AspNetCore.Identity;

namespace OnlineMusicStore.Areas.Identity
{
    public class CustomUserNameError : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            var error = base.InvalidUserName(userName);
            error.Description = $"Username '{userName}' is invalid, can only contain letters or digits or spaces in between letters/words.";
            return error;
        }
    }
}
