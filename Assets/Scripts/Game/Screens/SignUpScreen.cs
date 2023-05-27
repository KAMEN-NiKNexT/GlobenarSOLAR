using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using TMPro;
using System;
using UnityEngine.UI;

namespace Globenar
{
    public class SignUpScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Name Objects")]
        [SerializeField] private TMP_InputField _nameInput;

        [Header("Birthday Objects")]
        [SerializeField] private Button _birthdayButton;
        [SerializeField] private TextMeshProUGUI _birthdayText;

        [Header("Gender Objects")]
        [SerializeField] private Button _genderButton;
        [SerializeField] private TextMeshProUGUI _genderText;

        [Header("Email Objects")]
        [SerializeField] private TMP_InputField _emailInput;

        [Header("Password Objects")]
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private SwitchToggle _showPassword;

        [Header("Variables")]
        private string _userName = "";
        private string _userBirthdayInTicks = "";
        private string _userGender = "";
        private string _email = "";
        private string _password = "";
        private bool _isAcceptedDocs;

        #endregion

        #region Control Methods

        private void Start()
        {
            
        }
        private void OnDestroy()
        {
            _nameInput.onValueChanged.RemoveListener(SetName);
            _birthdayButton.onClick.RemoveListener(ClickOnBirthdayField);
            Calendar.Instance.OnChoosenDateTimeChanged -= SetBirthdayValue;
            _genderButton.onClick.RemoveListener(ClickOnGenderField);
            GenderSelector.Instance.OnGenderChoosen += SetGenderValue;
            _emailInput.onValueChanged.RemoveListener(SetEmail);
            _passwordInput.onValueChanged.RemoveListener(SetPassword);
            _showPassword.OnSwitched -= ClickOnShowPassword;
        }
        public override void Initialize()
        {
            base.Initialize();
            _nameInput.onValueChanged.AddListener(SetName);

            _birthdayButton.onClick.AddListener(ClickOnBirthdayField);
            Calendar.Instance.OnChoosenDateTimeChanged += SetBirthdayValue;

            _genderButton.onClick.AddListener(ClickOnGenderField);
            GenderSelector.Instance.OnGenderChoosen += SetGenderValue;

            _emailInput.onValueChanged.AddListener(SetEmail);

            _passwordInput.onValueChanged.AddListener(SetPassword);
            _showPassword.OnSwitched += ClickOnShowPassword;
        }
        private void ClearFields()
        {

        }
        public void Back()
        {
            ScreenManager.Instance.TransitionTo("Start");
            ClearFields();
        }

        #endregion

        #region Name Methods

        private void SetName(string newName) => _userName = newName;

        #endregion

        #region Birthday Methods

        private void ClickOnBirthdayField() => PopupManager.Instance.Show("Calendar");
        private void SetBirthdayValue(DateTime time)
        {
            _userBirthdayInTicks = time.Ticks.ToString();
            _birthdayText.text = $"{time.Day}.{time.Month}.{time.Year}";
        }

        #endregion

        #region Gender Methods

        private void ClickOnGenderField() => PopupManager.Instance.Show("GenderSelector");
        private void SetGenderValue(string value)
        {
            _userGender = value;
            _genderText.text = $"{value}";
        }

        #endregion

        #region Email Methods

        private void SetEmail(string newEmail) => _email = newEmail;

        #endregion

        #region Password Methods

        private void SetPassword(string newPassword) => _password = newPassword;
        private void ClickOnShowPassword(bool isOn) 
        {
            _passwordInput.contentType = isOn ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
            _passwordInput.ForceLabelUpdate();
        }

        #endregion

        #region Control Methods

        public void SignUp()
        {
           // FirebaseAuthManager.Instance.SignUp(_email, _password);
        }

        #endregion
    }
}