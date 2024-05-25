using UnityEngine;

public abstract class Shootable : MonoBehaviour
{
    private static readonly Color GIZMO_COLOR = new(1, 0, 0, 0.5f);

    public abstract void OnShot(RaycastHit hitInfo, Vector3 transformPosition);

    private void OnDrawGizmos()
    {
        Gizmos.color = GIZMO_COLOR;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}