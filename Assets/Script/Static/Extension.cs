using System.Collections.Generic;
using UnityEngine;

public static class Extension
{

    public static Vector2 viewDim = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        

    public static void ToggleCanvasGroup(this CanvasGroup canvasGroup, bool enable = false)
    {
        canvasGroup.alpha = enable ? 1 : 0;
        canvasGroup.interactable = enable;
        canvasGroup.blocksRaycasts = enable;
    }

    public static Vector2 ClampYPositionOnScreen(Vector2 actualPosition)
    {
        actualPosition.y = Mathf.Clamp(actualPosition.y, -viewDim.y, viewDim.y);
        actualPosition.x = Mathf.Clamp(actualPosition.x, -viewDim.x, viewDim.x+1);

        return actualPosition;
    }

    public static int ParseFast(this string s)
    {
        int r = 0;
        for (var i = 0; i < s.Length; i++)
        {
            char letter = s[i];
            r = 10 * r;
            r = r + (int)char.GetNumericValue(letter);
        }
        return r;
    }

}
