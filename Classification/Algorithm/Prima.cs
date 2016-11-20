using System;
using System.Collections.Generic;
using System.Linq;
using Classification.Model;

namespace Classification.Algorithm
{
    public static class Prima
    {
        private static List<Vertex> _selectedVertices;
        private static List<Vertex> _notSelectedVertices;
        private static int numberVertices;

        private static double[,] _distanceMatrix;
        private static int[,] _incidenceMatrix;
        
        public static int[,] GetIncidenceMatrix(IList<Vertex> vertices)
        {
            Initialize(vertices);

            CalculateDistanceMatrix(vertices);
            CalculateIncidenceMatrix(vertices);
            
            return _incidenceMatrix;
        }
        private static void Initialize(IList<Vertex> vertices)
        {
            _selectedVertices = new List<Vertex>();
            _notSelectedVertices = new List<Vertex>(vertices);

            numberVertices = vertices.Count;
            _incidenceMatrix = new int[numberVertices, numberVertices];
        }

        private static void CalculateDistanceMatrix(IList<Vertex> vertices)
        {
            _distanceMatrix = new double[numberVertices, numberVertices];

            for (int i = 0; i < numberVertices; i++)
            {
                for (int j = 0; j < numberVertices; j++)
                {
                    var vertex = vertices[i];
                    var nextVertex = vertices[j];
                    _distanceMatrix[i, j] =
                        Math.Sqrt(Math.Pow(vertex.X - nextVertex.X, 2) + Math.Pow(vertex.Y - nextVertex.Y, 2));
                }
            }
        }

        private static void SelectVertex(IList<Vertex> vertices, int index)
        {
            _selectedVertices.Add(vertices[index]);
            _notSelectedVertices.Remove(vertices[index]);
        }

        private static void CalculateIncidenceMatrix(IList<Vertex> vertices)
        {
            SelectVertex(vertices, 0);

            while (_notSelectedVertices.Count != 0)
            {
                int indexSelectedVertex = 0;
                int indexNotSelectedVertex = 0;

                double minDistance = _distanceMatrix[vertices.IndexOf(_notSelectedVertices.First()), 
                                                     vertices.IndexOf(_selectedVertices.First())];

                foreach (Vertex notSelectedVertex in _notSelectedVertices)
                {
                    foreach (Vertex selectedVertex in _selectedVertices)
                    {
                        if (_distanceMatrix[vertices.IndexOf(notSelectedVertex), 
                                            vertices.IndexOf(selectedVertex)] <= minDistance)
                        {
                            indexNotSelectedVertex = vertices.IndexOf(notSelectedVertex);
                            indexSelectedVertex = vertices.IndexOf(selectedVertex);

                            minDistance = _distanceMatrix[indexNotSelectedVertex, indexSelectedVertex];
                        }
                    }
                }

                SelectVertex(vertices, indexNotSelectedVertex);

                _incidenceMatrix[indexSelectedVertex, indexNotSelectedVertex] = 1;
            }
        }
    }
}
