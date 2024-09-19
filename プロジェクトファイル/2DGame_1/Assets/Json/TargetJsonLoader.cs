using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TargetJsonLoader : MonoBehaviour
{
    // �����Ƃ��ꂢ�ɏ�����Ǝv��
    // �ƂĂ��킩��Â炢

    // �ǂݍ���json�t�@�C��
    public TextAsset textAssetDefault;
    public TextAsset textAsset1;
    public TextAsset textAsset2;

    [SerializeField] private int jsonNum;

    // json��"title"�Ɣ�r���镶����
    private const string kTitleName = "TargetPattern";
    private const string kPattern = "pattern";
    private const string kPatternNum = "patternNum";

    private const string kTitle = "title";
    private const string kX = "posX";
    private const string kY = "posY";
    private const string kTiming = "timing";
    private const string kSpeed = "speed";
    private const string kStaytime = "staytime";
    private const string kType = "type";

    //private const string kEnd = "end";
    private const char kChar = ',';

    private const float kRandomJudgeNum = 99.00f;
    private float[] RandomX = { -7.00f, -4.66f, -2.33f, 0.00f, 2.33f, 4.66f, 7.00f };
    private float[] RandomY = { -3.00f, 0.00f, 3.00f };
    private const int kRandomXElements = 7;
    private const int kRandomYElements = 3;
    private const float kRandomSpeed = 0.15f;     // 0.20f ~ 0.15f
    private const float kRandomStaytime = 0.40f;  // 0.60f ~ 0.40f
    private const float kRandomTiming = kRandomSpeed * 2 + kRandomStaytime;
    private const float kFirstRandomTiming = 5.00f;

    private float randomSpeed = 0.2f;     // 0.20f ~ 0.15f
    private float randomStaytime = 0.6f;  // 0.60f ~ 0.40f
    private float randomTiming;  // = randomSpeed * 2 + randomStaytime;
    private int[] oldTarget1Pos = { -1, -1 };
    private int[] oldTarget2Pos = { -1, -1 };
    private int[] oldTarget3Pos = { -1, -1 };

    private bool randomPatternFlag = false;


    // pattern�̗v�f�������Ă���List��pattern���Ƃɂ܂Ƃ߂�Dictionary
    // �\���̂ɂ����ق��������H
    private Dictionary<int, Dictionary<string, float>> patternDicData = null;


    // �萔��Dic���O�ł��g����悤��
    // �v���p�e�B���Ă�H
    // �g���Ƃ��� �N���X.���\�b�h��;
    public string KTitle { get { return kTitle; } }
    public string KX { get { return kX; } }
    public string KY { get { return kY; } }
    public string KTiming { get { return kTiming; } }
    public string KSpeed { get { return kSpeed; } }
    public string KStaytime { get { return kStaytime; } }
    public string KType { get { return kType; } }
    public Dictionary<int, Dictionary<string, float>> PatternDicData { get { return patternDicData; } }


    // Start is called before the first frame update
    void Start()
    {
        TargetPatternLoad();

        Dictionary<string, float> dic = GetTargetPatternDic(0, 0);

        //Debug.Log($"x,y,timing,speed,staytime,type : {dic[kX]},{dic[kY]},{dic[kTiming]}," +
        //    $"{dic[kSpeed]},{dic[kStaytime]},{dic[kType]}");
        // x,y,timing,speed,staytime,type: -7,-3,1,1,3,0
    }

    public void TargetPatternLoad()
    {
        // dic�̏�����
        patternDicData = new Dictionary<int, Dictionary<string, float>>();

        // 
        string jsonText = "";

        switch (jsonNum)
        {
            case 1:
                jsonText = textAsset1.ToString();
                break;

            case 2:
                jsonText = textAsset2.ToString();
                break;

            default:
                Debug.LogWarning("JsonTextLoad number Error");
                jsonText = textAssetDefault.ToString();
                break;
        }

        JsonNode json = JsonNode.Parse(jsonText);
        //Json��ɂ���"title"�̒l��\������
        Debug.Log(json[kTitle].Get<string>());

        // Pattern��count�Ɨv�f�Ԗڂ�count
        int patternCount = 0;
        int count = 0;

        // Json��kPatternNum�ɏ����Ă��鐔(�ǂݍ���pattern��)
        Debug.Log(int.Parse(json[$"{kPatternNum}"].Get<string>()));

        // patternCount�� Json��kPatternNum�ɏ����Ă��鐔�ɂȂ�܂�
        // kPattern�ɏ����Ă���v�f��List�ɂ܂Ƃ߂� Dic�ɒǉ����Ă���
        while (patternCount < int.Parse(json[$"{kPatternNum}"].Get<string>()))
        {
            randomPatternFlag = false;

            Dictionary<string, float> dic = new Dictionary<string, float>();

            // �����_���ɂ��邩�̃t���O�Ǘ�
            foreach (JsonNode note in json[$"{kPattern}{patternCount}"])
            {
                float tempX = float.Parse(note[kX].Get<string>());
                float tempY = float.Parse(note[kY].Get<string>());

                randomSpeed = float.Parse(note[kSpeed].Get<string>());
                randomStaytime = float.Parse(note[kStaytime].Get<string>());
                randomTiming = float.Parse(note[kTiming].Get<string>());

                if (tempX == kRandomJudgeNum && tempY == kRandomJudgeNum)
                {
                    randomPatternFlag = true;
                }
            }

            // pattern�̍ŏ���x,y���w�肳�ꂽ�l�Ȃ�
            if (randomPatternFlag)
            {
                foreach (JsonNode note in json[$"{kPattern}{patternCount}"])
                {
                    bool[] temp = new bool[3];
                    int randomX = 0;
                    int randomY = 0;
                    
                    // ���Ȃ��悤�ɂ��鏈��(���艟��)
                    do
                    {
                        temp[0] = true;
                        temp[1] = true;
                        temp[2] = true;
                        randomX = Random.Range(0, kRandomXElements);
                        randomY = Random.Range(0, kRandomYElements);
                        if (randomX != oldTarget1Pos[0] || randomY != oldTarget1Pos[1]) temp[0] = false;
                        if (randomX != oldTarget2Pos[0] || randomY != oldTarget2Pos[1]) temp[1] = false;
                        if (randomX != oldTarget3Pos[0] || randomY != oldTarget3Pos[1]) temp[2] = false;

                    } while (temp[0] || temp[1] || temp[2]);

                    oldTarget3Pos[0] = oldTarget2Pos[0];
                    oldTarget3Pos[1] = oldTarget2Pos[1];
                    oldTarget2Pos[0] = oldTarget1Pos[0];
                    oldTarget2Pos[1] = oldTarget1Pos[1];
                    oldTarget1Pos[0] = randomX;
                    oldTarget1Pos[1] = randomY;

                    //Debug.Log($"{oldTarget2Pos[0]},{oldTarget2Pos[1]} / {oldTarget1Pos[0]},{oldTarget1Pos[1]} ({randomX},{randomY})");
                    Debug.Log($"({randomX},{randomY})");

                    float posX = RandomX[randomX];
                    float posY = RandomY[randomY];
                    //float timing = kRandomTiming;
                    //float speed = kRandomSpeed;
                    //float staytime = kRandomStaytime;
                    float timing = float.Parse(note[kTiming].Get<string>());
                    float speed = float.Parse(note[kSpeed].Get<string>());
                    float staytime = float.Parse(note[kStaytime].Get<string>());
                    int type = int.Parse(note[kType].Get<string>());

                    if (patternCount != 0 && count == 0) timing = kFirstRandomTiming;
                    //Debug.Log($"{posX},{posY}");

                    dic.Add($"{kX}{count}", posX);
                    dic.Add($"{kY}{count}", posY);
                    dic.Add($"{kTiming}{count}", timing);
                    dic.Add($"{kSpeed}{count}", speed);
                    dic.Add($"{kStaytime}{count}", staytime);
                    dic.Add($"{kType}{count}", type);

                    count++;
                }
            }
            else
            {
                // {kPattern}{patternCount}�̗v�f��List�ɓ����
                foreach (JsonNode note in json[$"{kPattern}{patternCount}"])
                {
                    float posX = float.Parse(note[kX].Get<string>());
                    float posY = float.Parse(note[kY].Get<string>());
                    float timing = float.Parse(note[kTiming].Get<string>());
                    float speed = float.Parse(note[kSpeed].Get<string>());
                    float staytime = float.Parse(note[kStaytime].Get<string>());
                    int type = int.Parse(note[kType].Get<string>());

                    //Debug.Log(posX);

                    dic.Add($"{kX}{count}", posX);
                    dic.Add($"{kY}{count}", posY);
                    dic.Add($"{kTiming}{count}", timing);
                    dic.Add($"{kSpeed}{count}", speed);
                    dic.Add($"{kStaytime}{count}", staytime);
                    dic.Add($"{kType}{count}", type);

                    count++;
                }
            }

            //Debug.Log($"{kPattern}{patternCount} Continue");

            // List��Dic�ɂ܂Ƃ߂�
            patternDicData.Add(patternCount, dic);

            patternCount++;
            count = 0;
        }

        // �����dic�ɕۑ�����Ă���List�̗v�f������
        for (int i = 0; i < patternCount; i++)
        {
            // GetDic���v�f���������Ă���̂�-1
            for (int j = 0; j < GetTargetTotalNum(patternDicData[i]); j++)
            {
                // i��Dic��patternNum�ԖځAj��List��TargetNum�Ԗ�
                //Debug.Log($"dic{i},{j}({GetTargetNum(patternDicData[i])}) : " +
                //    $"{patternDicData[i][kX+j]}," +
                //    $"{patternDicData[i][kY+j]}," +
                //    $"{patternDicData[i][kTiming+j]}," +
                //    $"{patternDicData[i][kSpeed+j]}," +
                //    $"{patternDicData[i][kStaytime+j]}," +
                //    $"{patternDicData[i][kType+j]}");
            }
        }

        //Debug.Log("Continue");
        return;
    }


    // ����ꂽDic����I�̑�����Ԃ�
    public int GetTargetTotalNum(Dictionary<string, float> dic)
    {
        int count = 0;

        foreach (string key in dic.Keys)
        {
            for (int i = 0; i < dic.Count; i++)
            {
                if (key == kType + i)
                {
                    // dic����Key�̐�(�v�f��)�����
                    // ��������kType�Ɩ����������̐����J�E���g���Ă���
                    // �����I�̐�
                    // ���ʑ����̂ł����Ƃ������������肻��
                    count++;
                }
            }
        }

        return count;
    }


    // ����ꂽDic����pattern�̑�����Ԃ�
    public int GetPatternTotalNum(Dictionary<int, Dictionary<string, float>> dic)
    {
        int count = 0;

        foreach (int key in dic.Keys)
        {
            // key��0����n�܂��Ă���̂�+1
            count = key + 1;
        }

        return count;
    }


    // input�ڂ̍��W�Atiming�Aspeed����Dic�ŕԂ�
    public Dictionary<string, float> GetTargetPatternDic(int patternNum, int targetNum)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        // dicX�ɂ���patternNum�Ԗڂ�List��targetNum�Ԗڂ�float�����Ă���
        // dic��Key(������)��string�Ǘ�
        //dic.Add(kX, dicX[patternNum][targetNum]);
        //dic.Add(kY, dicY[patternNum][targetNum]);
        //dic.Add(kTiming, dicTiming[patternNum][targetNum]);
        //dic.Add(kSpeed, dicSpeed[patternNum][targetNum]);
        //dic.Add(kStaytime, dicStaytime[patternNum][targetNum]);
        //dic.Add(kType, dicType[patternNum][targetNum]);

        return dic;
    }

    //public float[] StringToFloat(string input)
    //{
    //    foreach(char s in input)
    //    {
    //        string str = "";
    //        if(s == kChar)
    //        {
    //            // ���ʎq�������ꍇ

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