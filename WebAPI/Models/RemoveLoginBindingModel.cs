using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Поставщик входа")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Ключ поставщика")]
        public string ProviderKey { get; set; }
    }
}