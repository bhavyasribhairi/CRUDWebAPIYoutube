using CRUDWebAPIYoutube.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDWebAPIYoutube.Models
{
    [Table("Contacts")]
    public class Contact
    {
        public Guid Id { get; set; }
        [MinLength(3)]
        [MaxLength(5)]
        public string FullName { get; set; }
        [Required]
        [ValidEmail("bhavya@gmail.com",ErrorMessage ="Email cant be configured")]
        public string Email { get; set; }

        public long Phone { get; set; }


        public string Address { get; set; }
    }
}
