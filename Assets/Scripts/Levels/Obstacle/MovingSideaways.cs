using UnityEngine;

public class MovingSideaways : MonoBehaviour
{
    [SerializeField] private Vector3[] wayPoints;
    [SerializeField] private float speed;
    private float WRadius = 1;
    private int currentIndex = 0;
    private float rotationSpeed;
    public Transform target;

    private void Update()
    {
        MoveSideaways();

        if (transform.position.x == wayPoints[0].x)
        {
            transform.Rotate(180,0,0);
        }
        else if(transform.position.x == wayPoints[1].x)
        {
            transform.Rotate(-180,0,0);
        }
        
    }

    private void MoveSideaways()
    { 
        if(Vector3.Distance(wayPoints[currentIndex],transform.position) < WRadius) 
        {
            currentIndex++;
            
            if(currentIndex >= wayPoints.Length)
            {
                currentIndex = 0;
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position,wayPoints[currentIndex], Time.deltaTime * speed);
    }
}
