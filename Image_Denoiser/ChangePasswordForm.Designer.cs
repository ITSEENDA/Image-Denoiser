namespace PROTO
{
    partial class ChangePasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.oldPasswordLabel = new System.Windows.Forms.Label();
            this.changePasswordTitle = new System.Windows.Forms.Label();
            this.oldPasswordInput = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            this.newPasswordInput = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.confirmInput = new System.Windows.Forms.TextBox();
            this.confirmLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oldPasswordLabel
            // 
            this.oldPasswordLabel.AutoSize = true;
            this.oldPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.oldPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.oldPasswordLabel.Location = new System.Drawing.Point(135, 154);
            this.oldPasswordLabel.Name = "oldPasswordLabel";
            this.oldPasswordLabel.Size = new System.Drawing.Size(131, 24);
            this.oldPasswordLabel.TabIndex = 6;
            this.oldPasswordLabel.Text = "Old password:";
            // 
            // changePasswordTitle
            // 
            this.changePasswordTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changePasswordTitle.AutoSize = true;
            this.changePasswordTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.changePasswordTitle.Location = new System.Drawing.Point(221, 58);
            this.changePasswordTitle.Name = "changePasswordTitle";
            this.changePasswordTitle.Size = new System.Drawing.Size(346, 46);
            this.changePasswordTitle.TabIndex = 7;
            this.changePasswordTitle.Text = "Change Password";
            // 
            // oldPasswordInput
            // 
            this.oldPasswordInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oldPasswordInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.oldPasswordInput.Location = new System.Drawing.Point(348, 154);
            this.oldPasswordInput.Name = "oldPasswordInput";
            this.oldPasswordInput.PasswordChar = '●';
            this.oldPasswordInput.Size = new System.Drawing.Size(316, 22);
            this.oldPasswordInput.TabIndex = 8;
            // 
            // confirmButton
            // 
            this.confirmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.confirmButton.Location = new System.Drawing.Point(229, 298);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(98, 34);
            this.confirmButton.TabIndex = 9;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.OnConfirmButtonClick);
            // 
            // returnButton
            // 
            this.returnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.returnButton.Location = new System.Drawing.Point(469, 298);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(98, 34);
            this.returnButton.TabIndex = 10;
            this.returnButton.Text = "Return";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.OnReturnButtonClick);
            // 
            // newPasswordInput
            // 
            this.newPasswordInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newPasswordInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newPasswordInput.Location = new System.Drawing.Point(348, 192);
            this.newPasswordInput.Name = "newPasswordInput";
            this.newPasswordInput.PasswordChar = '●';
            this.newPasswordInput.Size = new System.Drawing.Size(316, 22);
            this.newPasswordInput.TabIndex = 12;
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.newPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.newPasswordLabel.Location = new System.Drawing.Point(135, 192);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(140, 24);
            this.newPasswordLabel.TabIndex = 11;
            this.newPasswordLabel.Text = "New password:";
            // 
            // confirmInput
            // 
            this.confirmInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.confirmInput.Location = new System.Drawing.Point(348, 230);
            this.confirmInput.Name = "confirmInput";
            this.confirmInput.PasswordChar = '●';
            this.confirmInput.Size = new System.Drawing.Size(316, 22);
            this.confirmInput.TabIndex = 14;
            // 
            // confirmLabel
            // 
            this.confirmLabel.AutoSize = true;
            this.confirmLabel.BackColor = System.Drawing.Color.Transparent;
            this.confirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.confirmLabel.Location = new System.Drawing.Point(135, 230);
            this.confirmLabel.Name = "confirmLabel";
            this.confirmLabel.Size = new System.Drawing.Size(207, 24);
            this.confirmLabel.TabIndex = 13;
            this.confirmLabel.Text = "Confirm new password:";
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.confirmInput);
            this.Controls.Add(this.confirmLabel);
            this.Controls.Add(this.newPasswordInput);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.oldPasswordInput);
            this.Controls.Add(this.changePasswordTitle);
            this.Controls.Add(this.oldPasswordLabel);
            this.Name = "ChangePasswordForm";
            this.Text = "ChangePasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label oldPasswordLabel;
        private System.Windows.Forms.Label changePasswordTitle;
        private System.Windows.Forms.TextBox oldPasswordInput;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.TextBox newPasswordInput;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.TextBox confirmInput;
        private System.Windows.Forms.Label confirmLabel;
    }
}