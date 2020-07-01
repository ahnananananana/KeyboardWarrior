using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    NORMAL,
    ELITE,
    BONUS,
    BOSS,
    count,
}

public class hMapManager : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera m_VCamera;
    [SerializeField]
    private hMapDatabase m_MapDatabase;
    [SerializeField]
    private hMap m_Map;
    [SerializeField]
    private Transform m_MapPos;

    private hLevelData m_CurLevelData;
    private Buff m_Buff;
    private Transform m_PlayerPos;
    [SerializeField]
    private Transform m_BasicPlayerPos, m_PlayerBossPos;
    private Player m_Player;
    [SerializeField]
    private Player m_PlayerPrefab;
    [SerializeField]
    private hPlayerUI m_PlayerUI;
    [SerializeField]
    private hClearPopup m_ClearPopup;

    public hLevelData curLevelData { get => m_CurLevelData; set => m_CurLevelData = value; }

    private void Awake()
    {
        if (m_MapDatabase == null) m_MapDatabase = Resources.Load("MapDatabase") as hMapDatabase;
        if (m_MapDatabase == null)
        {
            Debug.LogError("There is no mapdatabase!");
            return;
        }

        m_Map.enterEvent += EnterEntrance;

        if (m_Player == null) m_Player = FindObjectOfType<Player>();
        if (m_Player == null)
        {
            m_Player = Instantiate(m_PlayerPrefab);
            m_Player.gameObject.name = "Player";
            DontDestroyOnLoad(m_Player);
        }
        m_Player.AttachUI(m_PlayerUI);
        m_ClearPopup.gameObject.SetActive(false);
        m_Map.clearEvent += SetClearPopup;
    }

    private void Start()
    {
        LoadMap();
    }

    //맵씬 로드 후 맵 로드
    public void LoadMap()
    {
        hLevelData levelData = hLevelManager.current.LoadNextLevel();

        for (int i = 0; i < m_Map.entranceList.Length; ++i)
        {
            MapType type = (MapType)Random.Range(0, (int)MapType.BOSS + 1);
            // MapType type = MapType.BOSS;
            m_Map.entranceList[i].SetType(type);
        }
            
        m_Map.SetSpawnMonster(levelData.monsterList);

        if(levelData.buffList.Length > 0)
        {
            int index = Random.Range(0, levelData.buffList.Length);
            m_Buff = levelData.buffList[index];
        }

        if(m_Buff != null)
        {
            m_Buff.ApplyBuff(m_Player);
            m_PlayerUI.SetBuffPopup(m_Buff);
            /*m_BuffPopup.gameObject.SetActive(true);
            m_BuffPopup.Set(m_Buff);
            hLevelManager.current.buffList.Add(m_Buff);*/
        }

        m_PlayerPos = m_BasicPlayerPos;
        if (levelData.isBoss) m_PlayerPos = m_PlayerBossPos;

        m_Player.transform.position = m_PlayerPos.position;
        m_PlayerPos.SetParent(m_Player.transform);
        m_PlayerPos.localPosition = Vector3.zero;

        m_VCamera.Follow = m_PlayerPos;
        m_VCamera.LookAt = m_PlayerPos;

        m_CurLevelData = levelData;

        m_Map.StartMap();
    }

    private void EnterEntrance(hEntrance inEntrance)
    {
        Debug.Log("enter entrance " + inEntrance.id + " " + inEntrance.nextMapType);
        hLevelManager.current.curLevel++;
        hLevelManager.current.nextMapType = inEntrance.nextMapType;
        Destroy(m_PlayerPos);
        SceneLoad.I.SceneChange(2);
        //해당 출입구 id를 가지고 다음 맵을 선택
        //먼저 로딩씬으로 전환 후 다음 맵 로딩 후 전환
        //levelmanager에게 해당 leveldata 요청
    }

    public void SetClearPopup()
    {
        m_ClearPopup.gameObject.SetActive(true);
        m_ClearPopup.Set(hLevelManager.current.curLevel);
    }
}
