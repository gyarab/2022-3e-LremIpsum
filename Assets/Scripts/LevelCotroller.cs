using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point
{
    public ArrayList nighbors = new ArrayList();
    public ArrayList specialNighbors = new ArrayList();
    public int id;
    public GameObject button = null;
    public bool navstiveno = false;
    public int idSpecalny = -1; // -1 -> Nejedná se o specální
    public ArrayList cesta;
    public Point(GameObject butt)
    {
        button = butt;
    }
}
public class SpecialConnection
{
    public int idOkolnosti;
    public Point button;
    public GameObject direction;
    public float speed;
    public bool teleporatce = false;
    public GameObject middlePoint;
    public bool nejdrivTeleportuj = false;
    public SpecialConnections scScript;
    public SpecialConnection(int idOkolnosti, Point button, float speed, GameObject direction)
    {
        this.idOkolnosti = idOkolnosti;
        this.button = button;
        if(direction != null)
        {
            this.direction = direction;
        }
        else
        {
            this.direction = null;
        }
        if(speed > 0)
        {
            this.speed = speed;
        }
        else
        {
            this.speed = 0;
        }
    }
    public SpecialConnection(SpecialConnections scScript, Point button){
        this.speed = 0;
        this.scScript = scScript;
        this.direction = null;
        this.idOkolnosti = scScript.idSpojeni;
        this.button = button;
        this.teleporatce = scScript.teleporationConnection;
        button.button.GetComponent<Button>().Sc = this;
        if(this.teleporatce){
            if(scScript.firstEndPoint == button.button && scScript.isMiddlePointOverlappingFirtst){
                this.nejdrivTeleportuj = true;
            }
            if(scScript.secondEndPoint == button.button && !scScript.isMiddlePointOverlappingFirtst){
                this.nejdrivTeleportuj = true;
            }
            this.middlePoint = scScript.middlePoint;
        }
    }
}
public class LevelCotroller : MonoBehaviour
{
    // Vstupní data z inspektoru
    [Header("Místo, kde se objeví hr? po na?tení levelu")]
    [Space]
    public GameObject playerPosition;

    [Header("Seznam dlaždic jako tla?ítek")]
    [Space]
    [Header("Konec smy?ky se ozn?í vložením prázdného elementu do pole.")]
    public GameObject[] buttons;

    [Header("Odkaz na GameObject se skripty SpecialConnections pro tento level")]
    [Space]
    public GameObject specialConnectionsManager;

    [Header("ID spojení na které odkazuje skript SpecialConnection:")]
    public bool[] IDOkolnosti;

    ArrayList points = new ArrayList();


    // Saving level data variables
    [Space]
    [Header("Ukládání progresu ve h?e")]

    public bool autoSave = true;
    ArrayList moveableComponents = new ArrayList();
    public float[] additionalData = {};
    
    void Start()
    {
        // Vytvo?ení grafu z informací zadaných do LevelController
        GameObject pre = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            // Jsme na konci úseku ?etìzce?
            if (buttons[i] == null)
            {
                pre = null;
                continue;
            }
            // M?me již toto tlaè?tko zaregistrovan??
            Point p = null;
            for (int j = 0; j < points.Count; j++)
            {
                if (buttons[i] == ((Point)points[j]).button)
                {
                    p = (Point)points[j];
                    break;
                    // Ano, m?me a odkaz je v promìnn? p
                }
            }
            // Když nem?me, p?id?me do seznamu
            if (p == null)
            {
                points.Add(new Point(buttons[i]));
                p = (Point)points[(points.Count - 1)];
            }
            // Existuje p?edchoz? prvek?
            if (pre == null)
            {
                pre = buttons[i];
                continue;
            }

            // Pokud ano, najdeme ho
            for (int k = 0; k < points.Count; k++)
            {
                Point q = (Point)points[k];
                if (pre == q.button)
                {
                    // p - st?vaj?c? point q - p?edchoz? point

                    p.nighbors.Add(q);
                    q.nighbors.Add(p);
                    break;
                }
            }
            pre = buttons[i];
        }
        // P?id?n? speci?ln?ch spojen?
        SpecialConnections[] specialConnectionsScr = specialConnectionsManager.GetComponents<SpecialConnections>();
        for(int i = 0; i < specialConnectionsScr.Length; i++)
        {
            // Test regisrace prvku 1
            Point p = null;
            for (int j = 0; j < points.Count; j++)
            {
                if (specialConnectionsScr[i].firstEndPoint == ((Point)points[j]).button)
                {
                    p = (Point)points[j];
                    break;
                }
            }
            if(p == null)
            {
                points.Add(new Point(specialConnectionsScr[i].firstEndPoint));
                p = (Point)points[(points.Count-1)];
            }
            // Test regisrace prvku 2
            Point q = null;
            for (int j = 0; j < points.Count; j++)
            {
                if (specialConnectionsScr[i].secondEndPoint == ((Point)points[j]).button)
                {
                    q = (Point)points[j];
                    break;
                }
            }
            if (q == null)
            {
                points.Add(new Point(specialConnectionsScr[i].secondEndPoint));
                q = (Point)points[(points.Count - 1)];
            }
            p.specialNighbors.Add(new SpecialConnection(specialConnectionsScr[i], q));
            q.specialNighbors.Add(new SpecialConnection(specialConnectionsScr[i], p));
        }
        //Zve?ejn?n? mapy
        Player.mapa = points;

