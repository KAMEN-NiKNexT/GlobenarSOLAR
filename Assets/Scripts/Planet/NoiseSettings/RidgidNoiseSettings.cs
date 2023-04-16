using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar.Noise
{
    [Serializable] public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        #region Variables

        [SerializeField] private float _weightMultiplier;

        #endregion

        #region Properties

        public float WeightMultiplier { get => _weightMultiplier; }

        #endregion
    }
}