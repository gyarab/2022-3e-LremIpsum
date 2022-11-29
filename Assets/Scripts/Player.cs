 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Point currentPlayerPoint;
    public static ArrayList mapa; // <- Seznam v�ech dla�dic s odkazy na sv� sousedy
    public static ArrayList moveTo = new ArrayList(); // <- Seznam dla�dic tvo?�c�ch cestu do c�le
    public static float speed = 1; // <- Rychlost pohybu
    float speedBackup;
    public static float rotationSpeed = 3; // <- Rychlost rotace
    static float delka = 0;

    public GameObject parent = null;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    public static void najdiCestu(Point cil)
    {
        // Startovn� pozice je m�sto, na kter�m se pr�v� nach�z� hr��, toto m�sto je ulo�eno v prom�nn� currentPlayerPoint
        Point start = currentPlayerPoint;
        // Nastavit u v�ech vrchol� nav�t�venost na false
        for (int k = 0; k < mapa.Count; k++)
        {
            ((Point)mapa[k]).navstiveno = false;
            ((Point)mapa[k]).idSpecalny = -1;
        }

        // Fronta pro proch�zen� do ��?ky
        ArrayList fronta = new ArrayList();
        // Vlo�en� a nastaven� prvn�ho prvku
        fronta.Add(start);
        ((Point)fronta[0]).cesta = new ArrayList();
        ((Point)fronta[0]).navstiveno = true;
        
        // Proch�zen� fronty
        while (fronta.Count != 0)
        {
            Point p = (Point)fronta[0];
            fronta.RemoveAt(0);
            p.navstiveno = true;
            // Ukon�en� po nalezen� nejkrat�� cesty do c�le
            if(p.button == cil.button)
            {
                // D�lka cesty ->   Debug.Log(p.cesta.Count);
                moveTo = p.cesta;
                delka = moveTo.Count;
                // Vyps�n� cel� cesty do konzole
                /*for (int l = 0; l < p.cesta.Count; l++)
                {
                    Debug.Log(((Point)p.cesta[l]).button.gameObject.name);
                }*/
                break;
            }
            // Proch�zen� v�ech trval�ch nenav�t�ven�ch soused�
            for(int i = 0; i < p.nighbors.Count; i++)
            {
                if (((Point)p.nighbors[i]).navstiveno == false) {
                    ((Point)p.nighbors[i]).cesta = (ArrayList)p.cesta.Clone();
                    ((Point)p.nighbors[i]).cesta.Add(p.nighbors[i]);
                    fronta.Add(p.nighbors[i]);
                }
            }
            // Proch�zen� v�ech soused� soused�c�ch jen za ur?it�ch okolnost�
            for (int i = 0; i < p.specialNighbors.Count; i++)
            {   bool platiOkolnost = GameObject.Find("LevelController").GetComponent<LevelCotroller>().IDOkolnosti[((SpecialConnection)p.specialNighbors[i]).idOkolnosti];
                if (platiOkolnost && ((SpecialConnection)p.specialNighbors[i]).button.navstiveno == false)
                {
                    ((SpecialConnection)p.specialNighbors[i]).button.cesta = (ArrayList)p.cesta.Clone();
                    ((SpecialConnection)p.specialNighbors[i]).button.idSpecalny = ((SpecialConnection)p.specialNighbors[i]).idOkolnosti;
                    ((SpecialConnection)p.specialNighbors[i]).button.cesta.Add(((SpecialConnection)p.specialNighbors[i]).button);
                    fronta.Add(((SpecialConnection)p.specialNighbors[i]).button);
                }
            }
        }
    }

    void Start()
    {
        speedBackup = speed;
    }
    // Pohyby postavy
    float uprava = 0.6f;
    bool teleportovano = false;
    void Update()
    {
        // Oprava probl�mu se zm�nami velikosti postavy p�i rotaci p�edka
        /*if(parent == null)
        {
            transform.localScale = new Vector3(x, y, z); // Nastaven� na p�vpdn� hodnoty p�ed za��tkem hry
        }
        else
        {
           transform.localScale = new Vector3(x/parent.transform.localScale.x, y / parent.transform.localScale.y, z / parent.transform.localScale.z);
        }*/

        // M?�eme n�sleduj�c� dla�dici nav�t�vit?
        if (moveTo.Count != 0 && (((Point)moveTo[0]).idSpecalny == -1 || GameObject.Find("LevelController").GetComponent<LevelCotroller>().IDOkolnosti[((Point)moveTo[0]).idSpecalny] == true))
        {
            if(((Point)moveTo[0]).idSpecalny != -1){
                DragController.allowDrag = false;
            }
            GameObject dlazdice = ((Point)moveTo[0]).button;
            Vector3 nextNormalVector = dlazdice.transform.up.normalized;
            Vector3 posun = dlazdice.transform.up.normalized * uprava;
            // -------------------     Nastaven� c�lov�ho sm?ru a m�sta pohybu    -------------------
            Vector3 destination = dlazdice.transform.position + posun;
            Vector3 specialRot = Vector3.zero;
            bool teleportace = false;
            // Pokud je spoj speci�ln� a obsahuje teleportaci
            if(((Point)moveTo[0]).idSpecalny != -1 && dlazdice.GetComponent<Button>().Sc.teleporatce){
                teleportace = true;
                SpecialConnection Scon = dlazdice.GetComponent<Button>().Sc;
                Vector3 middlePosition = Scon.middlePoint.transform.position + posun;
                if(Scon.scScript.firstEndPoint == dlazdice && !Scon.scScript.isMiddlePointOverlappingFirtst && !teleportovano){
                    transform.position = middlePosition;
                    teleportovano = true;
                }
                if(Scon.scScript.secondEndPoint == dlazdice && Scon.scScript.isMiddlePointOverlappingFirtst && !teleportovano){
                    transform.position = middlePosition;
                    teleportovano = true;
                }
                if(!teleportovano){
                    destination = middlePosition;
                    nextNormalVector = Scon.scScript.middlePoint.transform.up.normalized;
                }
            }
            // Poud je spoj speci�ln� a obsahuje speci�ln� sm?r, nastav sm?r na n?j
            if (((Point)moveTo[0]).idSpecalny != -1 && dlazdice.GetComponent<Button>().direction != null)
            {
                specialRot = dlazdice.GetComponent<Button>().direction.transform.position + posun;
            }
            // Pokud je rychlost u speci�ln�ch spoj� upravena, nastav aktu�ln� rychlost na tuto speci�ln�
            if (((Point)moveTo[0]).idSpecalny != -1 && dlazdice.GetComponent<Button>().speed > 0)
            {

                speed = dlazdice.GetComponent<Button>().speed;
            }
            else
            {
                speed = speedBackup;
            }

            //  -------------------------   Jsme v c�li    ------------------------------------------------------

            // Kdy� doraz�me na po�adovan� m�sto, odstran�me jej ze seznamu a nastav�me jej jako st�vaj�c� pozici postavy
            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                if(teleportace && !teleportovano){
                    SpecialConnection Scon = dlazdice.GetComponent<Button>().Sc;
                    if(Scon.scScript.isMiddlePointOverlappingFirtst){
                        transform.position = Scon.scScript.firstEndPoint.transform.position + posun;
                    }else{
                        transform.position = Scon.scScript.secondEndPoint.transform.position + posun;
                    }
                }
                currentPlayerPoint = (Point)moveTo[0];
                moveTo.RemoveAt(0);
                teleportovano = false;
                DragController.allowDrag = true;
            }
            // -------------------------   Samotn� pohyb    ----------------------------------------------
            else
            {
                // Samotn� pohyb do c�lov�ho m�sta
                float midDistance = Vector3.Distance(currentPlayerPoint.button.transform.position+posun, destination)/1.5f;
                if((delka == moveTo.Count&&Vector3.Distance(currentPlayerPoint.button.transform.position+posun, transform.position)<midDistance)
                ||(moveTo.Count == 1)){
                    Vector3 slerpDestination = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
                    transform.position = Vector3.MoveTowards(transform.position, slerpDestination, speed * Time.deltaTime);
                }else{
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                }

                // --   Sm��ov�n� je na m�sto c�lov� dla�dice
                if (specialRot == Vector3.zero)
                {
                    // Prevence nulov�ho vektoru
                    if (destination - transform.position != Vector3.zero)
                    {
                    
                        Quaternion lookRotation = Quaternion.LookRotation((destination - transform.position).normalized, nextNormalVector);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                    }
                }
                //  --   Sm��ov�n� je speci�ln� upraveno
                else
                {
                    // Prevence nulov�ho vektoru
                    if (specialRot - transform.position != Vector3.zero)
                    {

                        Quaternion lookRotation = Quaternion.LookRotation((specialRot - transform.position).normalized, dlazdice.GetComponent<Button>().direction.transform.up.normalized);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                    }
                }
            }
        }
    }
}
