using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target;

    bool isFollowing = false;

    Vector3 offset = new Vector3(0, 0, -10);

    public void StartFollowing() { isFollowing = true; }

    void Update()
    {
        if (isFollowing)
            if (target)
                transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.03f);
    }
}
