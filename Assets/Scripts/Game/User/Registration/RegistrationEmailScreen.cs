using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Globenar
{
    public class RegistrationEmailScreen : Kamen.UI.Screen, IRegistration<string>
    {
        #region Variables

        [SerializeField] private TMP_InputField _inputField;

        #endregion

        public string GetValue()
        {
            return _inputField.text;
        }
    }

}