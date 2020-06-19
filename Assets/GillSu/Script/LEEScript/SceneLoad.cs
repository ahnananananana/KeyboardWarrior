using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : singleton<SceneLoad>
{
    //public Slider Sbar = null;
    //public Text LoadText = null;
    private float m_SliderValue;

    private void Awake()
    {
        DontDestroyOnLoad(this); // 해당 함수실행 시 해당 데이타는 씬이 바뀌어 제거되더라도 사라지지 않음.(억지로지우는건 가능.)
    }
    private float _loadingProgress = 0f;
    public void SceneChange(int i)
    {
        _loadingProgress = 0f;
        Gilsu.GameData.I.m_CurScene = i;

        StartCoroutine(SceneLoading(i));
    }

    IEnumerator SceneLoading(int i)
    {
        yield return SceneManager.LoadSceneAsync("LoadingScene"); // LoadSceneAsync코루틴방식으로 로딩이 이루어짐.

        yield return StartCoroutine(LoadScene(i));
    }

    private AsyncOperation m_Op;

    IEnumerator LoadScene(int i)
    {
        //yield return StartCoroutine(delay(1.0f));
        //AsyncOperation op = SceneManager.LoadSceneAsync(i);
        m_Op = SceneManager.LoadSceneAsync(i);
        // 씬로딩이 끝나기 전까지 씬을 전환하지 않는다
        //op.allowSceneActivation = false; // true로 바뀌기전까지 로딩화면이 지속된다.
        m_Op.allowSceneActivation = false; // true로 바뀌기전까지 로딩화면이 지속된다.
        //while (!op.isDone)// op.allowSceneActivation != false
        while (!m_Op.isDone)// op.allowSceneActivation != false
        {
            /*if (Sbar.value > 1f)
            {
                Sbar.value = Mathf.MoveTowards(Sbar.value, 1f, Time.deltaTime);
            }
            yield return null;*/

            /*_loadingProgress = Mathf.Clamp01(op.progress / 0.9f) * 100;
            m_SliderValue = _loadingProgress;
            if (op.progress >= 0.9f)
            {
                // 씬로딩이 끝났으므로 씬을 전환한다.
                op.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(5f);*/


            _loadingProgress = Mathf.Clamp01(m_Op.progress / 0.9f) * 100;
            m_SliderValue = _loadingProgress;
            yield return null;
        }
    }

    public float GetSliderValue()
    {
        return m_SliderValue;
    }

    public void SceneActive()
    {
        m_Op.allowSceneActivation = true;
    }

    IEnumerator delay(float i)
    {
        yield return new WaitForSeconds(i);
    }
}
