using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //add damage dealer
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && gameObject.tag == "Pistol Bullet")
        {
            GetComponent<SpriteRenderer>().enabled = false; 
            Destroy(gameObject, 0.1f);
            Debug.Log("Hit Enemy");
        }
    }
}
