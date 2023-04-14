using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Globenar
{
    public class CelestialBodyGenerator : MonoBehaviour
    {
        #region Classes

        [Serializable] private class ResolutionSettings
        {
            #region ResolutionSettings Variables

            [SerializeField] private string _name;
            [SerializeField] private int _lodLevel;

            #endregion

            #region ResolutionSettings Properties

            public string Name { get => _name; }
            public int LODLevel { get => _lodLevel; }

            #endregion
        }

        #endregion
    }
}

