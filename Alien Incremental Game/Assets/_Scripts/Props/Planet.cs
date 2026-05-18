using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Start()
    {
        InvokeRepeating(nameof(Rotate), 0f, 0.02f);
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0));
    }
}
