using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Music Mod")]
public class Mod_Music : IModObject
{
	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
        Modable.GetComponent<AudioSource>().Play();
	}

	protected override void UpdateInternal()
	{
	}

	protected override void OnDisableInternal()
	{
	}

	protected override void DestroyInternal()
	{
	}
}
