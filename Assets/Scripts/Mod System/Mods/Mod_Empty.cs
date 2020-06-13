using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Empty Mod")]
public class Mod_Empty : IModObject
{
	protected override void EnableInternal() { }

	protected override void UpdateInternal() { }

	protected override void DisableInternal() { }
}
