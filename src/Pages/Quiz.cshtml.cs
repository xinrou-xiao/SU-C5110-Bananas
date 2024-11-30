using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Handles the logic for the Anime Personality Quiz, including storing questions,
    /// calculating results, and managing the quiz state.
    /// </summary>
    public class QuizModel : PageModel
    {
        /// <summary>
        /// The index of the current question being displayed.
        /// </summary>
        public int CurrentQuestion { get; set; } = 0;

        /// <summary>
        /// Indicates whether the quiz has been completed and submitted.
        /// </summary>
        public bool IsSubmitted { get; set; } = false;

        /// <summary>
        /// Array of quiz questions.
        /// </summary>
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

        /// <summary>
        /// Array of answer options corresponding to each question.
        /// </summary>
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


        /// <summary>
        /// List of answers selected by the user.
        /// </summary>
        public List<int> Answers { get; set; } = new List<int>();

        /// <summary>
        /// The name of the character that matches the user's answers.
        /// </summary>
        public string ResultName { get; set; }

        /// <summary>
        /// The description of the character's personality and traits.
        /// </summary>
        public string ResultDescription { get; set; }

        /// <summary>
        /// The URL of the video associated with the character result.
        /// </summary>
        public string ResultVideoUrl { get; set; }

        public void OnPost()
        {
            // Check if the request contains an answer
            if (Request.Form.ContainsKey("Answer"))
            {
                int answer = int.Parse(Request.Form["Answer"]);
                CurrentQuestion = int.Parse(Request.Form["CurrentQuestion"]);
                Answers.Add(answer);
                CurrentQuestion++;
            }

            // If all questions are answered, calculate the result
            if (CurrentQuestion >= Questions.Length)
            {
                IsSubmitted = true;
                CalculateResult();
            }
        }

        /// <summary>
        /// Handles GET requests to initialize the quiz.
        /// </summary>
        public void OnGet()
        {
            IsSubmitted = false;
            CurrentQuestion = 0;
            Answers.Clear();
        }

        /// <summary>
        /// Calculates the user's result based on their answers.
        /// </summary>
        private void CalculateResult()
        {

            // Calculate dominant answer based on frequency (this logic can be adjusted for better customization)
            int dominantAnswer = Answers.GroupBy(x => x).OrderByDescending(g => g.Count()).Select(g => g.Key).First();

            // Assign the result based on the dominant answer
            ResultName = dominantAnswer switch
            {
                0 => "Naruto Uzumaki (Anime: Naruto)",
                1 => "Luffy D. Monkey (Anime: One Piece)",
                2 => "Tanjiro Kamado (Anime: Demon Slayer)",
                3 => "Roronoa Zoro (Anime: One Piece)",
                // optional fallback for unexpected cases
                _ => "A Unique Character" 
            };

            // Assign the description based on the result
            ResultDescription = ResultName switch
            {
                "Naruto Uzumaki (Anime: Naruto)" => "You're energetic, optimistic, and never give up on your dreams. You inspire others with your determination and your unwavering belief in the power of friendship." +
                                    "<br/><br/><strong>Pros:</strong><ul>" +
                                    "<li>Boundless energy and optimism.</li>" +
                                    "<li>Inspires loyalty and camaraderie.</li>" +
                                    "<li>Unshakable determination to achieve your goals.</li></ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're the underdog who rises to greatness, proving that anything is possible with hard work and belief!",

                "Luffy D. Monkey (Anime: One Piece)" =>"You're adventurous, carefree, and a born leader. You live for freedom and thrive in the vast unknown, with your friends by your side as your greatest treasure." +
                                             "<br/><br/><strong>Pros:</strong><ul>" +
                                             "<li>Infectious enthusiasm and courage.</li>" +
                                             "<li>Unyielding loyalty to your crew and loved ones.</li>" +
                                             "<li>Fearless explorer with a big dream to achieve.</li></ul>" +
                                             "<br/><strong>Cool Factor:</strong> You're the embodiment of freedom and resilience, inspiring others to chase their dreams!",

                "Tanjiro Kamado (Anime: Demon Slayer)" => "You're compassionate, kind, and deeply protective of those you care about. Your resilience and determination to do what's right make you a true hero." +
                                    "<br/><br/><strong>Pros:</strong><ul>" +
                                    "<li>Empathy and kindness in the face of adversity.</li>" +
                                    "<li>Unyielding determination and resilience.</li>" +
                                    "<li>Strong sense of justice and loyalty to loved ones.</li></ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're the perfect balance of strength and heart, inspiring everyone with your heroic journey!",

                "Roronoa Zoro (Anime: One Piece)" => "You're fiercely determined and focused, never wavering from your goals. Your loyalty to those you care about is unbreakable, and you're always striving to be the best version of yourself." +
                                  "<br/><br/><strong>Pros:</strong><ul>" +
                                  "<li>Unyielding focus and perseverance.</li>" +
                                  "<li>Incredible strength and discipline.</li>" +
                                  "<li>Quietly protective and fiercely loyal.</li></ul>" +
                                  "<br/><strong>Cool Factor:</strong> You're the strong, silent type who can back up every word with action—cool, confident, and unstoppable!",

                _ => "You have a unique personality that defies conventional categories. Your individuality shines in ways that can't be boxed into one description. " +
                                   "<br/><br/><strong>Pros:</strong><ul>" +
                                   "<li>Adaptable and resourceful in any situation.</li>" +
                                   "<li>Creative thinker with a unique perspective on life.</li>" +
                                   "<li>Unpredictable and full of surprises, keeping others intrigued.</li>" +
                                   "<li>Open-minded and welcoming to diverse ideas and people.</li>" +
                                   "</ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're a trendsetter with a vibe that's entirely your own. People admire your authenticity and your ability to stay true to yourself, no matter what!"
            };

            // Assign the video URL based on the result
            ResultVideoUrl = ResultName switch
            {
                "Naruto Uzumaki (Anime: Naruto)" => "https://www.desktophut.com/files/dkZH0secomXUvl9_Naruto%20Artistic%20Live%20Wallpaper_2_151011.mp4",
                "Luffy D. Monkey (Anime: One Piece)" => "https://motionbgs.com/media/6827/strawhat-luffy.960x540.mp4",
                "Tanjiro Kamado (Anime: Demon Slayer)" => "https://motionbgs.com/media/6009/tanjiro-water-dragon.960x540.mp4",
                "Roronoa Zoro (Anime: One Piece)" => "https://motionbgs.com/media/6005/fearless-zoro.960x540.mp4",
                _ => "https://www.desktophut.com/files/8cqM7yJhpqNnBuw_Black%20Roses%20Goku%204K_2_102359.mp4"
            };
        }
    }
}