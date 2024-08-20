using UnityEngine;
using UnityEngine.UI;

namespace TMPro {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/TextMeshPro - LongText (UI)", 11)]
    [ExecuteAlways]
    public class TextMeshProLongUGUI : TextMeshProUGUI {
        public bool isComma;
        [SerializeField]
        protected long m_value;

        #region PROPERTIES
        public virtual long value {
            get => m_value;
            set
            {
                m_value = value;
                if (isComma) {
                    text = value.ToCommasString();
                }
                else {
                    text = value.ToString();
                }
            }
        }
        #endregion
    }
}