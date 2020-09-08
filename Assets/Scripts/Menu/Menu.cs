using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Menuspace
{
    public class Menu : MonoBehaviour
    {
        public GameObject particle;
        public Image transitionPanelMask;
        public Transform spaceship;
        public Transform[] galaxyPos;
        public GameObject castObj;
        public GameObject castReturn;
        public Transform cast;
        public byte maskingSpeed;
        public float transferSpeed;
        public float cameraZoomSpeed;
        public float spaceshipSpeed;
        public float spaceshipDissolveSpeed;
        public float castSceneMoveDuration;
        public AnimationCurve acX, acY;

        Camera cam;
        AudioSource audioSource;
        DissolveEffect spaceshipControl;

        /// <summary>
        /// GameManager Scene Callback
        /// </summary>
        public void NextPage() { CameraTransfer(0, 1, transferSpeed); }
        public void LastPage() { CameraTransfer(1, 0, transferSpeed); }
        public void SinglePlay(Transform target) { SceneTransition(target); }

        // Menu2
        public void AboutUs() { CreditCast(); }
        public void BackToMenu() { HideCast(); }


        /// <summary>
        /// Feature Function
        /// </summary>
        void CreditCast()
        {
            Vector3 pos = new Vector3(80.8f, -32.7f, -27.3f);
            castObj.transform.DOMove(pos, castSceneMoveDuration).OnComplete(() => CastEffect());
        }
        void CastEffect()
        {
            Vector3 pos = castReturn.transform.position;
            castReturn.transform.position += new Vector3(0, 10, 0);
            Image image = castReturn.GetComponent<Image>();
            castReturn.transform.DOMove(pos, 1);
            image.DOFade(1, 1).OnComplete(() => castReturn.GetComponent<Button>().interactable = true);
        }
        void HideCast()
        {
            Vector3 pos = castReturn.transform.position;
            Image image = castReturn.GetComponent<Image>();
            castReturn.transform.DOMove(pos + new Vector3(0, 10, 0), 1).OnComplete(() => castReturn.transform.position = pos);;
            image.DOFade(0, 1).OnComplete(() => image.DOFade(0, 0));
            castReturn.GetComponent<Button>().interactable = false;
            Vector3 posY = castObj.transform.position + new Vector3(0, -10, -10);
            castObj.transform.DOMove(posY, castSceneMoveDuration).OnComplete(() =>
                castObj.transform.DOMove(cast.position, castSceneMoveDuration / 2)
            );
        }
        void SceneTransition(Transform target)
        {
            CameraMask();
            CameraTransfer(cam.transform.position, target.position, cameraZoomSpeed);
            StartCoroutine(CameraZoom(cam.fieldOfView, 0));
            MoveObjectToPlace(spaceship, target.position, spaceshipSpeed);
            Invoke("SpaceshipDissolve", 0f);
            Invoke("SinglePlayerScene", 1f);
        }
        void SinglePlayerScene() { ChangeScene(2); }
        void ChangeScene(int n)
        {
            SceneManager.LoadScene(n);
        }
        void CameraMask()
        {
            transitionPanelMask.enabled = true;
            StartCoroutine(CameraMasking());
        }
        IEnumerator CameraMasking()
        {
            byte timer = 175;
            transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, 175);
            while (timer < 255)
            {
                timer += maskingSpeed;
                transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, timer);
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
            particle.SetActive(true);
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
            particle.SetActive(false);
        }
        private void Start()
        {
            cam = Camera.main;
            audioSource = GetComponent<AudioSource>();
            spaceshipControl = spaceship.GetComponent<DissolveEffect>();
            transitionPanelMask.enabled = true;
            Invoke("StartFade", 0.1f);
        }
        void StartFade()
        {
            audioSource.Play();
            audioSource.DOFade(0.1f, 0.1f);
            transitionPanelMask.DOFade(0, 5).OnComplete(() => transitionPanelMask.enabled = false);
        }
    }

}