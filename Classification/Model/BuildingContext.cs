using System.Data.Entity;

namespace Classification.Model
{
    public class BuildingContext : DbContext
    {
        static BuildingContext()
        {
            Database.SetInitializer(new BuildingContextInitializer());
        }

        public BuildingContext() : base("DbConnection") { }
        
        public DbSet<Building> Buildings { get; set; }
    }
}
