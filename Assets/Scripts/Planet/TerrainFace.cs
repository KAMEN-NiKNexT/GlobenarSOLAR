using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Globenar
{
    public class TerrainFace
    {
        #region Variables

        [Header("Main Variables")]
        private ShapeGenerator _shapeGenerator;
        private Mesh _mesh;
        private int _resolution;
        private Vector3 _localUp;
        private Vector3 _axisA;
        private Vector3 _axisB;

        [Header("Additional Variables")]
        private Vector3[] _vertices;
        private int[] _triangles;
        private Vector2[] _uv;

        #endregion

        #region Constructors

        public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
        {
            _shapeGenerator = shapeGenerator;
            _mesh = mesh;
            _resolution = resolution;
            _localUp = localUp;

            _axisA = new Vector3(_localUp.y, localUp.z, _localUp.x);
            _axisB = Vector3.Cross(localUp, _axisA);
        }

        #endregion

        #region Methods

        public void ConstructMesh()
        {
            _vertices = new Vector3[(int)Mathf.Pow(_resolution, 2)];
            _triangles = new int[(int)Mathf.Pow(_resolution - 1, 2) * 6];
            _uv = new Vector2[_vertices.Length];
            int index = 0;
            int trianglesIndex = 0;

            Vector3 pointOnUnitSphere;
            float unscaledElevation;
            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    index = x + y * _resolution;
                    pointOnUnitSphere = GetPointOnUnitSphere(x, y);

                    unscaledElevation = _shapeGenerator.CalculateUnscaledElevation(pointOnUnitSphere);
                    _vertices[index] = pointOnUnitSphere * _shapeGenerator.GetScaledElevation(unscaledElevation);
                    _uv[index].y = unscaledElevation;

                    if (x != _resolution - 1 && y != _resolution - 1)
                    {
                        _triangles[trianglesIndex] = index;
                        _triangles[trianglesIndex + 1] = index + _resolution + 1;
                        _triangles[trianglesIndex + 2] = index + _resolution;

                        _triangles[trianglesIndex + 3] = index;
                        _triangles[trianglesIndex + 4] = index + 1;
                        _triangles[trianglesIndex + 5] = index + _resolution + 1;

                        trianglesIndex += 6;
                    }
                }
            }

            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
            _mesh.uv = _uv;
        }
        public void UpdateUVs(ColorGenerator colorGenerator)
        {
            Vector2[] uv = _mesh.uv;

            int index;
            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    index = x + y * _resolution;
                    uv[index].x = colorGenerator.BiomePercentFromPoint(GetPointOnUnitSphere(x, y));
                }
            }

            _mesh.uv = uv;
        }
        private Vector3 GetPointOnUnitSphere(int x, int y)
        {
            Vector2 percent = new Vector2(x, y) / (_resolution - 1);
            return (_localUp + (percent.x - .5f) * 2 * _axisA + (percent.y - .5f) * 2 * _axisB).normalized;
        }

        #endregion
    }
}