using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class ContinuousLogger : MonoBehaviour {

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private StreamWriter continuousWriter;
    private string[] continuousHeader = {
        "t",
        "headX", "headY", "headZ",
        "headRotX", "headRotY", "headRotZ",
        "leftHandX", "leftHandY", "leftHandZ",
        "leftHandRotX", "leftHandRotY", "leftHandRotZ",
        "rightHandX", "rightHandY", "rightHandZ",
        "rightHandRotX", "rightHandRotY", "rightHandRotZ",
    };


    void Start()
    {
        string filename = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        Logger.filename = filename + ".log";
        continuousWriter = new StreamWriter(filename + ".csv");
        continuousWriter.WriteLine(String.Join(",", continuousHeader) + "\n");
    }


    void Update()
    {
        string[] values = {
            Time.time.ToString(),
            head.position.x.ToString(),
            head.position.y.ToString(),
            head.position.z.ToString(),
            head.eulerAngles.x.ToString(),
            head.eulerAngles.y.ToString(),
            head.eulerAngles.z.ToString(),
            leftHand.position.x.ToString(),
            leftHand.position.y.ToString(),
            leftHand.position.z.ToString(),
            leftHand.eulerAngles.x.ToString(),
            leftHand.eulerAngles.y.ToString(),
            leftHand.eulerAngles.z.ToString(),
            rightHand.position.x.ToString(),
            rightHand.position.y.ToString(),
            rightHand.position.z.ToString(),
            rightHand.eulerAngles.x.ToString(),
            rightHand.eulerAngles.y.ToString(),
            rightHand.eulerAngles.z.ToString(),
        };
        string csv = String.Join(",", values);
        continuousWriter.WriteLine(csv + "\n");
    }


    void OnDestroy()
    {
        if (null != continuousWriter)
        {
            continuousWriter.Close();
        }
    }
}
