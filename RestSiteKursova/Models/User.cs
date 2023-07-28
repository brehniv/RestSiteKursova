using Microsoft.AspNetCore.Identity;

namespace RestSiteKursova.Models
{
    public class User:IdentityUser
    {
        public int Year { get; set; }
    }
}
