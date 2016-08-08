using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "�������� {0} ������ ��������� �� ����� {2} ��������.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "����� ������")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "������������� ������ ������")]
        [Compare("NewPassword", ErrorMessage = "����� ������ � ��� ������������� �� ���������.")]
        public string ConfirmPassword { get; set; }
    }
}