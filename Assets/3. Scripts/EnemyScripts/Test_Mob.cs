using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_Mob : Mob
{
    Test_Mob testMob;

    

    


    public override void Start()
    {
        base.Start();
        testMob = GetComponent<Test_Mob>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        this.enemyName = "Test_mob";
        this.speed = 1;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
