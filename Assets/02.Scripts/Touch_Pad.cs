using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Pad : MonoBehaviour
{

    private RectTransform touchpad;
    private Vector3 start_pos;
    private float radius = 150;
    private Player player;

    private int touch_id = -1;
    public bool isTouch = false;
    public float h = 0f;
    public float v = 0f;

    void Start()
    {
        touchpad = GetComponent<RectTransform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        start_pos = touchpad.position;
    }

    public void OnStickPos(Vector3 StickPos)
    {
        h = StickPos.x;
        v = StickPos.y;
    }

    public void ButtonDown()
    {
        isTouch = true;
    }

    public void ButtonUp()
    {
        isTouch = false;
        MoveInput(start_pos);
    }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            DragInput();
        }

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            MoveInput(Input.mousePosition);
        }
    }

    void DragInput()
    {
        int i = 0;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;

                Vector3 touch_pos = new Vector3(touch.position.x, touch.position.y);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch_pos.x <= radius + start_pos.x)
                    {
                        touch_id = i;
                    }

                    if (touch_pos.y <= radius + start_pos.y)
                    {
                        touch_id = i;
                    }
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    MoveInput(touch_pos);
                }

                if (touch.phase == TouchPhase.Ended)
                    touch_id = -1;
            }
        }
    }

    void MoveInput(Vector3 input)
    {
        if (isTouch)
        {
            Vector3 diff = input - start_pos;

            if (diff.sqrMagnitude > (radius * radius))
            {
                diff.Normalize();

                touchpad.position = start_pos + radius * diff;
            }

            else
                touchpad.position = input;
        }

        else
            touchpad.position = start_pos;

        Vector3 differ = touchpad.position - start_pos;
        Vector3 Normal_differ = new Vector3(differ.x / radius, differ.y / radius);

        if (player != null)
        {
            player.OnStickPos(Normal_differ);
        }
    }
}
