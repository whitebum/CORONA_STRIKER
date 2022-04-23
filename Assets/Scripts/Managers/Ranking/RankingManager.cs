using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class RankingManager : BaseManager<RankingManager>
{
    #region Ranking Manager's Managed Data
    [field: Header("Ranking Manager's Managed Data")]
    [field: SerializeField] public List<Ranking> rankingBank { get; private set; } = new List<Ranking>();
    #endregion

    #region Unity Messages
    private void Awake()
    {
        name = "Ranking Manager";

        var filePath = Application.streamingAssetsPath + "/Ranking/Data.txt";

        try
        {
            var rankingData = File.Exists(filePath) ? File.ReadAllLines(filePath) : throw new Exception();

            foreach (var ranking in rankingData)
            {
                var playerData = ranking.Split(' ');

                rankingBank.Add(new Ranking(playerData[0], playerData[1]));
            }

            rankingBank.Sort((r1, r2) => uint.Parse(r2.playerScore).CompareTo(uint.Parse(r1.playerScore)));
        }
        catch (Exception)
        {
            rankingBank.Add(new Ranking("AAA", "005000"));
            rankingBank.Add(new Ranking("BBB", "004000"));
            rankingBank.Add(new Ranking("CCC", "003000"));
            rankingBank.Add(new Ranking("DDD", "002000"));
            rankingBank.Add(new Ranking("EEE", "001000"));
        }
    }

    private void OnApplicationQuit()
    {
        var rankingFile = File.CreateText(Application.streamingAssetsPath + "/Ranking/Data.txt");

        byte index = 0;
        foreach (var ranking in rankingBank)
        {
            rankingFile.Write(index < rankingBank.Count - 1 ? $"{ranking.playerName} {ranking.playerScore}\n" :
                                                              $"{ranking.playerName} {ranking.playerScore}");

            ++index;
        }

        rankingFile.Close();
    }
    #endregion

    #region
    public void RegistNewRanking(Ranking newRanking)
    {
        rankingBank.Add(newRanking);
        rankingBank.Sort((r1, r2) => uint.Parse(r2.playerScore).CompareTo(uint.Parse(r1.playerScore)));
        rankingBank.Remove(rankingBank[rankingBank.Count - 1]);
    }
    #endregion
}
