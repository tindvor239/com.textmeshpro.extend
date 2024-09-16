using TMPro;
using UnityEditor;

[CustomEditor(typeof(TextMeshProCurrencyUGUI))]
public class TextMeshProCurrencyUGUIEditor : TextMeshProLongUGUIEditor
{
    SerializedProperty m_Unit;
    SerializedProperty isUnitAtLast;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Fetch the objects from the GameObject script to display in the inspector
        m_Unit = serializedObject.FindProperty("m_Unit");
        isUnitAtLast = serializedObject.FindProperty("isUnitAtLast");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(m_Unit);
        EditorGUILayout.PropertyField(isUnitAtLast);

        var currency = (TextMeshProCurrencyUGUI)target;
        currency.Unit = m_Unit.stringValue;

        serializedObject.ApplyModifiedProperties();
    }
}
