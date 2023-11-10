using UnityEngine;

public class Sounds : MonoBehaviour
{
	[SerializeField] AudioSource soundSource;
	
	private void Start()
	{
		soundSource.volume = SaveLoad.soundVolume;
	}
	
	public void SetSoundVolume(float value)
	{
		soundSource.volume = value;
	}
}
