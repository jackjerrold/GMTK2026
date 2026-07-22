using UnityEngine;
using System.Collections.Generic;
public class MovingObject : MonoBehaviour
{
    public Vector2 startPosition = new Vector2(0, 0);
    public Vector2 endPosition = new Vector2(0, 0);
    public float time = 1f;

    private float moveSpeed;
    private Vector2 nextPosition;
    private void Start()
    {
        transform.position = startPosition;
        nextPosition = startPosition;
        //Done this cause time feels easier to use than "movespeed"
        moveSpeed = (endPosition - startPosition).magnitude / time;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, moveSpeed*Time.deltaTime);
        //(Vector2) before transform.position just forces it to be a vector2 value so the compiler doesnt scream at you
        if ((Vector2)transform.position == nextPosition)
        {
            //weird operator bullshit i saw from a tutorial
            //if the comparison is true, nextPosition will be startPosition
            //if the comparison is false, it'll be endPosition
            nextPosition = (nextPosition == endPosition) ? startPosition : endPosition;
        }

    }
}
