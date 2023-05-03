using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Globenar
{
    public class RegistrationAgeScreen : Kamen.UI.Screen, IRegistration<int>
    {
        #region Variables

        [SerializeField] private TMP_InputField _inputField;

        #endregion

        public int GetValue()
        {
            return Convert.ToInt32(_inputField.text);
        }
    }

}
