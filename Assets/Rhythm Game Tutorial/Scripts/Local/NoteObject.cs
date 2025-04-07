using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private bool canBePressed = false;
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
                Debug.Log("note can be pressed now");
                if (transform.position.y > 0f)
                {
                    Debug.Log("perfect hit");
                    isHit = true;
                    GameManager.Instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                    gameObject.SetActive(false);

                } else if (transform.position.y > -0.2f) {
                    isHit = true;
                    GameManager.Instance.GoodHit();
                    Debug.Log("good hit");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                    gameObject.SetActive(false);

                }
                else if (transform.position.y > -0.4f) 
                {
                    isHit = true;
                    GameManager.Instance.NormalHit();
                    Debug.Log("normal hit");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    gameObject.SetActive(false);
                }
            }
        }
    }

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Activator")) 
    {
        canBePressed = true; 
    }
}


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            if (!isHit)
            {
                canBePressed = false; 
                if(GameManager.Instance != null) {
                GameManager.Instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                gameObject.SetActive(false);
                Debug.Log("missed note");}
            }

        }
    }
}
