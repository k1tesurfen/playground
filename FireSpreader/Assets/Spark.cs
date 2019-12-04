using UnityEngine;

public class Spark : MonoBehaviour
{
    public Fire fireObject;

    private void OnTriggerEnter(Collider collider)
    {
        Fire fire = Instantiate(fireObject, transform.parent) as Fire;
        fire.transform.position = transform.position;
        fire.StartSpreading();

        Destroy(gameObject);
    }
}
