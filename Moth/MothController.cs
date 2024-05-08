

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine.UI;
using System.Collections;
using LottiePlugin.UI;

public class MothController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float verticalSpeed = 7f;
    [SerializeField] private float fallingSpeed = 4f;
    [SerializeField] private float boostedSpeed = 5f;


    private bool canMoveRight = true;
    private bool attractToLamp = false;
    private bool shielding = false;
    private Rigidbody2D rb;
    public HealthSys hp;
    public int nectarCount;
    public int nectarToIncreaseSpeed= 3;

    GameObject lampInSight = null;

    public GameObject nectar0;
    public GameObject nectar1;
    public GameObject nectar2;
    public GameObject nectar3;
    public GameObject nectar4;
    public GameObject nectarAnim1;
    public GameObject nectarAnim2;
    public GameObject nectarAnim3;
    public GameObject nectarAnim4;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp= GetComponent<HealthSys>();
    }

    private void Update()
    {
        NectarChecking();
        HealthChecking();
    }

    private void FixedUpdate()
    {
        Vector3 deltaMove = new Vector3();
        if (canMoveRight)
        {
            deltaMove.Set(horizontalSpeed, -fallingSpeed, 0);
        }
        if (attractToLamp && !shielding)
        {
            float xDistanceToLamp = (lampInSight.transform.position.x - rb.transform.position.x) / 5;
            float yDistanceToLamp = (lampInSight.transform.position.y - rb.transform.position.y) / 8;

            deltaMove.Set(xDistanceToLamp * horizontalSpeed, yDistanceToLamp * verticalSpeed, 0);
        }
        
        rb.MovePosition(rb.transform.position + deltaMove * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nectar"))
        {
            NectarCollect();
        }
        if (other.gameObject.CompareTag("Lamp") && !shielding)
        {
            attractToLamp = true;
            lampInSight = other.gameObject;

            canMoveRight = false;            
        }
        else if (other.gameObject.CompareTag("ShieldingObject"))
        {
            shielding = true;
            canMoveRight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lamp"))
        {
            attractToLamp = false;
            canMoveRight = true;
            lampInSight = null;
        }
        else if (other.gameObject.CompareTag("ShieldingObject")) 
        {
            shielding = false;
        }
    }
    
    private void NectarCollect()
    {
        nectarCount++;
        if(nectarCount>=nectarToIncreaseSpeed)
        {
            NectarBoost();
            nectarCount = 0;
        }
    }

    private void NectarChecking()
    {
        if(nectarCount==0)
        {
            Nectar0();
        }
        if (nectarCount == 1)
        {
            Nectar1();
        }
        if (nectarCount == 2)
        {
            Nectar2();
        }
        if (nectarCount == 3)
        {
            Nectar3();
        }
        if (nectarCount == 4)
        {
            Nectar4();
        }
    }

    private void NectarBoost()
    {
        if(nectarCount >= 3)
        {
            horizontalSpeed += boostedSpeed;
        }
    }

    public void Nectar0()
    {
        nectar0.SetActive(true);
        nectar1.SetActive(false);
        nectar2.SetActive(false);
        nectar3.SetActive(false);
        nectar4.SetActive(false);
    }

    public void Nectar1()
    {
        
        nectar0.SetActive(false);
        nectar1.SetActive(true);
        nectar2.SetActive(false);
        nectar3.SetActive(false);
        nectar4.SetActive(false);
    }

    public void Nectar2()
    {
        nectar0.SetActive(false);
        nectar1.SetActive(false);
        nectar2.SetActive(true);
        nectar3.SetActive(false);
        nectar4.SetActive(false);
    }

    public void Nectar3()
    {
        nectar0.SetActive(false);
        nectar1.SetActive(false);
        nectar2.SetActive(false);
        nectar3.SetActive(true);
        nectar4.SetActive(false);
    }
    public void Nectar4()
    {
        nectar0.SetActive(false);
        nectar1.SetActive(false);
        nectar2.SetActive(false);
        nectar3.SetActive(false);
        nectar4.SetActive(true);
    }


    public void HealthChecking()
    {
        if(hp.currentHealth== 3)
        {
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
        }
        if (hp.currentHealth == 2)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
        }
        if (hp.currentHealth ==1)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(true);
        }
        if (hp.currentHealth <= 0)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
        }
    }
}


