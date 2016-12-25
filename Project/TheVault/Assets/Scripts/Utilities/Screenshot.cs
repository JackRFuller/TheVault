using System.Collections;
using System.IO;
using UnityEngine;

public class Screenshot : BaseMonoBehaviour
{
    private int screenShotCount = 0;

    private void Start()
    {
        screenShotCount = PlayerPrefs.GetInt("ScreenshotCount");
    }

    public override void UpdateNormal()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            StartCoroutine(ScreenshotEncode());  
    }

    private IEnumerator ScreenshotEncode()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height),0, 0);
        texture.Apply();

        yield return 0;

        byte[] bytes = texture.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/../screenshot-" + screenShotCount + ".png", bytes);
        screenShotCount++;

        PlayerPrefs.SetInt("ScreenshotCount", screenShotCount);
        DestroyObject(texture);

        Debug.Log("Screenshot Taken");

    }
}
