using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROTO.Filters;
using PROTO.Tools;
using System.Drawing.Drawing2D;

namespace PROTO
{
    public partial class ImageProcessingForm : Form
    {
        private Rectangle _inputImageRect, _outputImageRect, 
            _processButtonRect, _filterOptionRect, _loadButtonRect, _saveButtonRect,
            _zoomBarRect, _outputWidthDimensionRect, _outputHeightDimensionRect, _pxUnitRect,
            _currentSelectedSizeRect, _resetButtonRect;

        private Size _baseFormSize;
        private CropTool _cropProcess, _cropSave;
        private ZoomTool _zoomInput, _zoomOutput;

        public ImageProcessingForm()
        {
            InitializeComponent();
            InitializeFilterOption();
            InitializeSizes();
            _cropProcess = new CropTool(Color.Yellow, inputImageBox, processTimer);
            _cropSave = new CropTool(Color.Red, outputImageBox, saveTimer, currentSelectedSizeLabel);
            _zoomInput = new ZoomTool(inputZoomBar);
            _zoomOutput = new ZoomTool(outputZoomBar);
            currentSelectedSizeLabel.Text = string.Empty;
            InitializeEvents();
        }

        private void InitializeSizes()
        {
            _baseFormSize = this.Size;
            _inputImageRect = new Rectangle(inputImageBox.Location, inputImageBox.Size);
            _outputImageRect = new Rectangle(outputImageBox.Location, outputImageBox.Size);
            _processButtonRect = new Rectangle(processButton.Location, processButton.Size);
            _filterOptionRect = new Rectangle(filterOptionBox.Location, filterOptionBox.Size);
            _loadButtonRect = new Rectangle(loadButton.Location, loadButton.Size);
            _saveButtonRect = new Rectangle(saveButton.Location, saveButton.Size);
            _zoomBarRect = new Rectangle(outputZoomBar.Location, outputZoomBar.Size);
            _outputWidthDimensionRect = new Rectangle(outputWidthDimension.Location, outputWidthDimension.Size);
            _outputHeightDimensionRect = new Rectangle(outputHeightDimension.Location, outputHeightDimension.Size);
            _pxUnitRect = new Rectangle(pxUnitLabel.Location, pxUnitLabel.Size);
            _currentSelectedSizeRect = new Rectangle(currentSelectedSizeLabel.Location, currentSelectedSizeLabel.Size);
            _resetButtonRect = new Rectangle(resetButton.Location, resetButton.Size);
        }

        private void InitializeEvents()
        {
            this.Resize += OnFormResize;
            inputImageBox.MouseEnter += OnSelectionEnter;
            inputImageBox.MouseLeave += OnSelectionLeave;
            inputImageBox.MouseUp += _cropProcess.OnSelectionUp;
            inputImageBox.MouseDown += _cropProcess.OnSelectionDown;
            inputImageBox.MouseMove += _cropProcess.OnSelectionMove;
            //inputImageBox.MouseDoubleClick += _cropProcessPen.OnSelectionDoubleClick;
            outputImageBox.MouseEnter += OnSelectionEnter;
            outputImageBox.MouseLeave += OnSelectionLeave;
            outputImageBox.MouseUp += _cropSave.OnSelectionUp;
            outputImageBox.MouseDown += _cropSave.OnSelectionDown;
            outputImageBox.MouseMove += _cropSave.OnSelectionMove;
            //outputImageBox.MouseDoubleClick += _cropSavePen.OnSelectionDoubleClick;

            inputZoomBar.Scroll += _zoomInput.OnZoomBarScroll;
            outputZoomBar.Scroll += _zoomOutput.OnZoomBarScroll;
        }
        private void InitializeFilterOption()
        {
            filterOptionBox.DataSource = new FilterOptionBase[]
            {
                new GaussianFilter(),
                new BilateralFilter(),
                new MedianFilter(),
                new KuwaharaFilter(),
                new WienerFilter(),
            };
        }


