using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Globenar
{
    public class RegistrationGenderScreen : Kamen.UI.Screen, IRegistration<string>
    {
        #region Variables

        [SerializeField] private Button _maleButton;
        [SerializeField] private Button _femaleButton;
        private string _gender;

        #endregion

        public string GetValue()
        {
            return _gender;
        }
        public void ClickOnMale()
        {
            _gender = "Мужчина";
        }
        public void ClickOnFemale()
        {
            _gender = "Женщина";
        }
    }

}
