using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	private static Dictionary<string, int> ms_ResourceInstanceCounter = null;
	private static List<string> ms_ResourceLoadCounter = null;


	private static void IncreaseResourceInstance(string szKey)
	{
		if (null == ms_ResourceInstanceCounter)
		{
			ms_ResourceInstanceCounter = new Dictionary<string, int>();
		}

		if (true == ms_ResourceInstanceCounter.ContainsKey(szKey))
		{
			ms_ResourceInstanceCounter[szKey] += 1;
		}
		else
		{
			ms_ResourceInstanceCounter.Add(szKey, 1);
		}
	}


	private static void DecreaseInstanceResource(string szKey)
	{
		if (null == szKey || szKey.Length <= 0)
		{
			return;
		}

		if (null != ms_ResourceInstanceCounter && ms_ResourceInstanceCounter.ContainsKey(szKey))
		{
			ms_ResourceInstanceCounter[szKey]--;
			if (ms_ResourceInstanceCounter[szKey] <= 0)
			{
				ms_ResourceInstanceCounter.Remove(szKey);
			}
		}
	}

	private static void IncreaseResourceLoadCount(string szKey)
	{
		if (null == ms_ResourceLoadCounter)
		{
			ms_ResourceLoadCounter = new List<string>();
		}

		if (!ms_ResourceLoadCounter.Contains(szKey))
		{
			ms_ResourceLoadCounter.Add(szKey);
		}
	}

	/// <summary>
	/// 内存中加载一个资源，没有实例化GameObject
	/// </summary>
	public static UnityEngine.Object LoadResource(string resPath, System.Type systemTypeInstance = null)
	{
		UnityEngine.Object resObject = null;
		if (null == systemTypeInstance)
		{
			resObject = Resources.Load(resPath);
		}
		else
		{
			resObject = Resources.Load(resPath, systemTypeInstance);
		}

		if (null != resObject)
		{
			IncreaseResourceLoadCount(resPath);
		}

		return resObject;
	}

	/// <summary>
	/// 根据资源，实例化GameObject
	/// </summary>
	public static UnityEngine.Object InstantiateResource(string resPath, string szKey = "", System.Type systemTypeInstance = null)
	{
		UnityEngine.Object resObject = null;
		if (null == systemTypeInstance)
		{
			resObject = Resources.Load(resPath);
		}
		else
		{
			resObject = Resources.Load(resPath, systemTypeInstance);
		}

		if (null != resObject)
		{
			UnityEngine.Object modelObject = (GameObject)GameObject.Instantiate(resObject);
			if (null == modelObject)
			{
				return null;
			}
			else
			{
				IncreaseResourceInstance(szKey);

				//如果没有传入名字，则使用Object的默认名字
				if (szKey.Length > 0)
				{
					modelObject.name = szKey;
				}
			}

			return modelObject;
		}

		return null;
	}

	/// <summary>
	/// 销毁GameObject，计数-1，不会自动置空
	/// </summary>
	public static void DestroyResource(GameObject obj, bool bImmediate = false)
	{
		if (null != obj)
		{
			string szName = obj.name;
			if (false == bImmediate)
			{
				GameObject.Destroy(obj);
			}
			else
			{
				GameObject.DestroyImmediate(obj);
			}

			//减少记数
			DecreaseInstanceResource(szName);

		}
	}

	/// <summary>
	/// 销毁GameObject，计数-1，会自动置空
	/// </summary>
	public static void DestroyResource(ref GameObject obj, bool bImmediate = false)
	{
		if (null != obj)
		{
			string szName = obj.name;
			if (false == bImmediate)
			{
				GameObject.Destroy(obj);
			}
			else
			{
				GameObject.DestroyImmediate(obj);
			}
			obj = null;

			//减少记数
			DecreaseInstanceResource(szName);
		}
	}

}
