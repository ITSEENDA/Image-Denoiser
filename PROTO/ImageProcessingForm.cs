using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using PROTO.Filters;
using PROTO.Tools;

namespace PROTO
{
    public partial class ImageProcessingForm : Form
    {
        public event Action<string, string> DeleteAccountClick;

        private Rectangle _inputImageRect, _outputImageRect, 
            _processButtonRect, _filterOptionRect, _loadButtonRect, _saveButtonRect,
            _currentSelectedSizeRect, _resetButtonRect;

        private Size _baseFormSize;
        private SelectionPen _cropProcess, _cropSave;
        private string _currentUsername, _currentPassword;

        //Khởi tạo filter, mốc vị trí kích cỡ, các tool và các event
        public ImageProcessingForm(string username, string password)
        {
            InitializeComponent();
            InitializeFilterOption();
            InitializeSizes();
            _cropProcess = new SelectionPen(Color.Yellow, inputImageBox, processTimer);
            _cropSave = new SelectionPen(Color.Red, outputImageBox, saveTimer, currentSelectedSizeLabel);
            currentSelectedSizeLabel.Text = string.Empty;
            _currentUsername = username;
            _currentPassword = password;
            helloLabel.Text = $"Hello user {username}!";
            InitializeEvents();
        }


        //Khởi tạo các mốc vị trí và kích cỡ ban đầu để offset và resize các control khi
        //window resize
        private void InitializeSizes()
        {
            _baseFormSize = this.Size;
            _inputImageRect = new Rectangle(inputImageBox.Location, inputImageBox.Size);
            _outputImageRect = new Rectangle(outputImageBox.Location, outputImageBox.Size);
            _processButtonRect = new Rectangle(processButton.Location, processButton.Size);
            _filterOptionRect = new Rectangle(filterOptionBox.Location, filterOptionBox.Size);
            _loadButtonRect = new Rectangle(loadButton.Location, loadButton.Size);
            _saveButtonRect = new Rectangle(saveButton.Location, saveButton.Size);
            _currentSelectedSizeRect = new Rectangle(currentSelectedSizeLabel.Location, currentSelectedSizeLabel.Size);
            _resetButtonRect = new Rectangle(resetButton.Location, resetButton.Size);
        }

        //Khởi tạo event cho các control và form
        private void InitializeEvents()
        {
            this.Resize += OnFormResize;
            inputImageBox.MouseEnter += OnSelectionEnter;
            inputImageBox.MouseLeave += OnSelectionLeave;
            inputImageBox.MouseUp += _cropProcess.OnSelectionUp;
            inputImageBox.MouseDown += _cropProcess.OnSelectionDown;
            inputImageBox.MouseMove += _cropProcess.OnSelectionMove;

            outputImageBox.MouseEnter += OnSelectionEnter;
            outputImageBox.MouseLeave += OnSelectionLeave;
            outputImageBox.MouseUp += _cropSave.OnSelectionUp;
            outputImageBox.MouseDown += _cropSave.OnSelectionDown;
            outputImageBox.MouseMove += _cropSave.OnSelectionMove;
        }

        //Khởi tạo lựa chọn cho các filter
        private void InitializeFilterOption()
        {
            filterOptionBox.DataSource = new FilterOptionBase[]
            {
                new GaussianFilter(),
                new BilateralFilter(),
                new MedianFilter(),
                //new AnisotropicKuwaharaFilter(),
                new AdaptiveWienerFilter(),
            };
        }

        //Event thực hiện cập nhật ảnh output thành ảnh input ban đầu khi ấn nút reset
        private void OnResetButtonClick(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
            {
                return;
            }
            outputImageBox.Image = inputImageBox.Image.Clone() as Bitmap;
        }

        private void OnDeleteAccountClick(object sender, EventArgs e)
        {
            var dialogBox = MessageBox.Show(
                "Do you want to delete current account?",
                "Delete account",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (dialogBox == DialogResult.No)
            {
                return;
            }
            DeleteAccountClick?.Invoke(_currentUsername, _currentPassword);
            Logout();
        }

        //Event thực hiện đóng process đang chạy phần mềm nếu tắt form này
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            inputImageBox.Dispose();
            outputImageBox.Dispose();
            base.OnFormClosed(e);
            Application.Exit();
        }

        private void OnChangePasswordButtonClick(object sender, EventArgs e)
        {
            Hide();
            var changePasswordForm = new ChangePasswordForm(_currentUsername, _currentPassword);
            changePasswordForm.ShowDialog();
            Close();
        }

