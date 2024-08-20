using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public sealed class SwitchToggle : Toggle
{
    public Graphic handle;

    public Transition handleTransition;
    public Color colorOn;
    public Color colorOff;

    public Sprite spriteOn;
    public Sprite spriteOff;

    public string animatorOn;
    public string animatorOff;

    [SerializeField]
    private RectTransform _rectHandleTransform;
    
    private Animator _animator;
    private Vector2 _handlePosition = Vector2.zero;

    protected override void Awake()
    {
        GetAnimator();
        Debug.Log(isOn);
        OnSwitch(isOn);
        _rectHandleTransform = handle.GetComponent<RectTransform>();
        _handlePosition = _rectHandleTransform.anchoredPosition;
    }

    protected override void Start()
    {
        base.Start();
        OnSwitch(isOn);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        OnSwitch(isOn);
    }
#endif

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        OnSwitch(isOn);
    }

    private void OnSwitch(bool isOn)
    {
        if (handle == null)
        {
            return;
        }
        
        GetHandleRect();

        // Debug.Log(handleTransition);
        switch(handleTransition)
        {
            case Transition.ColorTint:
                handle.color = isOn ? colorOn : colorOff;
            break;
            case Transition.SpriteSwap:
                Image handleImage = (Image)handle;
                handleImage.sprite = isOn ? spriteOn : spriteOff;
            break;
            case Transition.Animation:
                GetAnimator();
                string animatorString = isOn ? animatorOn : animatorOff;
                _animator.SetBool(animatorString, isOn);
            break;
        }

        Vector3 offPosition = new Vector2(Mathf.Abs(_handlePosition.x), Mathf.Abs(_handlePosition.y));
        _rectHandleTransform.anchoredPosition = isOn ? offPosition : offPosition  * -1f;
    }

    private void GetHandleRect()
    {
        if (_rectHandleTransform == null)
        {
            _rectHandleTransform = handle.GetComponent<RectTransform>();
        }
        if (_handlePosition == Vector2.zero)
        {
            _handlePosition = _rectHandleTransform.anchoredPosition;
        }
    }

    private void GetAnimator()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }
}