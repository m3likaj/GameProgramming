using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 15;
    public int turnSpeed = 5;
    public float horizontalInput;
    public float verticalInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move the vehicle forward
        horizontalInput = Input.GetAxis("Horizontal"); //x ekseni
        verticalInput = Input.GetAxis("Vertical");// y ekseni
        transform.Translate(Vector3.forward* Time.deltaTime * speed * verticalInput); // ileri ve geri hareket
       transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput); // sag ve sol hareket
       //transform.Rotate(Vector3.up * Time.deltaTime* turnSpeed * horizontalInput);  //bunu dönmek için kullanırız

    }
}
