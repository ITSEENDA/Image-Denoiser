using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class KuwaharaFilter : FilterOptionBase
    {
        public override string FilterName => "Kuwahara";

        internal override Image FitlerImage(Image inputImage)
        {
            Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            Mat output = input.Clone();

            var outputImage = BitmapConverter.ToBitmap(output);
            return outputImage;
        }
    }
}
