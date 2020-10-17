using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FloatingClicking : MonoBehaviour
{
    public Image handle;
    void Start()
    {
        handle.gameObject.SetActive(false);
    }

    void Update()
    {
        //Screen Touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                handle.gameObject.SetActive(true);
                handle.transform.localPosition = touch.position;
            }
            else handle.gameObject.SetActive(false);
        }

        //Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            handle.gameObject.SetActive(true);
            handle.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else handle.gameObject.SetActive(false);
    }
}
