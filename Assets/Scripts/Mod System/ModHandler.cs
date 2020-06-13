using System.Collections.Generic;
using UnityEngine;

public class ModHandler : Singleton<ModHandler>
{

	private HashSet<IModObject> _loadedMods = new HashSet<IModObject>();
	private HashSet<IModObject> _activeMods = new HashSet<IModObject>();

	public bool TryLoadMod(IModObject toAdd)
	{
		return _loadedMods.Add(toAdd);
	}

	public bool TryUnloadMod(IModObject toRemove)
	{
		return _loadedMods.Remove(toRemove);
	}

	public bool TryEnableMod(IModObject toActivate)
	{
		if (_loadedMods.Contains(toActivate) &&
			CheckAllModsActive(toActivate.Dependencies) &&
			!CheckSomeModsActive(toActivate.Incompatibles))
		{
			toActivate.IsActivated = true;
			_activeMods.Add(toActivate);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool TryDisableMod(IModObject toDisable)
	{
		toDisable.IsActivated = false;
		return _activeMods.Remove(toDisable);
	}

	public bool CheckAllModsActive(IModObject[] toCheck)
	{
		return _activeMods.IsSupersetOf(toCheck);
	}

	public bool CheckSomeModsActive(IModObject[] toCheck)
	{
		return _activeMods.Overlaps(toCheck);
	}

}
