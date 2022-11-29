using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point
{
    public ArrayList nighbors = new ArrayList();
    public ArrayList specialNighbors = new ArrayList();
    public int id;
    public GameObject button = null;
    public bool navstiveno = false;
    public int idSpecalny = -1; // -1 -> Nejedn? se o speci?ln?
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
    // Vstupn? data z inspektoru
    [Header("M?sto, kde se objev? hr?è po  naèten? levelu")]
    [Space]
    public GameObject playerPosition;

    [Header("Tlaè?tka k pohybu")]
    [Space]
    [Header("Konec smyèky se oznaè? vložen?m pr?zdn?ho elementu do pole.")]
    public GameObject[] buttons;

    [Header("Odkaz na GameObject se skripty SpecialConnections pro tento level")]
    [Space]
    public GameObject specialConnectionsManager;

    [Header("ID okolnost? a p?edem dan? hodnoty:")]
    public bool[] IDOkolnosti;

    ArrayList points = new ArrayList();
    // Vytvo?en? grafu z informac? zadan?ch do LevelController
    void Start()
    {
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
}
