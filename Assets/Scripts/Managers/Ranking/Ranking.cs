using UnityEngine;

public struct Ranking
{
    #region Player's Ranking Info
    [field: Header("Player's Ranking Info")]
    [field: SerializeField] public string playerName { get; private set; }
    [field: SerializeField] public string playerScore { get; private set; }
    #endregion

    #region Constructors
    public Ranking(string playerName, uint playerScore)
    {
        (this.playerName, this.playerScore) = (playerName, $"{playerScore:000000}");
    }

    public Ranking(string playerName, string playerScore)
    {
        (this.playerName, this.playerScore) = (playerName, playerScore);
    }
    #endregion
}
