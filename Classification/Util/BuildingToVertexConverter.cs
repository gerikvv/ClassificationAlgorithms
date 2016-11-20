using System.Collections.Generic;
using System.Linq;
using Classification.Model;

namespace Classification.Util
{
    public static class BuildingToVertexConverter
    {
        public static IList<Vertex> ToListVertices(this ICollection<Building> buildings)
        {
            return buildings.Select(building => new Vertex(building.Id, building.CountFloors, building.Year)).ToList();
        }

        public static Vertex ToVertex(this Building building)
        {
            return new Vertex(building.Id, building.CountFloors, building.Year);
        }
    }
}
