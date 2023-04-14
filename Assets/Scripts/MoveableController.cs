using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableController : MonoBehaviour
{
    [Header("Dlaždice nacházející se na tomto objektu:")]
    public GameObject[] buttons;

    [Header("Typ pohyblivého komponentu")]
    public bool vertical = false;
    [Header("Hodnoty zadávat od nejmenší po nejvìtší")]
    public float[] pointsY;

    public bool horizontalX = false;
    [Header("Hodnoty zadávat od nejmenší po nejvìtší")]
    public float[] pointsX;

    public bool horizontalZ = false;
    [Header("Hodnoty zadávat od nejmenší po nejvìtší")]
    public float[] pointsZ;

    public bool horizontalRotation = false;
    public float speedOfRotation = 1;
    public float speedOfSlidingRotation = 1;
    [Space]
    public float speed = 10;

    [Header("Dále nemìnit")]
    public bool interacting = false;
    public bool sliding = false;
    public bool afterInteraction = false;
    public float goal;
    public float distance;
    public float distanceX;
    public float distanceY;

    float shift = 0;
    //bool isThere = false;
    Player plSc;
    SoundManager sm = null;
    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        plSc = GameObject.Find("Player").GetComponent<Player>();
        sm = SoundManager.getManager();
        audioS = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioS.spatialBlend = 1;
    }
    public void interactionStarted()
    {
        // Pokud je na tomto elementu postava, bude se pohybovat spoleènì s tímto elementem
        for(int i = 0; i < buttons.Length; i++)
        {
            if(Player.currentPlayerPoint == najdiPoint(buttons[i]))
            {
                //isThere = true;
                GameObject.Find("Player").transform.SetParent(transform);
                break;
            }
        }
    }

    public Point najdiPoint(GameObject obj)
    {
        Point point = null;
        for (int k = 0; k < Player.mapa.Count; k++)
        {
            if (((Point)Player.mapa[k]).button == obj)
            {
                point = (Point)Player.mapa[k];
                break;
            }
        }
        return point;
    }

    void endOfInteraction()
    {
        sliding = false;
        if(plSc.parent != null){
            GameObject.Find("Player").transform.SetParent(plSc.parent.transform);
        }
        
        //isThere = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Sound design
        if(sm != null && shift > sm.distBtSounds){
            audioS.PlayOneShot(sm.getRandomClickingSound(),sm.clickingVolume);
            shift = 0;
        }

        // Moving the component
        if (interacting)
        {
            if (vertical)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + (distanceY * speed), transform.position.z);
                
                if(newPosition.y > pointsY[0] && newPosition.y < pointsY[pointsY.Length-1])
                {
                    shift += Vector3.Distance(newPosition, transform.position);
                    transform.position = newPosition;
                }
            }
            if (horizontalX)
            {
                Vector3 newPosition = new Vector3(transform.position.x + (distanceX * speed), transform.position.y , transform.position.z);

                if (newPosition.x > pointsX[0] && newPosition.x < pointsX[pointsX.Length - 1])
                {
                    shift += Vector3.Distance(newPosition, transform.position);
                    transform.position = newPosition;
                }
            }
            if (horizontalZ)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-distanceX * speed));

                if (newPosition.z > pointsZ[0] && newPosition.z < pointsZ[pointsZ.Length - 1])
                {
                    shift += Vector3.Distance(newPosition, transform.position);
                    transform.position = newPosition;
                }
            }
            if (horizontalRotation)
            {
                Quaternion rot = transform.rotation*Quaternion.Euler(0, (-distanceX * speedOfRotation), 0);
                transform.rotation = rot;
                shift += Mathf.Abs(-distanceX * speedOfRotation)/60;
            }
        }
        if (afterInteraction)
        {
            afterInteraction = false;
            // Najdi, jaký je nejbližší záchytný bod
            if (vertical)
            {
                for(int i = 0; i < pointsY.Length-1; i++)
                {
                    // Najdi interval, kde jsi mezi body
                    if(transform.position.y >= pointsY[i] && transform.position.y <= pointsY[i + 1])
                    {
                        // Najdi ten nejbližší
                        if(transform.position.y - pointsY[i] <= pointsY[i+1] - transform.position.y)
                        {
                            // Je to ten pod námi
                            goal = pointsY[i];
                        }
                        else
                        {
                            goal = pointsY[i+1];
                        }
                    }
                }
            }
            if (horizontalX)
            {
                for (int i = 0; i < pointsX.Length - 1; i++)
                {
                    // Najdi interval, kde jsi mezi body
                    if (transform.position.x >= pointsX[i] && transform.position.x <= pointsX[i + 1])
                    {
                        // Najdi ten nejbližší
                        if (transform.position.x - pointsX[i] <= pointsX[i + 1] - transform.position.x)
                        {
                            // Je to ten pod námi
                            goal = pointsX[i];
                        }
                        else
                        {
                            goal = pointsX[i + 1];
                        }
                        break;
                    }
                }
            }
            if (horizontalZ)
            {
                for (int i = 0; i < pointsZ.Length - 1; i++)
                {
                    // Najdi interval, kde jsi mezi body
                    if (transform.position.z >= pointsZ[i] && transform.position.z <= pointsZ[i + 1])
                    {
                        // Najdi ten nejbližší
                        if (transform.position.z - pointsZ[i] <= pointsZ[i + 1] - transform.position.z)
                        {
                            // Je to ten pod námi
                            goal = pointsZ[i];
                        }
                        else
                        {
                            goal = pointsZ[i + 1];
                        }
                        break;
                    }
                }
            }
            if (horizontalRotation)
            {
                int predchozi = 0;
                for (int i = 90; i <= 360; i += 90)
                {
                    if (transform.rotation.eulerAngles.y >= predchozi && transform.rotation.eulerAngles.y <= i)
                    {
                        // Najdi ten nejbližší
                        if (transform.rotation.eulerAngles.y - predchozi <= i - transform.rotation.eulerAngles.y)
                        {
                            // Je to ten pod námi
                            goal = predchozi;
                        }
                        else
                        {
                            goal = i;
                        }
                        break;
                    }
                    predchozi = i;
                }
            }
            sliding = true;
        }
        if (sliding)
        {
            if (vertical)
            {
                if (Mathf.Abs(transform.position.y - goal) < 0.001)
                {
                    endOfInteraction();
                }
                else
                {
                    Vector3 velocity = Vector3.zero;
                    Vector3 newPos = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, goal, transform.position.z), ref velocity, 0.125f);
    	            shift += Vector3.Distance(newPos, transform.position);
                    transform.position = newPos;
                }
            }
            if (horizontalX)
            {
                if (Mathf.Abs(transform.position.x - goal) < 0.001)
                {
                    endOfInteraction();
                }
                else
                {
                    Vector3 velocity = Vector3.zero;
                    Vector3 newPos = Vector3.SmoothDamp(transform.position, new Vector3(goal, transform.position.y, transform.position.z), ref velocity, 0.125f);
                    shift += Vector3.Distance(newPos, transform.position);
                    transform.position = newPos;
                }
            }
            if (horizontalZ)
            {
                if (Mathf.Abs(transform.position.z - goal) < 0.001)
                {
                    endOfInteraction();
                }
                else
                {
                    Vector3 velocity = Vector3.zero;
                    Vector3 newPos = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, goal), ref velocity, 0.125f);
                    shift += Vector3.Distance(newPos, transform.position);
                    transform.position = newPos;
                }
            }
            if (horizontalRotation)
            {
                if (transform.rotation.eulerAngles.y == goal)
                {
                    endOfInteraction();
                }
                else
                {
                    Vector3 velocity = Vector3.zero;
                    Quaternion rot = Quaternion.Euler(transform.rotation.eulerAngles.x, goal, transform.rotation.z);
                    shift += Mathf.Abs(Quaternion.Slerp(transform.rotation, rot, speedOfSlidingRotation * Time.deltaTime).eulerAngles.y - transform.rotation.eulerAngles.y)/40;
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, speedOfSlidingRotation * Time.deltaTime);
                }
            }
        }
    }
    void clicked()
    {

    }
}
