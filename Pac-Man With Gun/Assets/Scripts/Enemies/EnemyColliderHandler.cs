using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyColliderHandler : MonoBehaviour
{
    private Timer _delayTimer;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if (_delayTimer != null)
        {
            _delayTimer.Tick(Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
            StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
