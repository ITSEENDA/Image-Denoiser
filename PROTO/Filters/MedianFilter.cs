using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class MedianFilter : FilterOptionBase
    {
        public override string FilterName => "Median";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            using Mat output = new Mat(input.Size(), input.Type());
            Cv2.MedianBlur(input, output, 9);
            var outputBitmap = BitmapConverter.ToBitmap(output);
            return outputBitmap;
        }
    }
}
