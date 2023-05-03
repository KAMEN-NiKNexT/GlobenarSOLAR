using System;
using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using Kamen.DataSave;
using UnityEngine.Device;

namespace Globenar
{
    public class Registration : SingletonComponent<Registration>
    {
        #region Classes 

        [Serializable] private class RegistrationScreen
        {
            #region RegistrationScreen Variables

            [SerializeField] private string _screenName;
            [SerializeField] private RegistrationVariablesType _variablesType;
            [Space]
            [SerializeField] private GameObject _object;

            #endregion

            #region RegistrationScreen Properties

            public string ScreenName { get => _screenName; }
            public RegistrationVariablesType VariablesType { get => _variablesType; }
            public GameObject Object { get => _object; }

            #endregion
        }

        #endregion

        #region Enums

        public enum RegistrationVariablesType
        {
            Name,
            Age,
            Gender,
            Country,
            Email
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        [Header("Settings")]
        [SerializeField] private RegistrationScreen[] _registrationScreen;
        [SerializeField] private string _menuScreenName;

        [Header("Variables")]
        private string _name;
        private int _age;
        private string _gender;
        private string _country;
        private string _email;

        private int _currentScreen;

        #endregion

        #region Control Methods

        public void StartRegistration()
        {
            _currentScreen = 0;
        }

        public void ChangeScreen(int direction)
        {
            WriteVariables(_registrationScreen[_currentScreen]);

            _currentScreen += direction;
            if (_currentScreen >= _registrationScreen.Length) EndRegistration();
            else ScreenManager.Instance.TransitionTo(_registrationScreen[_currentScreen].ScreenName);
        }
        private void WriteVariables(RegistrationScreen screen)
        {
            switch (screen.VariablesType)
            {
                case RegistrationVariablesType.Name:
                    _name = screen.Object.GetComponent<IRegistration<string>>().GetValue();
                    break;
                case RegistrationVariablesType.Age:
                    _age = screen.Object.GetComponent<IRegistration<int>>().GetValue();
                    break;
                case RegistrationVariablesType.Gender:
                    _gender = screen.Object.GetComponent<IRegistration<string>>().GetValue();
                    break;
                case RegistrationVariablesType.Country:
                    _country = screen.Object.GetComponent<IRegistration<string>>().GetValue();
                    break;
                case RegistrationVariablesType.Email:
                    _email = screen.Object.GetComponent<IRegistration<string>>().GetValue();
                    break;
            }
        }
        private void EndRegistration()
        {
            DataSaveManager.Instance.MyData.UserProfile = new Profile(_name, _age, _gender, _country, _email);
            _leftButton.gameObject.SetActive(false);
            _rightButton.gameObject.SetActive(false);
            ScreenManager.Instance.TransitionTo(_menuScreenName);
        }

        #endregion
    }
}