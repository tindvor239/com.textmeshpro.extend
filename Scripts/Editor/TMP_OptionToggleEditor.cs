using UnityEditor.UI;
using UnityEditor;

[CustomEditor(typeof(TMP_OptionsButton), true)]
[CanEditMultipleObjects]
public class TMP_OptionToggleButton : SelectableEditor
{
    SerializedProperty m_Value;
    SerializedProperty m_IsLoop;
    SerializedProperty m_Graphic;
    SerializedProperty m_Direction;
    SerializedProperty m_Options;
    SerializedProperty m_OnClick;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();
        if (m_Value.intValue < 0)
        {
            m_Value.intValue = 0;
        }
        EditorGUILayout.PropertyField(m_Value);
        EditorGUILayout.PropertyField(m_IsLoop);
        EditorGUILayout.PropertyField(m_Graphic);
        EditorGUILayout.PropertyField(m_Direction);
        EditorGUILayout.PropertyField(m_Options);
        EditorGUILayout.PropertyField(m_OnClick);
        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_Value = serializedObject.FindProperty("m_Value");
        m_IsLoop = serializedObject.FindProperty("m_IsLoop");
        m_Graphic = serializedObject.FindProperty("m_Graphic");
        m_Direction = serializedObject.FindProperty("m_Direction");
        m_Options = serializedObject.FindProperty("m_Options");
        m_OnClick = serializedObject.FindProperty("m_OnClick");
    }
}
