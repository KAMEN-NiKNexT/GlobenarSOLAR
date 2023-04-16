using Globenar.Noise;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    [Serializable] public class BiomeColorSettings
    {
        #region Classes

        [Serializable] public class Biome
        {
            #region Biome Variables

            [SerializeField] private Gradient _gradient;
            [SerializeField] private Color _tint;
            [Range(0f, 1f)][SerializeField] private float _startHeight;
            [Range(0f, 1f)][SerializeField] private float _tintPercent;

            #endregion

            #region Biome Properties

            public Gradient BiomeGradient { get => _gradient; }
            public Color Tint { get => _tint; }
            public float StartHeight { get => _startHeight; }
            public float TintPercent { get => _tintPercent; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private Biome[] _biomes;
        [SerializeField] private NoiseSettings _noise;
        [SerializeField] private float _noiseOffset;
        [SerializeField] private float _noiseStrength;
        [Range(0f, 1f)][SerializeField] private float _blendAmount;

        #endregion

        #region Properties

        public Biome[] Biomes { get => _biomes; }
        public NoiseSettings Noise { get => _noise; }
        public float NoiseOffset { get => _noiseOffset; }
        public float NoiseStrength { get => _noiseStrength; }
        public float BlendAmount { get => _blendAmount; }

        #endregion
    }
}