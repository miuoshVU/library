using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class UpdateProposedBookDto
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public Uri? UrlLink { get; set; }
        public int? Points { get; set; }
        public Uri? Cover { get; set; }
        public string? Authors { get; set; }
        public string? Categories { get; set; }
    }
}
