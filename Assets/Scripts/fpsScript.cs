using UnityEngine;
using System.Collections;

public class fpsScript : MonoBehaviour {
    float timeA;
    int fps;
    int lastFPS;
    public GUIStyle textStyle;
    // Use this for initialization
    void Start () {
        timeA = Time.timeSinceLevelLoad;
        DontDestroyOnLoad (this);
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(Time.timeSinceLevelLoad+" "+timeA);
        if(Time.timeSinceLevelLoad  - timeA <= 1) {
            fps++;
        } else {
            lastFPS = fps;
            timeA = Time.timeSinceLevelLoad;
            fps = 0;
        }
    }
    void OnGUI() {
        GUI.Label(new Rect(10, 10, 60, 60),"" + lastFPS, textStyle);
    }
}
