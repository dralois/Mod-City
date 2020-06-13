using UnityEngine;

public abstract class IModable : MonoBehaviour
{

	[SerializeField] protected IModObject[] _mods = { };

	private void Awake()
	{
		foreach (var mod in _mods)
		{
			mod.ModSetup(this);
		}
	}

	private void OnDestroy()
	{
		foreach (var mod in _mods)
		{
			mod.ModDisable();
		}
	}

}
