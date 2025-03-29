using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video_script : MonoBehaviour
{

    VideoPlayer videoPlayer;
    cutsceneScript cs;
    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<cutsceneScript>();
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += HandleVideoEnd;
        // Start preparing the video
        videoPlayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {

        videoPlayer.Play();
    }

    private void HandleVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video has finished playing!");
        cs.videoIndex += 1;
    }


}