        // find player position point
        for (int k = 0; k < points.Count; k++)
        {
            if (playerPosition == ((Point)points[k]).button)
            {
                Player.currentPlayerPoint = (Point)points[k];
                break;
            }
        }

        // Získání dat pro ukládání a na?ítání levelu
        if(autoSave){
            MoveableController[] saveMoveable = GameObject.FindObjectsOfType<MoveableController>();
            for(int i = 0;i < saveMoveable.Length;i++){
                moveableComponents.Add(saveMoveable[i].gameObject);
            }
        }
        load();
        InvokeRepeating("save", 20, 20);
    }

    // Test kliknut?
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                   hit.transform.gameObject.SendMessage("clicked");
                }
            }
        }
    }
    
    public void save(){
        TransformSerializable positionRotationSerialize(GameObject input){
            Transform tr = input.transform;
            TransformSerializable returnVal = new TransformSerializable(tr.localPosition.x, tr.localPosition.y, tr.localPosition.z,
            tr.localRotation.eulerAngles.x, tr.localRotation.eulerAngles.y, tr.localRotation.eulerAngles.z);
            return returnVal;
        }
        if(autoSave){
            GameObject player = GameObject.Find("Player");
            Player playerScript = (Player)player.gameObject.GetComponent("Player");
            LevelCotroller lc = (LevelCotroller)Object.FindObjectOfType(typeof(LevelCotroller));
            GameObject camera = GameObject.Find("Main Camera");
            
            // -------------    Data, která se budou ukládat
            // Název levelu, ve kterém se hrá? nyní nachází
            string sceneName = SceneManager.GetActiveScene().name;
            // Hrá?
            TransformSerializable playerPosition = positionRotationSerialize(player);
            //GameObject playerParent = playerScript.parent;
            Point currPos = Player.currentPlayerPoint;
            ArrayList map = Player.mapa;
            int currPlayerPosId = -1;
            for(int i=0;i<map.Count;i++){
                if(map[i] == currPos){
                    currPlayerPosId = i;
                    break;
                }
            }
            // Level Controller
            
            bool[] idOkolnosti = lc.IDOkolnosti;
            // Camera
            TransformSerializable cameraPosition = positionRotationSerialize(camera);
            // Moveable components
            TransformSerializable[] moveableComponentsPosition = new TransformSerializable[moveableComponents.Count];
            for(int i = 0;i < moveableComponents.Count;i++){
                moveableComponentsPosition[i] = positionRotationSerialize((GameObject)moveableComponents[i]);
            }

            SaveData saveData = new SaveData(sceneName,playerPosition,currPlayerPosId,idOkolnosti,
                    cameraPosition,moveableComponentsPosition,additionalData);

            if (!Directory.Exists(GlobalVariables.savedirectoryName))
            Directory.CreateDirectory(GlobalVariables.savedirectoryName);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Create(GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin");

        formatter.Serialize(saveFile, saveData);

        saveFile.Close();
        //print("Game Saved to " + Directory.GetCurrentDirectory().ToString() + "/Saves/" + GlobalVariables.saveName + ".bin");
        Debug.Log("Game saved");
        }
    }

    public void load(){
        void setTransform(GameObject obj, TransformSerializable tr){
            obj.transform.localEulerAngles = new Vector3(tr.rx, tr.ry, tr.rz);
            obj.transform.localPosition = new Vector3(tr.px, tr.py, tr.pz);
        }
        if(autoSave && System.IO.File.Exists(GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin")){
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream loadFile = File.Open(GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin", FileMode.Open);

            SaveData loadData = (SaveData) formatter.Deserialize(loadFile);

            // Setting the data to the game
            setTransform(GameObject.Find("Player"), loadData.playerPosition);
            if(loadData.idOfPointPlayerIsStandingOn != -1){
                Player.currentPlayerPoint = (Point)Player.mapa[loadData.idOfPointPlayerIsStandingOn];
            }
            IDOkolnosti = loadData.idOkolnosti;
            setTransform(GameObject.Find("Main Camera"), loadData.cameraPosition);
            // Seting position of moveable compnents
            for(int i = 0;i<loadData.moveableComponentsPosition.Length;i++){
                setTransform((GameObject)moveableComponents[i], loadData.moveableComponentsPosition[i]);
            }
            additionalData = loadData.extra;
            
            // Print all of the data
            /*print("~~~ LOADED GAME DATA ~~~");
            print("Scene name: " + loadData.sceneName);
            print("Player position");
            print("X = "+loadData.playerPosition.px);
            print("y = "+loadData.playerPosition.py);
            print("z = "+loadData.playerPosition.pz);
            print("Rotation:");
            print("X = "+loadData.playerPosition.rx);
            print("Y = "+loadData.playerPosition.ry);
            print("Z = "+loadData.playerPosition.rz);
            print("Player position index:" + loadData.idOfPointPlayerIsStandingOn);
            print("ID okolnosti");
            print(IDOkolnosti);
            print("Number of moveable components: " + loadData.moveableComponentsPosition.Length);
            print("Test pos. x of first component");
            print(loadData.moveableComponentsPosition[0].px);*/

            loadFile.Close();
        }
    }
}
