using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSys : MonoBehaviour
{
    public GameObject firEff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEffect()
    {
        firEff = Instantiate(Resources.Load("frepab/skillEffect") as GameObject);
    }

}
