using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandObtainer : WebCamera
{
    private CascadeClassifier cClassifier;
    private Mat image;
    private Mat processImage = new Mat();
    // Start is called before the first frame update
    void Start()
    {
        cClassifier = new CascadeClassifier();
        var cascadeFilename = Application.dataPath + "/Resources/" + "fist.xml";
        cClassifier.Load(cascadeFilename);
    }

    // Update is called once per frame
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        Cv2.EqualizeHist(image, processImage);
        var hand = cClassifier.DetectMultiScale(image,1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());     
        if (hand.Length >= 1)
        {
            Debug.Log(hand[0].Location);
        }
        return true;
    }
}
