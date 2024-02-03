using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine;
using System.Collections.Generic;

public class PassHarnessToHumanBodyTracking : MonoBehaviour
{
    public HarnessCaryForwardData harnessData;
    public HumanBodyTracker hBT;
    List<GameObject> prefenSkeletonData;
    public int count;
    public ARController _aRController;
    public int reduceCount;
    [SerializeField] bool retake;

    private void Awake()
    {
        prefenSkeletonData = new List<GameObject>();
        hBT = FindObjectOfType<HumanBodyTracker>();
        _aRController = FindObjectOfType<ARController>();
        hBT.harnessIndex = harnessData.productItemScriptableIndex;
        foreach (var item in harnessData.collectionHarness)
        {
            prefenSkeletonData.Add(item.gameObject);
        }
        hBT.SkeletonPrefebCollection = prefenSkeletonData;
        //Passharness();
        retake = true;
    }

    
    public void ChangeHarness()
    {
        if (retake)
        {
            retake = false;
        count++;
        if (count > hBT.SkeletonPrefebCollection.Count - reduceCount )
        {
            count = 0;
        }

        hBT.harnessIndex = count;

        _aRController.ReStartArSession();
            Invoke(nameof(RetakeInvoke), 1);
        }
    }

    void RetakeInvoke()
    {
        retake = true;
    }
}
