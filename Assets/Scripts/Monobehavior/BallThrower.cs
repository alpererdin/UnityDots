using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class BallThrower : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float MinThrowPeriod = 1.0f;
    public float MaxThrowPeriod = 3.0f; 
    
    public float MinThrowSpeed = 1.34f;
    public float MaxThrowSpeed = 1.67f;
    public int MaxBalls = 10000;

    [SerializeField]
    private int ballsThrown = 0;
    void Start()
    {
        StartCoroutine(ThrowBalls());
    }

    IEnumerator ThrowBalls()
    {
        while(ballsThrown < MaxBalls)
        {
            GameObject newBall = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
            newBall.transform.position = transform.position;
            newBall.GetComponent<Rigidbody>().AddForce(-transform.forward * Random.Range(MinThrowSpeed, MaxThrowSpeed));

            yield return new WaitForSeconds(Random.Range(MinThrowPeriod, MaxThrowPeriod));
            ballsThrown += 1;
        }
        Debug.Log(gameObject.name+" throwballs");
    }
}*/