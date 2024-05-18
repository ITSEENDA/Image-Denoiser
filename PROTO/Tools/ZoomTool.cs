using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTO.Tools
{
    internal class ZoomTool
    {
        private TrackBar _zoomBar;
        public ZoomTool(TrackBar zoomBar)
        {
            _zoomBar = zoomBar;
        }
        internal void OnZoomBarScroll(object sender, EventArgs e)
        {
            //if (outputImageBox.Image == null)
            //{
            //    return;
            //}
            //if (zoomBar.Value > 0)
            //{
            //    outputImageBox.Image = ZoomImage(outputImageBox.Image, 
            //        inputImageBox.Image.Size,
            //        zoomBar.Value);
            //}
        }

        //private Image ZoomImage(Image image, Size originalSize, int zoomValue)
        //{
        //    var bitmap = new Bitmap(image, 
        //        originalSize.Width + (originalSize.Width * zoomValue / 100), 
        //        originalSize.Height + (originalSize.Height * zoomValue / 100));
        //    Graphics drawer = Graphics.FromImage(bitmap);
        //    drawer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //    return bitmap;
        //}
    }
}
