using UnityEngine;

namespace TMPro {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/TextMeshPro - CurrencyText (UI)", 11)]
    [ExecuteAlways]
    public class TextMeshProCurrencyUGUI : TextMeshProLongUGUI {
        public bool isCurrencyAtLast;

        [SerializeField]
        private string m_currencyUnit = "$";

        #region PROPERTIES
        public string currencyUnit {
            get => m_currencyUnit;
            set
            {
                m_currencyUnit = value;
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
            if (isCurrencyAtLast)
            {
                text = $"{sValue} {m_currencyUnit}";
            }
            else
            {
                text = $"{m_currencyUnit} {sValue}";
            }
        }
    }
}
