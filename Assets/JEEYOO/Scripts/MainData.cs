using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SWORDID
{
    BROADSWORD,  // == 0, 따로 sort하지 않아도 되도록 파일명도 00Ironsword으로
    IRONBLADE,  // == 1, 따로 sort하지 않아도 되도록 파일명도 01firesword으로
    LONGSWORD,
    RAPIDBLADE,
    GIANTSWORD,
    RUNESWORD,
    PIERCINGSWORD,
    SKELETONBLADE,
    DRAGONSWORD,
    ULTIMATEBLADE,
}

public class MainData : MonoBehaviour
{
    //private static MainData instance = null;

    //public static MainData mainData
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            instance = new MainData();
    //        }
    //        return instance;
    //    }
    //}


    public Item[] ItemData;
    public Buff[] BuffData;
    public Monster[] MonsterData;
    public hLevelData[] LevelData;

    public bool isLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        ItemData = Resources.LoadAll<Item>("Prefabs");
        BuffData = Resources.LoadAll<Buff>("Prefabs");
        MonsterData = Resources.LoadAll<Monster>("Prefabs");
        LevelData = Resources.LoadAll<hLevelData>("LevelData");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
