using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GetPointTextGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _generateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateText(Vector3 targetPos, int point)
    {
        GameObject _text = null;
        GetPointTextController _textClass = null;

        _text = Instantiate(_generateText, targetPos, Quaternion.identity);
        RectTransform rectTransform = _text.GetComponent<RectTransform>();

        Vector3 targetScreenPos = WorldTransformConversion(/*rectTransform, */targetPos);

        rectTransform.position = targetScreenPos;
        _text.transform.SetParent(_canvas.transform, false);
        _textClass = _text.GetComponent<GetPointTextController>();
        _textClass.SetState(point);
    }

    private Vector3 WorldTransformConversion(/*RectTransform rectTransform, */Vector3 targetPos)
    {
        // ワールド座標からスクリーン座標への変換
        Vector3 targetScreenPos = _camera.WorldToScreenPoint(targetPos);
        return targetScreenPos;

        // 使わなくても実装できたのでコメントアウト
        //// RectTransformで使えるように変換
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //rectTransform,
        //targetScreenPos,
        //null, // オーバーレイモードの場合はnull
        //out Vector2 uiLocalPos
        //);

        //return uiLocalPos;
    }
}
