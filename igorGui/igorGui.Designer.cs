namespace igorGui
{
    partial class igorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(igorForm));
            this.cfgTextBox = new System.Windows.Forms.TextBox();
            this.namesTextBox = new System.Windows.Forms.TextBox();
            this.weightsTextBox = new System.Windows.Forms.TextBox();
            this.cfgFileButton = new System.Windows.Forms.Button();
            this.namesFileButton = new System.Windows.Forms.Button();
            this.weightsFileButton = new System.Windows.Forms.Button();
            this.modelDetailsLabel = new System.Windows.Forms.Label();
            this.numberOfObjectsLabel = new System.Windows.Forms.Label();
            this.processingLabel = new System.Windows.Forms.Label();
            this.modelDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.processButton = new System.Windows.Forms.Button();
            this.cpuLabel = new System.Windows.Forms.Label();
            this.gpuLabel = new System.Windows.Forms.Label();
            this.modelDetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cfgTextBox
            // 
            this.cfgTextBox.Enabled = false;
            this.cfgTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cfgTextBox.Location = new System.Drawing.Point(176, 35);
            this.cfgTextBox.Name = "cfgTextBox";
            this.cfgTextBox.Size = new System.Drawing.Size(364, 24);
            this.cfgTextBox.TabIndex = 0;
            // 
            // namesTextBox
            // 
            this.namesTextBox.Enabled = false;
            this.namesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namesTextBox.Location = new System.Drawing.Point(176, 95);
            this.namesTextBox.Name = "namesTextBox";
            this.namesTextBox.Size = new System.Drawing.Size(364, 24);
            this.namesTextBox.TabIndex = 1;
            // 
            // weightsTextBox
            // 
            this.weightsTextBox.Enabled = false;
            this.weightsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightsTextBox.Location = new System.Drawing.Point(176, 65);
            this.weightsTextBox.Name = "weightsTextBox";
            this.weightsTextBox.Size = new System.Drawing.Size(364, 24);
            this.weightsTextBox.TabIndex = 2;
            // 
            // cfgFileButton
            // 
            this.cfgFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cfgFileButton.Location = new System.Drawing.Point(13, 35);
            this.cfgFileButton.Name = "cfgFileButton";
            this.cfgFileButton.Size = new System.Drawing.Size(144, 24);
            this.cfgFileButton.TabIndex = 3;
            this.cfgFileButton.Text = "Choose Cfg File";
            this.cfgFileButton.UseVisualStyleBackColor = true;
            this.cfgFileButton.Click += new System.EventHandler(this.cfgFileButton_Click);
            // 
            // namesFileButton
            // 
            this.namesFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namesFileButton.Location = new System.Drawing.Point(13, 96);
            this.namesFileButton.Name = "namesFileButton";
            this.namesFileButton.Size = new System.Drawing.Size(144, 24);
            this.namesFileButton.TabIndex = 4;
            this.namesFileButton.Text = "Choose Names File";
            this.namesFileButton.UseVisualStyleBackColor = true;
            this.namesFileButton.Click += new System.EventHandler(this.namesFileButton_Click);
            // 
            // weightsFileButton
            // 
            this.weightsFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightsFileButton.Location = new System.Drawing.Point(13, 66);
            this.weightsFileButton.Name = "weightsFileButton";
            this.weightsFileButton.Size = new System.Drawing.Size(144, 24);
            this.weightsFileButton.TabIndex = 5;
            this.weightsFileButton.Text = "Choose Weights File";
            this.weightsFileButton.UseVisualStyleBackColor = true;
            this.weightsFileButton.Click += new System.EventHandler(this.weightsFileButton_Click);
            // 
            // modelDetailsLabel
            // 
            this.modelDetailsLabel.AutoSize = true;
            this.modelDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelDetailsLabel.Location = new System.Drawing.Point(10, 15);
            this.modelDetailsLabel.Name = "modelDetailsLabel";
            this.modelDetailsLabel.Size = new System.Drawing.Size(157, 20);
            this.modelDetailsLabel.TabIndex = 6;
            this.modelDetailsLabel.Text = "YOLO Model Details:";
            // 
            // numberOfObjectsLabel
            // 
            this.numberOfObjectsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.numberOfObjectsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberOfObjectsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfObjectsLabel.Location = new System.Drawing.Point(13, 134);
            this.numberOfObjectsLabel.Name = "numberOfObjectsLabel";
            this.numberOfObjectsLabel.Size = new System.Drawing.Size(527, 23);
            this.numberOfObjectsLabel.TabIndex = 7;
            this.numberOfObjectsLabel.Text = "The selected model will code images for XX different object classes.";
            this.numberOfObjectsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // processingLabel
            // 
            this.processingLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.processingLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.processingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processingLabel.Location = new System.Drawing.Point(186, 216);
            this.processingLabel.Name = "processingLabel";
            this.processingLabel.Size = new System.Drawing.Size(467, 29);
            this.processingLabel.TabIndex = 8;
            this.processingLabel.Text = "Processing: ";
            this.processingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modelDetailsGroupBox
            // 
            this.modelDetailsGroupBox.Controls.Add(this.weightsTextBox);
            this.modelDetailsGroupBox.Controls.Add(this.numberOfObjectsLabel);
            this.modelDetailsGroupBox.Controls.Add(this.cfgTextBox);
            this.modelDetailsGroupBox.Controls.Add(this.namesTextBox);
            this.modelDetailsGroupBox.Controls.Add(this.weightsFileButton);
            this.modelDetailsGroupBox.Controls.Add(this.cfgFileButton);
            this.modelDetailsGroupBox.Controls.Add(this.namesFileButton);
            this.modelDetailsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelDetailsGroupBox.Location = new System.Drawing.Point(10, 12);
            this.modelDetailsGroupBox.Name = "modelDetailsGroupBox";
            this.modelDetailsGroupBox.Size = new System.Drawing.Size(556, 182);
            this.modelDetailsGroupBox.TabIndex = 9;
            this.modelDetailsGroupBox.TabStop = false;
            this.modelDetailsGroupBox.Text = "YOLO Model Details";
            // 
            // processButton
            // 
            this.processButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processButton.Location = new System.Drawing.Point(23, 211);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(144, 40);
            this.processButton.TabIndex = 8;
            this.processButton.Text = "Process Images!";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // cpuLabel
            // 
            this.cpuLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cpuLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuLabel.Location = new System.Drawing.Point(585, 36);
            this.cpuLabel.Name = "cpuLabel";
            this.cpuLabel.Size = new System.Drawing.Size(68, 61);
            this.cpuLabel.TabIndex = 10;
            this.cpuLabel.Text = "CPU";
            this.cpuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpuLabel
            // 
            this.gpuLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gpuLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gpuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpuLabel.Location = new System.Drawing.Point(585, 117);
            this.gpuLabel.Name = "gpuLabel";
            this.gpuLabel.Size = new System.Drawing.Size(68, 61);
            this.gpuLabel.TabIndex = 11;
            this.gpuLabel.Text = "GPU";
            this.gpuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // igorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 266);
            this.Controls.Add(this.gpuLabel);
            this.Controls.Add(this.cpuLabel);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.processingLabel);
            this.Controls.Add(this.modelDetailsLabel);
            this.Controls.Add(this.modelDetailsGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "igorForm";
            this.Text = "Igor GUI: \"The Third Switch\" Edition";
            this.modelDetailsGroupBox.ResumeLayout(false);
            this.modelDetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cfgTextBox;
        private System.Windows.Forms.TextBox namesTextBox;
        private System.Windows.Forms.TextBox weightsTextBox;
        private System.Windows.Forms.Button cfgFileButton;
        private System.Windows.Forms.Button namesFileButton;
        private System.Windows.Forms.Button weightsFileButton;
        private System.Windows.Forms.Label modelDetailsLabel;
        private System.Windows.Forms.Label numberOfObjectsLabel;
        private System.Windows.Forms.Label processingLabel;
        private System.Windows.Forms.GroupBox modelDetailsGroupBox;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.Label cpuLabel;
        private System.Windows.Forms.Label gpuLabel;
    }
}

