using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;

namespace Globenar
{
    public class SignUpScreen : Kamen.UI.Screen
    {
        #region Variables



        #endregion

        #region Control Methods

        private void ClearFields()
        {

        }
        public void Back()
        {
            ScreenManager.Instance.TransitionTo("Start");
            ClearFields();
        }

        #endregion
    }
}