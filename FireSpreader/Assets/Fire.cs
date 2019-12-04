using UnityEngine;

public class Fire : MonoBehaviour
{
    private float timer = 0.5f;

    public Spark sparkObject;

    private bool spreading = true;

    public GameObject flames;

    public void StartSpreading()
    {
        timer = 2f;
        spreading = true;
        flames.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (spreading)
        {
            if (timer < 0f)
            {
                Spark spark = Instantiate(sparkObject, transform.position + 0.5f * Vector3.up, Quaternion.identity, transform.parent) as Spark;
                spark.GetComponent<Rigidbody>().AddForce(300 * Vector3.up + Quaternion.Euler(Vector3.up * Random.Range(0, 359)) * (300 * Vector3.left));
                spreading = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }


        }
    }
}
