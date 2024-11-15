using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages
{
    public class DevelopersModel : PageModel
    {
        public List<Developer> Developers { get; set; }

        public void OnGet()
        {
            // Sample data for four developers
            Developers = new List<Developer>
            {
                new Developer { Name = "Xin Rou Xiao", Email = "xxiao@seattleu.edu", ImageUrl = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/50925624-027b-4835-bd54-9d934e1a8719/dfpfvo6-a6fb18d1-f982-4351-9a13-16ae03888ff9.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzUwOTI1NjI0LTAyN2ItNDgzNS1iZDU0LTlkOTM0ZTFhODcxOVwvZGZwZnZvNi1hNmZiMThkMS1mOTgyLTQzNTEtOWExMy0xNmFlMDM4ODhmZjkuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.XTT6YCEkGDGKZ66I2O-G2L9fHhO9-4rq0K5UlaSsBJM" },
                new Developer { Name = "Shanvi Sinha", Email = "ssinha1@seattleu.edu", ImageUrl = "https://img.freepik.com/premium-photo/female-developer-background_665280-9655.jpg?w=740" },
                new Developer { Name = "Samarth Tanwar", Email = "stanwar@seattleu.edu", ImageUrl = "https://img.freepik.com/premium-photo/dog-with-headphones-it-words-sound-back_887562-1432.jpg" },
                new Developer { Name = "Vineet Somwanshi", Email = "vsomwanshi@seattleu.edu", ImageUrl = "https://t3.ftcdn.net/jpg/06/01/17/18/360_F_601171862_l7yZ0wujj8o2SowiKTUsfLEEx8KunYNd.jpg" }
            };
        }
    }

    public class Developer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}
