using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ResultBackController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private const float kMaxAlpha = 0.3333f;
    private const float kAnimTime = 0.5f;
    private float time = 0.0f;

    private bool isChangeAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangeAnim)
        {
            time += Time.deltaTime;
            SceneChangeAnim();
        }
    }

    public void SceneChangeAnim()
    {
        isChangeAnim = true;

        float endAnimTime = kAnimTime;

        if (endAnimTime < time)
        {
            this.gameObject.SetActive(false);
            return;
        }

        float c = time / kAnimTime;
        c = 1.0f - c;
        c *= kMaxAlpha;
        _spriteRenderer.color = new Color(255, 255, 255, c);
    }
}
