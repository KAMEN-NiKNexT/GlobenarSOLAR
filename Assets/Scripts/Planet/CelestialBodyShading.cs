using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    public abstract class CelestialBodyShading : ScriptableObject
    {
        #region Variables

        [Header("View")]
        [SerializeField] private bool _isRandomize;
        [SerializeField] private int _seed;
        [SerializeField] private Material _terrainMaterial;

        [Header("Ocean")]
        [SerializeField] private bool _isHasOcean;
        [SerializeField] private float _oceanLevel;
        [SerializeField] private OceanSettings _oceanSettings;
        [SerializeField] private ComputeShader _shadingDataCompute;

        [Header("Additional")]
        protected Vector4[] _cachedShadingData;
        private ComputeBuffer _shadingBuffer;
        public Action OnSettingsChaged;

        #endregion

        #region Properties

        public bool IsRandomize { get => _isRandomize; }
        public int Seed { get => _seed; }
        public Material TerrainMaterial { get => _terrainMaterial; }
        public bool IsHasOcean { get => _isHasOcean; }
        public float OceanLevel { get => _oceanLevel; }
        public ComputeShader ShadingDataCompute { get => _shadingDataCompute; }

        #endregion

        #region Unity Methods

        protected virtual void OnValidate() => OnSettingsChaged?.Invoke();

        #endregion

        #region Control Methods

        public abstract void Initialize(CelestialBodyShape shape);
        public Vector4 GenerateShadingData(ComputeBuffer vertexBuffer)
        {
            int numberVertices = vertexBuffer.count;
            Vector4[] shadingData = new Vector4[numberVertices];

            if (_shadingDataCompute)
            {
                SetShadingDataComputeProperties();

                _shadingDataCompute.SetInt("numberVertices", numberVertices);
                _shadingDataCompute.SetBuffer(0, "vertices", vertexBuffer);
                ComputeHelper.CreateAndSetBuffer<Vector4>(ref shadingBuffer, numberVertices, _shadingDataCompute, "_shadingData");
                ComputeHelper.Run(_shadingDataCompute, numberVertices);

                _shadingBuffer.GetData(shadingData);
            }
            _cachedShadingData = shadingData;
            return shadingData;
        }
        public abstract void SetTerrainProperties(Material material, Vector2 heightMinMax, float bodyScale);
        public virtual void SetOceanProperties(Material oceanMaterial)
        {
            if (_oceanSettings) _oceanSettings.SetProperties(oceanMaterial, _seed, _isRandomize);
        }
        public abstract void SetShadingDataComputeProperties();
        public virtual void ReleaseBuffers() => ComputeHelper.Release(_shadingBuffer);\
        public static void TextureFromGradient(ref Texture2D texture, int width, Gradient gradient)
        {
            //TODO: END THIS FUNCTION
        }

        #endregion
    }
}