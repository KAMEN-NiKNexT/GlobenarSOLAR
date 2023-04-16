using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    [CreateAssetMenu(fileName = "Color Settings", menuName = "Celestial Body/Color Settings", order = 1)]
    public class ColorSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private Material _planetMaterial;
        [SerializeField] private BiomeColorSettings _biomeColor;
        [SerializeField] private Gradient _oceanColor;

        #endregion

        #region Properties

        public Material PlanetMaterial { get => _planetMaterial; }
        public BiomeColorSettings BiomeColor { get => _biomeColor; }
        public Gradient OceanColor { get => _oceanColor; }

        #endregion
    }
}

