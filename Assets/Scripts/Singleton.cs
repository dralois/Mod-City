using UnityEngine;

public class Singleton<T> : MonoBehaviour
	where T : MonoBehaviour
{
	private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				var objs = FindObjectsOfType(typeof(T)) as T[];
				if (objs.Length > 0)
				{
					_instance = objs[0];
				}
				else if (objs.Length > 1)
				{
					Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
				}
				else if (_instance == null)
				{
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}
}

public abstract class SingletonPersistent<T> : MonoBehaviour
	where T : MonoBehaviour
{

	private static readonly object _lock = new object();

	public static T Instance { get; private set;  }

	private void Awake()
	{
		lock (_lock)
		{
			if (Instance == null)
			{
				Instance = this as T;
				DontDestroyOnLoad(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}


}
