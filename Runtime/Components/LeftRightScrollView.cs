using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DanielLochner.Assets.SimpleScrollSnap;

public class LeftRightScrollView : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] _rectOptions;
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private SimpleScrollSnap _simpleScrollSnap;
    private TMP_Text _template;

    public UnityEvent<int> OnSnap => _simpleScrollSnap.OnPanelSelected;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _template = _scrollRect.content.GetChild(0).GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        // _leftButton.onClick.RemoveAllListeners();
        // _rightButton.onClick.RemoveAllListeners();
    }

    public void AddOptions(List<string> options)
    {
        _rectOptions = new RectTransform[options.Count];
        for (int i = 0; i < options.Count; i++)
        {
            TMP_Text option = Instantiate(_template, _scrollRect.content);
            option.gameObject.SetActive(true);
            option.text = options[i];
            _simpleScrollSnap.AddToBack(option.gameObject);
        }
    }

    public void ClearOptions()
    {
        for (int i = 0; i < _rectOptions.Length; i++)
        {
            Destroy(_rectOptions[i].gameObject);
        }
        _rectOptions = new RectTransform[0];
    }

}
