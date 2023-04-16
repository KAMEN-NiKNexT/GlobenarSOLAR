using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    [CreateAssetMenu(fileName = "Ocean Settigns", menuName = "Celestial Body/Ocean Settings", order = 1)]
    public class OceanSettings : ScriptableObject
    {
        #region Variables

        [Header("Water")]
        [SerializeField] private float _depthMultiplier;
        [SerializeField] private float _alphaMultiplier;
        [SerializeField] private Color32 _colorA;
        [SerializeField] private Color32 _colorB;
        [SerializeField] private Color32 _specularColor;

        [Header("Waves")]
        [SerializeField] private Texture2D _waveNormalA;
        [SerializeField] private Texture2D _waveNormalB;
        [SerializeField] private float _waveStrength;
        [SerializeField] private float _waveScale;
        [SerializeField] private float _waveSpeed;
        [SerializeField] private float _smoothness;
        [SerializeField] private Vector4 _params;

        #endregion

        #region Methods

        public void SetProperties(Material material, int seed, bool isRadomize)
        {
            material.SetFloat("_depthMultiplier", _depthMultiplier);
            material.SetFloat("_alphaMultiplier", _alphaMultiplier);

            material.SetTexture("_waveNormalA", _waveNormalA);
            material.SetTexture("_waveNormalB", _waveNormalB);

            material.SetFloat("_waveStrength", _waveStrength);
            material.SetFloat("_waveScale", _waveScale);
            material.SetFloat("_waveSpeed", _waveSpeed);
            material.SetFloat("_smoothness", _smoothness);
            material.SetVector("_params", _params);

            if (isRadomize)
            {
                //PRNG random = new PRNG(seed);
                //Color randomColorA = Color.HSVToRGB(random.Value().random.Range(0.6f, 0.8f), random.Range(0.65f, 1f));
                //Color randomColorB = ColourHelper.TweakHSV(randomColorB, random.SignedValue() * 0.2f, random.SignedValue() * 0.2f, random.Range(-0.5f, -0.4f));

                //SetColor(material, randomColorA, randomColorB, Color.white);
            }
            else SetColor(material, _colorA, _colorB, _specularColor);
        }
        private void SetColor(Material material, Color colorA, Color colorB, Color specularColor)
        {
            material.SetColor("_colorA", colorA);
            material.SetColor("_colorB", colorB);
            material.SetColor("_specularColor", specularColor);
        }

        #endregion
    }
}