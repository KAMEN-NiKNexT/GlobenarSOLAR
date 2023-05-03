using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Kamen.DataSave;

namespace Globenar
{
    public class ProfileInfo : Kamen.UI.Popup
    {
        #region Variables

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _age;
        [SerializeField] private TextMeshProUGUI _gender;
        [SerializeField] private TextMeshProUGUI _country;

        #endregion

        public override void Show()
        {
            base.Show();
            _name.text = DataSaveManager.Instance.MyData.UserProfile.Name;
            _age.text = $"Возраст: {DataSaveManager.Instance.MyData.UserProfile.Age}";
            _gender.text = $"Пол: {DataSaveManager.Instance.MyData.UserProfile.Gender}";
            _country.text = $"Страна: {DataSaveManager.Instance.MyData.UserProfile.Country}";
        }
    }
}