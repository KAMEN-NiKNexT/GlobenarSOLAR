using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    public class ShapeGenerator
    {
        #region Variables

        private ShapeSettings _settings;
        private INoiseFilter[] _noiseFilters;
        public MinMax ElevationMinMax { get; private set; }

        #endregion

        #region Control Methods

        public void UpdateSettings(ShapeSettings settings)
        {
            _settings = settings;
            _noiseFilters = new INoiseFilter[_settings.NoiseLayers.Length];
            for (int i = 0; i < _noiseFilters.Length; i++)
            {
                _noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(_settings.NoiseLayers[i].Settings);
            }
            ElevationMinMax = new MinMax();
        }
        public float CalculateUnscaledElevation(Vector3 pointOnUnitSphere)
        {
            float firstLayerValue = 0;
            float elevation = 0;

            if (_noiseFilters.Length > 0)
            {
                firstLayerValue = _noiseFilters[0].Evaluate(pointOnUnitSphere);
                if (_settings.NoiseLayers[0].IsEnabled)
                {
                    elevation = firstLayerValue;
                }
            }

            for (int i = 1; i < _noiseFilters.Length; i++)
            {
                if (_settings.NoiseLayers[i].IsEnabled)
                {
                    float mask = (_settings.NoiseLayers[i].IsFirstLayerAsMask) ? firstLayerValue : 1;
                    elevation += _noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
                }
            }
            ElevationMinMax.AddValue(elevation);
            return elevation;
        }
        public float GetScaledElevation(float unscaledElevation)
        {
            float elevation = Mathf.Max(0, unscaledElevation);
            elevation = _settings.PlanetRadius * (1 + elevation);
            return elevation;
        }

        #endregion
    }
}