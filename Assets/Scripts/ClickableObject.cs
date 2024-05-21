using UnityEngine;

public abstract class ClickableObject : MonoBehaviour
{
    private static readonly Color GIZMO_COLOR = new(0, 1, 1, 0.5f);
    
    public abstract void OnClick();

    private void OnDrawGizmos()
    {
        Gizmos.color = GIZMO_COLOR;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}