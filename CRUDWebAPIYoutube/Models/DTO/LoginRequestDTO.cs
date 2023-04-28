using System.ComponentModel.DataAnnotations;

namespace CRUDWebAPIYoutube.Models.DTO
{
    public class LoginRequestDTO
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
