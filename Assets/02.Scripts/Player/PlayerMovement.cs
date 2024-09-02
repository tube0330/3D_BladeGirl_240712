﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{

    protected Animator ani;
    float h = 0f, v = 0f;
    Rigidbody rbody;

    float lastAttackTime, lastSkillTime, lastDashTime;
    public bool attacking = false;
    public bool dashing = false;
    public float ba;

    void Start ()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        attacking = false;
        ba = 0f;
    }
	public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;

    }
	void Update ()
    {
        if(ani)
        {
            ba = 1f;
            if (v < 0f) ba = -1f;
            ani.SetFloat("Speed", h * h + v * v);
           if(rbody)
            {
                Vector3 speed = rbody.velocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rbody.velocity = speed;
                if(h!=0f && v!=0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }
		
	}
    public void OnAttackDown()
    {
        attacking = true;
        ani.SetBool("Combo", true);
        StartCoroutine(StartAttack());
    }
    public void OnAttackUp()
    {
        ani.SetBool("Combo", false);
        attacking = false;
    }
    IEnumerator StartAttack()
    {
        if((Time.time - lastAttackTime)>1f)
        {
            lastAttackTime = Time.time;
            while(attacking)
            {
                ani.SetTrigger("AttackStart");
                yield return new WaitForSeconds(1f);
            }

        }


    }
    public void OnSkillDown()
    {
        if(Time.time - lastSkillTime >1f)
        {
            ani.SetBool("Skill", true);
            lastSkillTime = Time.time;
        }
    }
    public void OnSkillUp()
    {
     ani.SetBool("Skill", false);  
    }
    public void OnDashDown()
    {
        if(Time.time - lastDashTime>1f)
        {
            lastDashTime = Time.time;
            dashing = true;
            ani.SetTrigger("Dash");
        }

    }
    public void OnDashUp()
    {
        dashing = false;
    }
}
