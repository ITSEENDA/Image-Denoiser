using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROTO.Tools;
using PROTO.Utils;

namespace PROTO
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void OnLoginFormLoad(object sender, EventArgs e)
        {

        }

        private void OnRegisterButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show(
                    "Username or password is empty",
                    "Null input",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                this.ActiveControl = usernameInput;
                return;
            }
            string usernameInputValue = usernameInput.Text;
            string passwordInputValue = Hasher.HashPassword(passwordInput.Text);

            string filePath = FileDB.GetFilePath();

            if (File.Exists(filePath))
            {
                using (StreamReader fs = new StreamReader(filePath))
                {
                    string account;
                    while ((account = fs.ReadLine()) != null)
                    {
                        var accountComponent = account.Split('|');
                        string username = accountComponent[0];

                        //If there is a match then annouce account already existed
                        if (usernameInputValue == username)
                        {
                            fs.Dispose();
                            fs.Close();
                            MessageBox.Show(
                                "Username is already existed, please try a different username",
                                "Username existed",
                                MessageBoxButtons.OK, MessageBoxIcon.Information
                            );
                            this.ActiveControl = usernameInput;
                            return;
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{usernameInputValue}|{passwordInputValue}");
                }
            }
            MessageBox.Show(
                "Account created successfully, enter again to login",
                "Registration succeeded",
                MessageBoxButtons.OK, MessageBoxIcon.Information
            );
            usernameInput.Text = null;
            passwordInput.Text = null;
            this.ActiveControl = usernameInput;
        }
        private void OnLoginButtonClick(object sender, EventArgs e)
        {
            string usernameInputValue = usernameInput.Text;
            string passwordInputValue = passwordInput.Text;

            string filePath = FileDB.GetFilePath();
            Debug.WriteLine(filePath);

            if (File.Exists(filePath))
            {
                using (StreamReader fs = new StreamReader(filePath))
                {
                    string account;
                    while ((account = fs.ReadLine()) != null && !string.IsNullOrEmpty(account))
                    {
                        //We proclaimed that accounts are saved with format {username}|{password} in DB
                        //(check db_login_accounts.txt to see the format)
                        var accountComponent = account.Split('|');
                        string username = accountComponent[0];
                        string password = accountComponent[1];

                        //If there is a match then switch to image processing form and close this form
                        if (usernameInputValue == username && Hasher.VerifyPassword(passwordInputValue, password))
                        {
                            Hide();
                            fs.Dispose();
                            fs.Close();
                            var imageProcessingForm = new ImageProcessingForm(username, password);
                            imageProcessingForm.DeleteAccountClick += OnDeleteAccountButtonClick;
                            imageProcessingForm.ShowDialog();
                            Close();
                            return;
                        }
                    }
                }
            }

            MessageBox.Show(
                "Incorrect username or password, please try again",
                "Incorrect username or password",
                MessageBoxButtons.OK, MessageBoxIcon.Information
            );
            usernameInput.Text = null;
            passwordInput.Text = null;
            this.ActiveControl = usernameInput;
        }
        private void OnDeleteAccountButtonClick(string deleteUsername, string deletePassword)
        {
            string filePath = FileDB.GetFilePath();

            if (File.Exists(filePath))
            {
                StringBuilder newContent = new StringBuilder();
                using (StreamReader fs = new StreamReader(filePath))
                {
                    string account;
                    while ((account = fs.ReadLine()) != null && !string.IsNullOrEmpty(account))
                    {
                        if (account == "\n")
                        {
                            continue;
                        }
                        var accountComponent = account.Split('|');
                        string username = accountComponent[0];

                        if (deleteUsername == username)
                        {
                            continue;
                        }
                        newContent.Append(account).Append('\n');
                    }
                }
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    sw.Write(newContent.ToString());
                }
            }
        }
        #region KeyEvents
        private void CheckPasswordEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(passwordInput.Text))
            {
                this.ActiveControl = loginButton;
                e.SuppressKeyPress = true;
            }
        }

        private void CheckUsernameEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(usernameInput.Text))
            {
                this.ActiveControl = passwordInput;
                e.SuppressKeyPress = true;
            }
        }

        private void CheckLoginButtonEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnLoginButtonClick(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        #endregion

    }
}
