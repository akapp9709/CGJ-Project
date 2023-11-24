using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyColliderHandler : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
