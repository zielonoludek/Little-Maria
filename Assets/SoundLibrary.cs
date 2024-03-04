using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary",menuName = "ScriptableObjects/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    [SerializeField] private AudioClip[] laughClips;
    [SerializeField] private AudioClip[] screamClips;
    [SerializeField] private AudioClip[] musicClips;

    public AudioClip[] GetLaughClips()
    {
        return laughClips;
    }
    public AudioClip RandomLaughClip() 
    {
        return laughClips[Random.Range(0, laughClips.Length)];
    }

    public AudioClip[] GetScreamClips()
    {
        return screamClips;
    }
    public AudioClip RandomScreamClip()
    {
        return screamClips[Random.Range(0, screamClips.Length)];
    }

    public AudioClip[] GetMusicClips()
    {
        return musicClips;
    }

    private void Test()
    {
        var smth = laughClips[1].length;
    }

}
