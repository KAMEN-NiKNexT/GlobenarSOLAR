using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Kamen.UI
{
    public class CalendarPanel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TextMeshProUGUI _value;

        #endregion

        #region Control Methods

        public void UpdatePanel(string newText)
        {
            _value.text = newText;
        }

        #endregion
    }
}