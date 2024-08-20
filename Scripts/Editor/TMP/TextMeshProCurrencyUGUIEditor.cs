using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;

[CustomEditor(typeof(TextMeshProCurrencyUGUI))]
public class TextMeshProCurrencyUGUIEditor : TextMeshProLongUGUIEditor
{
    SerializedProperty m_CurrencyUnit;
    SerializedProperty isCurrencyAtLast;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Fetch the objects from the GameObject script to display in the inspector
        m_CurrencyUnit = serializedObject.FindProperty("m_currencyUnit");
        isCurrencyAtLast = serializedObject.FindProperty("isCurrencyAtLast");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(m_CurrencyUnit);
        EditorGUILayout.PropertyField(isCurrencyAtLast);

        var currency = (TextMeshProCurrencyUGUI)target;
        currency.currencyUnit = m_CurrencyUnit.stringValue;

        serializedObject.ApplyModifiedProperties();
    }
}
