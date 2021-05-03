using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineMusicStore.Helpers
{
    public static class ArtistBadgeClassHelperExtension
    {
        public static string GetCSSClass(this IHtmlHelper htmlHelper, float rating)
        {
            string classToBereturned = "badge rounded-pill";

            if (rating <= 2.5)
                classToBereturned += " badge-danger";
            else if (rating > 2.5 && rating <= 3.5)
                classToBereturned += " badge-warning";
            else
                classToBereturned += " badge-success";

            return classToBereturned;
        }
    }
}