using UnityEngine;

public abstract class IModable : MonoBehaviour
{

	[SerializeField] protected IModObject[] _mods = { };

	protected abstract void AwakeInternal();
	protected abstract void OnEnableInternal();
	protected abstract void UpdateInternal();
	protected abstract void OnDisableInternal();
	protected abstract void OnDestroyInternal();

	private void Awake()
	{
		AwakeInternal();
		foreach (var mod in _mods)
		{
			Debug.Log(this);
			mod.ModAwake(this);
		}
	}

	private void OnEnable()
	{
		OnEnableInternal();
		foreach (var mod in _mods)
		{
			mod.ModOnEnable();
		}
	}

	private void Update()
	{
		UpdateInternal();
		foreach (var mod in _mods)
		{
			mod.ModUpdate();
		}
	}

	private void OnDisable()
	{
		OnDisableInternal();
		foreach (var mod in _mods)
		{
			mod.ModOnDisable();
		}
	}

	private void OnDestroy()
	{
		OnDestroyInternal();
		foreach (var mod in _mods)
		{
			mod.ModDestroy();
		}
	}

}
