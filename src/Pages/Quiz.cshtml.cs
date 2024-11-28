using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContosoCrafts.WebSite.Pages
{
    public class QuizModel : PageModel
    {
        public int CurrentQuestion { get; set; } = 0;
        public bool IsSubmitted { get; set; } = false;
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
        public string[][] Options { get; set; } = new[]
        {
            new[] { "Calm and serene mountains for reflection and peace", "Vast and endless oceans full of adventure", "Dense and mysterious forests to explore", "Hot and challenging deserts that test your limits" },
            new[] { "Brave and always willing to take on challenges", "Calm and composed in any situation", "Cheerful and uplifting to those around you", "Mysterious and often keeping to yourself" },
            new[] { "Early morning when the world feels fresh and new", "Afternoon when energy and activity are at their peak", "Evening when everything feels calm and reflective", "Late at night, a time for creativity and solitude" },
            new[] { "A sharp sword that represents power and honor", "A bow and arrows to strike from a distance", "Magical spells to manipulate the world around you", "Your fists to prove your strength through physical combat" },
            new[] { "The love and support of my family and friends", "The thrill of adventure and discovering new things", "The pursuit of knowledge and understanding", "The desire to uphold justice and honor" },
            new[] { "Action-packed adventures filled with battles", "Lighthearted comedies to make me laugh", "Emotional dramas with deep storylines", "Mysteries that keep me guessing until the end" },
            new[] { "A hero who protects and inspires others", "A strategist or tactician guiding from behind the scenes", "An explorer seeking out new horizons", "A mentor or teacher who helps others grow" },
            new[] { "The blazing and intense force of fire", "The calming and adaptable flow of water", "The grounding and reliable strength of earth", "The free and untamed power of wind" },
            new[] { "The leader who guides the group", "The supportive and dependable friend", "The troublemaker who keeps things exciting", "The thinker who analyzes every situation" },
            new[] { "A powerful dragon with untold strength", "A graceful phoenix that symbolizes rebirth", "A magical unicorn representing purity", "A fierce griffin that commands the skies" }
    };
        private List<int> Answers { get; set; } = new List<int>();
        public string ResultName { get; set; }
        public string ResultDescription { get; set; }
        public string ResultVideoUrl { get; set; }

        public void OnPost()
        {
            if (Request.Form.ContainsKey("Answer"))
            {
                int answer = int.Parse(Request.Form["Answer"]);
                CurrentQuestion = int.Parse(Request.Form["CurrentQuestion"]);
                Answers.Add(answer);
                CurrentQuestion++;
            }

            if (CurrentQuestion >= Questions.Length)
            {
                IsSubmitted = true;
                CalculateResult();
            }
        }
        public void OnGet()
        {
            IsSubmitted = false;
            CurrentQuestion = 0;
            Answers.Clear();
        }

        private void CalculateResult()
        {
            // Calculate dominant answer based on frequency (this logic can be adjusted for better customization)
            int dominantAnswer = Answers.GroupBy(x => x).OrderByDescending(g => g.Count()).Select(g => g.Key).First();

            ResultName = dominantAnswer switch
            {
                0 => "Naruto Uzumaki",
                1 => "Luffy D. Monkey",
                2 => "Tanjiro Kamado",
                3 => "Roronoa Zoro",
                _ => "A Unique Character"
            };

        }
    }
}