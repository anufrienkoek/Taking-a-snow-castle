using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject finishTrigger;

    private void Update()
    {
        if (transform.position.x >= finishTrigger.transform.position.x)
        {
            GameManager.Instance.Win();
        }
    }
}
