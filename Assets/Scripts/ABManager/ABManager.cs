using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABManager : Singleton<ABManager>
{
    private Dictionary<string, AssetBundle> abDic= new Dictionary<string, AssetBundle>();
    private AssetBundle mainAB=null;
    private AssetBundleManifest manifest=null; 

    //AB包存放路径
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        //加载主包 
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle ab = null;
        //加载依赖包 
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载目标包
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    /// <summary>
    /// 同步加载AB包资源，不指定类型
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    public object LoadRes(string abName, string resName)
    {
        LoadAB(abName);
        //加载资源
        return abDic[abName].LoadAsset(resName);
    }

    /// <summary>
    /// 同步加载AB包资源，指定类型
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public object LoadRes(string abName, string resName,System.Type type)
    {
        LoadAB(abName);
        return abDic[abName].LoadAsset(resName, type);
    }

    /// <summary>
    /// 同步加载AB包资源，泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        LoadAB(abName);
        return abDic[abName].LoadAsset<T>(resName);
    }


    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    public void LoadResAsync(string abName, string resName,UnityAction<Object> callback)
    {
       StartCoroutine(ILoadResAsync(abName, resName, callback));
    }

    private IEnumerator ILoadResAsync(string abName, string resName, UnityAction<Object> callback)
    {
        LoadAB(abName);
        AssetBundleRequest request=abDic[abName].LoadAssetAsync(resName);
        yield return request;
        callback(request.asset);
    }

    /// <summary>
    /// 异步加载资源，指定类型
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public void LoadResAsync(string abName, string resName, System.Type type,UnityAction<Object> callback)
    {
        StartCoroutine(ILoadResAsync(abName, resName,type ,callback));
    }

    private IEnumerator ILoadResAsync(string abName, string resName,System.Type type,UnityAction<Object> callback)
    {
        LoadAB(abName);
        AssetBundleRequest request = abDic[abName].LoadAssetAsync(resName,type);
        yield return request;
        callback(request.asset);
    }

    /// <summary>
    /// 异步加载资源，泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callback) where T:Object
    {
        StartCoroutine(ILoadResAsync<T>(abName, resName, callback));
    }

    private IEnumerator ILoadResAsync<T>(string abName, string resName, UnityAction<T> callback) where T:Object
    {
        LoadAB(abName);
        AssetBundleRequest request = abDic[abName].LoadAssetAsync<T>(resName);
        yield return request;
        callback(request.asset as T);
    }

    /// <summary>
    /// 单个包卸载
    /// </summary>
    /// <param name="abName"></param>
    public void Unload(string abName)
    {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    /// <summary>
    /// 所有包卸载
    /// </summary>
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest=null;
    }
}
