using UnityEngine;

public abstract class IModable : MonoBehaviour
{

	[SerializeField] protected IModObject[] _mods = { };

	private void Awake()
	{
		foreach (var mod in _mods)
		{
			ModHandler.Instance.TryLoadMod(mod);
			Debug.Log($"Loaded mods for {this}", this);
		}
	}

	private void OnDestroy()
	{
		foreach (var mod in _mods)
		{
			ModHandler.Instance.TryUnloadMod(mod);
			Debug.Log($"Unloaded mods for {this}", this);
		}
	}

}
