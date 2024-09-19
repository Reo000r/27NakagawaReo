using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectGeneratorController : MonoBehaviour
{
    [SerializeField] private CursorOffest _cursorOffset;
    [SerializeField] private GameObject _effectFragment;
    [SerializeField] private GameObject _canvas;

    private int kDefaultScreenWidth = 1280;
    private int kDefaultScreenHeight = 720;
    private int screenWidth;
    private int screenHeight;
    private float widthOffset = 0.0f;
    private float heightOffset = 0.0f;

    private const float kEffectDirection = 0.0f;
    private const float kEffectSpeed = 1.0f;
    private const float kEffectLifeTime = 0.6f;
    private const float kEffectPosOffset = 4.0f;

    private int spriteType;

    // 値返す系
    public float KEffectDirection { get { return kEffectDirection; } }
    public float KEffectSpeed { get { return kEffectSpeed; } }
    public float KEffectLifeTime { get { return kEffectLifeTime; } }
    public int GenerateSpriteType { get { return spriteType; } }

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectGenerate(int inputSpriteType)
    {
        spriteType = inputSpriteType;

       // Vector2 screenOffset = _cursorOffset.GetScreenOffset();

        Vector3 v3 = Input.mousePosition;
        //v3.x *= screenOffset.x;
        //v3.y *= screenOffset.y;
        GameObject _fragment = null;
        EffectFragmentController _fClass = null;
        switch (spriteType)
        {
            case 0:
                // コピー用
                v3 = Input.mousePosition;
                v3 = new Vector3(v3.x, v3.y, v3.z);

                for (int i = 0; i < 4; i++)
                {
                    // 生成、Canvasの子にする、Class取得
                    v3 = Input.mousePosition;

                    Vector2 v2 = new Vector2(0, 0);
                    // 上から時計回りに生成
                    switch (i)
                    {
                        case 0:
                            v2 = new Vector2(0, 1);
                            break;
                        case 1:
                            v2 = new Vector2(1, 0);
                            break;
                        case 2:
                            v2 = new Vector2(0, -1);
                            break;
                        case 3:
                            v2 = new Vector2(-1, 0);
                            break;
                    }
                    // これでランダムな方向にもできる
                    //v2 = new Vector2(Random.Range(0, 3)-1, Random.Range(0, 3)-1);
                    v2 = v2.normalized * kEffectPosOffset;
                    v3 = new Vector3(v3.x + v2.x, v3.y + v2.y, v3.z);

                    _fragment = Instantiate(_effectFragment, v3, Quaternion.identity);
                    _fragment.transform.SetParent(_canvas.transform, false);
                    _fClass = _fragment.GetComponent<EffectFragmentController>();
                    _fClass.SetState(v2, spriteType);
                }

                break;

            case 1:
                // コピー用
                v3 = Input.mousePosition;
                v3 = new Vector3(v3.x, v3.y, v3.z);

                for (int i = 0; i < 8; i++)
                {
                    // 生成、Canvasの子にする、Class取得
                    v3 = Input.mousePosition;

                    Vector2 v2 = new Vector2(0, 0);
                    // 上から時計回りに生成
                    switch (i)
                    {
                        case 0:
                            v2 = new Vector2(0, 1);
                            break;
                        case 1:
                            v2 = new Vector2(1, 1);
                            break;
                        case 2:
                            v2 = new Vector2(1, 0);
                            break;
                        case 3:
                            v2 = new Vector2(1, -1);
                            break;
                        case 4:
                            v2 = new Vector2(0, -1);
                            break;
                        case 5:
                            v2 = new Vector2(-1, -1);
                            break;
                        case 6:
                            v2 = new Vector2(-1, 0);
                            break;
                        case 7:
                            v2 = new Vector2(-1, 1);
                            break;
                    }
                    v2 = v2.normalized * kEffectPosOffset;
                    v3 = new Vector3(v3.x + v2.x, v3.y + v2.y, v3.z);

                    _fragment = Instantiate(_effectFragment, v3, Quaternion.identity);
                    _fragment.transform.SetParent(_canvas.transform, false);
                    _fClass = _fragment.GetComponent<EffectFragmentController>();
                    _fClass.SetState(v2, spriteType);
                }
                break;
        }

    }
}
