using UnityEngine;

public class CamMove_MainScreen : MonoBehaviour
{

    public float movespeed { get; set; } = 10f;


    private void Awake()
    {
        transform.position = new Vector3(0, 0, -10);
    }
    // Update is called once per frame
    void Update()
    {
        float horizonal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizonal, vertical, 0);

        transform.position += direction * movespeed * Time.deltaTime;
        if (transform.position.x > 35)
        {
            transform.position = new Vector3(35f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -5)
        {
            transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 15)
        {
            transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
        }
        if (transform.position.y < -16)
        {
            transform.position = new Vector3(transform.position.x, -16f, transform.position.z);
        }
    }


}
