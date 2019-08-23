using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunny : MonoBehaviour
{
    // Start is called before the first frame update
public Animator animator;
public Collider col1;
public Collider col2;
public Renderer ren;
public float speed = 1;
Color  c = new Color(1,1,1,1);
float alpha = 1;
bool dead = false;
    void Start()
    {
        ren.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            alpha -= Time.deltaTime*.2f;
            if (alpha <=0)
            {
                Destroy(gameObject);
                alpha = 0;
            }
            c.a = alpha;
            ren.material.color = c;
            
        }
        else
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            controller c = other.transform.GetComponent<controller>();
            c.getHit();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("FireLayer"))
        {
            print("slime hit");
            animator.Play("Death",-1,0);
            Destroy(col1);
            Destroy(col2);
            dead = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
                Vector3 newAngle = transform.eulerAngles;
                newAngle.y = 180;
                transform.eulerAngles = newAngle;
        
    }
}
