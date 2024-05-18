using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class BilateralFilter : FilterOptionBase
    {
        public override string FilterName => "Bilateral";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            using Mat convertedColorInput = input.CvtColor(ColorConversionCodes.BGRA2BGR);
            using Mat output = new Mat(convertedColorInput.Size(), convertedColorInput.Type());
            Cv2.BilateralFilter(convertedColorInput, output, 15, 75, 75);
            var outputImage = BitmapConverter.ToBitmap(output);
            return outputImage;
        }
    }
}
