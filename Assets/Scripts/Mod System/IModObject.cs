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

	protected abstract void EnableInternal();

	protected abstract void UpdateInternal();

	protected abstract void DisableInternal();

	public bool ModLoad()
	{
		return ModHandler.Instance.TryLoadMod(this);
	}

	public bool ModUnload()
	{
		return ModHandler.Instance.TryUnloadMod(this);
	}

	public bool ModEnable()
	{
		if (IsLoaded && !IsActivated && ModHandler.Instance.TryEnableMod(this))
		{
			EnableInternal();
			return true;
		}
		else
		{
			Debug.Log($"Couldnt enable mod {this} for {Modable}", this);
			return false;
		}
	}

	public void ModSetup(IModable myModable)
	{
		Modable = myModable;
		Debug.Log($"Setup mod {this} for {Modable as ModTester}", this);
	}

	public void ModUpdate()
	{
		if (IsLoaded && IsActivated)
		{
			UpdateInternal();
		}
	}

	public bool ModDisable()
	{
		if (IsLoaded && IsActivated && ModHandler.Instance.TryDisableMod(this))
		{
			DisableInternal();
			Modable = null;
			return true;
		}
		else
		{
			Debug.Log($"Couldnt disable mod {this} for {Modable}", this);
			return false;
		}
	}
}
