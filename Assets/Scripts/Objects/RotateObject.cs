using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float speed = 50.0f;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
