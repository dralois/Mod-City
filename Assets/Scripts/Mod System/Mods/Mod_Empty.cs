using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Empty Mod")]
public class Mod_Empty : IModObject
{
	protected override void EnableInternal()
	{
		Debug.Log($"Enabled mod {this}", this);
	}

	protected override void UpdateInternal()
	{
		Debug.Log($"Updating mod {this} for {Modable as ModTester}", this);
	}

	protected override void DisableInternal()
	{
		Debug.Log($"Disabled mod {this}", this);
	}
}