        //Event load hình ảnh trong drive khi ấn vào nút load
        private void OnLoadButtonClick(object sender, EventArgs e)
        {

            //Tạo dialog để mở các file định dạng .jpg, .png, .bmp
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Load an image",
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|" +
                "BMP Files (*.bmp)|*.bmp"
            };

            //Nếu người dùng ấn đồng ý thì kiểm tra đường dẫn file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                //Nếu đường dẫn file không trống thì đọc dữ liệu file theo các byte để chuyển về memory stream
                //và chuyển thành ảnh từ memory stream đó
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


        //Event xử lý ảnh qua filter được chọn khi ấn vào nút process
        private void OnProcessButtonClick(object sender, EventArgs e)
        {
            //Nếu input không có ảnh thì không làm gì cả
            if (inputImageBox.Image == null)
            {
                return;
            }

            //Lấy filter đang được chọn trong filterOptionBox để thực hiện áp filter
            var selectedFilter = filterOptionBox.SelectedItem as FilterOptionBase;
            //Nếu filter được chọn rỗng thì bỏ qua
            if (selectedFilter != null)
            {
                //Nếu đang trong selection của crop thì thực hiện lấy ảnh trong vùng selection
                //rồi áp filter lên ảnh vừa lấy cuối cùng thì cập nhật lại vùng selection đó với ảnh
                //vừa được xử lý qua filter
                if (_cropProcess.IsSelecting)
                {
                    var selectedBox = _cropProcess.GetAbsoluteSelectedBox();
                    var selectedImage = _cropProcess.GetImageWithinBounds();
                    var filteredImage = selectedFilter.FitlerImage(selectedImage) as Bitmap;
                    _cropSave.UpdateFilteredImageFromSelection(filteredImage, selectedBox);
                    return;
                }

                //Nếu không trong selection thì áp filter lên kích thước gốc của ảnh
                outputImageBox.Image = selectedFilter.
                    FitlerImage(inputImageBox.Image);
            }
        }
        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            //Nếu output không có ảnh thì không làm gì cả
            if (outputImageBox.Image == null)
            {
                return;
            }

            //Tạo khung dialog để yêu cầu người dùng lưu file trong các định dạng
            //.jpg; .png; .bmp
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save processed image",
                Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp",
                OverwritePrompt = true
            };
            saveFileDialog.ShowDialog();

            //Nếu đường dẫn file không trống thì thực hiện save
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                //Nếu đang trong selection của crop thì output hình ảnh được lưu trong miền selection
                //không thì lưu theo kích cỡ gốc của ảnh
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


        //Event thực hiện tính toán thay đổi kích thước tương ứng khi window resize
        private void OnFormResize(object sender, EventArgs e)
        {
            ResizeControl(inputImageBox, _inputImageRect);
            ResizeControl(outputImageBox, _outputImageRect);
            ResizeControl(processButton, _processButtonRect);
            ResizeControl(filterOptionBox, _filterOptionRect);
            ResizeControl(loadButton, _loadButtonRect);
            ResizeControl(saveButton, _saveButtonRect);
            ResizeControl(currentSelectedSizeLabel, _currentSelectedSizeRect);
            ResizeControl(resetButton, _resetButtonRect);
        }

        //Hàm tính toán tỉ lệ thay đổi theo % x, y rồi offset và resize control theo tỉ lệ phần trăm đó
        private void ResizeControl(Control control, Rectangle baseControlRect)
        {
            float xRatio = Width / _baseFormSize.Width;
            float yRatio = Height / _baseFormSize.Height;
            int newPosX = (int)(baseControlRect.X * xRatio);
            int newPosY = (int)(baseControlRect.Y * yRatio);

            int newWidth = (int)(baseControlRect.Width * xRatio);
            int newHeight = (int)(baseControlRect.Height * yRatio);

            control.Location = new Point(newPosX, newPosY);
            //Không thực hiện resize với các control là button, label và textbox
            if (!(control is Button) && !(control is Label) && !(control is TextBox))
            {
                control.Size = new Size(newWidth, newHeight);
            }
        }

        //Event chuyển về login form khi ấn nút logout
        private void OnLogoutButtonClick(object sender, EventArgs e)
        {
            Logout();
        }
        private void Logout()
        {
            Hide();
            var loginForm = new LoginForm();
            loginForm.ShowDialog();
            Close();
        }
        //Event chuyển con trỏ chuột thành hình thập khi rời khởi vùng có thể selection
        private void OnSelectionEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }
        //Event chuyển con trỏ chuột thành mặc định khi rời khởi vùng có thể selection
        private void OnSelectionLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
