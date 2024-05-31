using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PROTO.Tools;
using PROTO.Utils;

namespace PROTO
{
    public partial class ChangePasswordForm : Form
    {
        private readonly string _currentUsername, _currentPassword;
        public ChangePasswordForm(string username, string password)
        {
            InitializeComponent();
            _currentUsername = username;
            _currentPassword = password;
            MaximizeBox = false;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
        private void OnConfirmButtonClick(object sender, EventArgs e)
        {
            var newPassword = HandleGettingNewPassword();
            if (string.IsNullOrEmpty(newPassword))
            {
                return;
            }
            var filePath = FileDB.GetFilePath();

            if (File.Exists(filePath))
            {
                StringBuilder newContent = new();
                using (StreamReader fs = new StreamReader(filePath))
                {
                    string account;
                    while ((account = fs.ReadLine()) != null && !string.IsNullOrEmpty(account))
                    {
                        var accountComponent = account.Split('|');
                        string username = accountComponent[0];
                        string password = accountComponent[1];

                        if (_currentUsername == username)
                        {
                            password = newPassword;
                        }
                        newContent.Append($"{username}|{password}").Append('\n');
                    }
                }
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    sw.Write(newContent.ToString());
                }
                MessageBox.Show(
                    "Password changed successfully",
                    "Password changed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
        }

        private string HandleGettingNewPassword()
        {
            string oldPassword = oldPasswordInput.Text;
            string newPassword = newPasswordInput.Text;
            string confirm = confirmInput.Text;

            if (!Hasher.VerifyPassword(oldPassword, _currentPassword))
            {
                MessageBox.Show(
                    "Incorrect old password, please try again",
                    "Incorrect old password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                return string.Empty;
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show(
                    "New password must not be null or empty",
                    "Null or empty new password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                return string.Empty;
            }
            if (newPassword == oldPassword)
            {
                MessageBox.Show(
                    "New password must not matches with old password, please try again",
                    "Matching old password and new password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                return string.Empty;
            }
            if (confirm != newPassword)
            {
                MessageBox.Show(
                    "Confirm password and new password does not match, please try again",
                    "Unmatching confirm password and new password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                return string.Empty;
            }

            return Hasher.HashPassword(confirm);
        }

        private void OnReturnButtonClick(object sender, EventArgs e)
        {
            Hide();
            var loginForm = new LoginForm();
            loginForm.ShowDialog();
            Close();
        }
    }
}
