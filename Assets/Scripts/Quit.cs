using UnityEngine;

public class Quit : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
