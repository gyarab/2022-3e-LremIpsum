using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static string[] sceneNames;
    public static string saveName = "savedGame";
    public static string savedirectoryName = "Saves";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load(){
        if(System.IO.File.Exists(savedirectoryName + "/" + saveName + ".bin")){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream loadFile = File.Open(GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin", FileMode.Open);
            SaveData loadData = (SaveData) formatter.Deserialize(loadFile);
            SceneManager.LoadScene(loadData.sceneName);
            loadFile.Close();
        }
    }
}
