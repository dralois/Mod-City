using UnityEngine;

public abstract class IModable : MonoBehaviour
{

	[SerializeField] protected IModObject[] _mods = { };

	protected virtual void Awake()
	{
		foreach (var mod in _mods)
		{
			mod.ModAwake(this);
		}
	}

	protected virtual void OnEnable()
	{
		foreach (var mod in _mods)
		{
			mod.ModOnEnable();
		}
	}

	protected virtual void Update()
	{
		foreach (var mod in _mods)
		{
			mod.ModUpdate();
		}
	}

	protected virtual void OnDisable()
	{
		foreach (var mod in _mods)
		{
			mod.ModOnDisable();
		}
	}

	protected virtual void OnDestroy()
	{
		foreach (var mod in _mods)
		{
			mod.ModDestroy();
		}
	}

}
