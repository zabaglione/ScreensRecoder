using UnityEngine;
using System.Collections;

public class ScreensRecoder : MonoBehaviour
{
	public KeyCode[] screenCaptureKeys;
	public KeyCode[] keyModifiers;

	public int minimumWidth = 2048;
    public int minimumHeight = 2048;
	public string directory = "D:\\captures\\";
    public string baseFilename;
    public int framerate = 60;
    public bool isRecoding = false;
    public int endframeno = 0;

    private int frameno = -1;

	void Reset ()
	{
		screenCaptureKeys = new KeyCode[]{ KeyCode.R };
		keyModifiers = new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift };
    
        baseFilename = System.DateTime.Now.ToString("yyyyMMdd");
    }

    void Start()
    {
        Time.captureFramerate = framerate;
    }

	void Update ()
	{
        checkRecodingKey();

        if (isRecoding == true)
        {
            TakeScreenShot();
        }
	}

    bool checkRecodingKey()
    {
        bool isModifierPressed = false;
        bool ret = false;
        if (keyModifiers.Length > 0)
        {
            foreach (KeyCode keyCode in keyModifiers)
            {
                if (Input.GetKey(keyCode))
                {
                    isModifierPressed = true;
                    break;
                }
            }
        }

        if (isModifierPressed)
        {
            foreach (KeyCode keyCode in screenCaptureKeys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    isRecoding = !isRecoding;
                }
            }
        }
        return ret;
    }

	public void TakeScreenShot ()
	{
		float rw = (float)minimumWidth / Screen.width;
		float rh = (float)minimumHeight / Screen.height;
		int scale = (int)Mathf.Ceil(Mathf.Max(rw, rh));

        frameno++;
        string path = directory + baseFilename + "_" + frameno.ToString() + ".png";

		Application.CaptureScreenshot(path, scale);
		Debug.Log(string.Format("screen shot : path = {0}, scale = {1} (screen = {2}, {3})",
			path, scale, Screen.width, Screen.height), this);

        if (endframeno > 0 && frameno >= endframeno)
        {
            isRecoding = false;
        }
	}
}
