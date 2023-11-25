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
        // StartCoroutine("WaitForMovieEnd");

        video.loopPointReached += OnMovieEnded;
    }

    void OnMovieEnded(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainScene");
    }
}
