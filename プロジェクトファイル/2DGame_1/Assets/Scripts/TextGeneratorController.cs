using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class TextGeneratorController : MonoBehaviour
{
    // State‚ªStart‚ÆEnd‚É‚È‚Á‚½‚É
    // ŠÖ”‚ªŒÄ‚Î‚êtext‚ª¶¬‚³‚ê‚é(I‚í‚Á‚½‚çÁ‚·)

    [SerializeField] private GameObject _canvas;

    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _endText;

    private const float kEndTime = 3.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void StartText()
    {
        Debug.Log($"StartText");
        Vector3 v3 = new Vector3(-1000,0,0);
        GameObject _text = Instantiate(_startText, v3, Quaternion.identity);
        _text.transform.SetParent(_canvas.transform, false);
    }

    public void EndText()
    {
        Debug.Log($"EndText");
        Vector3 v3 = new Vector3(-1000,0,0);
        GameObject _text = Instantiate(_endText, v3, Quaternion.identity);
        _text.transform.SetParent(_canvas.transform, false);
        //await Task.Delay(3000);
        _text.SetActive(true);
    }
}
