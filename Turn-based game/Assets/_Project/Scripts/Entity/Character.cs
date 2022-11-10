using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public Turn Side 
    { 
        get => _side;
    }
    public bool IsActing { get; set; }

    [SerializeField] private Turn _side;
}
