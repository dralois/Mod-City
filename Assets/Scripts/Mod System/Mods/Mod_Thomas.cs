using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Thomas Mod")]
public class Mod_Thomas : IModObject
{
    public GameObject thomas;

	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
        Instantiate(thomas);
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
