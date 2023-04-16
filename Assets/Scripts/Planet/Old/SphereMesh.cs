using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Globenar
{
    public class SphereMesh
    {
        #region Classes

        public class FixedSizeList<T>
        {
            #region FixedSizeList Variables

            public T[] Items { get; private set; }
            public int NextIndex { get; private set; }

            #endregion

            #region FixedSizeList Contstructors

            public FixedSizeList(int size)
            {
                Items = new T[size];
            }

            #endregion

            #region FixedSizeList Methods

            public void Add(T item) => Items[NextIndex++] = item;
            public void AddRange(IEnumerable<T> items)
            {
                foreach (T item in items) Add(item);
            }

            #endregion
        }
        public class Edge
        {
            #region Edge Variables

            public int[] VertexIndices { get; private set; }

            #endregion

            #region Edge Constructors

            public Edge(int[] vertexIndices) => VertexIndices = vertexIndices;

            #endregion
        }

        #endregion

        #region Variables

        private readonly int[] _vertexPairs = { 0, 1, 0, 2, 0, 3, 0, 4, 1, 2, 2, 3, 3, 4, 4, 1, 5, 1, 5, 2, 5, 3, 5, 4 };
        private readonly int[] _edgeTriplets = { 0, 1, 4, 1, 2, 5, 2, 3, 6, 3, 0, 7, 8, 9, 4, 9, 10, 5, 10, 11, 6, 11, 8, 7 };
        private readonly Vector3[] _baseVertices = { Vector3.up, Vector3.left, Vector3.back, Vector3.right, Vector3.forward, Vector3.down };
        private readonly int _numberVerticesPerFace;

        #endregion

        #region Properties

        public int Resolution { get; private set; }
        public FixedSizeList<Vector3> Vertices { get; private set; }
        public FixedSizeList<int> Triangles { get; private set; }

        #endregion

        #region Contstructors

        public SphereMesh(int resolution)
        {
            Resolution = Mathf.Max(0, resolution);
            _numberVerticesPerFace = ((int)Mathf.Pow(Resolution + 3, 2) - (Resolution + 3)) / 2;
            int numberVertices = _numberVerticesPerFace * 8 - (Resolution + 2) * 12 + 6;
            int numberTrisPerFace = (int)Mathf.Pow(Resolution, 2);

            Vertices = new FixedSizeList<Vector3>(numberVertices);
            Triangles = new FixedSizeList<int>(numberTrisPerFace * 8 * 3);

            Vertices.AddRange(_baseVertices);

            Edge[] edges = new Edge[12];
            for (int i = 0; i < _vertexPairs.Length; i+= 2)
            {
                Vector3 startVertex = Vertices.Items[_vertexPairs[i]];
                Vector3 endVertex = Vertices.Items[_vertexPairs[i + 1]];

                int[] edgeVertexIndices = new int[Resolution + 2];
                edgeVertexIndices[0] = _vertexPairs[i];

                for (int j = 0; j < Resolution; j++)
                {
                    float t = (j + 1f) / (Resolution + 1f);
                    edgeVertexIndices[j + 1] = Vertices.NextIndex;
                    Vertices.Add(Vector3.Slerp(startVertex, endVertex, t));
                }
                edgeVertexIndices[Resolution + 1] = _vertexPairs[i + 1];
                int edgeIndex = i / 2;
                edges[edgeIndex] = new Edge(edgeVertexIndices);
            }

            for (int i = 0; i < _edgeTriplets.Length; i += 3)
            {
                int faceIndex = i / 3;
                bool reverse = faceIndex >= 4;
                CreateFace(edges[_edgeTriplets[i]], edges[_edgeTriplets[i + 1]], edges[_edgeTriplets[i + 2]], reverse);
            }
        }
        private void CreateFace(Edge sideA, Edge sideB, Edge bottom, bool reverse)
        {
            int numberPointsInEdge = sideA.VertexIndices.Length;
            FixedSizeList<int> vertexMap = new FixedSizeList<int>(_numberVerticesPerFace);
            vertexMap.Add(sideA.VertexIndices[0]);

            for (int i = 1; i < numberPointsInEdge - 1; i++)
            {
                vertexMap.Add(sideA.VertexIndices[i]);

                Vector3 sideAVertex = Vertices.Items[sideA.VertexIndices[i]];
                Vector3 sideBVertex = Vertices.Items[sideB.VertexIndices[i]];

                int numberInnerPoints = i - 1;
                for (int j = 0; j < numberInnerPoints; j++)
                {
                    float t = (j + 1f) / (numberInnerPoints + 1f);
                    vertexMap.Add(Vertices.NextIndex);
                    Vertices.Add(Vector3.Slerp(sideAVertex, sideBVertex, t));
                }
                vertexMap.Add(sideB.VertexIndices[i]);
            }
            for (int i = 0; i < numberPointsInEdge; i++)
            {
                vertexMap.Add(bottom.VertexIndices[i]);
            }

            int topVertex;
            int bottomVertex;
            int numberTriangelsInRow;
            int v0, v1, v2;

            int numberRows = Resolution + 1;
            for (int row = 0; row < numberRows; row++)
            {
                topVertex = ((int)Mathf.Pow(row + 1, 2) - (row + 1)) / 2;
                bottomVertex = ((int)Mathf.Pow(row + 2, 2) - (row + 2)) / 2;

                numberTriangelsInRow = 1 + 2 * row;
                for (int column = 0; column < numberTriangelsInRow; column++)
                {
                    if (column % 2 == 0)
                    {
                        v0 = topVertex;
                        v1 = bottomVertex + 1;
                        v2 = bottomVertex;
                        topVertex++;
                        bottomVertex++;
                    }
                    else
                    {
                        v0 = topVertex;
                        v1 = bottomVertex;
                        v2 = topVertex - 1;
                    }

                    Triangles.Add(vertexMap.Items[v0]);
                    Triangles.Add(vertexMap.Items[reverse ? v2 : v1]);
                    Triangles.Add(vertexMap.Items[reverse ? v1 : v2]);
                }
            }
        }

        #endregion
    }
}