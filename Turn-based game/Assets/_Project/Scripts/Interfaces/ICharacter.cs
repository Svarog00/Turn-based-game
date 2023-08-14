public interface ICharacter
{
    Turn Side { get; }
    bool IsActing { get; set; }
    float DistanceCanTravel { get; set; }
    int ActionsAvailable { get; set; }

    void ResetTurn();
}
