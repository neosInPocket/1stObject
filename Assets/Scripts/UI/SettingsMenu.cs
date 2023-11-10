using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private Slider slider;
	
	private void Start()
	{
		slider.value = SaveLoad.soundVolume;
	}
	
	public void SaveVolumeValue()
	{
		SaveLoad.soundVolume = slider.value;
	}
}
