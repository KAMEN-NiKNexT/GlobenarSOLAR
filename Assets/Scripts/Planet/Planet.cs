using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globenar.Planet;

namespace Globenar
{
    public class Planet : MonoBehaviour
    {
        #region Enums

        public enum FaceRenderMask
        {
            All,
            Top,
            Bottom,
            Left,
            Right,
            Front,
            Back
        };

        #endregion

        #region Variables

        [Header("Settings")]
        [Range(2, 256)][SerializeField] private int _resolution;
        [SerializeField] private bool _isAutoUpdate;
        [SerializeField] private FaceRenderMask _renderMask;

        public  ShapeSettings _shapeSettings;
        public ColorSettings _colorSettings;

        private MeshFilter[] _meshFilters;
        private TerrainFace[] _terrainFaces;
        public bool IsShapeSettingsFoldout;//{ get; private set; }
        public bool IsColorSettingsFoldout;//{ get; private set; }
        private ShapeGenerator _shapeGenerator = new ShapeGenerator();
        private ColorGenerator _colorGenerator = new ColorGenerator();


        #endregion

        #region Control Methods

        private void Initialize()
        {
            _shapeGenerator.UpdateSettings(_shapeSettings);
            _colorGenerator.UpdateSettings(_colorSettings);

            if (_meshFilters == null || _meshFilters.Length == 0)
            {
                _meshFilters = new MeshFilter[6];
            }
            _terrainFaces = new TerrainFace[6];

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < 6; i++)
            {
                if (_meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;
                    meshObj.transform.localPosition = Vector3.zero;

                    meshObj.AddComponent<MeshRenderer>();
                    _meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    _meshFilters[i].sharedMesh = new Mesh();
                }
                _meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = _colorSettings.PlanetMaterial;

                _terrainFaces[i] = new TerrainFace(_shapeGenerator, _meshFilters[i].sharedMesh, _resolution, directions[i]);
                bool renderFace = _renderMask == FaceRenderMask.All || (int)_renderMask - 1 == i;
                _meshFilters[i].gameObject.SetActive(renderFace);
            }
        }

        private void Start()
        {
            GeneratePlanet();
        }
        public void GeneratePlanet()
        {
            Initialize();
            GenerateMesh();
            GenerateColours();
        }

        public void OnShapeSettingsUpdated()
        {
            if (_isAutoUpdate)
            {
                Initialize();
                GenerateMesh();
            }
        }

        public void OnColourSettingsUpdated()
        {
            if (_isAutoUpdate)
            {
                Initialize();
                GenerateColours();
            }
        }

        void GenerateMesh()
        {
            for (int i = 0; i < 6; i++)
            {
                if (_meshFilters[i].gameObject.activeSelf)
                {
                    _terrainFaces[i].ConstructMesh();
                }
            }

            _colorGenerator.UpdateElevation(_shapeGenerator.ElevationMinMax);
        }

        void GenerateColours()
        {
            _colorGenerator.UpdateColours();
            for (int i = 0; i < 6; i++)
            {
                if (_meshFilters[i].gameObject.activeSelf)
                {
                    _terrainFaces[i].UpdateUVs(_colorGenerator);
                }
            }
        }

        #endregion
    }
}