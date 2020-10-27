using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Triggered is false until true
    [SerializeField] bool triggered = false;
    private void Update()
    {
        if (triggered)
        {
            print("I am Triggered");
        }
        else if  (!triggered)
        {
            print("I am not triggered");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
