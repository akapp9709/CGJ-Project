using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MovieEventHandler : MonoBehaviour
{
    VideoPlayer video;

    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        StartCoroutine("WaitForMovieEnd");
    }


    public IEnumerator WaitForMovieEnd()
    {
        while (video.isPlaying)
        {
            yield return new WaitForEndOfFrame();

        }
        OnMovieEnded();
    }

    void OnMovieEnded()
    {
        SceneManager.LoadScene("MainScene");
    }
}
