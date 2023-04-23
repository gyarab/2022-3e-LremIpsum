using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static string[] sceneNames = {"lvl. retez"};
    public static string saveName = "savedGame";
    public static string savedirectoryName = "Saves";
    public static string menuSceneName = "Menu 2";

    public static bool loadFromSave = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void load(){
        if(System.IO.File.Exists(Application.persistentDataPath+"/"+ savedirectoryName + "/" + saveName + ".bin")){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream loadFile = File.Open(Application.persistentDataPath+"/"+ savedirectoryName + "/" + saveName + ".bin", FileMode.Open);
            SaveData loadData = (SaveData) formatter.Deserialize(loadFile);
            SceneManager.LoadScene(loadData.sceneName);
            loadFile.Close();
        }
    }
}
