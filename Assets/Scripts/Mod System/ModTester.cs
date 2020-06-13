﻿using UnityEngine;

public class ModTester : IModable
{
	private void Start()
	{
		foreach (var mod in _mods)
		{
			mod.ModLoad();
			mod.ModSetup(this);
			mod.ModEnable();
		}
	}
}
