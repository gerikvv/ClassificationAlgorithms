﻿using System.Data.Entity;

namespace Classification.Model
{
    public class BuildingContext : DbContext
    {
        static BuildingContext()
        {
            Database.SetInitializer<BuildingContext>(null);
        }

        public BuildingContext() : base("DbConnection") { }
        
        public DbSet<Building> Buildings { get; set; }
    }
}
