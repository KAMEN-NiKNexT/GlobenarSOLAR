using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen;
using Kamen.DataSave;
using System;

namespace ComputerQuiz
{
    public class TextFormater : SingletonComponent<TextFormater>
    {
        #region Classes

       [Serializable] private struct TextSettings
        {
            #region TextSettings Variables

            [SerializeField] private Text _textField;
            [SerializeField] private string _kidMaleText;
            [SerializeField] private string _kidFemaleText;
            [SerializeField] private string _youngMaleText;
            [SerializeField] private string _youngFemaleText;
            [SerializeField] private string _oldMaleText;
            [SerializeField] private string _oldFemaleText;

            #endregion

            #region TextSettigns Methods

            public void UseTextStyle(TextStyle style, string nameCode, string realName)
            {
                string newText = "";
                switch (style)
                {
                    case TextStyle.KidMale:
                        newText = _kidMaleText.Replace(nameCode, realName);
                        break;
                    case TextStyle.KidFemale:
                        newText = _kidFemaleText.Replace(nameCode, realName);
                        break;
                    case TextStyle.YoungMale:
                        newText = _youngMaleText.Replace(nameCode, realName);
                        break;
                    case TextStyle.YoungFemale:
                        newText = _youngFemaleText.Replace(nameCode, realName);
                        break;
                    case TextStyle.OldMale:
                        newText = _oldMaleText.Replace(nameCode, realName);
                        break;
                    case TextStyle.OldFemale:
                        newText = _oldFemaleText.Replace(nameCode, realName);
                        break;
                }

                _textField.text = newText;
            }

            #endregion
        }

        #endregion

        #region Enums

        private enum TextStyle
        {
            KidMale,
            KidFemale,
            YoungMale,
            YoungFemale,
            OldMale,
            OldFemale
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private TextSettings[] _textSettings;
        [SerializeField] private string _codeForName;

        [Header("Variables")]
        private TextStyle _currentStyle;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            if (DataSaveManager.Instance.MyData.UserProfile != null) UpdateText();
        }

        #endregion

        #region Control Methods

        public void UpdateText()
        {
            DefineStyle();
            for (int i = 0; i < _textSettings.Length; i++)
            {
                _textSettings[i].UseTextStyle(_currentStyle, _codeForName, DataSaveManager.Instance.MyData.UserProfile.Name);
            }
        }
        private void DefineStyle()
        {
            int ageIndex = 2;
            bool isMale = DataSaveManager.Instance.MyData.UserProfile.Gender == "Male";
            if (ageIndex == 1)
            {
                if (isMale) _currentStyle = TextStyle.KidMale;
                else _currentStyle = TextStyle.KidFemale;
            }
            else if (ageIndex == 2)
            {
                if (isMale) _currentStyle = TextStyle.YoungMale;
                else _currentStyle = TextStyle.YoungFemale;
            }
            else if (ageIndex == 3)
            {
                if (isMale) _currentStyle = TextStyle.OldMale;
                else _currentStyle = TextStyle.OldFemale;
            }
        }

        #endregion

        #region Calculate Methods

        public string GetWordForm(int number, string singular, string plural1, string plural2)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number), "Number cannot be negative");

            if (singular == null)
                throw new ArgumentNullException(nameof(singular));

            if (plural1 == null)
                throw new ArgumentNullException(nameof(plural1));

            if (plural2 == null)
                throw new ArgumentNullException(nameof(plural2));

            int mod10 = number % 10;
            int mod100 = number % 100;

            if (mod100 >= 11 && mod100 <= 20)
                return plural2;

            switch (mod10)
            {
                case 1:
                    return singular;

                case 2:
                case 3:
                case 4:
                    return plural1;

                default:
                    return plural2;
            }
        }

        #endregion
    }
}