using System;
using System.Windows.Forms;

namespace PROTO
{
    partial class ImageProcessingForm
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
            this.components = new System.ComponentModel.Container();
            this.inputImageBox = new System.Windows.Forms.PictureBox();
            this.outputImageBox = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.outputWidthDimension = new System.Windows.Forms.TextBox();
            this.outputHeightDimension = new System.Windows.Forms.TextBox();
            this.pxUnitLabel = new System.Windows.Forms.Label();
            this.filterOptionBox = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.processButton = new System.Windows.Forms.Button();
            this.currentSelectedSizeLabel = new System.Windows.Forms.Label();
            this.outputZoomBar = new System.Windows.Forms.TrackBar();
            this.processTimer = new System.Windows.Forms.Timer(this.components);
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.inputZoomBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.inputImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputImageBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputZoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // inputImageBox
            // 
            this.inputImageBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputImageBox.Location = new System.Drawing.Point(12, 73);
            this.inputImageBox.Name = "inputImageBox";
            this.inputImageBox.Size = new System.Drawing.Size(254, 214);
            this.inputImageBox.TabIndex = 0;
            this.inputImageBox.TabStop = false;
            // 
            // outputImageBox
            // 
            this.outputImageBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.outputImageBox.Location = new System.Drawing.Point(534, 73);
            this.outputImageBox.Name = "outputImageBox";
            this.outputImageBox.Size = new System.Drawing.Size(254, 214);
            this.outputImageBox.TabIndex = 1;
            this.outputImageBox.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.loadButton.Location = new System.Drawing.Point(52, 299);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(119, 33);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.OnLoadButtonClick);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.saveButton.Location = new System.Drawing.Point(626, 347);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(119, 33);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // logoutButton
            // 
            this.logoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logoutButton.Location = new System.Drawing.Point(663, 404);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(125, 34);
            this.logoutButton.TabIndex = 6;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.OnLogoutButtonClick);
            // 
            // outputWidthDimension
            // 
            this.outputWidthDimension.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.outputWidthDimension.Location = new System.Drawing.Point(564, 302);
            this.outputWidthDimension.Name = "outputWidthDimension";
            this.outputWidthDimension.Size = new System.Drawing.Size(81, 22);
            this.outputWidthDimension.TabIndex = 7;
            // 
            // outputHeightDimension
            // 
            this.outputHeightDimension.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.outputHeightDimension.Location = new System.Drawing.Point(674, 302);
            this.outputHeightDimension.Name = "outputHeightDimension";
            this.outputHeightDimension.Size = new System.Drawing.Size(81, 22);
            this.outputHeightDimension.TabIndex = 8;
            // 
            // pxUnitLabel
            // 
            this.pxUnitLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pxUnitLabel.AutoSize = true;
            this.pxUnitLabel.Location = new System.Drawing.Point(761, 302);
            this.pxUnitLabel.Name = "pxUnitLabel";
            this.pxUnitLabel.Size = new System.Drawing.Size(21, 16);
            this.pxUnitLabel.TabIndex = 9;
            this.pxUnitLabel.Text = "px";
            // 
            // filterOptionBox
            // 
            this.filterOptionBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.filterOptionBox.FormattingEnabled = true;
            this.filterOptionBox.Location = new System.Drawing.Point(303, 114);
            this.filterOptionBox.Name = "filterOptionBox";
            this.filterOptionBox.Size = new System.Drawing.Size(191, 24);
            this.filterOptionBox.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.inputZoomBar);
            this.panel3.Controls.Add(this.resetButton);
            this.panel3.Controls.Add(this.processButton);
            this.panel3.Controls.Add(this.currentSelectedSizeLabel);
            this.panel3.Controls.Add(this.filterOptionBox);
            this.panel3.Controls.Add(this.outputZoomBar);
            this.panel3.Controls.Add(this.outputImageBox);
            this.panel3.Controls.Add(this.inputImageBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 450);
            this.panel3.TabIndex = 14;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.resetButton.Location = new System.Drawing.Point(346, 155);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(106, 29);
            this.resetButton.TabIndex = 12;
            this.resetButton.Text = "ResetFilter";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.OnResetButtonClick);
            // 
            // processButton
            // 
            this.processButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.processButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.processButton.Location = new System.Drawing.Point(317, 238);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(167, 49);
            this.processButton.TabIndex = 3;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.OnProcessButtonClick);
            // 
            // currentSelectedSizeLabel
            // 
            this.currentSelectedSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.currentSelectedSizeLabel.AutoSize = true;
            this.currentSelectedSizeLabel.Location = new System.Drawing.Point(543, 54);
            this.currentSelectedSizeLabel.Name = "currentSelectedSizeLabel";
            this.currentSelectedSizeLabel.Size = new System.Drawing.Size(202, 16);
            this.currentSelectedSizeLabel.TabIndex = 11;
            this.currentSelectedSizeLabel.Text = "Current selected size: 200x200 px";
            // 
            // outputZoomBar
            // 
            this.outputZoomBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.outputZoomBar.Location = new System.Drawing.Point(526, 12);
            this.outputZoomBar.Maximum = 100;
            this.outputZoomBar.Name = "outputZoomBar";
            this.outputZoomBar.Size = new System.Drawing.Size(271, 56);
            this.outputZoomBar.TabIndex = 10;
            this.outputZoomBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // processTimer
            // 
            this.processTimer.Interval = 50;
            // 
            // saveTimer
            // 
            this.saveTimer.Interval = 50;
            // 
            // inputZoomBar
            // 
            this.inputZoomBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inputZoomBar.Location = new System.Drawing.Point(3, 14);
            this.inputZoomBar.Maximum = 100;
            this.inputZoomBar.Name = "inputZoomBar";
            this.inputZoomBar.Size = new System.Drawing.Size(271, 56);
            this.inputZoomBar.TabIndex = 13;
            this.inputZoomBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ImageProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pxUnitLabel);
            this.Controls.Add(this.outputHeightDimension);
            this.Controls.Add(this.outputWidthDimension);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.panel3);
            this.MinimumSize = new System.Drawing.Size(818, 497);
            this.Name = "ImageProcessingForm";
            this.Text = "ImageProcessing";
            ((System.ComponentModel.ISupportInitialize)(this.inputImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputImageBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputZoomBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }




        #endregion

        private System.Windows.Forms.PictureBox inputImageBox;
        private System.Windows.Forms.PictureBox outputImageBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.TextBox outputWidthDimension;
        private System.Windows.Forms.TextBox outputHeightDimension;
        private System.Windows.Forms.Label pxUnitLabel;
        private System.Windows.Forms.ComboBox filterOptionBox;
        private System.Windows.Forms.Panel panel3;
        private Label currentSelectedSizeLabel;
        private TrackBar outputZoomBar;
        private Button processButton;
        private Timer processTimer;
        private Timer saveTimer;
        private Button resetButton;
        private TrackBar inputZoomBar;
    }
}