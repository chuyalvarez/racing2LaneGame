using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class replayGame : MonoBehaviour
{
    public Text totalTime;
    // Start is called before the first frame update
    void Start()
    {
        totalTime.text = "Tiempo Total: "+PlayerPrefs.GetFloat("time").ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
