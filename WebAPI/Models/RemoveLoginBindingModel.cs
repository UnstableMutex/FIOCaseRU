using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "��������� �����")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "���� ����������")]
        public string ProviderKey { get; set; }
    }
}