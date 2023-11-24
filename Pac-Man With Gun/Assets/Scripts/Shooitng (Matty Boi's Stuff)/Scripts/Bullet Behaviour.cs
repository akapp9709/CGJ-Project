using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public ProjectileSO projectileSO;
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
        projectileSO.DoDamage(collision.gameObject, collision.tag, this.gameObject);
        Debug.Log("Enemy Hit");
    }
}
