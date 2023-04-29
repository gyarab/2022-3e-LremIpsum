using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Analytics : MonoBehaviour
{
    public static string connectURL = "http://localhost/JednoduchServerLanaCatcher/index.php";

    [System.Obsolete]
    void Start()
     {
        StartCoroutine(Upload());
     }

    //Unity data to PHP
    [System.Obsolete]
    IEnumerator Upload()
     {
         using (UnityWebRequest www = UnityWebRequest.Post("http://fravoj.wz.cz/lana-check.php", new WWWForm()))
        {
            // set the form data
            WWWForm form = new WWWForm();
            form.AddField("info", getSystemInfo());

            // send the request
            www.uploadHandler = new UploadHandlerRaw(form.data);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return www.SendWebRequest();

            // handle the response
            if (www.result == UnityWebRequest.Result.Success)
            {
            }
            else
            {
            }
        }
     }
    // Update is called once per frame
    void Update()
    {
        
    }
     static public string getSystemInfo()
 {
     string str = "";

     #if UNITY_IOS
     str += "<td>"+UnityEngine.iOS.Device.generation.ToString()+"</td>";
     #endif

     #if UNITY_ANDROID
     str += "<td>" + SystemInfo.deviceModel+"</td>";
     #endif

     str += "<td>" + SystemInfo.deviceType+"</td>";
     str += "<td>" + SystemInfo.operatingSystem+"</td>";
     str += "<td>" + SystemInfo.systemMemorySize+"</td>";
     str += "<td>" + Application.platform+"</td>";
     str += "<td>" + Screen.width + " x " + Screen.height+"</td>";
     return str;
 }
}
