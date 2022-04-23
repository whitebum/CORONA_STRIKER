using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SoundManager : BaseManager<SoundManager>
{
    private enum SoundType
    {
        BGM,
        SFX,
        COUNT,
    }

    #region Sound Manager's Managed Data
    [field: Header("Sound Manager's Managed Data")]
    [field: SerializeField] private Dictionary<SoundType, AudioSource>  playerBank  { get; set; } = new Dictionary<SoundType, AudioSource>();

    [field: SerializeField] private Dictionary<string, AudioClip>       bgmBank     { get; set; } = new Dictionary<string, AudioClip>();
    [field: SerializeField] private Dictionary<string, AudioClip>       sfxBank     { get; set; } = new Dictionary<string, AudioClip>();
    #endregion

    #region Unity Messages
    private void Awake()
    {
        name = "Sound Manager";

        for (var type = SoundType.BGM; type < SoundType.COUNT; ++type)
        {
            var newPlayer = gameObject.AddComponent<AudioSource>();

            newPlayer.volume = PlayerPrefs.HasKey($"{nameof(type)} Volume") ? PlayerPrefs.GetFloat($"{nameof(type)} Volume") : 0.5f;
            newPlayer.playOnAwake = false;

            playerBank.Add(type, newPlayer);
        }

        foreach (var bgm in Resources.LoadAll<AudioClip>("Sounds/BGM"))
        {
            bgmBank.Add(bgm.name, bgm);
        }

        foreach (var sfx in Resources.LoadAll<AudioClip>("Sounds/SFX"))
        {
            sfxBank.Add(sfx.name, sfx);
        }
    }
    #endregion

    #region Sound Manager's Base Method
    public void PlayBGM()
    {
        if (playerBank[SoundType.BGM].clip)
        {
            playerBank[SoundType.BGM].Play();
        }
    }

    public void PlayBGM(string name, bool isLoop)
    {
        playerBank[SoundType.BGM].clip = bgmBank[name] ? bgmBank[name] : bgmBank["BGM_Unknown"];
        playerBank[SoundType.BGM].loop = isLoop;

        PlayBGM();
    }

    public void PauseBGM()
    {
        playerBank[SoundType.BGM].Pause();
    }

    public void StopBGM()
    {
        playerBank[SoundType.BGM].Stop();
    }

    public void PlaySFX(string name)
    {
        playerBank[SoundType.BGM].PlayOneShot(sfxBank[name] ? sfxBank[name] : sfxBank["SFX_Unknown"]);
    }
    #endregion
}
