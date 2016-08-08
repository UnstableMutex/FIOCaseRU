using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "����� ����������� �����")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "�������� {0} ������ ��������� �� ����� {2} ��������.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "������������� ������")]
        [Compare("Password", ErrorMessage = "������ � ��� ������������� �� ���������.")]
        public string ConfirmPassword { get; set; }
    }
}