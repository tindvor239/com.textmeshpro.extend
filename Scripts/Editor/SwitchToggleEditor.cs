using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(SwitchToggle))]
public class SwitchToggleEditor : ToggleEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var switchToggle = (SwitchToggle)target;
        switchToggle.handle = (Graphic)EditorGUILayout.ObjectField("Handle", switchToggle.handle, typeof(Graphic), true);

        switchToggle.handleTransition = (Selectable.Transition)EditorGUILayout.EnumPopup("Handle Transition", switchToggle.handleTransition);
        switch (switchToggle.handleTransition)
        {
            case Selectable.Transition.ColorTint:
                switchToggle.colorOn = EditorGUILayout.ColorField("On", switchToggle.colorOn);
                switchToggle.colorOff = EditorGUILayout.ColorField("Off", switchToggle.colorOff);
            break;
            case Selectable.Transition.SpriteSwap:
                switchToggle.spriteOn = (Sprite)EditorGUILayout.ObjectField("On", switchToggle.spriteOn, typeof(Sprite), true);
                switchToggle.spriteOff = (Sprite)EditorGUILayout.ObjectField("Off", switchToggle.spriteOff, typeof(Sprite), false);
            break;
            case Selectable.Transition.Animation:
                switchToggle.animatorOn = EditorGUILayout.TextField("On", switchToggle.animatorOn);
                switchToggle.animatorOff = EditorGUILayout.TextField("Off", switchToggle.animatorOff);
            break;
        }
    }
}
