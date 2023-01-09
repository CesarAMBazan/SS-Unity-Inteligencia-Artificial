using UnityEngine;

public class BoxStats : MonoBehaviour
{
    [SerializeField] private int boxNumber;

    public int getBoxNumber
    {
        get => boxNumber;
        set => boxNumber = value;
    }
}
