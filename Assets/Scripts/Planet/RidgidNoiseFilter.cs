using Globenar.Noise;
using Globenar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{

    private RidgidNoiseSettings settings;
    private GIT.Noise noise = new GIT.Noise();

    public RidgidNoiseFilter(RidgidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.BaseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.NumberLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.Centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.WeightMultiplier);

            noiseValue += v * amplitude;
            frequency *= settings.Roughness;
            amplitude *= settings.Persistence;
        }

        noiseValue = noiseValue - settings.MinValue;
        return noiseValue * settings.Strength;
    }
}