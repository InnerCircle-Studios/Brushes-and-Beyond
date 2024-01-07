using System;
using System.Collections.Generic;

using UnityEngine;

public static class InteractionEvents {
    //See quest events
    public static event Action<int> OnPaintBucketActivated;
    public static void ActivatePaintBucket(int amount) => OnPaintBucketActivated?.Invoke(amount);



    public static event Action<string> OnShowObject;
    public static void ShowObject(string name) => OnShowObject?.Invoke(name);

    public static event Action<string> OnHideObject;
    public static void HideObject(string name) => OnHideObject?.Invoke(name);

}