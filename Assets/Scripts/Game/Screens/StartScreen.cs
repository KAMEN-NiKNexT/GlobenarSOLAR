using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Globenar
{
    public class StartScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _appName;

        [Header("Settings")]
        [SerializeField] private StartScreenAnimationSettings _animationSettings;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
            ShowAppName();
        }
        public void SignUp()
        {

        }
        public void LogIn()
        {

        }

        #endregion

        #region Additional Methods

        private void ShowAppName()
        {
            _appName.alpha = 0f;
            _appName.DOFade(1f, _animationSettings.AppearAppNameDuration).SetDelay(_animationSettings.AppearAppNameDelay).SetEase(_animationSettings.AppearAppNameEase);
        }

        #endregion
    }
}