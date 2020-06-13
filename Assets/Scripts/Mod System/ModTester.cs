using UnityEngine;

public class ModTester : IModable
{
	private void Start()
	{
		foreach (var mod in _mods)
		{
			mod.ModEnable(this);
		}
	}
}
