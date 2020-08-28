using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menuspace
{
    public class Menu : MonoBehaviour
    {
        public Image transitionMask;
        public Transform spaceship;
        public Transform[] galaxyPos;
        public float maskingSpeed;
        public float transferSpeed;
        public float cameraZoomSpeed;
        public float spaceshipSpeed;
        public float spaceshipDissolveSpeed;
        public AnimationCurve acX, acY;
        Camera cam;
        Spaceship spaceshipControl;


        /// <summary>
        /// GameManager Scene Callback
        /// </summary>
        public void NextPage() { CameraTransfer(0, 1, transferSpeed); }
        public void LastPage() { CameraTransfer(1, 0, transferSpeed); }
        public void SinglePlay(Transform target) { SceneTransition(target); }


        /// <summary>
        /// Feature Function
        /// </summary>
        void SceneTransition(Transform target)
        {
            StartCoroutine(CameraZoom(cam.fieldOfView, 0));
            CameraTransfer(cam.transform.position, target.position, cameraZoomSpeed);
            MoveObjectToPlace(spaceship, target.position, spaceshipSpeed);
            CameraMask();
            Invoke("SpaceshipDissolve", 0f);
        }
        void CameraMask()
        {
            transitionMask.enabled = true;
            StartCoroutine(CameraMasking());
        }
        IEnumerator CameraMasking()
        {
            byte fade = 0;
            while(fade < 100)
            {
                fade += 1;
                Debug.Log(new Color32(255, 255, 255, fade));
                transitionMask.GetComponent<Image>().color = new Color32(255, 255, 255, fade);
                yield return null;
            }
        }
        void MoveObjectToPlace(Transform target, Vector3 somePos, float speed)
        {
            StartCoroutine(GameObjectTransfer(target, spaceship.position, somePos, speed, acX, acY));
        }
        void SpaceshipDissolve()
        {
            spaceshipControl.Dissolve(spaceshipDissolveSpeed);
        }
        IEnumerator GameObjectTransfer(Transform target, Vector3 aPos, Vector3 bPos, float speed)
        {
            float timer = 0;
            while (target.position != bPos)
            {
                timer += speed * Time.deltaTime;
                target.position = Vector3.Lerp(aPos, bPos, timer);
                yield return null;
            }
        }
        IEnumerator GameObjectTransfer(Transform target, Vector3 aPos, Vector3 bPos, float speed, AnimationCurve acX, AnimationCurve acY)
        {
            float timer = 0;
            while (target.position != bPos)
            {
                timer += speed * Time.deltaTime;
                float newPosX = Mathf.Lerp(aPos.x, bPos.x, acX.Evaluate(timer));
                float newPosY = Mathf.Lerp(aPos.y, bPos.y, acY.Evaluate(timer));
                target.position = new Vector2(newPosX, newPosY);
                yield return null;
            }
        }
        IEnumerator CameraZoom(float aField, float bField)
        {
            float timer = 0;
            while (cam.fieldOfView != bField)
            {
                timer += cameraZoomSpeed * Time.deltaTime;
                cam.fieldOfView = Mathf.Lerp(aField, bField, timer);
                yield return null;
            }
        }
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
            float timer = 0;
            while (cam.transform.position != bPos)
            {
                timer += speed * Time.deltaTime;
                cam.transform.position = Vector3.Lerp(aPos, bPos, timer);
                yield return null;
            }
        }
        private void Start()
        {
            cam = Camera.main;
            spaceshipControl = spaceship.GetComponent<Spaceship>();
        }
    }

}