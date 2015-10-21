using System.Data.Entity;

namespace NerdDinner.Models
{
    public class NerdDinnersDataContext : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP> Rsvps { get; set; }
    }
}