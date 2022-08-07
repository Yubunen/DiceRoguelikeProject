using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class ViewPointMove : MonoBehaviour
    {
        public static ViewPointMove Inst;

        [SerializeField] private float moveTime;
        [SerializeField] private float moveSpeed;

        Coroutine moveCo;
        Transform target = null;
        bool canMove = false;
        bool onFollow = false;

        void Awake() => Inst = this;

        // Start is called before the first frame update
        void Start()
        {
            canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MoveToParentCo());
                return;
            }

            if (!canMove) return;

            var mousePos = new Vector2(Input.mousePosition.x / Screen.width
                , Input.mousePosition.y / Screen.height);

            //Mouse
            {
                if (mousePos.x > 0.9f)
                {
                    onFollow = false;
                    transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                    if (mousePos.x > 0.95f)
                        transform.Translate(moveSpeed * 3 * Time.deltaTime, 0, 0);
                }
                else if (mousePos.x < 0.1f)
                {
                    onFollow = false;
                    transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                    if (mousePos.x < 0.05f)
                        transform.Translate(-moveSpeed * 3 * Time.deltaTime, 0, 0);
                }

                if (mousePos.y > 0.9f)
                {
                    onFollow = false;
                    onFollow = false;
                    transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                    if (mousePos.y > 0.95f)
                        transform.Translate(0, 0, moveSpeed * 3 * Time.deltaTime);
                }
                else if (mousePos.y < 0.1f)
                {
                    onFollow = false;
                    transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
                    if (mousePos.y < 0.05f)
                        transform.Translate(0, 0, -moveSpeed * 3 * Time.deltaTime);
                }
            }

            //Axis
            var axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(axis * moveSpeed * Time.deltaTime);
            if(axis != Vector3.zero)
                onFollow = false;

            if (onFollow)
                transform.position = target.position;
        }

        public void ResetTarget() => target = null;

        public void MoveTo(Transform target)
        {
            this.target = target;
            if(moveCo != null) StopCoroutine(moveCo);
            moveCo =  StartCoroutine(MoveToParentCo());
        }

        IEnumerator MoveToParentCo()
        {
            canMove = false;
            onFollow = false;

            Vector3 pos = target ? target.position : Vector3.zero;
            float cameraSpeed = (transform.position - pos).magnitude * (1 / moveTime);
            while (pos != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, cameraSpeed * Time.deltaTime);
                yield return null;
            }
            canMove = true;
            onFollow = true;
            moveCo = null;
        }
    }
}