using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globenar.Noise;

namespace Globenar
{
    public class NoiseFilterFactory : MonoBehaviour
    {
        public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
        {
            switch (settings.Type)
            {
                case NoiseSettings.FilterType.Simple:
                    return new SimpleNoiseFilter(settings.SimpleSettings);
                case NoiseSettings.FilterType.Ridgid:
                    return new RidgidNoiseFilter(settings.RidgidSettings);
            }
            return null;
        }
    }
}