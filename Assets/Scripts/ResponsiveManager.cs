using UnityEngine;
using UnityEngine.UI;

public class ResponsiveManager : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    private const float TARGET_ASPECT = 16f / 9f;
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        SetScreenOrientation();
        AdjustCanvasScaling();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            AdjustCanvasScaling();
        }
    }

    private void SetScreenOrientation()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    private void AdjustCanvasScaling()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        
        if (currentAspect > TARGET_ASPECT)
        {
            canvasScaler.matchWidthOrHeight = 1f; // Match height
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f; // Match width
        }
        
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }
}
