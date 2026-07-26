using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
public class MovingObject : MonoBehaviour
{
    public List<Transform> nodes = new List<Transform>();
    public bool smoothInterpolation = false;
    public float waitBetweenNodes = 0;
    public bool useTime = true;
    public float time = 1f;
    public float moveSpeed;
    public bool moveOnTouch = false;


    private bool objectTouched = false;
    private bool shouldWait = false;
    private float elapsedTime;
    private int currentNode = 0;
    private int nextNode = 1;
    private void Start()
    {
        transform.position = nodes[0].transform.position;

        if (useTime)
        {
            //calculates the distance of all the nodes
            float sumOfDistancesBetweenNodes = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                int next = (i + 1) % nodes.Count;
                sumOfDistancesBetweenNodes += (nodes[next].transform.position - nodes[i].transform.position).magnitude;
            }
            moveSpeed = (sumOfDistancesBetweenNodes / time);
        } 
    }

    private void Update()
    {
        if (moveOnTouch && !objectTouched)
        {
            return;
        }

        elapsedTime += Time.deltaTime;


        if (shouldWait)
        {
            if (elapsedTime < waitBetweenNodes) { return; } else { elapsedTime = 0f; }
        }

        shouldWait = false;

        if (!smoothInterpolation)
        {
            transform.position = Vector2.MoveTowards(transform.position, nodes[nextNode].transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            float percentageComplete = elapsedTime / (time / nodes.Count);
            transform.position = Vector2.Lerp(nodes[currentNode].transform.position, nodes[nextNode].transform.position, Mathf.SmoothStep(0, 1, percentageComplete));
        }
        //(Vector2) before transform.position just forces it to be a vector2 value so the compiler doesnt scream at you
        if ((Vector2)transform.position == (Vector2)nodes[nextNode].transform.position)
        {
            currentNode = nextNode;
            //This is also the formula you use for circle queues, which I think is pretty neat!
            nextNode = (nextNode + 1) % nodes.Count;
            Debug.Log(elapsedTime);
            elapsedTime = 0;

            if (nextNode == 1)
            {
                objectTouched = false;
            }

            if (waitBetweenNodes > 0)
            {
                shouldWait = true;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DEATH SHALL COME TO US ALL
        objectTouched = true;
    }
}
