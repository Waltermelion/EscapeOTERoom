using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFinder : WebCamera
{
    [SerializeField] private FlipMode imageFlip;
    [SerializeField] private float threshold = 96.4f;
    [SerializeField] private bool showProcessingImage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float MinArea = 5000f;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierachy;
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        Cv2.Flip(image, image, imageFlip);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        Cv2.Threshold(processImage, processImage, threshold, 255, ThresholdTypes.BinaryInv);
        Cv2.FindContours(processImage, out contours,out hierachy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
        
        foreach (Point[] contour in contours)
        {
            Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            var area = Cv2.ContourArea(contour);

            if (area > MinArea)
            {
                drawContour(processImage, new Scalar(127, 127, 127), 2, points);
                // Calcular o centro
                Point center = new Point(0, 0);

                // Iteramos por todos os pontos
                foreach (var point in points)
                {
                    // Somamos todos os pontos ao centro
                    center.X += point.X;
                    center.Y += point.Y;
                }

                // Dividimos pelo numero de pontos para obter o centro
                center.X /= points.Length;
                center.Y /= points.Length;
                Debug.Log(center);
                Cv2.Line(processImage, center, center, new Scalar(127, 127, 127), 3);
            }            
        }

        if (output == null)        
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);        
        else        
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);        

        return true;
    }

    private void drawContour(Mat Image, Scalar Color, int Thickness, Point[] Points)
    {
        for(int i = 1;i< Points.Length; i++)
        {
            Cv2.Line(Image, Points[i - 1], Points[i], Color, Thickness);
        }
        Cv2.Line(Image, Points[Points.Length - 1], Points[0], Color, Thickness);
    }
}