﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerAttack : MonoBehaviour
{
    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;
	
 public	void NormalAttack ()
 {
    List<Collider> targetList = new List<Collider>(normalTarget.targetList);
        foreach(Collider one in targetList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                StartCoroutine(enemy.StartDamage(NormalDamage, 
                                    transform.position, 0.5f, 0.5f));
            }
        }

 }
	
    public void DashAttack ()
    {


		
	}
}
