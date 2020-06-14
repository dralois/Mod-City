using System;
using UnityEngine;

[System.Serializable]
public abstract class IModObject : ScriptableObject
{

	[SerializeField] private Sprite _icon;
	[SerializeField] private string _name;
	[SerializeField] private string _author;
	[SerializeField] private int _version;
	[SerializeField] private bool _requiresRestart;
	[SerializeField] private IModObject[] _dependencies = { };
	[SerializeField] private IModObject[] _incompatibles = { };

	[NonSerialized] private bool _isLoaded = false;
	[NonSerialized] private bool _isActivated = false;
	[NonSerialized] private IModable _modable = null;

	public Sprite Icon { get => _icon; }
	public string Name { get => _name; }
	public string Author { get => _author; }
	public int Version { get => _version; }
	public IModObject[] Dependencies { get => _dependencies; }
	public IModObject[] Incompatibles { get => _incompatibles; }

	public bool IsLoaded { get => _isLoaded; set => _isLoaded = value; }
	public bool IsActivated { get => _isActivated; set => _isActivated = value; }
	public IModable Modable { get => _modable; set => _modable = value; }

	protected abstract void AwakeInternal();
	protected abstract void OnEnableInternal();
	protected abstract void UpdateInternal();
	protected abstract void OnDisableInternal();
	protected abstract void DestroyInternal();

	public bool ModLoad()
	{
		return ModHandler.Instance.TryLoadMod(this);
	}

	public bool ModUnload()
	{
		return ModHandler.Instance.TryUnloadMod(this);
	}

	public bool ModActivate()
	{
		if (IsLoaded && !IsActivated && ModHandler.Instance.TryEnableMod(this))
		{
			return true;
		}
		else
		{
			Debug.Log($"Couldnt enable mod {this} for {Modable}", this);
			return false;
		}
	}

	public bool ModDeactivate()
	{
		if (IsLoaded && IsActivated && ModHandler.Instance.TryDisableMod(this))
		{
			Modable = null;
			return true;
		}
		else
		{
			Debug.Log($"Couldnt disable mod {this} for {Modable}", this);
			return false;
		}
	}

	public void ModAwake(IModable myModable)
	{
		if (IsLoaded && IsActivated)
		{
			Modable = myModable;
			AwakeInternal();
		}
	}

	public void ModOnEnable()
	{
		if (IsLoaded && IsActivated)
		{
			OnEnableInternal();
		}
	}

	public void ModUpdate()
	{
		if (IsLoaded && IsActivated)
		{
			UpdateInternal();
		}
	}

	public void ModOnDisable()
	{
		if (IsLoaded && IsActivated)
		{
			OnDisableInternal();
		}
	}

	public void ModDestroy()
	{
		if (IsLoaded && IsActivated)
		{
			DestroyInternal();
			Modable = null;
		}
	}

}
