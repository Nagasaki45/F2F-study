using System;
using System.IO;
using UnityEngine;


public class Logger {

    static public string filename;


    static public void Event(string msg)
    {
        Debug.Log(msg);
        StreamWriter writer = new StreamWriter(filename, append: true);
        writer.WriteLine(Time.time.ToString() + ": " + msg + "\n");
        writer.Close();
    }
}
