using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globenar.Noise;

namespace Globenar
{
    public class ColorGenerator
    {
        #region Variables

        private ColorSettings settings;
        private Texture2D texture;
        private const int textureResolution = 50;
        private INoiseFilter biomeNoiseFilter;

        #endregion

        public void UpdateSettings(ColorSettings settings)
        {
            this.settings = settings;
            if (texture == null || texture.height != settings.BiomeColor.Biomes.Length)
            {
                texture = new Texture2D(textureResolution * 2, settings.BiomeColor.Biomes.Length, TextureFormat.RGBA32, false);
            }
            biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.BiomeColor.Noise);
        }

        public void UpdateElevation(MinMax elevationMinMax)
        {
            settings.PlanetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
        }

        public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
        {
            float heightPercent = (pointOnUnitSphere.y + 1) / 2f;
            heightPercent += (biomeNoiseFilter.Evaluate(pointOnUnitSphere) - settings.BiomeColor.NoiseOffset) * settings.BiomeColor.NoiseStrength;
            float biomeIndex = 0;
            int numBiomes = settings.BiomeColor.Biomes.Length;
            float blendRange = settings.BiomeColor.BlendAmount / 2f + .001f;

            for (int i = 0; i < numBiomes; i++)
            {
                float dst = heightPercent - settings.BiomeColor.Biomes[i].StartHeight;
                float weight = Mathf.InverseLerp(-blendRange, blendRange, dst);
                biomeIndex *= (1 - weight);
                biomeIndex += i * weight;
            }

            return biomeIndex / Mathf.Max(1, numBiomes - 1);
        }

        public void UpdateColours()
        {
            Color[] colours = new Color[texture.width * texture.height];
            int colourIndex = 0;
            foreach (var biome in settings.BiomeColor.Biomes)
            {
                for (int i = 0; i < textureResolution * 2; i++)
                {
                    Color gradientCol;
                    if (i < textureResolution)
                    {
                        gradientCol = settings.OceanColor.Evaluate(i / (textureResolution - 1f));
                    }
                    else
                    {
                        gradientCol = biome.BiomeGradient.Evaluate((i - textureResolution) / (textureResolution - 1f));
                    }
                    Color tintCol = biome.Tint;
                    colours[colourIndex] = gradientCol * (1 - biome.TintPercent) + tintCol * biome.TintPercent;
                    colourIndex++;
                }
            }
            texture.SetPixels(colours);
            texture.Apply();
            settings.PlanetMaterial.SetTexture("_texture", texture);
        }
    }
}

