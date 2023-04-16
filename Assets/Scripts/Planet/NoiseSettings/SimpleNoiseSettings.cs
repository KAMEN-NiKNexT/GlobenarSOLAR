using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar.Noise
{
    [Serializable] public class SimpleNoiseSettings
    {
        #region Variables

        [SerializeField] protected float _strength;
        [Range(1, 8)][SerializeField] protected int _numberLayers;
        [SerializeField] protected float _baseRoughness;
        [SerializeField] protected float _roughness;
        [SerializeField] protected float _persistence;
        [SerializeField] protected Vector3 _centre;
        [SerializeField] protected float _minValue;

        #endregion

        #region Properties

        public float Strength { get => _strength; }
        public int NumberLayers { get => _numberLayers; }
        public float BaseRoughness { get => _baseRoughness; }
        public float Roughness { get => _roughness; }
        public float Persistence { get => _persistence; }
        public Vector3 Centre { get => _centre; }
        public float MinValue { get => _minValue; }

        #endregion
    }
}       
        
        