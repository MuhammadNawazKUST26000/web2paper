
using System.Data.Entity;


namespace Labourproject.Models
{
    public class Labour
    {
        public int ID { get; set; }
        public string LabourName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
    public class LabourDBContext : DbContext
    {
        public DbSet<Labour> Labour { get; set; }
    }
}