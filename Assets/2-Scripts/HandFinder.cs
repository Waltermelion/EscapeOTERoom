using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;
using UnityEngine.Events;

public class HandFinder : WebCamera
{
    [SerializeField] private FlipMode imageFlip;
    [SerializeField] private bool showProcessingImage = true;

    private CascadeClassifier fistClassifier, leftClassifier, rightClassifier, lPalmClassifier, rPalmClassifier;
    private Mat image, processImage;
    private OpenCvSharp.Rect myFist, myLeft, myRight, myLPalm, myRPalm;

    // Events
    public UnityEvent<OpenCvSharp.Rect> fistDetected, leftDetected, rightDetected, lPalmDetected, rPalmDetected;

    private void Start()
    {
        // Initializations
        fistClassifier = new CascadeClassifier();
        leftClassifier = new CascadeClassifier();
        rightClassifier = new CascadeClassifier();
        lPalmClassifier = new CascadeClassifier();
        rPalmClassifier = new CascadeClassifier();
        processImage = new Mat();

        // Paths for the XML files
        var fistFile = Application.dataPath + "/Resources/" + "fist.xml";
        var leftFile = Application.dataPath + "/Resources/" + "left.xml";
        var rightFile = Application.dataPath + "/Resources/" + "right.xml";
        var leftPalmFile = Application.dataPath + "/Resources/" + "lpalm.xml";
        var rightPalmFile = Application.dataPath + "/Resources/" + "rpalm.xml";

        // Loading
        fistClassifier.Load(fistFile);
        leftClassifier.Load(leftFile);
        rightClassifier.Load(rightFile);
        lPalmClassifier.Load(leftPalmFile);
        rPalmClassifier.Load(rightPalmFile);

    }
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);        

        // Image Filtering 
        Cv2.Flip(image, image, imageFlip);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        //Size dsize = new Size(image.Width / 2, image.Height / 2);
        //Cv2.Resize(image, processImage, dsize);

        //  Cascade Detection
        var fist = fistClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var left = leftClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var right = rightClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var leftPalm = lPalmClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var rightPalm = rPalmClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());

        // Fire Events
        if (fist.Length >= 1)
        {
            myFist = fist[0];
            if (myFist != null)
            {
                // Fire Event
                fistDetected?.Invoke(myFist);
                processImage.Rectangle(myFist, new Scalar(250, 0, 0), 2);
            }
        }else if (left.Length >= 1)
        {
            myLeft = left[0];
            if (myLeft != null)
            {
                // Fire Event
                leftDetected?.Invoke(myLeft);
                processImage.Rectangle(myLeft, new Scalar(250, 0, 0), 2);
            }
        }else if (right.Length >= 1)
        {
            myRight = right[0];
            if (myRight != null)
            {
                // Fire Event
                rightDetected?.Invoke(myRight);
                processImage.Rectangle(myRight, new Scalar(250, 0, 0), 2);
            }
        }else if (leftPalm.Length >= 1)
        {
            myLPalm = leftPalm[0];
            if (myLPalm != null)
            {
                // Fire Event
                lPalmDetected?.Invoke(myLPalm);
                processImage.Rectangle(myLPalm, new Scalar(250, 0, 0), 2);
            }
        }else if (rightPalm.Length >= 1)
        {
            myRPalm = rightPalm[0];
            if (myRPalm != null)
            {
                // Fire Event
                rPalmDetected?.Invoke(myRPalm);
                processImage.Rectangle(myRPalm, new Scalar(250, 0, 0), 2);
            }
        }

        if (output == null)        
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);        
        else        
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);        

        return true;
    }
}