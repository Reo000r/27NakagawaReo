using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    // 絶対もっときれいに書けるぞこれ
    // とてもわかりづらい

    // 読み込むjsonファイル
    public TextAsset textAsset;


    // jsonの"title"と比較する文字列
    private const string kTitleName = "TargetPattern";
    private const string kPattern = "pattern";
    private const string kPatternNum = "patternNum";

    public const string kTitle = "title";
    public const string kX = "posX";
    public const string kY = "posY";
    public const string kTiming = "timing";
    public const string kSpeed = "speed";
    public const string kStaytime = "staytime";
    public const string kType = "type";

    //private const string kEnd = "end";
    private const char kChar = ',';


    // patternの要素が書いてあるListをpatternごとにまとめるDictionary
    // 構造体にしたほうがいい？
    Dictionary<int, List<float>> dicX = null;         // XY座標
    Dictionary<int, List<float>> dicY = null;
    Dictionary<int, List<float>> dicTiming = null;    // 出るタイミング、前の的の出現からどれだけ待つか
    Dictionary<int, List<float>> dicSpeed = null;     // 出現、消滅の速度
    Dictionary<int, List<float>> dicStaytime = null;  // 留まる時間
    Dictionary<int, List<int>> dicType = null;        // 的の種類(int)

    //List<float> targetPatternListX = null;
    //List<float> targetPatternListY = null;
    //List<float> targetPatternListTiming = null;

    //List<List<float>> listList = new List<List<float>>();


    // Start is called before the first frame update
    void Start()
    {
        TargetPatternLoad();

        Dictionary<string, float> dic = GetTargetPatternDic(0,0);

        Debug.Log($"x,y,timing,speed,staytime,type : {dic[kX]},{dic[kY]},{dic[kTiming]}," +
            $"{dic[kSpeed]},{dic[kStaytime]},{dic[kType]}");
        // x,y,timing,speed,staytime,type: -7,-3,1,1,3,0
    }

    public void TargetPatternLoad()
    {
        // dicの初期化
        dicX = new Dictionary<int, List<float>>();
        dicY = new Dictionary<int, List<float>>();
        dicTiming = new Dictionary<int, List<float>>();
        dicSpeed = new Dictionary<int, List<float>>();
        dicStaytime = new Dictionary<int, List<float>>();
        dicType = new Dictionary<int, List<int>>();

        // 
        string jsonText = textAsset.ToString();
        JsonNode json = JsonNode.Parse(jsonText);
        //Json上にある"title"の値を表示する
        Debug.Log(json[kTitle].Get<string>());

        // Patternのcountと要素番目のcount
        int patternCount = 0;
        int count = 0;

        // JsonのkPatternNumに書いてある数(読み込むpattern数)
        Debug.Log(int.Parse(json[$"{kPatternNum}"].Get<string>()));

        // patternCountが JsonのkPatternNumに書いてある数になるまで
        // kPatternに書いてある要素をListにまとめて Dicに追加している
        while (patternCount < int.Parse(json[$"{kPatternNum}"].Get<string>()))
        {
            // Listの初期化
            List<float> targetPatternListX = new List<float>();
            List<float> targetPatternListY = new List<float>();
            List<float> targetPatternListTiming = new List<float>();
            List<float> targetPatternListSpeed = new List<float>();
            List<float> targetPatternListStaytime = new List<float>();
            List<int> targetPatternListType = new List<int>();

            // {kPattern}{patternCount}の要素をListに入れる
            foreach (JsonNode note in json[$"{kPattern}{patternCount}"])
            {
                float posX = float.Parse(note[kX].Get<string>());
                float posY = float.Parse(note[kY].Get<string>());
                float timing = float.Parse(note[kTiming].Get<string>());
                float speed = float.Parse(note[kSpeed].Get<string>());
                float staytime = float.Parse(note[kStaytime].Get<string>());
                int type = int.Parse(note[kType].Get<string>());

                targetPatternListX.Add(posX);
                targetPatternListY.Add(posY);
                targetPatternListTiming.Add(timing);
                targetPatternListSpeed.Add(speed);
                targetPatternListStaytime.Add(staytime);
                targetPatternListType.Add(type);

                //string str = $"{posX}{kChar}{posY}{kChar}{timing}{kChar}" +
                //    $"{speed}{kChar}{staytime}{kChar}{type}";

                //Debug.Log($"{kPattern}{patternCount}  x,y,timing,speed,staytime,type : " +
                //    $"{targetPatternListX[count]}," +
                //    $"{targetPatternListY[count]}," +
                //    $"{targetPatternListTiming[count]}," +
                //    $"{targetPatternListSpeed[count]}," +
                //    $"{targetPatternListStaytime[count]}," +
                //    $"{targetPatternListType[count]}");

                //Debug.Log(str);

                count++;
            }

            Debug.Log($"{kPattern}{patternCount} Continue");

            // ListをDicにまとめる
            dicX.Add(patternCount, targetPatternListX); 
            dicY.Add(patternCount, targetPatternListY); 
            dicTiming.Add(patternCount, targetPatternListTiming); 
            dicSpeed.Add(patternCount, targetPatternListSpeed); 
            dicStaytime.Add(patternCount, targetPatternListStaytime); 
            dicType.Add(patternCount, targetPatternListType); 

            patternCount++;
            count = 0;
        }

        // これでdicに保存されているListの要素を取れる
        for (int i = 0; i < patternCount; i++)
        {
            for (int j = 0; j < dicX[i].Count; j++)
            {
                // iがDicのpatternNum番目、jがListのTargetNum番目
                Debug.Log($"dic{i},{j} : {dicX[i][j]},{dicY[i][j]},{dicTiming[i][j]}" +
                    $",{dicSpeed[i][j]},{dicStaytime[i][j]},{dicType[i][j]}");
            }
        }

        Debug.Log("Continue");
        return;
    }


    // input個目の座標、timing、speed等をDicで返す
    public Dictionary<string, float> GetTargetPatternDic(int patternNum, int targetNum)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        // dicXにあるpatternNum番目のListのtargetNum番目のfloatを入れている
        // dicのKey(第一引数)はstring管理
        dic.Add(kX, dicX[patternNum][targetNum]);
        dic.Add(kY, dicY[patternNum][targetNum]);
        dic.Add(kTiming, dicTiming[patternNum][targetNum]);
        dic.Add(kSpeed, dicSpeed[patternNum][targetNum]);
        dic.Add(kStaytime, dicStaytime[patternNum][targetNum]);
        dic.Add(kType, dicType[patternNum][targetNum]);

        return dic;
    }

    //public float[] StringToFloat(string input)
    //{
    //    foreach(char s in input)
    //    {
    //        string str = "";
    //        if(s == kChar)
    //        {
    //            // 識別子だった場合

    //        }
    //        else
    //        {
    //            str += s;
    //        }
    //        Debug.Log(str);
    //    }

    //    return null;
    //}
}