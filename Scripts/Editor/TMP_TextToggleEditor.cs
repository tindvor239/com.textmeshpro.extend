using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(TMP_TextToggle))]
public class TMP_TextToggleEditor : ToggleEditor
{
    private SerializedProperty _offText;
    private SerializedProperty _onGraphics;
    private SerializedProperty _offGraphics;

    protected override void OnEnable()
    {
        base.OnEnable();
        _onGraphics = serializedObject.FindProperty("_onGraphics");
        _offText = serializedObject.FindProperty("offGraphic");
        _offGraphics = serializedObject.FindProperty("_offGraphics");

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_offText);
        EditorGUILayout.PropertyField(_onGraphics);
        EditorGUILayout.PropertyField(_offGraphics);
        serializedObject.ApplyModifiedProperties();
    }
}
