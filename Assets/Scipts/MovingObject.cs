using UnityEngine;
using System.Collections.Generic;
public class MovingObject : MonoBehaviour
{
    public List<Transform> nodes = new List<Transform>();
    public bool useTime = true;
    public float time = 1f;
    public float moveSpeed;

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
        transform.position = Vector2.MoveTowards(transform.position, nodes[nextNode].transform.position, moveSpeed*Time.deltaTime);
        //(Vector2) before transform.position just forces it to be a vector2 value so the compiler doesnt scream at you
        if ((Vector2)transform.position == (Vector2)nodes[nextNode].transform.position)
        {
            //This is also the formula you use for circle queues, which I think is pretty neat!
            nextNode = (nextNode + 1) % nodes.Count;
        }

    }
}
