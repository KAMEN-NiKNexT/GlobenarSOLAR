using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    public class MinMax
    {
        #region Properties

        public float Min { get; private set; }
        public float Max { get; private set; }

        #endregion

        #region Constructors

        public MinMax()
        {
            Min = float.MaxValue;
            Max = float.MinValue;
        }

        #endregion

        #region Methods

        public void AddValue(float value)
        {
            if (value > Max) Max = value;
            if (value < Min) Min = value;
        }

        #endregion
    }
}