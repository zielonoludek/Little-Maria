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

    public AudioClip[] GetScreamClips()
    {
        return screamClips;
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
