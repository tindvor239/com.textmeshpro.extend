using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMP_TextToggle : Toggle
{
    public TextMeshProUGUI offGraphic;

    [SerializeField]
    private Graphic[] _onGraphics;
    [SerializeField]
    private Graphic[] _offGraphics;

    public string on
    {
        get => ((TMP_Text)graphic).text;
        set => ((TMP_Text)graphic).text = value;
    }

    public string off
    {
        get => offGraphic.text;
        set => offGraphic.text = value;
    }

    protected override void Set(bool value, bool sendCallback = true)
    {
        base.Set(value, sendCallback);
        offGraphic.CrossFadeAlpha(isOn == false ? 1f : 0f, value ? 0f : 0.1f, true);
        OnSetEnable(_onGraphics, isOn, value);
        OnSetEnable(_offGraphics, isOn == false, value);
    }

    private void OnSetEnable(Graphic[] graphics, bool isOn, bool value)
    {
        foreach (Graphic graphic in graphics)
        {
            graphic.CrossFadeAlpha(isOn? 1f : 0f, value ? 0f : 0.1f, true);
        }
    }
}
