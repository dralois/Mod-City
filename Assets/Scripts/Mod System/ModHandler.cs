using System.Collections.Generic;
using UnityEngine;

public class ModHandler : Singleton<ModHandler>
{

	private HashSet<IModObject> _loadedMods = new HashSet<IModObject>();
	private HashSet<IModObject> _activeMods = new HashSet<IModObject>();

	public bool TryLoadMod(IModObject toAdd)
	{
		if (_loadedMods.Add(toAdd))
		{
			toAdd.IsLoaded = true;
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool TryUnloadMod(IModObject toRemove)
	{
		if(_loadedMods.Remove(toRemove))
		{
			toRemove.IsLoaded = false;
			return true;
		}
		else
		{
			return false;
		}
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
		if (_activeMods.Remove(toDisable))
		{
			toDisable.IsActivated = false;
			return true;
		}
		else
		{
			return false;
		}
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
