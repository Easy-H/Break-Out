using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] Vector2 cameraSize = new Vector2(13f, 28f);
    [SerializeField] Vector2 letterBox = Vector2.zero;

    void Start ()
    {
        float targetWidthAspect = cameraSize.x + letterBox.x;
        float targetHeightAspect = cameraSize.y + letterBox.y;

        Camera camera = gameObject.GetComponent<Camera>();

        camera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = 0f;
        float widthadd = 0f;

        if (heightRatio > widthRatio)
            heightadd = ((widthRatio / (heightRatio / 100) - 100)) / 200;
        else
            widthadd = ((heightRatio / (widthRatio / 100) - 100)) / 200;

        camera.rect = new Rect(
            camera.rect.x + Mathf.Abs(widthadd),
            camera.rect.x + Mathf.Abs(heightadd),
            camera.rect.width + (widthadd * 2),
            camera.rect.height + (heightadd * 2));

    }
}
