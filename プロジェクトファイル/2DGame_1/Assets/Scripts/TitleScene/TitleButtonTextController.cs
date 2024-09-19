using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleButtonTextController : MonoBehaviour
{
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private string text;

    [SerializeField] private Vector3 scaleOffset;

    private Vector3 offset = new Vector3(0, 0, 0);
    private const float kScaleOffset = 2.0f;

    private void Start()
    {
        _buttonText.text = text;
        _buttonText.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // キャラクターの位置にoffsetを加えた位置にUIを移動させる
        _buttonText.transform.position = Camera.main.WorldToScreenPoint(_buttonTransform.position + offset);
        // xが引き延ばされているので丸める
        //_buttonText.transform.localScale = new Vector3
        //    ((_buttonTransform.localScale.x/1.5f) * kScaleOffset, 
        //    _buttonTransform.localScale.y * kScaleOffset,
        //    _buttonTransform.localScale.z * kScaleOffset);
        _buttonText.transform.localScale = new Vector3
            (_buttonTransform.localScale.x * scaleOffset.x, 
            _buttonTransform.localScale.y * scaleOffset.y,
            _buttonTransform.localScale.z * scaleOffset.z);
    }
}