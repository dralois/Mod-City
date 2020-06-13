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

	public IModObject[] Dependencies { get => _dependencies; }

	public IModObject[] Incompatibles { get => _incompatibles; }

	protected abstract void EnableInternal();

	protected abstract void UpdateInternal();

	protected abstract void DisableInternal();

	public void ModEnable()
	{
		if (ModHandler.Instance.TryEnableMod(this))
		{
			Debug.Log($"Enabled mod {this}", this);
			EnableInternal();
		}
		else
		{
			Debug.Log($"Couldnt enable mod {this}", this);
		}
	}

	public void ModUpdate()
	{
		UpdateInternal();
	}

	public void ModDisable()
	{
		if (ModHandler.Instance.TryDisableMod(this))
		{
			Debug.Log($"Disabled mod {this}", this);
			DisableInternal();
		}
		else
		{
			Debug.Log($"Couldnt disable mod {this}", this);
		}
	}
}
