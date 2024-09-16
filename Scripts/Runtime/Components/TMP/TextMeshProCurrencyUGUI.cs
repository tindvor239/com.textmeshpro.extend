using UnityEngine;

namespace TMPro {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/TextMeshPro - CurrencyText (UI)", 11)]
    [ExecuteAlways]
    public class TextMeshProCurrencyUGUI : TextMeshProLongUGUI {
        public bool isUnitAtLast;

        [SerializeField]
        private string m_Unit = "$";

        #region PROPERTIES
        public string Unit {
            get => m_Unit;
            set
            {
                m_Unit = value;
                this.value = m_value;
            }
        }

        public override long value {
            get => base.value;
            set
            {
                m_value = value;
                string sValue = GetStringValue(value);

                OnSetValue(sValue);
            }
        }
        #endregion
        
        private string GetStringValue(long value)
        {
            string sValue;
            if (isComma)
            {
                sValue = value.ToCommasString();
            }
            else
            {
                sValue = value.ToString();
            }

            return sValue;
        }

        private void OnSetValue(string sValue)
        {
            if (isUnitAtLast)
            {
                text = $"{sValue} {m_Unit}";
            }
            else
            {
                text = $"{m_Unit} {sValue}";
            }
        }
    }
}
