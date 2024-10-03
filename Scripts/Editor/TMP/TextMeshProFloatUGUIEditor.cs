using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;

namespace Editor.TMP
{
    [CustomEditor(typeof(TextMeshProFloatUGUI))]
    public class TextMeshProFloatUGUIEditor : TMP_EditorPanelUI
    {
        SerializedProperty m_Value;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Value = serializedObject.FindProperty("m_value");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Value);

            var floatUI = (TextMeshProFloatUGUI)target;
            floatUI.value = m_Value.floatValue;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
