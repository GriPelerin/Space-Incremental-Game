using UnityEngine;

public class Planet : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(Rotate), 0f, 0.02f);
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(0.3f *Time.deltaTime, 0.3f * Time.deltaTime, 0));
    }
}
