﻿
using BulletinBoard.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Data.ViewModels
{
    public class PostVM
    {
        [Required(ErrorMessage = "Titile is required")]
        [MaxLength(70)]
        public string Titile { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(1), MaxLength(9000)]
        public string Description { get; set; }

    }
}
