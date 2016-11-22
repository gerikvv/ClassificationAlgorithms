using System.Data.Entity;

namespace Classification.Model
{
    public class BuildingContextInitializer : CreateDatabaseIfNotExists<BuildingContext>
    {
        protected override void Seed(BuildingContext db)
        {
            db.Buildings.Add(new Building(1889, 2));
            db.Buildings.Add(new Building(1894, 5));
            db.Buildings.Add(new Building(1899, 4));
            db.Buildings.Add(new Building(1900, 3));
            db.Buildings.Add(new Building(1939, 7));
            db.Buildings.Add(new Building(1955, 12));
            db.Buildings.Add(new Building(1962, 8));
            db.Buildings.Add(new Building(1965, 5));
            db.Buildings.Add(new Building(1967, 11));
            db.Buildings.Add(new Building(1977, 9));
            db.Buildings.Add(new Building(1983, 9));
            db.Buildings.Add(new Building(1984, 10));
            db.Buildings.Add(new Building(1986, 30));
            db.Buildings.Add(new Building(1994, 12));
            db.Buildings.Add(new Building(1995, 25));
            db.Buildings.Add(new Building(2002, 8));
            db.Buildings.Add(new Building(2003, 3));
            db.Buildings.Add(new Building(2005, 35));
            db.Buildings.Add(new Building(2007, 25));
            db.Buildings.Add(new Building(2012, 15));

            db.SaveChanges();
        }
    }
}
