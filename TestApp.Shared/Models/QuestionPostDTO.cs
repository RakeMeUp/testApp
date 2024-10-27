﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class QuestionPostDTO
    {
        [Required]
        public string QuestionText { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public float MaxGrade { get; set; }
    }
}