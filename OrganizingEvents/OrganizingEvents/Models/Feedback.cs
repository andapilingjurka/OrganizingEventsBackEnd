using System.ComponentModel.DataAnnotations;

namespace OrganizingEvents.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int EventsId { get; set; }

        public User User { get; set; }

        public Events Events { get; set; }
    }
}