        private void OnResetButtonClick(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
            {
                return;
            }
            outputImageBox.Image = inputImageBox.Image.Clone() as Bitmap;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            inputImageBox.Dispose();
            outputImageBox.Dispose();
            base.OnFormClosed(e);
            Application.Exit();
        }
        private void OnLoadButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Load an image",
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|" +
                "BMP Files (*.bmp)|*.bmp"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(filePath))
                {
                    var bytes = System.IO.File.ReadAllBytes(filePath);
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(bytes);
                    inputImageBox.Image = Image.FromStream(memoryStream);
                    outputImageBox.Image = Image.FromStream(memoryStream);

                    _cropProcess.CalculateImageScale();
                    _cropSave.CalculateImageScale();
                }
            }
        }



        private void OnProcessButtonClick(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
            {
                return;
            }
            var selectedFilter = filterOptionBox.SelectedItem as FilterOptionBase;
            if (selectedFilter != null)
            {
                if (_cropProcess.IsSelecting)
                {
                    var selectedBox = _cropProcess.GetAbsoluteSelectedBox();
                    var selectedImage = _cropProcess.GetImageWithinBounds();
                    var filteredImage = selectedFilter.FitlerImage(selectedImage) as Bitmap;
                    //Debug.WriteLine($"{selectedImage.Size} - {filteredImage.Size}");
                    _cropSave.UpdateFilteredImageFromSelection(filteredImage, selectedBox);
                    return;
                }
                outputImageBox.Image = selectedFilter.
                    FitlerImage(inputImageBox.Image);
            }
        }
        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (outputImageBox.Image == null)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save processed image",
                Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp",
                OverwritePrompt = true
            };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                if (saveFileDialog.FileName == inputImageBox.Name)
                {
                    inputImageBox.Dispose();
                }

                var saveImage = _cropSave.IsSelecting ?
                    _cropSave.GetImageWithinBounds() : new Bitmap(outputImageBox.Image);

                switch (saveFileDialog.FilterIndex)
                {
                    //Save as JPEG file
                    case 1:
                        saveImage.Save(saveFileDialog.FileName, 
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    //Save as PNG file
                    case 2:
                        saveImage.Save(saveFileDialog.FileName, 
                            System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    //Save as bitmap file
                    case 3:
                        saveImage.Save(saveFileDialog.FileName, 
                            System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    //Not an image extension then dont save
                    default:
                        break;
                }
            }
        }



        private void OnFormResize(object sender, EventArgs e)
        {
            ResizeControl(inputImageBox, _inputImageRect);
            ResizeControl(outputImageBox, _outputImageRect);
            ResizeControl(processButton, _processButtonRect);
            ResizeControl(filterOptionBox, _filterOptionRect);
            ResizeControl(loadButton, _loadButtonRect);
            ResizeControl(saveButton, _saveButtonRect);
            ResizeControl(outputZoomBar, _zoomBarRect);
            ResizeControl(outputWidthDimension, _outputWidthDimensionRect);
            ResizeControl(outputHeightDimension, _outputHeightDimensionRect);
            ResizeControl(pxUnitLabel, _pxUnitRect);
            ResizeControl(currentSelectedSizeLabel, _currentSelectedSizeRect);
            ResizeControl(resetButton, _resetButtonRect);
        }
        private void ResizeControl(Control control, Rectangle baseControlRect)
        {
            float xRatio = Width / _baseFormSize.Width;
            float yRatio = Height / _baseFormSize.Height;
            int newPosX = (int)(baseControlRect.X * xRatio);
            int newPosY = (int)(baseControlRect.Y * yRatio);

            int newWidth = (int)(baseControlRect.Width * xRatio);
            int newHeight = (int)(baseControlRect.Height * yRatio);

            control.Location = new Point(newPosX, newPosY);
            if (!(control is Button) && !(control is Label) && !(control is TextBox))
            {
                control.Size = new Size(newWidth, newHeight);
            }
        }
        private void OnLogoutButtonClick(object sender, EventArgs e)
        {
            Hide();
            var loginForm = new LoginForm();
            loginForm.ShowDialog();
            Close();
        }
        private void OnSelectionEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }
        private void OnSelectionLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
