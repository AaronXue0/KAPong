using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public Transform spaceship;
    public Transform[] galaxyPos;
    public float transferSpeed;
    public float cameraZoomSpeed;
    public float spaceshipSpeed;
    public float spaceshipDissloingSpeed;
    Camera cam;
    MenuSpaceship spaceshipControl;

    public void SinglePlayer(Transform target)
    {
        StartCoroutine(CameraZoom(cam.fieldOfView, 0));
        CameraTransfer(cam.transform.position, target.position, cameraZoomSpeed);
        MoveObjectToPlace(spaceship, target.position, spaceshipSpeed);
        Invoke("SpaceshipDissolving", 0f);
    }

    void MoveObjectToPlace(Transform target, Vector3 somePos, float speed)
    {
        StartCoroutine(GameObjectTransfer(target, spaceship.position, somePos, speed));
    }

    void SpaceshipDissolving()
    {
        spaceshipControl.Dissolving(spaceshipDissloingSpeed);
    }

    IEnumerator GameObjectTransfer(Transform target, Vector3 aPos, Vector3 bPos, float speed)
    {
        float timeCounter = 0;
        while (target.position != bPos)
        {
            timeCounter += speed * Time.deltaTime;
            target.position = Vector3.Lerp(aPos, bPos, timeCounter);
            yield return null;
        }
    }

    IEnumerator CameraZoom(float aField, float bField)
    {
        float timeCounter = 0;
        while (cam.fieldOfView != bField)
        {
            timeCounter += cameraZoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(aField, bField, timeCounter);
            yield return null;
        }
    }

    public void NextPage() { CameraTransfer(0, 1, transferSpeed); }
    public void LastPage() { CameraTransfer(1, 0, transferSpeed); }

    void CameraTransfer(int start, int end, float speed)
    {
        StartCoroutine(CameraTransferCoroutine(new Vector3(galaxyPos[start].position.x, galaxyPos[start].position.y, -10),
                                                 new Vector3(galaxyPos[end].position.x, galaxyPos[end].position.y, -10),
                                                 speed));
    }
    void CameraTransfer(Vector3 aPos, Vector3 bPos, float speed)
    {
        StartCoroutine(CameraTransferCoroutine(new Vector3(aPos.x, aPos.y, -10),
                                                 new Vector3(bPos.x, bPos.y, -10),
                                                 speed));
    }
    IEnumerator CameraTransferCoroutine(Vector3 aPos, Vector3 bPos, float speed)
    {
        float timeCounter = 0;
        while (cam.transform.position != bPos)
        {
            timeCounter += speed * Time.deltaTime;
            cam.transform.position = Vector3.Lerp(aPos, bPos, timeCounter);
            yield return null;
        }
    }

    private void Start()
    {
        cam = Camera.main;
        spaceshipControl = spaceship.GetComponent<MenuSpaceship>(); 
    }
}
