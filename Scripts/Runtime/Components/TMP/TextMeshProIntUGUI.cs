using UnityEngine;
using UnityEngine.UI;

namespace TMPro {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/TextMeshPro - IntText (UI)", 11)]
    [ExecuteAlways]
    public class TextMeshProIntUGUI : TextMeshProUGUI {
        public bool isComma;
        
        [SerializeField]
        private int m_value;

        #region PROPERTIES
        /// <summary>
        /// Get the material that will be used for rendering.
        /// </summary>
        public override Material materialForRendering {
            get { return TMP_MaterialManager.GetMaterialForRendering(this, m_sharedMaterial); }
        }


        /// <summary>
        /// Determines if the size of the text container will be adjusted to fit the text object when it is first created.
        /// </summary>
        public override bool autoSizeTextContainer {
            get { return m_autoSizeTextContainer; }

            set { if (m_autoSizeTextContainer == value) return; m_autoSizeTextContainer = value; if (m_autoSizeTextContainer) { CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this); SetLayoutDirty(); } }
        }


        /// <summary>
        /// Reference to the Mesh used by the text object.
        /// </summary>
        public override Mesh mesh {
            get { return m_mesh; }
        }

        public virtual int Value {
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
