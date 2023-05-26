using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using UnityEngine.UI;
using System;
using Kamen.UI;

namespace Globenar
{
    public class GenderSelector : SingletonComponent<GenderSelector>
    {
        #region Enums

        public enum GenderType
        {
            Male,
            Female,
        }

        #endregion

        #region Classes 

        [Serializable] private class GenderSettings
        {
            #region GenderSettings Variables

            [SerializeField] private GenderType _type;
            [SerializeField] private string _name;
            [SerializeField] private Button _chooseButton;

            #endregion

            #region GenderSettings Properties

            public GenderType Type { get => _type; }
            public string Name { get => _name; }
            public Button ChooseButton { get => _chooseButton; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private GenderSettings[] _genderSettings;
        public Action<string> OnGenderChoosen;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
        }
        private void OnDestroy()
        {
            for (int i = 0; i < _genderSettings.Length; i++)
            {
                _genderSettings[i].ChooseButton.onClick.RemoveAllListeners();
            }
        }

        #endregion

        #region Methods

        private void InitializeButtons()
        {
            for(int i = 0; i < _genderSettings.Length; i++)
            {
                GenderSettings settings = _genderSettings[i];
                _genderSettings[i].ChooseButton.onClick.AddListener(() => ChooseGender(settings));
            }
        }
        private void ChooseGender(GenderSettings choosenGenderSettings)
        {
            OnGenderChoosen?.Invoke(choosenGenderSettings.Name);
            PopupManager.Instance.Hide("GenderSelector");
        }

        #endregion
    }
}

