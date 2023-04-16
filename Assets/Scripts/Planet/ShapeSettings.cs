using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    [CreateAssetMenu(fileName = "Shape Settings", menuName = "Celestial Body/Shape Settings", order = 1)]
    public class ShapeSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _planetRadius;
        [SerializeField] private NoiseLayer[] _noiseLayers;

        #endregion

        #region Properties

        public float PlanetRadius { get => _planetRadius; }
        public NoiseLayer[] NoiseLayers { get => _noiseLayers; }

        #endregion
    }
}