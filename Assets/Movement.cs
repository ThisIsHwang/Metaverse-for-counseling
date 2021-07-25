using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;


   private void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, y, 0);
        //transform.position = transform.position + new Vector3(1, 0, 0) * 1;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
