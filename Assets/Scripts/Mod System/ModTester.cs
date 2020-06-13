public class ModTester : IModable
{
	private void Start()
	{
		foreach (var mod in _mods)
		{
			mod.ModLoad();
			mod.ModActivate();
			mod.ModAwake(this);
		}
	}
}
