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
            this.btnSolveAndSync = new System.Windows.Forms.Button();
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
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblCurrentCoordinates = new System.Windows.Forms.Label();
            this.tbCurrentCoordinates = new System.Windows.Forms.TextBox();
            this.gbSolveResult = new System.Windows.Forms.GroupBox();
            this.tbSolvedCoordinates = new System.Windows.Forms.TextBox();
            this.lblSolvedFileName = new System.Windows.Forms.Label();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSearchTiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldWidth)).BeginInit();
            this.gbScope.SuspendLayout();
            this.gbSolveResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectMount
            // 
            this.btnConnectMount.Location = new System.Drawing.Point(166, 25);
            this.btnConnectMount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectMount.Name = "btnConnectMount";
            this.btnConnectMount.Size = new System.Drawing.Size(88, 35);
            this.btnConnectMount.TabIndex = 0;
            this.btnConnectMount.Text = "Connect mount";
            this.btnConnectMount.UseVisualStyleBackColor = true;
            this.btnConnectMount.Click += new System.EventHandler(this.btnConnectMount_Click);
            // 
            // btnSolveAndSync
            // 
            this.btnSolveAndSync.Enabled = false;
            this.btnSolveAndSync.Location = new System.Drawing.Point(13, 469);
            this.btnSolveAndSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSolveAndSync.Name = "btnSolveAndSync";
            this.btnSolveAndSync.Size = new System.Drawing.Size(204, 35);
            this.btnSolveAndSync.TabIndex = 1;
            this.btnSolveAndSync.Text = "Open - Solve- Sync";
            this.btnSolveAndSync.UseVisualStyleBackColor = true;
            this.btnSolveAndSync.Click += new System.EventHandler(this.button1_Click);
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
            this.gbSettings.Size = new System.Drawing.Size(461, 165);
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
            this.numSearchTiles.ValueChanged += new System.EventHandler(this.numSearchTiles_ValueChanged);
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
            this.numFieldHeight.ValueChanged += new System.EventHandler(this.numFieldHeight_ValueChanged);
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
            this.numFieldWidth.ValueChanged += new System.EventHandler(this.numFieldWidth_ValueChanged);
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
            this.gbScope.Location = new System.Drawing.Point(18, 193);
            this.gbScope.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbScope.Name = "gbScope";
            this.gbScope.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbScope.Size = new System.Drawing.Size(461, 151);
            this.gbScope.TabIndex = 4;
            this.gbScope.TabStop = false;
            this.gbScope.Text = "Scope";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(264, 25);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(112, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblCurrentCoordinates
            // 
            this.lblCurrentCoordinates.AutoSize = true;
            this.lblCurrentCoordinates.Location = new System.Drawing.Point(12, 75);
            this.lblCurrentCoordinates.Name = "lblCurrentCoordinates";
            this.lblCurrentCoordinates.Size = new System.Drawing.Size(149, 20);
            this.lblCurrentCoordinates.TabIndex = 6;
            this.lblCurrentCoordinates.Text = "Current coordinates";
            // 
            // tbCurrentCoordinates
            // 
            this.tbCurrentCoordinates.Location = new System.Drawing.Point(16, 99);
            this.tbCurrentCoordinates.Name = "tbCurrentCoordinates";
            this.tbCurrentCoordinates.ReadOnly = true;
            this.tbCurrentCoordinates.Size = new System.Drawing.Size(426, 26);
            this.tbCurrentCoordinates.TabIndex = 7;
            // 
            // gbSolveResult
            // 
            this.gbSolveResult.Controls.Add(this.lblSolvedFileName);
            this.gbSolveResult.Controls.Add(this.tbSolvedCoordinates);
            this.gbSolveResult.Location = new System.Drawing.Point(18, 362);
            this.gbSolveResult.Name = "gbSolveResult";
            this.gbSolveResult.Size = new System.Drawing.Size(461, 99);
            this.gbSolveResult.TabIndex = 5;
            this.gbSolveResult.TabStop = false;
            this.gbSolveResult.Text = "Solve Result";
            // 
            // tbSolvedCoordinates
            // 
            this.tbSolvedCoordinates.Location = new System.Drawing.Point(13, 58);
            this.tbSolvedCoordinates.Name = "tbSolvedCoordinates";
            this.tbSolvedCoordinates.ReadOnly = true;
            this.tbSolvedCoordinates.Size = new System.Drawing.Size(429, 26);
            this.tbSolvedCoordinates.TabIndex = 9;
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
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 510);
            this.Controls.Add(this.gbSolveResult);
            this.Controls.Add(this.gbScope);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnSolveAndSync);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectMount;
        private System.Windows.Forms.Button btnSolveAndSync;
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
    }
}

