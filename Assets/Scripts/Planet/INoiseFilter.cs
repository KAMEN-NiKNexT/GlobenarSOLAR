using UnityEngine;

namespace Globenar
{
    public interface INoiseFilter
    {
        #region Methods

        public float Evaluate(Vector3 point);

        #endregion
    }
}