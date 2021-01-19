using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private float x;

        private float y;

        public float speed = 5;
        public float dodgeSpeed = 2;
        public float dodgeTime = .03f;
        private float startTime;

        private Rigidbody2D thisRigid;

        private bool isDodging;
        private Vector2 movePosition;

        //interacting object code
        public TextMeshProUGUI objectCounter;
        private int objectCount;


        // Start is called before the first frame update
        void Start()
        {
            thisRigid = this.GetComponent<Rigidbody2D>();
            isDodging = false;
            objectCounter.SetText(objectCount.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDodging && Input.GetAxisRaw("Dodge") > 0)
            {
                isDodging = true;
                speed *= dodgeSpeed;
                startTime = Time.time;
            }
            if (isDodging)
            {
                if (Math.Abs(Time.time - startTime) > dodgeTime)
                {
                    isDodging = false;
                    speed /= dodgeSpeed;
                }    
            }

            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            movePosition = new Vector2(x, y) * speed;
            thisRigid.velocity = movePosition;
        }
    
        //handling object interaction
        private void OnCollisionStay2D(Collision2D collision)
        {
            GameObject collidingObj = collision.gameObject;
            if (Input.GetAxisRaw("Interact") > 0 && collidingObj.tag.Equals("Item"))
            {
                Debug.Log("player interacting with object");
                collidingObj.GetComponent<Interactable>().Interact();

            }
        }
    }
}
