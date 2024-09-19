using UnityEngine;
using TMPro;

public class ButtonTextController : MonoBehaviour
{
    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private string text;

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
        _buttonText.transform.localScale = _buttonTransform.localScale * kScaleOffset;
    }
}