using UnityEngine;

public class TailSegment : MonoBehaviour
{
    public TailSegment NextTailSegment = null;

    public void DestroySegment()
    {
        if (NextTailSegment != null)
        {
            NextTailSegment.DestroySegment();
            NextTailSegment = null;
        }

        Destroy(gameObject);
    }
}