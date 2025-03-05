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
    void Start()
    {
        
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
                if (transform.position.y > 0.25f)
                {
                    Debug.Log("perfect hit");
                    GameManager.instance.PerfectHit();
                } else if (transform.position.y > 0.1f) {
                    GameManager.instance.GoodHit();
                    Debug.Log("good hit");
                }
                else if (transform.position.y > 0) 
                {
                    GameManager.instance.NormalHit();
                    Debug.Log("normal hit");
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
            }

        }
    }
}
