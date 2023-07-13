using UnityEngine;

public class LineControl : MonoBehaviour
{
    public void SetLinePositions(Vector3 pos1, Vector3 pos2)
    {
        pos1.z = -0.1f;
        pos1.z = -0.1f;

        GetComponent<LineRenderer>().SetPosition(0, pos1);
        GetComponent<LineRenderer>().SetPosition(1, pos2);
    }
}
