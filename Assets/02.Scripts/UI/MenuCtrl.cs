using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCtrl : MonoBehaviour
{
    public RectTransform pauseBG;
    public RectTransform pauseMenu;
    public RectTransform soundMenu;
    public RectTransform screenMenu;
    public GameObject player;
    public bool isPause;

    void Start()
    {
        isPause = false;
        pauseBG = GameObject.Find("Canvas-joystic&menu").transform.GetChild(4).GetComponent<RectTransform>();
        pauseMenu = pauseBG.GetChild(0).GetComponent<RectTransform>();
        soundMenu = pauseBG.GetChild(1).GetComponent<RectTransform>();
        screenMenu = pauseBG.GetChild(2).GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();    //뒤로가기버튼
        }
    }

    void Pause()
    {
        isPause = !isPause;

        if (!pauseBG.gameObject.activeInHierarchy) //활성화 여부 판단
        {
            if (!pauseMenu.gameObject.activeInHierarchy)
            {
                pauseMenu.gameObject.SetActive(true);
                soundMenu.gameObject.SetActive(false);
                screenMenu.gameObject.SetActive(false);
            }

            pauseBG.gameObject.SetActive(true);
            Time.timeScale = 0;
            player.SetActive(false);
        }
    }

    //Resume Button OnClick 이벤트
    public void Resume()
    {
        pauseMenu.gameObject.SetActive(true);
        pauseBG.gameObject.SetActive(false);
        soundMenu.gameObject.SetActive(false);
        screenMenu.gameObject.SetActive(false);

        Time.timeScale = 1;
        player.SetActive(true);
    }

    public void Sound(bool isOpen)
    {
        if (isOpen)
        {
            pauseMenu.gameObject.SetActive(false);
            soundMenu.gameObject.SetActive(isOpen);
            screenMenu.gameObject.SetActive(false);
        }

        else
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void Screen(bool isOpen)
    {
        if (isOpen)
        {
            pauseMenu.gameObject.SetActive(false);
            soundMenu.gameObject.SetActive(false);
            screenMenu.gameObject.SetActive(isOpen);
        }

        else
        {
            screenMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
}
