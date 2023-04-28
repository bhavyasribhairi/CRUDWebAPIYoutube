using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CRUDWebAPIYoutube.Models.DTO
{
    public class RegisterRequestDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }




    }
}
