using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "����� ����������� �����")]
        public string Email { get; set; }
    }
}