using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleBGM;
    [SerializeField] private TitlePerformanceManager _titlePerformanceManager;
    [SerializeField] private TitleSoundManager _titleSoundManager;


    // Start is called before the first frame update
    void Start()
    {
        _titleSoundManager.PlayTitleBGM();
        _titlePerformanceManager.TitleAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DataDelete()
    {
        Debug.LogWarning($"PlayerPrefs Init");

        PlayerPrefs.DeleteAll();
    }

    public void SceneChange(string sceneName)
    {
        if (sceneName == "GameScene")
        {
            SceneManager.LoadScene($"{sceneName}");
        }
        else if(sceneName == "Ranking")
        {
            Debug.Log($"ƒ‰ƒ“ƒLƒ“ƒO‚Í–¢ŽÀ‘•");
        }
        else
        {
            {
                Debug.Log($"not found scene : {sceneName}");
            }
        }
    }
}
