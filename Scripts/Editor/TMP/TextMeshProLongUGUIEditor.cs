using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;

[CustomEditor(typeof(TextMeshProLongUGUI))]
public class TextMeshProLongUGUIEditor : TMP_EditorPanelUI
{
    SerializedProperty m_IsComma;
    SerializedProperty m_Value;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_IsComma = serializedObject.FindProperty("isComma");
        m_Value = serializedObject.FindProperty("m_value");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(m_Value);
        EditorGUILayout.PropertyField(m_IsComma);

        var longUI = (TextMeshProLongUGUI)target;
        longUI.value = m_Value.longValue;

        serializedObject.ApplyModifiedProperties();
    }
}
