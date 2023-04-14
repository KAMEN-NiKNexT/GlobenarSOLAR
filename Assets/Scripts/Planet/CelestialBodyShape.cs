using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    public abstract class CelestialBodyShape : ScriptableObject
    {
        #region Variables

        [Header("Map settings")]
        [SerializeField] private bool _isRandomize;
        [SerializeField] private int _seed;
        [SerializeField] ComputeShader _heightMapCompute;

        [Header("Perturb")]
        [SerializeField] private bool _isPerturbVertices;
        [SerializeField] private ComputeShader _perturbCompute;
        [Range(0f, 1f)][SerializeField] private float _perturbStrength;

        public Action OnSettignsChaged;
        private ComputeBuffer _heightBuffer;

        #endregion

        #region Properties

        public bool IsRandomize { get => _isRandomize; }
        public int Seed { get => _seed; }
        public ComputeShader HeightMapCompute { get => _heightMapCompute; }
        public bool IsPerturbVertices { get => _isPerturbVertices; }
        public ComputeShader PerturbCompute { get => _perturbCompute; }
        public float PerturbStrength { get => _perturbStrength; }

        #endregion

        #region Unity Methods

        protected virtual void OnValidate()
        {
            OnSettignsChaged?.Invoke();
        }

        #endregion

        #region Control Methods

        public virtual float[] CalculateHeights(ComputeBuffer vertexBuffer)
        {
            SetShapeData();
            _heightMapCompute.SetInt("_numberVertices", vertexBuffer.count);
            _heightMapCompute.SetBuffer(0, "vertices", vertexBuffer);
            ComputeHelper.CreateAbdSetBuffer<float>(ref _heightBuffer, vertexBuffer.count, _heightMapCompute, "heights");

            ComputeHelper.Run(_heightMapCompute, vertexBuffer.count);

            float[] heights = new float[vertexBuffer.count];
            _heightBuffer.GetData(heights);
            return heights;
        }
        //public virtual void ReleaseBuffers() => ComputeHelper.Release(_heightBuffer);
        protected abstract void SetShapeData();

        #endregion
    }
}