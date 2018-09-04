namespace PlateSolveWrapper
{
    partial class fmMain
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
            this.btnConnectMount = new System.Windows.Forms.Button();
            this.btnOpenSolveAndSync = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.numSearchTiles = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseSolver = new System.Windows.Forms.Button();
            this.tbPlateSolverPath = new System.Windows.Forms.TextBox();
            this.lblSolverPath = new System.Windows.Forms.Label();
            this.lbField = new System.Windows.Forms.Label();
            this.numFieldHeight = new System.Windows.Forms.NumericUpDown();
            this.numFieldWidth = new System.Windows.Forms.NumericUpDown();
            this.lblScopeName = new System.Windows.Forms.Label();
            this.gbScope = new System.Windows.Forms.GroupBox();
            this.tbCurrentCoordinates = new System.Windows.Forms.TextBox();
            this.lblCurrentCoordinates = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.gbSolveResult = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSolvedFileName = new System.Windows.Forms.Label();
            this.tbSolvedCoordinates = new System.Windows.Forms.TextBox();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.numExposure = new System.Windows.Forms.NumericUpDown();
            this.lblExposure = new System.Windows.Forms.Label();
            this.btnDisconnectCamera = new System.Windows.Forms.Button();
            this.lblCameraName = new System.Windows.Forms.Label();
            this.btnConnectCamera = new System.Windows.Forms.Button();
            this.btnShotSolveSync = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSearchTiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldWidth)).BeginInit();
            this.gbScope.SuspendLayout();
            this.gbSolveResult.SuspendLayout();
            this.gbCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnectMount
            // 
            this.btnConnectMount.Location = new System.Drawing.Point(14, 49);
            this.btnConnectMount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectMount.Name = "btnConnectMount";
            this.btnConnectMount.Size = new System.Drawing.Size(88, 35);
            this.btnConnectMount.TabIndex = 0;
            this.btnConnectMount.Text = "Connect mount";
            this.btnConnectMount.UseVisualStyleBackColor = true;
            this.btnConnectMount.Click += new System.EventHandler(this.btnConnectMount_Click);
            // 
            // btnOpenSolveAndSync
            // 
            this.btnOpenSolveAndSync.Enabled = false;
            this.btnOpenSolveAndSync.Location = new System.Drawing.Point(16, 760);
            this.btnOpenSolveAndSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenSolveAndSync.Name = "btnOpenSolveAndSync";
            this.btnOpenSolveAndSync.Size = new System.Drawing.Size(194, 35);
            this.btnOpenSolveAndSync.TabIndex = 1;
            this.btnOpenSolveAndSync.Text = "Open - Solve- Sync";
            this.btnOpenSolveAndSync.UseVisualStyleBackColor = true;
            this.btnOpenSolveAndSync.Click += new System.EventHandler(this.btnOpenSolveSync_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.numSearchTiles);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.btnBrowseSolver);
            this.gbSettings.Controls.Add(this.tbPlateSolverPath);
            this.gbSettings.Controls.Add(this.lblSolverPath);
            this.gbSettings.Controls.Add(this.lbField);
            this.gbSettings.Controls.Add(this.numFieldHeight);
            this.gbSettings.Controls.Add(this.numFieldWidth);
            this.gbSettings.Location = new System.Drawing.Point(18, 18);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSettings.Size = new System.Drawing.Size(460, 165);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // numSearchTiles
            // 
            this.numSearchTiles.Location = new System.Drawing.Point(123, 112);
            this.numSearchTiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numSearchTiles.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numSearchTiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSearchTiles.Name = "numSearchTiles";
            this.numSearchTiles.Size = new System.Drawing.Size(100, 26);
            this.numSearchTiles.TabIndex = 7;
            this.numSearchTiles.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search tiles";
            // 
            // btnBrowseSolver
            // 
            this.btnBrowseSolver.Location = new System.Drawing.Point(285, 25);
            this.btnBrowseSolver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowseSolver.Name = "btnBrowseSolver";
            this.btnBrowseSolver.Size = new System.Drawing.Size(87, 35);
            this.btnBrowseSolver.TabIndex = 5;
            this.btnBrowseSolver.Text = "Browse";
            this.btnBrowseSolver.UseVisualStyleBackColor = true;
            this.btnBrowseSolver.Click += new System.EventHandler(this.btnBrowseSolver_Click);
            // 
            // tbPlateSolverPath
            // 
            this.tbPlateSolverPath.Enabled = false;
            this.tbPlateSolverPath.Location = new System.Drawing.Point(159, 29);
            this.tbPlateSolverPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPlateSolverPath.Name = "tbPlateSolverPath";
            this.tbPlateSolverPath.Size = new System.Drawing.Size(118, 26);
            this.tbPlateSolverPath.TabIndex = 4;
            // 
            // lblSolverPath
            // 
            this.lblSolverPath.AutoSize = true;
            this.lblSolverPath.Location = new System.Drawing.Point(12, 34);
            this.lblSolverPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSolverPath.Name = "lblSolverPath";
            this.lblSolverPath.Size = new System.Drawing.Size(144, 20);
            this.lblSolverPath.TabIndex = 3;
            this.lblSolverPath.Text = "Path to PlateSolver";
            // 
            // lbField
            // 
            this.lbField.AutoSize = true;
            this.lbField.Location = new System.Drawing.Point(10, 72);
            this.lbField.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbField.Name = "lbField";
            this.lbField.Size = new System.Drawing.Size(104, 20);
            this.lbField.TabIndex = 2;
            this.lbField.Text = "Field (arcmin)";
            // 
            // numFieldHeight
            // 
            this.numFieldHeight.Location = new System.Drawing.Point(232, 69);
            this.numFieldHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numFieldHeight.Maximum = new decimal(new int[] {
            21600,
            0,
            0,
            0});
            this.numFieldHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFieldHeight.Name = "numFieldHeight";
            this.numFieldHeight.Size = new System.Drawing.Size(100, 26);
            this.numFieldHeight.TabIndex = 1;
            this.numFieldHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numFieldWidth
            // 
            this.numFieldWidth.Location = new System.Drawing.Point(123, 69);
            this.numFieldWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numFieldWidth.Maximum = new decimal(new int[] {
            21600,
            0,
            0,
            0});
            this.numFieldWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFieldWidth.Name = "numFieldWidth";
            this.numFieldWidth.Size = new System.Drawing.Size(100, 26);
            this.numFieldWidth.TabIndex = 0;
            this.numFieldWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblScopeName
            // 
            this.lblScopeName.AutoSize = true;
            this.lblScopeName.Location = new System.Drawing.Point(9, 25);
            this.lblScopeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScopeName.Name = "lblScopeName";
            this.lblScopeName.Size = new System.Drawing.Size(113, 20);
            this.lblScopeName.TabIndex = 3;
            this.lblScopeName.Text = "Not connected";
            // 
            // gbScope
            // 
            this.gbScope.Controls.Add(this.tbCurrentCoordinates);
            this.gbScope.Controls.Add(this.lblCurrentCoordinates);
            this.gbScope.Controls.Add(this.btnDisconnect);
            this.gbScope.Controls.Add(this.lblScopeName);
            this.gbScope.Controls.Add(this.btnConnectMount);
            this.gbScope.Location = new System.Drawing.Point(18, 192);
            this.gbScope.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbScope.Name = "gbScope";
            this.gbScope.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbScope.Size = new System.Drawing.Size(460, 205);
            this.gbScope.TabIndex = 4;
            this.gbScope.TabStop = false;
            this.gbScope.Text = "Scope";
            // 
            // tbCurrentCoordinates
            // 
            this.tbCurrentCoordinates.Location = new System.Drawing.Point(16, 151);
            this.tbCurrentCoordinates.Name = "tbCurrentCoordinates";
            this.tbCurrentCoordinates.ReadOnly = true;
            this.tbCurrentCoordinates.Size = new System.Drawing.Size(426, 26);
            this.tbCurrentCoordinates.TabIndex = 7;
            // 
            // lblCurrentCoordinates
            // 
            this.lblCurrentCoordinates.AutoSize = true;
            this.lblCurrentCoordinates.Location = new System.Drawing.Point(12, 128);
            this.lblCurrentCoordinates.Name = "lblCurrentCoordinates";
            this.lblCurrentCoordinates.Size = new System.Drawing.Size(149, 20);
            this.lblCurrentCoordinates.TabIndex = 6;
            this.lblCurrentCoordinates.Text = "Current coordinates";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(111, 49);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(112, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // gbSolveResult
            // 
            this.gbSolveResult.Controls.Add(this.lblStatus);
            this.gbSolveResult.Controls.Add(this.lblSolvedFileName);
            this.gbSolveResult.Controls.Add(this.tbSolvedCoordinates);
            this.gbSolveResult.Location = new System.Drawing.Point(16, 583);
            this.gbSolveResult.Name = "gbSolveResult";
            this.gbSolveResult.Size = new System.Drawing.Size(460, 146);
            this.gbSolveResult.TabIndex = 5;
            this.gbSolveResult.TabStop = false;
            this.gbSolveResult.Text = "Solve Result";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 98);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status";
            // 
            // lblSolvedFileName
            // 
            this.lblSolvedFileName.AutoSize = true;
            this.lblSolvedFileName.Location = new System.Drawing.Point(12, 35);
            this.lblSolvedFileName.Name = "lblSolvedFileName";
            this.lblSolvedFileName.Size = new System.Drawing.Size(78, 20);
            this.lblSolvedFileName.TabIndex = 10;
            this.lblSolvedFileName.Text = "File name";
            // 
            // tbSolvedCoordinates
            // 
            this.tbSolvedCoordinates.Location = new System.Drawing.Point(14, 58);
            this.tbSolvedCoordinates.Name = "tbSolvedCoordinates";
            this.tbSolvedCoordinates.ReadOnly = true;
            this.tbSolvedCoordinates.Size = new System.Drawing.Size(428, 26);
            this.tbSolvedCoordinates.TabIndex = 9;
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.numExposure);
            this.gbCamera.Controls.Add(this.lblExposure);
            this.gbCamera.Controls.Add(this.btnDisconnectCamera);
            this.gbCamera.Controls.Add(this.lblCameraName);
            this.gbCamera.Controls.Add(this.btnConnectCamera);
            this.gbCamera.Location = new System.Drawing.Point(18, 406);
            this.gbCamera.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCamera.Size = new System.Drawing.Size(460, 169);
            this.gbCamera.TabIndex = 6;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "Camera";
            // 
            // numExposure
            // 
            this.numExposure.Location = new System.Drawing.Point(123, 102);
            this.numExposure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numExposure.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numExposure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numExposure.Name = "numExposure";
            this.numExposure.Size = new System.Drawing.Size(124, 26);
            this.numExposure.TabIndex = 10;
            this.numExposure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblExposure
            // 
            this.lblExposure.AutoSize = true;
            this.lblExposure.Location = new System.Drawing.Point(15, 105);
            this.lblExposure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExposure.Name = "lblExposure";
            this.lblExposure.Size = new System.Drawing.Size(98, 20);
            this.lblExposure.TabIndex = 9;
            this.lblExposure.Text = "Exposure (s)";
            // 
            // btnDisconnectCamera
            // 
            this.btnDisconnectCamera.Enabled = false;
            this.btnDisconnectCamera.Location = new System.Drawing.Point(111, 49);
            this.btnDisconnectCamera.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDisconnectCamera.Name = "btnDisconnectCamera";
            this.btnDisconnectCamera.Size = new System.Drawing.Size(112, 35);
            this.btnDisconnectCamera.TabIndex = 8;
            this.btnDisconnectCamera.Text = "Disconnect";
            this.btnDisconnectCamera.UseVisualStyleBackColor = true;
            this.btnDisconnectCamera.Click += new System.EventHandler(this.btnDisconnectCamera_Click);
            // 
            // lblCameraName
            // 
            this.lblCameraName.AutoSize = true;
            this.lblCameraName.Location = new System.Drawing.Point(12, 25);
            this.lblCameraName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCameraName.Name = "lblCameraName";
            this.lblCameraName.Size = new System.Drawing.Size(113, 20);
            this.lblCameraName.TabIndex = 7;
            this.lblCameraName.Text = "Not connected";
            // 
            // btnConnectCamera
            // 
            this.btnConnectCamera.Location = new System.Drawing.Point(14, 49);
            this.btnConnectCamera.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectCamera.Name = "btnConnectCamera";
            this.btnConnectCamera.Size = new System.Drawing.Size(88, 35);
            this.btnConnectCamera.TabIndex = 6;
            this.btnConnectCamera.Text = "Connect mount";
            this.btnConnectCamera.UseVisualStyleBackColor = true;
            this.btnConnectCamera.Click += new System.EventHandler(this.btnConnectCamera_Click);
            // 
            // btnShotSolveSync
            // 
            this.btnShotSolveSync.Enabled = false;
            this.btnShotSolveSync.Location = new System.Drawing.Point(284, 760);
            this.btnShotSolveSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShotSolveSync.Name = "btnShotSolveSync";
            this.btnShotSolveSync.Size = new System.Drawing.Size(194, 35);
            this.btnShotSolveSync.TabIndex = 7;
            this.btnShotSolveSync.Text = "Shot - Solve- Sync";
            this.btnShotSolveSync.UseVisualStyleBackColor = true;
            this.btnShotSolveSync.Click += new System.EventHandler(this.btnShotSolveSync_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(364, 805);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(112, 35);
            this.btnAbort.TabIndex = 8;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(498, 857);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnShotSolveSync);
            this.Controls.Add(this.gbCamera);
            this.Controls.Add(this.gbSolveResult);
            this.Controls.Add(this.gbScope);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnOpenSolveAndSync);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "fmMain";
            this.Text = "Plate Solver Wrapper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSearchTiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldWidth)).EndInit();
            this.gbScope.ResumeLayout(false);
            this.gbScope.PerformLayout();
            this.gbSolveResult.ResumeLayout(false);
            this.gbSolveResult.PerformLayout();
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExposure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectMount;
        private System.Windows.Forms.Button btnOpenSolveAndSync;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label lbField;
        private System.Windows.Forms.NumericUpDown numFieldHeight;
        private System.Windows.Forms.NumericUpDown numFieldWidth;
        private System.Windows.Forms.Button btnBrowseSolver;
        private System.Windows.Forms.TextBox tbPlateSolverPath;
        private System.Windows.Forms.Label lblSolverPath;
        private System.Windows.Forms.NumericUpDown numSearchTiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScopeName;
        private System.Windows.Forms.GroupBox gbScope;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox tbCurrentCoordinates;
        private System.Windows.Forms.Label lblCurrentCoordinates;
        private System.Windows.Forms.GroupBox gbSolveResult;
        private System.Windows.Forms.TextBox tbSolvedCoordinates;
        private System.Windows.Forms.Label lblSolvedFileName;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.Button btnDisconnectCamera;
        private System.Windows.Forms.Label lblCameraName;
        private System.Windows.Forms.Button btnConnectCamera;
        private System.Windows.Forms.NumericUpDown numExposure;
        private System.Windows.Forms.Label lblExposure;
        private System.Windows.Forms.Button btnShotSolveSync;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnAbort;
    }
}

