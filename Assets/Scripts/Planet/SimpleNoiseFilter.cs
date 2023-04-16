using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globenar.Noise;
using System.Runtime.CompilerServices;

namespace Globenar
{
    public class SimpleNoiseFilter : INoiseFilter
    {
        private SimpleNoiseSettings settings;
        private GIT.Noise noise = new GIT.Noise();
        public SimpleNoiseFilter(SimpleNoiseSettings settings)
        {
            this.settings = settings;
        }

        public float Evaluate(Vector3 point)
        {
            float noiseValue = 0;
            float frequency = settings.BaseRoughness;
            float amplitude = 1;

            for (int i = 0; i < settings.NumberLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + settings.Centre);
                noiseValue += (v + 1) * .5f * amplitude;
                frequency *= settings.Roughness;
                amplitude *= settings.Persistence;
            }

            noiseValue = noiseValue - settings.MinValue;
            return noiseValue * settings.Strength;
        }
    }
}