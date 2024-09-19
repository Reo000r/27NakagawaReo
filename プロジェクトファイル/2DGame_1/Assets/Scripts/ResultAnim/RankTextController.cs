using UnityEngine;
using TMPro;

public class RankTextController : MonoBehaviour
{
    [SerializeField] private Transform _rankTransform;
    [SerializeField] private TextMeshProUGUI _rankText;

    private const string kScoreUpdateText = "<br><sup>new!</sup>   ";
    private const string kScoreText = "<sup>pts</sup>";
    private const string kEnterText = "<br>";

    [SerializeField] private string text;
    private bool isScoreUpdate = false;
    private int dispRankScore = 0;
    private int dispUpdateRankScore = 0;

    private Vector3 offset = new Vector3(-0.6f, 0, 0);

    public bool IsScoreUpdate { set { isScoreUpdate = value; } }
    public int DispRankScore { set { dispRankScore = value; } }
    public int DispUpdateRankScore { set { dispUpdateRankScore = value; } }
    
    private void Start()
    {
        
    }

    void Update()
    {
        // キャラクターの位置にoffsetを加えた位置にUIを移動させる
        _rankText.transform.position = Camera.main.WorldToScreenPoint(_rankTransform.position + offset);
    }

    public void TextDisp()
    {
        //dispScore = 999;
        //isScoreUpdate = true;

        string dispText = text + dispRankScore + kEnterText + kScoreText;
        _rankText.text = dispText;
    }

    public void TextUpdate()
    {
        //dispScore = 999;
        //isScoreUpdate = true;
        string dispText;
        if (isScoreUpdate) dispText = text + dispUpdateRankScore + kScoreUpdateText + kScoreText;
        else               dispText = text + dispUpdateRankScore + kEnterText + kScoreText;
        _rankText.text = dispText;
    }

    public void Active()
    {
        this.gameObject.SetActive(true);
    }

    public void InActive()
    {
        this.gameObject.SetActive(false);
    }
}