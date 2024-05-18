using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PROTO.Tools
{
    internal class CropTool
    {
        private enum CropState
        {
            None,
            Selecting,
            Moving
        }

        private const int marginValue = 7;
        private PictureBox _imageBox;

        private CropState _currentState;
        private Point _startPoint, _endPoint, _movePoint;
        private Region _selectionRegion;
        private GraphicsPath _selectionPath;
        private Rectangle _selectionBounds;


        private Rectangle _currentImageBounds;
        private Color _cropColor;
        private float _scaleX, _scaleY;
        private Label _currentSizeLabel;
        private float _dashOffset;
        private Timer _timer;

        public bool IsSelecting => _selectionBounds != null && !_selectionBounds.IsEmpty 
            && _selectionBounds.Width > 0 && _selectionBounds.Height > 0;
        public Image CurrentImage => _imageBox.Image;
        private (float, float) DPI
        {
            get { 
                using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    return (graphics.DpiX, graphics.DpiY);
                }
            }
        }
        public double Ratio
        {
            get
            {
                return Math.Max(
                    CurrentImage.Width / (Math.Min(Math.Round(CurrentImage.Width * _scaleX), _imageBox.Width - marginValue * 2)),
                    CurrentImage.Height / (Math.Min(Math.Round(CurrentImage.Height * _scaleY), _imageBox.Height - marginValue * 2))
                );
            }
        }

        public CropTool(Color cropColor, PictureBox imageBox, Timer timer, Label sizeLabel = null)
        {
            _cropColor = cropColor;
            _imageBox = imageBox;
            _timer = timer;
            _currentSizeLabel = sizeLabel;

            _selectionRegion = new Region();
            _selectionRegion.MakeEmpty();
            _selectionPath = new GraphicsPath();
            _selectionPath.Reset();

            _imageBox.SizeChanged += OnImageBoxResize;
            _imageBox.Paint += OnImageBoxPaint;
            _timer.Tick += OnMarchingTick;

            _currentState = CropState.None;
        }
        public void CalculateImageScale()
        {
            (var dbiX, var dbiY) = DPI;
            _scaleX = dbiX / CurrentImage.HorizontalResolution;
            _scaleY = dbiY / CurrentImage.VerticalResolution;
        }
        public Rectangle GetAbsoluteSelectedBox()
        {
            var ratio = Ratio;
            return new Rectangle(
                (int)Math.Round((_selectionBounds.X - marginValue) * ratio),
                (int)Math.Round((_selectionBounds.Y - marginValue) * ratio),
                (int)Math.Round(_selectionBounds.Width * ratio),
                (int)Math.Round(_selectionBounds.Height * ratio)
            );
        }
        public Image GetImageWithinBounds()
        {
            var source = CurrentImage;
            var bounds = GetAbsoluteSelectedBox();

            Bitmap result = new Bitmap(bounds.Width, bounds.Height);
            result.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (var drawer = Graphics.FromImage(result))
            {
                drawer.InterpolationMode = InterpolationMode.HighQualityBicubic;
                drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
                drawer.CompositingQuality = CompositingQuality.HighQuality;
                drawer.DrawImage(source, 0, 0, bounds, GraphicsUnit.Pixel);
            }
            return result;
        }

        public void UpdateFilteredImageFromSelection(Bitmap filteredImage, Rectangle selectedBounds)
        {
            var oldImage = CurrentImage as Bitmap;
            var position = selectedBounds.Location;
            var width = Math.Min(filteredImage.Width, oldImage.Width);
            var height = Math.Min(filteredImage.Height, oldImage.Height);
            for (int i = position.X; i < width; i++)
            {
                for (int j = position.Y; j < height; j++)
                {
                    var pixel = filteredImage.GetPixel(i - position.X, j - position.Y);
                    oldImage.SetPixel(i, j, pixel);
                }
            }
            _imageBox.Image = oldImage;
        }
        private void PaintImage(Graphics drawer)
        {
            var ratio = Ratio;
            _currentImageBounds = new Rectangle(
                marginValue, marginValue,
                (int)Math.Round(CurrentImage.Width / ratio),
                (int)Math.Round(CurrentImage.Height / ratio));

            drawer.DrawRectangle(Pens.Gray, _currentImageBounds);
            drawer.DrawImage(CurrentImage, _currentImageBounds);
        }
        private void SetSelection(Rectangle bounds)
        {
            _selectionRegion.MakeEmpty();
            _selectionPath.Reset();

            if (!bounds.IsEmpty)
            {
                _selectionPath.AddRectangle(bounds);
                _selectionRegion.Union(_selectionPath);
            }
        }
        public void PaintSelection(Graphics g)
        {
            using (var fill = new SolidBrush(Color.FromArgb(40, 0, 138, 244)))
            {
                g.FillRegion(fill, _selectionRegion);
            }
            using (var pen = new Pen(_cropColor, 1f))
            {
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[2] { 3, 3 };
                pen.DashOffset = _dashOffset;

                using (var dash = new Bitmap(_imageBox.Width, _imageBox.Height))
                {
                    using (var drawer = Graphics.FromImage(dash))
                    {
                        drawer.Clear(Color.Magenta);

                        using (var outline = MakeOutlinePath())
                        {
                            drawer.DrawPath(Pens.Transparent, outline);
                            drawer.DrawPath(pen, outline);
                        }
                        drawer.FillRegion(Brushes.Magenta, _selectionRegion);
                    }
                    dash.MakeTransparent(Color.Magenta);
                    g.DrawImageUnscaled(dash, 0, 0);
                }
            }
        }
        private GraphicsPath MakeOutlinePath()
        {
            var path = new GraphicsPath();
            if (_selectionPath.PointCount > 0)
            {
                path.AddPath(_selectionPath, false);
                path.Widen(Pens.White);
            }
            return path;
        }

        private void UpdateCurrentSelectedText(Size size)
        {
            if (_currentSizeLabel != null)
            {
                if (size.Width <= 0 || size.Height <= 0)
                {
                    _currentSizeLabel.Text = string.Empty;
                    return;
                }
                _currentSizeLabel.Text = $"Current selected size: {size.Width} - {size.Height} px";
            }
        }
        private void SelectRegion(Point location)
        {
            ConstrainsLocation(ref location, _currentImageBounds);
            if (!_currentImageBounds.Contains(_startPoint))
            {
                _startPoint.X = location.X;
                _startPoint.Y = location.Y;
            }
            _endPoint.X = location.X;
            _endPoint.Y = location.Y;

            ConstrainsLocation(ref _endPoint, _currentImageBounds);
            _selectionBounds = new Rectangle(
                Math.Min(_startPoint.X, _endPoint.X),
                Math.Min(_startPoint.Y, _endPoint.Y),
                Math.Abs(_startPoint.X - _endPoint.X),
                Math.Abs(_startPoint.Y - _endPoint.Y)
            );
            SetSelection(_selectionBounds);
            _imageBox.Refresh();
        }
        private void MoveRegion(Point location)
        {
            var startPoint = new Point(_startPoint.X, _startPoint.Y);
            startPoint.Offset(location.X - _movePoint.X, location.Y - _movePoint.Y);

            if (!_currentImageBounds.Contains(startPoint))
            {
                _movePoint = location;
                ConstrainsLocation(ref _movePoint, _selectionBounds);
                return;
            }
            var endPoint = new Point(_endPoint.X, _endPoint.Y);
            endPoint.Offset(location.X - _movePoint.X, location.Y - _movePoint.Y);

            if (!_currentImageBounds.Contains(endPoint))
            {
                _movePoint = location;
                ConstrainsLocation(ref _movePoint, _selectionBounds);
                return;
            }

            _startPoint.X = startPoint.X;
            _startPoint.Y = startPoint.Y;
            _endPoint.X = endPoint.X;
            _endPoint.Y = endPoint.Y;

            _selectionBounds = new Rectangle(
                    Math.Min(_startPoint.X, _endPoint.X),
                    Math.Min(_startPoint.Y, _endPoint.Y),
                    Math.Abs(_startPoint.X - _endPoint.X),
                    Math.Abs(_startPoint.Y - _endPoint.Y));

            SetSelection(_selectionBounds);
            _imageBox.Refresh();
            _movePoint = location;
        }
        private void ConstrainsLocation(ref Point location, Rectangle bounds)
        {
            if (!_currentImageBounds.Contains(location))
            {
                if (location.X < bounds.Left)
                {
                    location.X = bounds.Left;
                }
                else if (location.X > bounds.Right)
                {
                    location.X = bounds.Right;
                }
                if (location.Y < bounds.Top)
                {
                    location.Y = bounds.Top;
                }
                else if (location.Y > bounds.Bottom)
                {
                    location.Y = bounds.Bottom;
                }
            }
        }
        private void OnImageBoxPaint(object sender, PaintEventArgs e)
        {
            using (var brush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.DarkGray, Color.Gray))
            {
                e.Graphics.FillRectangle(brush, 0, 0, _imageBox.Width, _imageBox.Height);
            }
            if (CurrentImage != null)
            {
                PaintImage(e.Graphics);
            }
            if (!_selectionRegion.IsEmpty(e.Graphics))
            {
                PaintSelection(e.Graphics);
            }
        }


        internal void OnSelectionUp(object sender, MouseEventArgs e)
        {
            if (CurrentImage == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (_currentState == CropState.Selecting)
                {
                    if (_currentImageBounds.Contains(e.Location))
                    {
                        _endPoint.X = e.Location.X;
                        _endPoint.Y = e.Location.Y;
                    }
                    _selectionBounds = new Rectangle(
                        Math.Min(_startPoint.X, _endPoint.X),
                        Math.Min(_startPoint.Y, _endPoint.Y),
                        Math.Abs(_startPoint.X - _endPoint.X),
                        Math.Abs(_startPoint.Y - _endPoint.Y)
                    );
                    SetSelection(_selectionBounds);
                    if (_selectionBounds.IsEmpty)
                    {
                        _timer.Stop();
                    }
                    else if (!_timer.Enabled)
                    {
                        _timer.Start();
                    }
                    _imageBox.Refresh();

                }
                _currentState = CropState.None;
            }
        }

        private void OnImageBoxResize(object sender, EventArgs e)
        {
            var drawer = _imageBox.CreateGraphics();

            if (_selectionRegion != null && !_selectionRegion.IsEmpty(drawer))
            {
                var bounds = _selectionRegion.GetBounds(drawer);
                
                if (bounds.Right > _imageBox.Right || bounds.Bottom > _imageBox.Bottom)
                {
                    var right = Math.Min(bounds.Right, _imageBox.Right);
                    var bottom = Math.Min(bounds.Bottom, _imageBox.Bottom);
                    
                    if (right <= _imageBox.Left || bottom <= _imageBox.Top)
                    {
                        SetSelection(Rectangle.Empty);
                    }
                    else
                    {
                        _endPoint.X = (int)right;
                        _endPoint.Y = (int)bottom;

                        SetSelection(new Rectangle(
                            (int)bounds.X,
                            (int)bounds.Y,
                            (int)(right - bounds.X),
                            (int)(bottom - bounds.Y)
                        ));
                    }
                }
            }
            _imageBox.Refresh();
        }


        public void OnSelectionMove(object sender, MouseEventArgs e)
        {
            if (_imageBox.Image == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                switch (_currentState)
                {
                    case CropState.Selecting:
                            SelectRegion(e.Location);
                        break;
                    case CropState.Moving:
                            MoveRegion(e.Location);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void OnSelectionDown(object sender, MouseEventArgs e)
        {
            if (CurrentImage == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (_selectionBounds.Contains(e.Location))
                {
                    _movePoint = e.Location;
                    _currentState = CropState.Moving;
                    return;
                }
                if (_currentImageBounds.Contains(e.Location))
                {
                    _startPoint.X = e.X;
                    _startPoint.Y = e.Y;
                    _selectionBounds = new Rectangle(e.X, e.Y, 0, 0);
                }
                else
                {
                    _startPoint.X = _startPoint.Y = -1;
                    _selectionBounds = Rectangle.Empty;
                }
                SetSelection(Rectangle.Empty);
                if (!_timer.Enabled)
                {
                    _timer.Start();
                }
                _currentState = CropState.Selecting;
            }
        }
        internal void OnSelectionClick(object sender, EventArgs e)
        {
            OnSelectionDown(_imageBox, new MouseEventArgs(MouseButtons.Left
                , 1, marginValue, marginValue, 0));
            var point = new Point(marginValue + _imageBox.Width, marginValue + _imageBox.Height);
            SelectRegion(point);
            OnSelectionUp(_imageBox, new MouseEventArgs(MouseButtons.Left
                , 1, point.X, point.Y, 0));
        }


        private void OnMarchingTick(object sender, EventArgs e)
        {
            _dashOffset--;
            _dashOffset %= 6;
            _imageBox.Refresh();
        }
        //internal void OnSelectionDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        _imageBox.Refresh();
        //        ResetSelection();
        //        _currentState = CropState.None;
        //    }
        //}

        private void ResetSelection()
        {
            SetSelection(Rectangle.Empty);
            UpdateCurrentSelectedText(new Size(0, 0));
        }
    }
}
