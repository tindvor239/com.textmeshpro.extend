using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(TMP_ToggleDropdown))]
public class TMP_ToggleDropdownEditor : DropdownEditor
{
    private SerializedProperty _off;
    private SerializedProperty _on;
    private SerializedProperty m_IsOn;
    private SerializedProperty _isLableNumber;
    private SerializedProperty _numberFormat;
    private SerializedProperty _numberSeperator;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_on);
        EditorGUILayout.PropertyField(_off);
        EditorGUILayout.PropertyField(m_IsOn);
        EditorGUILayout.PropertyField(_isLableNumber);
        if (_isLableNumber.boolValue)
        {
            EditorGUILayout.PropertyField(_numberFormat);
            EditorGUILayout.PropertyField(_numberSeperator);
        }
        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _off = serializedObject.FindProperty("off");
        _on = serializedObject.FindProperty("graphic");
        m_IsOn = serializedObject.FindProperty("m_IsOn");
        _isLableNumber = serializedObject.FindProperty("m_isLableNumber");
        _numberFormat = serializedObject.FindProperty("m_numberFormat");
        _numberSeperator = serializedObject.FindProperty("m_numberSperator");
    }
}
