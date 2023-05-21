using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    [CreateAssetMenu(fileName = "StartScreen Animation Settings", menuName = "Globenar/Settings/StartScreen Animation Settings")]
    public class StartScreenAnimationSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _appearAppNameDelay;
        [SerializeField] private float _appearAppNameDuration;
        [SerializeField] private Ease _appearAppNameEase;

        #endregion

        #region Methods

        public float AppearAppNameDelay { get => _appearAppNameDelay; }
        public float AppearAppNameDuration { get => _appearAppNameDuration; }
        public Ease AppearAppNameEase { get => _appearAppNameEase; }

        #endregion
    }
}