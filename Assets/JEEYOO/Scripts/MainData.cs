using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SWORDID
{
    BROADSWORD,  // == 0, 따로 sort하지 않아도 되도록 파일명도 00_BroadSword으로
    IRONBLADE,  // == 1, 따로 sort하지 않아도 되도록 파일명도 01_IronBlade으로
    LONGSWORD,
    RAPIDBLADE,
    GIANTSWORD,
    RUNESWORD,
    PIERCINGSWORD,
    SKELETONBLADE,
    DRAGONSWORD,
    ULTIMATEBLADE,
}

public enum BUFFID
{
    HPUP10,
    HPDOWN10,
    ATKUP10,
    ATKDOWN10,
    DEFUP10,
    DEFDOWN10,
    MAGUP10,
    MAGDOWN10,
    RESUP10,
    RESDOWN10,
    MOVESPDUP10,
    MOVESPDDOWN10,
    ATKSPDUP10,
    ATKSPDDOWN10,
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
    //public Monster[] MonsterData;
    //public hLevelData[] LevelData;

    // Start is called before the first frame update
    void Start()
    {
        ItemData = Resources.LoadAll<Item>("Prefabs");
        BuffData = Resources.LoadAll<Buff>("Prefabs");
        //MonsterData = Resources.LoadAll<Monster>("Prefabs");
        //LevelData = Resources.LoadAll<hLevelData>("LevelData");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
