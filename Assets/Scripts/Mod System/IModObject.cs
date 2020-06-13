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

	public bool IsActivated { get; set; } = false;

	public IModable Modable { get; private set; }

	public IModObject[] Dependencies { get => _dependencies; }

	public IModObject[] Incompatibles { get => _incompatibles; }

	protected abstract void EnableInternal();

	protected abstract void UpdateInternal();

	protected abstract void DisableInternal();

	public void ModEnable(IModable myModable)
	{
		if (ModHandler.Instance.TryEnableMod(this))
		{
			Modable = myModable;
			EnableInternal();
		}
		else
		{
			Debug.Log($"Couldnt enable mod {this} for {Modable}", this);
		}
	}

	public void ModUpdate()
	{
		if (IsActivated)
		{
			UpdateInternal();
		}
	}

	public void ModDisable()
	{
		if (ModHandler.Instance.TryDisableMod(this))
		{
			DisableInternal();
			Modable = null;
		}
		else
		{
			Debug.Log($"Couldnt disable mod {this} for {Modable}", this);
		}
	}
}
