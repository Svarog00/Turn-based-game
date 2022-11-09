using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public Turn Side 
    { 
        get => _side;
    }

    [SerializeField] private Turn _side;
}
