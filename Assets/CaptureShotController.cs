using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CaptureShotController : MonoBehaviour {

    [SerializeField]
    Rect avatarRect = new Rect(0,0,100,100);

    [SerializeField]
    Rect avatarBustRect = new Rect(0, 0, 100, 100);


    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Capture);
    }

    /// <summary>
    /// キャプチャー処理
    /// </summary>
    /// <param name="filePath">ファイルのパスを指定</param>
    void Capture()
    {
        StartCoroutine(WaitSave("Avatar" + numIndex++));
    }

    int numIndex = 1;

    byte[] TextureEncodeToBytes(Rect rect)
    {
        var texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, false);

        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        return texture.EncodeToJPG();
    }

    IEnumerator WaitSave(string path)
    {
        yield return new WaitForEndOfFrame();

        File.WriteAllBytes(path + ".jpg", TextureEncodeToBytes(avatarRect));
        File.WriteAllBytes(path + "_Bust.jpg", TextureEncodeToBytes(avatarBustRect));
    }
}
