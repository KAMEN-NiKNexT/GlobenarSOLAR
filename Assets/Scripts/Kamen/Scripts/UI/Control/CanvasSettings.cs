using UnityEngine;
using UnityEngine.UI;

namespace Kamen.UI
{
    public class CanvasSettings : SingletonComponent<CanvasSettings>
    {
        #region Variables

        private RectTransform _canvasRectTransform;
        private CanvasScaler _canvasScaler;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _canvasRectTransform = gameObject.GetComponent<RectTransform>();
            _canvasScaler = gameObject.GetComponent<CanvasScaler>();

            if (_canvasRectTransform.sizeDelta.y / _canvasRectTransform.sizeDelta.x > 1.666f) _canvasScaler.matchWidthOrHeight = 0;
            else _canvasScaler.matchWidthOrHeight = 1;
        }

        #endregion

        #region Get Methods

        public Vector2 GetCurrentReferenceResolution() => _canvasScaler.referenceResolution;
        public Vector2 GetCurrentScreenSize() => _canvasRectTransform.sizeDelta;

        #endregion
    }
}