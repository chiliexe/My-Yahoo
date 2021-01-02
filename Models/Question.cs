using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyYahoo.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Answer> Answers { get; set; }
    }
}