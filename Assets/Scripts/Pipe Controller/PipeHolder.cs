using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHolder : MonoBehaviour
{
    public int speed;
<<<<<<< HEAD
=======
    void Start()
    {

    }
>>>>>>> 121b2f88aa521366e1cb0c08b92ef87aeaf7d134

    void Update()
    {
        if (BirdController.instance != null)
        {
            if (BirdController.instance.flag == 1)
            {
                Destroy(GetComponent<PipeHolder>());
            }
        }
        PipeMovement();
    }
    void PipeMovement()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        if (other.CompareTag("Destroy"))
=======
        if (other.tag == "Destroy")
>>>>>>> 121b2f88aa521366e1cb0c08b92ef87aeaf7d134
        {
            Destroy(gameObject);
        }
    }
}
