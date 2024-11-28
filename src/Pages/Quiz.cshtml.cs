using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContosoCrafts.WebSite.Pages
{
    public class QuizModel : PageModel
    {
        public string[] Questions { get; set; } = new[]
        {
            "What is your favorite type of environment?",
            "Which trait best describes you?",
            "What’s your favorite time of day?",
            "Which of these best represents your weapon of choice?",
            "What motivates you the most?",
            "Which genre do you prefer?",
            "What’s your dream job?",
            "What’s your favorite element?",
            "Which role do you take in your friend group?",
            "Which mythical creature do you prefer?"
        };
    
    }
}