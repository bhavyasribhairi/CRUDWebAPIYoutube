using CRUDWebAPIYoutube.Utilities;

namespace CRUDWebAPIYoutube.Models
{
    public class Details
    {
        public Guid Id { get; set; }
        public long  PanNumber { get; set; }
        [ValidateAadhar(aadharNumber:123123123123, ErrorMessage ="Enter correct AAdhar Number")]
        public long AadharNumber { get; set; }
        public Guid ContactId { get; set; }

        public Contact contact { get; set; }


    }
}
