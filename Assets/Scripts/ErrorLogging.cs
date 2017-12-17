using System;
using System.IO;
using UnityEngine;


    class ErrorLogging: MonoBehaviour
    {
    private StreamWriter SW;
    public string LogName = "ErrorLogs.txt";


    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        SW = new StreamWriter(Application.persistentDataPath + "/" + LogName);
        Debug.Log(Application.persistentDataPath + "/" + LogName); 
    }
    private void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
       
    }
    private void OnDisable()
    {
        Application.RegisterLogCallback(HandleLog);

    }


    void HandleLog(string ErrorName, string ErrorTrace, LogType ErrorType) {
        SW.WriteLine("The error was in" + System.DateTime.Now.ToString() + " . The name of error is: "+ErrorName+" the trace is: "+ErrorTrace+" and the type of error is"+ErrorType.ToString());

    }

    private void OnDestroy()
    {
        SW.Close();
        
    }


}

