using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using TMPro;

public class TMP_OptionsButton : Selectable, IPointerClickHandler, ISubmitHandler
{
    [SerializeField]
    private int m_Value = 0;
    [SerializeField]
    private bool m_IsLoop;
    [SerializeField]
    private Graphic m_Graphic;
    [SerializeField]
    private Direction m_Direction = Direction.bonus;
    [Space]
    // Items that will be visible when the dropdown is shown.
    // We box this into its own class so we can use a Property Drawer for it.
    [SerializeField]
    private OptionDataList m_Options = new OptionDataList();
    [Space]
    // Event delegates triggered on click.
    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    #region CLASS_DECLARATION
    [Serializable]
    /// <summary>
    /// Function definition for a button click event.
    /// </summary>
    public class ButtonClickedEvent : UnityEvent<int> { }

    [Serializable]
    /// <summary>
    /// Class to store the text and/or image of a single option in the dropdown list.
    /// </summary>
    public class OptionData
    {
        [SerializeField]
        private string m_Text;
        [SerializeField]
        private Sprite m_Image;

        /// <summary>
        /// The text associated with the option.
        /// </summary>
        public string text { get { return m_Text; } set { m_Text = value; } }

        /// <summary>
        /// The image associated with the option.
        /// </summary>
        public Sprite image { get { return m_Image; } set { m_Image = value; } }

        public OptionData() { }

        public OptionData(string text)
        {
            this.text = text;
        }

        public OptionData(Sprite image)
        {
            this.image = image;
        }

        /// <summary>
        /// Create an object representing a single option for the dropdown list.
        /// </summary>
        /// <param name="text">Optional text for the option.</param>
        /// <param name="image">Optional image for the option.</param>
        public OptionData(string text, Sprite image)
        {
            this.text = text;
            this.image = image;
        }
    }

    [Serializable]
    /// <summary>
    /// Class used internally to store the list of options for the dropdown list.
    /// </summary>
    /// <remarks>
    /// The usage of this class is not exposed in the runtime API. It's only relevant for the PropertyDrawer drawing the list of options.
    /// </remarks>
    public class OptionDataList
    {
        [SerializeField]
        private List<OptionData> m_Options;

        /// <summary>
        /// The list of options for the dropdown list.
        /// </summary>
        public List<OptionData> options { get { return m_Options; } set { m_Options = value; } }


        public OptionDataList()
        {
            options = new List<OptionData>();
        }
    }
    #endregion

    #region PROPERIES
    public int value
    {
        get
        {
            return m_Value;
        }
        set
        {
            SetValue(value);
        }
    }

    public Graphic graphic
    {
        get
        {
            return m_Graphic;
        }
    }

    public ButtonClickedEvent onClick => m_OnClick;
    #endregion

    #region UNITY
    protected override void Awake()
    {
        base.Awake();
        SetValue(m_Value);
    }
    #endregion

    public void AddOptions(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            m_Options.options.Add(new OptionData(options[i]));
        }
        m_Value = 0;
        RefreshShownValue();
    }

    public void ClearOptions()
    {
        m_Options.options.Clear();
        m_Value = 0;
    }

    protected virtual void SetValue(int value)
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("OptionsButton.onClick", this);
        ClampBonusValue();
        RefreshShownValue();
    }

    protected virtual void RefreshShownValue()
    {
        if (m_Options.options.Count == 0 || m_Value >= m_Options.options.Count)
        {
            return;
        }

        if (m_Graphic is TextMeshProUGUI)
        {
            var label = (TextMeshProUGUI)m_Graphic;
            // Debug.Log(m_Value);
            label.text = m_Options.options[m_Value].text;
        }
        else if (m_Graphic is Image)
        {
            var image = (Image)m_Graphic;
            image.sprite = m_Options.options[m_Value].image;
        }
    }

    #region HANDLER
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Press();
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        Press();

        // if we get set disabled during the press
        // don't run the coroutine.
        if (!IsActive() || !IsInteractable())
            return;

        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }
    #endregion

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }

    private void Press()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("OptionsButton.onClick", this);
        switch (m_Direction)
        {
            case Direction.bonus:
                m_Value++;
                ClampBonusValue();
                break;
            case Direction.minus:
                m_Value--;
                ClampMinusValue();
                break;
        }
        RefreshShownValue();
    }

    private void ClampMinusValue()
    {
        if (m_IsLoop && m_Value < 0)
        {
            m_Value = m_Options.options.Count - 1;
        }
        else
        {
            m_Value = Mathf.Clamp(m_Value, 0, m_Options.options.Count - 1);
        }
        m_OnClick.Invoke(m_Value);
    }

    private void ClampBonusValue()
    {
        if (m_IsLoop && m_Value >= m_Options.options.Count)
        {
            m_Value = 0;
        }
        else
        {
            m_Value = Mathf.Clamp(m_Value, 0, m_Options.options.Count - 1);
        }
        m_OnClick.Invoke(m_Value);
    }

    public enum Direction { bonus, minus }
}
