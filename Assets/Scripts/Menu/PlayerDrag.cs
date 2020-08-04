using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerDrag : MonoBehaviour
{
    [SerializeField]
    Vector3 spawnPoint;
    Vector3 startPos;
    Vector3 endPos;
    Camera cam;
    LineRenderer lr;
    Rigidbody2D rb;
    bool isClicked;
    bool isReseting;
    float idleTime;

    Menu menu;

    void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        cam = Camera.main;
        menu = FindObjectOfType<Menu>();
        StartCoroutine(Hint());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                                                 Vector3.zero);
            if (hit)
            {
                Debug.Log(hit.collider.gameObject.name);
                Move(Vector3.zero);
                isClicked = true;
                startPos = transform.position;
                lr.positionCount = 1;
                lr.SetPosition(0, startPos);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isClicked)
            {
                Move(Vector3.zero);
                endPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                lr.positionCount = 2;
                lr.SetPosition(1, endPos);
                Rotation(startPos - endPos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
            if(isClicked) Move(startPos - endPos);
            isClicked = false;
        }
    }
    void Move(Vector3 force)
    {
        Rotation(force);
        rb.velocity = force;
    }
    void Rotation(Vector3 movement)
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Reset()
    {
        isReseting = true;
        Move(Vector3.zero);
        transform.position = spawnPoint;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.name){
            case "Store":
                menu.LoadScene(1);
                break;
            case "Play":
                menu.LoadScene(2);
                break;
        }
        if (other.tag == "Boundary")
        {
            Reset();
            menu.ColoredText(255);
            menu.BroadCast("Lost Signal...");
            StartCoroutine(ResetBroadCast(1.5f));
        }
    }
    IEnumerator ResetBroadCast(float sec)
    {
        yield return new WaitForSeconds(sec);
        menu.BroadCast("Try another area");
        yield return new WaitForSeconds(sec);
        isReseting = false;
        StartCoroutine(Hint());
    }
    IEnumerator Hint()
    {
        int n = 255;
        while (true)
        {
            if(isReseting || isClicked) break;
            menu.ColoredText(n);
            switch (n)
            {
                case 0:
                    yield return new WaitForSeconds(0.4f);
                    n = 255;
                    break;
                case 255:
                    yield return new WaitForSeconds(0.6f);
                    n = 0;
                    break;
            }
        }
        menu.ColoredText(255);
        if(isClicked) menu.ColoredText(0);
    }
}
