using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;

[CustomEditor(typeof(TextMeshProIntUGUI))]
public class TextMeshProIntUGUIEditor : TMP_EditorPanelUI
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

        var intTextUI = (TextMeshProIntUGUI)target;
        intTextUI.Value = m_Value.intValue;

        serializedObject.ApplyModifiedProperties();
    }
}
