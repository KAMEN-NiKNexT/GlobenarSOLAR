using Globenar;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        public Action OnDataChanged;
        [SerializeField] private Profile _userProfile;

        #endregion

        #region Properties

        public Profile UserProfile 
        { 
            get => _userProfile; 
            set { if (value != null) _userProfile = value; }
        }

        #endregion

        #region Methods


        #endregion
    }
}