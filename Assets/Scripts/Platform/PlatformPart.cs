using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlatformPart : MonoBehaviour
{
    [SerializeField] protected Platform Platform;
    [SerializeField] protected int Durable = 1;

    private void Start()
    {
        Platform = GetComponentInParent<Platform>();
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThrowedObject throwedObject))
        {
            if (Platform.IsActivated)
            {
                throwedObject.Hit();

                Collision();
            }
        }
    }

    protected virtual void Collision()
    {
        Platform.Destroy();
    }
}
