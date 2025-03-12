using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    // Start is called before the first frame update
    private bool isHit = false; 
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    void Start()
    {
        float xPos = transform.position.x;

        if (Mathf.Approximately(xPos, -1.5f))
        {
            keyToPress = KeyCode.D;
        }
        else if (Mathf.Approximately(xPos, -0.5f))
        {
            keyToPress = KeyCode.F;
        }
        else if (Mathf.Approximately(xPos, 0.5f))
        {
            keyToPress = KeyCode.J;
        }
        else if (Mathf.Approximately(xPos, 1.5f))
        {
            keyToPress = KeyCode.K;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                isHit = true;
                gameObject.SetActive(false);
                //GameManager.instance.NoteHit();
                if (transform.position.y > 0f)
                {
                    Debug.Log("perfect hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                /*} else if (transform.position.y > 0.1f) {
                    GameManager.instance.GoodHit();
                    Debug.Log("good hit");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (transform.position.y > 0) 
                {
                    GameManager.instance.NormalHit();
                    Debug.Log("normal hit");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);*/

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            if (!isHit)
            {
                canBePressed = false; 
                GameManager.instance.NoteMissed();
                //Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                gameObject.SetActive(false);

            }

        }
    }
}
