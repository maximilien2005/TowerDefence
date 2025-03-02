using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float vitesse = 10, sensibility = 10;
    float xRotation, YRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xRotation = transform.eulerAngles.x;
        YRotation = transform.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;
        transform.position += move * vitesse * Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            xRotation -= Input.GetAxis("Mouse Y") * sensibility ;
            YRotation += Input.GetAxis("Mouse X") * sensibility ;

            xRotation = Mathf.Clamp(xRotation, 0f, 90f); // Limite l'angle vertical


            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f); // Rotation de la caméra (tête)
        }

    }
}

