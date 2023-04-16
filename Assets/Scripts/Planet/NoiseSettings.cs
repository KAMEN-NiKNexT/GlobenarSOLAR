using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar.Noise
{
    [Serializable] public class NoiseSettings
    {
        #region Enums

        public enum FilterType
        {
            Simple,
            Ridgid
        };

        #endregion

        #region Variables

        [SerializeField] private FilterType _filterType;
        [ConditionalHide("_filterType", 0)][SerializeField] private SimpleNoiseSettings _simpleSettings;
        [ConditionalHide("_filterType", 1)][SerializeField] private RidgidNoiseSettings _ridgidSettings;

        #endregion

        #region Properties

        public FilterType Type { get => _filterType; }
        public SimpleNoiseSettings SimpleSettings { get => _simpleSettings; }
        public RidgidNoiseSettings RidgidSettings { get => _ridgidSettings; }

        #endregion
    }
}