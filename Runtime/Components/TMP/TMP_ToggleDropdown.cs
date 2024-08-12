using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class TMP_ToggleDropdown : TMP_Dropdown
{
    public Graphic graphic;
    public Graphic off;
    [SerializeField]
    private bool m_isLableNumber;
    [SerializeField]
    private string m_numberFormat;
    [SerializeField]
    private string m_numberSperator;
    
    [SerializeField]
    private bool m_IsOn = false;
    [SerializeField]
    private bool m_AllowToSwitchOff = false;
    [SerializeField]
    private bool m_switchOffOnStart = false;
    [Tooltip("Reset on close drop down, which mean the first toggle will set on when dropdown is enable.")]
    public bool isReset = false;
    [SerializeField]
    private UnityEvent _onSwitchOff = new UnityEvent();

    private ScrollRect _scrollRect;
    private List<Toggle> _toggles = new List<Toggle>();
    private bool _toggleSelected;
    private float _height = 0;

    public bool isOn
    {
        get => m_IsOn;
        set 
        {
            m_IsOn = value;
            Set(m_IsOn);
        }
    }

    public UnityEvent OnSwitchOff => _onSwitchOff;

    /// <summary>
    /// Handling for when the dropdown is initially 'clicked'. Typically shows the dropdown
    /// </summary>
    /// <param name="eventData">The associated event data.</param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        Set(!m_IsOn);
        _scrollRect = transform.GetChild(transform.childCount - 1).GetComponent<ScrollRect>();
    }

    /// <summary>
    /// Handling for when the dropdown is selected and a submit event is processed. Typically shows the dropdown
    /// </summary>
    /// <param name="eventData">The associated event data.</param>
    public override void OnSubmit(BaseEventData eventData)
    {
        Set(!m_IsOn);
    }

    protected override void SetValue(int value, bool sendCallback = true)
    {
        if (value >= _toggles.Count || value < 0)
        {
            return;
        }
        
        if (_scrollRect != null && _toggleSelected == false && !IsToggleVisible(value))
        {
            ScrollToToggle(value);
        }

        base.SetValue(value, sendCallback);

        if (sendCallback)
        {
            _toggleSelected = false;
        }
        
        if (!m_AllowToSwitchOff)
        {
            _toggles[value].SetIsOnWithoutNotify(true);
            return;
        }
        
        if(_toggles[value].isOn == false && sendCallback)
        {
            SetValueWithoutNotify(-1);
            _onSwitchOff?.Invoke();
        }
    }

    protected override void SetupToggle(Toggle toggle, int index)
    {
        base.SetupToggle(toggle, index);

        if (toggle.group.allowSwitchOff != m_AllowToSwitchOff)
        {
            toggle.group.allowSwitchOff = m_AllowToSwitchOff;
        }

        if (m_AllowToSwitchOff && m_switchOffOnStart)
        {
            toggle.SetIsOnWithoutNotify(false);
        }

        if (toggle is not TMP_TextToggle)
        {
            _toggles.Add(toggle);
            return;
        }
        
        var textToggle = (TMP_TextToggle)toggle;
        textToggle.on = textToggle.off;
        if (m_isLableNumber)
        {
            TMP_Text[] texts = textToggle.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (index + 1).ToString(m_numberFormat) + m_numberSperator;
            texts[2].text = (index + 1).ToString(m_numberFormat) + m_numberSperator;
        }
        _toggles.Add(toggle);
        
    }

    protected override void OnSelectItem(Toggle toggle)
    {
        int selectedIndex = -1;
        Transform tr = toggle.transform;
        Transform parent = tr.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == tr)
            {
                // Subtract one to account for template child.
                selectedIndex = i - 1;
                break;
            }
        }

        if (selectedIndex < 0)
            return;
        _toggleSelected = true;
        value = selectedIndex;
    }

    protected override void Awake()
    {
        base.Awake();

        Toggle toggle = template.GetComponentInChildren<Toggle>();
        _height = toggle.GetComponent<RectTransform>().rect.height;

        Set(m_IsOn);
    }

    protected override void Start()
    {
        base.Start();
        Set(m_IsOn);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        Set(m_IsOn);

        var toggleGroup = template.GetComponentInChildren<ToggleGroup>();
        if (toggleGroup != null)
        {
            toggleGroup.allowSwitchOff = m_AllowToSwitchOff;
        }
    }
#endif

    protected virtual void Set(bool value, bool sendCallback = true)
    {
        // if we are in a group and set to true, do group logic
        m_IsOn = value;
        if (!m_IsOn)
        {
            _toggles.Clear();
            Hide();
            PlayEffect(true);
            return;
        }

        int tempValue = this.value;
        Show(false);
        if (!isReset)
        {
            this.value = tempValue;
        }
        PlayEffect(true);
    }

    /// <summary>
    /// Play the appropriate effect.
    /// </summary>
    private void PlayEffect(bool instant)
    {
        if (off != null)
        {
            off.CrossFadeAlpha(m_IsOn == false ? 1f : 0f, instant ? 0f : 0.1f, true);
        }

        if (graphic == null)
            return;

#if UNITY_EDITOR
        if (!Application.isPlaying)
            graphic.canvasRenderer.SetAlpha(m_IsOn ? 1f : 0f);
        else
#endif
            graphic.CrossFadeAlpha(m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
    }

    // Phương thức để cuộn đến phần tử cụ thể
    private void ScrollToToggle(int index)
    {
        Canvas.ForceUpdateCanvases();
        float contentHeight = _scrollRect.content.rect.height - _scrollRect.viewport.rect.height;
        float normalizedPosition = _height / contentHeight;

        // Đặt vị trí cuộn của ScrollRect
        float position = Mathf.Sign(value - index);

        if (_toggles.Count - 1 == index)
        {
            var verticalLayout = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
            float vertical = verticalLayout.padding.bottom / contentHeight;
            _scrollRect.verticalNormalizedPosition -= vertical;
        }
        _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition + normalizedPosition * position);
    }

    public bool IsToggleVisible(int index)
    {
        // Lấy tọa độ của viewport và phần tử trong không gian thế giới
        Vector3[] viewportCorners = new Vector3[4];
        _scrollRect.viewport.GetWorldCorners(viewportCorners);

        Vector3[] targetCorners = new Vector3[4];
        _toggles[index].GetComponent<RectTransform>().GetWorldCorners(targetCorners);
        
        bool isVisibleInVertical = targetCorners[0].y >= viewportCorners[0].y && targetCorners[1].y <= viewportCorners[1].y;
        bool isVisibleInHorizontal = targetCorners[0].x >= viewportCorners[0].x && targetCorners[3].x <= viewportCorners[3].x;
        
        // Kiểm tra xem phần tử có nằm trong viewport hay không
        return isVisibleInVertical && isVisibleInHorizontal;
    }

}
