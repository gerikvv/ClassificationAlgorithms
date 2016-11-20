using System;
using System.Collections.Generic;
using System.Linq;
using Classification.Model;

namespace Classification.Util
{
    public static class StatisticsOperations
    {
        private static double CalculateMeanValue(IList<double> items)
        {
            double totalNumberYears = items.Sum();
            double countBuildings = items.Count;

            return totalNumberYears / countBuildings;
        }

        private static double CalculateStandardDeviation(IList<double> items, double meanValue)
        {
            double dispersion = items.Sum(item => Math.Pow(item - meanValue, 2));
            return Math.Sqrt(dispersion / items.Count);
        }

        public static IList<Vertex> Normalize(this IList<Vertex> vertices)
        {
            var allCoordX = vertices.Select(vertex => vertex.X).ToList();
            var allCoordY = vertices.Select(vertex => vertex.Y).ToList();

            var meanValueCoordX = CalculateMeanValue(allCoordX);
            var meanValueCoordY = CalculateMeanValue(allCoordY);

            var standardDeviationCoordX = CalculateStandardDeviation(allCoordX, meanValueCoordX);
            var standardDeviationCoordY = CalculateStandardDeviation(allCoordY, meanValueCoordY);

            return vertices.Select(
                   v => new Vertex(v.Id, 
                                  (v.X - meanValueCoordX)/standardDeviationCoordX,
                                  (v.Y - meanValueCoordY)/standardDeviationCoordY)).ToList();
        }
    }
}