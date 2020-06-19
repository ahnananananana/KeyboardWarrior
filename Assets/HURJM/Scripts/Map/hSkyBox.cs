using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hSkyBox : MonoBehaviour
{
    [SerializeField]
    private Material m_SkyBoxMat;
    private void Awake()
    {
        RenderSettings.skybox = m_SkyBoxMat;
    }
}
