using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globenar.Noise;

namespace Globenar
{
    [Serializable] public class NoiseLayer
    {
        #region Variables

        [SerializeField] private bool _isEnabled;
        [SerializeField] private bool _isFirstLayerAsMask;
        [SerializeField] private NoiseSettings _settings;

        #endregion

        #region Properties

        public bool IsEnabled { get => _isEnabled; }
        public bool IsFirstLayerAsMask { get => _isFirstLayerAsMask; }
        public NoiseSettings Settings { get => _settings; }

        #endregion
    }
}