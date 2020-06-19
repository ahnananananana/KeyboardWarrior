using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    /*private float _loadingProgress = 0f;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        DontDestroyOnLoad(this);
    }

    public void SceneChange(int i)
    {
        _loadingProgress = 0f;
        GameData.current.m_CurScene = i;
        StartCoroutine(SceneLoading(i));
    }

    IEnumerator SceneLoading(int i)
    {
        //yield return SceneManager.LoadSceneAsync("Loading");
        yield return null;
        StartCoroutine(LoadScene(i));
    }

    IEnumerator LoadScene(int i)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(i);
        //씬로딩이 끝나기 전까진 활성화ㄴ
        op.allowSceneActivation = false;
        while(!op.isDone)
        {
            _loadingProgress = Mathf.Clamp01(op.progress / .9f) * 100f;
            if(op.progress >= .9f)
            {
                //씬로딩이 끝났으므로 씬을 활성화ㄱ
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }*/
}
