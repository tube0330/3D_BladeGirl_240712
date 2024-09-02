using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
/* �� ���� ������Ʈ���� Animator ������Ʈ�� ������ �ȵ�
 * �����ڰ� �Ǽ��� Animator ������Ʈ�� �����ϸ� ����� ���
 */

public class Player : MonoBehaviour
{
    public Animator ani;
    protected Rigidbody rb;
    public GameObject Button;
    public AudioClip AudioClip;
    public AudioSource AudioSource;

    float h = 0f, v = 0f;
    float lastAttackTime;
    bool isSkill = false;
    bool isCombo = false;
    bool isDash = false;

    IEnumerator Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        AudioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        lastAttackTime = 0f;

        yield return null;
    }
    public void OnStickPos(Vector3 StickPos)
    {
        h = StickPos.x;
        v = StickPos.y;
    }

    public void SkillDown()
    {
        isSkill = true;

        if (isSkill)
            ani.SetTrigger("skillTrigger");
    }

    public void SkillUp()
    {
        isSkill = false;
    }

    public void AttackDown()
    {
        StartCoroutine(Attack());
    }

    public void Coconut()
    {
        AudioSource.PlayOneShot(AudioClip, 3.0f);
    }

    IEnumerator Attack()
    {
        if (Time.time - lastAttackTime > 1f)
        {
            isCombo = true;
            lastAttackTime = Time.time;

            while (isCombo)
            {
                ani.SetBool("IsCombo", true);

                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void AttackUp()
    {
        isCombo = false;
        ani.SetBool("IsCombo", isCombo);
    }


    public void DashDown()
    {
        isDash = true;

        if (isDash)
            ani.SetTrigger("dashTrigger");

        isDash = false;
    }

    public void DashUp()
    {
        isDash = false;
    }


    void Update()
    {
        if (ani != null)
        {
            ani.SetFloat("speed", (h * h + v * v));

            if (rb != null)
            {
                Vector3 speed = rb.velocity;

                speed.x = 4 * h;
                speed.z = 4 * v;

                rb.velocity = speed;

                if (h != 0 && v != 0)
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            }
        }
    }
}