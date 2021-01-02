using System.ComponentModel.DataAnnotations;

namespace MyYahoo.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public Question question { get; set; }

    }
}