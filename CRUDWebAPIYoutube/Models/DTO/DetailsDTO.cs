namespace CRUDWebAPIYoutube.Models.DTO
{
    public class DetailsDTO
    {
        public Guid Id { get; set; }
        public long PanNumber { get; set; }
        public long AadharNumber { get; set; }
        public Guid ContactId { get; set; }

        public Contact contact { get; set; }

    }
}
