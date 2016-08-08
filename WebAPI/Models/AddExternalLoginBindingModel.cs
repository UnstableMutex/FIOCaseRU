using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    // Модели, используемые в качестве параметров действий AccountController.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "Внешний маркер доступа")]
        public string ExternalAccessToken { get; set; }
    }
}
