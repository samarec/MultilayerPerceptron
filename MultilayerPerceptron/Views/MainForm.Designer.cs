namespace MultilayerPerceptron
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.learnButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.hiddenLayers = new System.Windows.Forms.NumericUpDown();
            this.hiddenLayersText = new System.Windows.Forms.Label();
            this.firstLayerNeuronsText = new System.Windows.Forms.Label();
            this.firstLayerNeurons = new System.Windows.Forms.NumericUpDown();
            this.secondtLayerNeurons = new System.Windows.Forms.NumericUpDown();
            this.secondtLayerNeuronsText = new System.Windows.Forms.Label();
            this.thirdtLayerNeurons = new System.Windows.Forms.NumericUpDown();
            this.thirdtLayerNeuronsText = new System.Windows.Forms.Label();
            this.learningSpeedText = new System.Windows.Forms.Label();
            this.learningCycles = new System.Windows.Forms.NumericUpDown();
            this.learningCyclesText = new System.Windows.Forms.Label();
            this.learningSpeed = new System.Windows.Forms.NumericUpDown();
            this.minimalError = new System.Windows.Forms.NumericUpDown();
            this.minimalErrorText = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.learningDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmText = new System.Windows.Forms.Label();
            this.algorithmBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.hiddenLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstLayerNeurons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondtLayerNeurons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdtLayerNeurons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimalError)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // learnButton
            // 
            this.learnButton.Location = new System.Drawing.Point(5, 259);
            this.learnButton.Name = "learnButton";
            this.learnButton.Size = new System.Drawing.Size(77, 23);
            this.learnButton.TabIndex = 0;
            this.learnButton.Text = "Learn";
            this.learnButton.UseVisualStyleBackColor = true;
            this.learnButton.Click += new System.EventHandler(this.learnButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(88, 259);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(77, 23);
            this.testButton.TabIndex = 1;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // hiddenLayers
            // 
            this.hiddenLayers.Location = new System.Drawing.Point(137, 34);
            this.hiddenLayers.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.hiddenLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hiddenLayers.Name = "hiddenLayers";
            this.hiddenLayers.Size = new System.Drawing.Size(66, 20);
            this.hiddenLayers.TabIndex = 2;
            this.hiddenLayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hiddenLayers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hiddenLayers.ValueChanged += new System.EventHandler(this.hiddenLayers_ValueChanged);
            // 
            // hiddenLayersText
            // 
            this.hiddenLayersText.AutoSize = true;
            this.hiddenLayersText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hiddenLayersText.Location = new System.Drawing.Point(12, 36);
            this.hiddenLayersText.Name = "hiddenLayersText";
            this.hiddenLayersText.Size = new System.Drawing.Size(78, 13);
            this.hiddenLayersText.TabIndex = 3;
            this.hiddenLayersText.Text = "Hidden Layers:";
            // 
            // firstLayerNeuronsText
            // 
            this.firstLayerNeuronsText.AutoSize = true;
            this.firstLayerNeuronsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstLayerNeuronsText.Location = new System.Drawing.Point(12, 63);
            this.firstLayerNeuronsText.Name = "firstLayerNeuronsText";
            this.firstLayerNeuronsText.Size = new System.Drawing.Size(101, 13);
            this.firstLayerNeuronsText.TabIndex = 4;
            this.firstLayerNeuronsText.Text = "First Layer Neurons:";
            // 
            // firstLayerNeurons
            // 
            this.firstLayerNeurons.Location = new System.Drawing.Point(137, 60);
            this.firstLayerNeurons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.firstLayerNeurons.Name = "firstLayerNeurons";
            this.firstLayerNeurons.Size = new System.Drawing.Size(66, 20);
            this.firstLayerNeurons.TabIndex = 5;
            this.firstLayerNeurons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.firstLayerNeurons.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // secondtLayerNeurons
            // 
            this.secondtLayerNeurons.Location = new System.Drawing.Point(137, 87);
            this.secondtLayerNeurons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.secondtLayerNeurons.Name = "secondtLayerNeurons";
            this.secondtLayerNeurons.Size = new System.Drawing.Size(66, 20);
            this.secondtLayerNeurons.TabIndex = 7;
            this.secondtLayerNeurons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.secondtLayerNeurons.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.secondtLayerNeurons.Visible = false;
            // 
            // secondtLayerNeuronsText
            // 
            this.secondtLayerNeuronsText.AutoSize = true;
            this.secondtLayerNeuronsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondtLayerNeuronsText.Location = new System.Drawing.Point(12, 89);
            this.secondtLayerNeuronsText.Name = "secondtLayerNeuronsText";
            this.secondtLayerNeuronsText.Size = new System.Drawing.Size(119, 13);
            this.secondtLayerNeuronsText.TabIndex = 6;
            this.secondtLayerNeuronsText.Text = "Second Layer Neurons:";
            this.secondtLayerNeuronsText.Visible = false;
            // 
            // thirdtLayerNeurons
            // 
            this.thirdtLayerNeurons.Location = new System.Drawing.Point(137, 113);
            this.thirdtLayerNeurons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thirdtLayerNeurons.Name = "thirdtLayerNeurons";
            this.thirdtLayerNeurons.Size = new System.Drawing.Size(66, 20);
            this.thirdtLayerNeurons.TabIndex = 9;
            this.thirdtLayerNeurons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.thirdtLayerNeurons.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thirdtLayerNeurons.Visible = false;
            // 
            // thirdtLayerNeuronsText
            // 
            this.thirdtLayerNeuronsText.AutoSize = true;
            this.thirdtLayerNeuronsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thirdtLayerNeuronsText.Location = new System.Drawing.Point(12, 115);
            this.thirdtLayerNeuronsText.Name = "thirdtLayerNeuronsText";
            this.thirdtLayerNeuronsText.Size = new System.Drawing.Size(106, 13);
            this.thirdtLayerNeuronsText.TabIndex = 8;
            this.thirdtLayerNeuronsText.Text = "Third Layer Neurons:";
            this.thirdtLayerNeuronsText.Visible = false;
            // 
            // learningSpeedText
            // 
            this.learningSpeedText.AutoSize = true;
            this.learningSpeedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.learningSpeedText.Location = new System.Drawing.Point(12, 144);
            this.learningSpeedText.Name = "learningSpeedText";
            this.learningSpeedText.Size = new System.Drawing.Size(85, 13);
            this.learningSpeedText.TabIndex = 10;
            this.learningSpeedText.Text = "Learning Speed:";
            // 
            // learningCycles
            // 
            this.learningCycles.Location = new System.Drawing.Point(137, 173);
            this.learningCycles.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.learningCycles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.learningCycles.Name = "learningCycles";
            this.learningCycles.Size = new System.Drawing.Size(66, 20);
            this.learningCycles.TabIndex = 13;
            this.learningCycles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.learningCycles.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // learningCyclesText
            // 
            this.learningCyclesText.AutoSize = true;
            this.learningCyclesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.learningCyclesText.Location = new System.Drawing.Point(12, 175);
            this.learningCyclesText.Name = "learningCyclesText";
            this.learningCyclesText.Size = new System.Drawing.Size(85, 13);
            this.learningCyclesText.TabIndex = 12;
            this.learningCyclesText.Text = "Learning Cycles:";
            // 
            // learningSpeed
            // 
            this.learningSpeed.DecimalPlaces = 3;
            this.learningSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            196608});
            this.learningSpeed.Location = new System.Drawing.Point(137, 142);
            this.learningSpeed.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.learningSpeed.Name = "learningSpeed";
            this.learningSpeed.Size = new System.Drawing.Size(66, 20);
            this.learningSpeed.TabIndex = 14;
            this.learningSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.learningSpeed.Value = new decimal(new int[] {
            20,
            0,
            0,
            196608});
            // 
            // minimalError
            // 
            this.minimalError.DecimalPlaces = 3;
            this.minimalError.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.minimalError.Location = new System.Drawing.Point(137, 201);
            this.minimalError.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minimalError.Name = "minimalError";
            this.minimalError.Size = new System.Drawing.Size(66, 20);
            this.minimalError.TabIndex = 17;
            this.minimalError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minimalError.Value = new decimal(new int[] {
            10,
            0,
            0,
            196608});
            // 
            // minimalErrorText
            // 
            this.minimalErrorText.AutoSize = true;
            this.minimalErrorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minimalErrorText.Location = new System.Drawing.Point(12, 203);
            this.minimalErrorText.Name = "minimalErrorText";
            this.minimalErrorText.Size = new System.Drawing.Size(70, 13);
            this.minimalErrorText.TabIndex = 16;
            this.minimalErrorText.Text = "Minimal Error:";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(222, 34);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(274, 248);
            this.outputBox.TabIndex = 18;
            this.outputBox.Text = "";
            this.outputBox.TextChanged += new System.EventHandler(this.outputBox_TextChanged);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(171, 259);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(45, 23);
            this.clearButton.TabIndex = 19;
            this.clearButton.Text = "X";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.learningDataToolStripMenuItem,
            this.testingDataToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(508, 24);
            this.mainMenu.TabIndex = 20;
            this.mainMenu.Text = "mainMenu";
            // 
            // learningDataToolStripMenuItem
            // 
            this.learningDataToolStripMenuItem.Name = "learningDataToolStripMenuItem";
            this.learningDataToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.learningDataToolStripMenuItem.Text = "Learning Data";
            this.learningDataToolStripMenuItem.Click += new System.EventHandler(this.learningDataToolStripMenuItem_Click);
            // 
            // testingDataToolStripMenuItem
            // 
            this.testingDataToolStripMenuItem.Name = "testingDataToolStripMenuItem";
            this.testingDataToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.testingDataToolStripMenuItem.Text = "Testing Data";
            this.testingDataToolStripMenuItem.Click += new System.EventHandler(this.testingDataToolStripMenuItem_Click);
            // 
            // algorithmText
            // 
            this.algorithmText.AutoSize = true;
            this.algorithmText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.algorithmText.Location = new System.Drawing.Point(12, 233);
            this.algorithmText.Name = "algorithmText";
            this.algorithmText.Size = new System.Drawing.Size(53, 13);
            this.algorithmText.TabIndex = 21;
            this.algorithmText.Text = "Algorithm:";
            // 
            // algorithmBox
            // 
            this.algorithmBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmBox.FormattingEnabled = true;
            this.algorithmBox.Items.AddRange(new object[] {
            "BackPropagation",
            "QuickProp"});
            this.algorithmBox.Location = new System.Drawing.Point(82, 230);
            this.algorithmBox.Name = "algorithmBox";
            this.algorithmBox.Size = new System.Drawing.Size(121, 21);
            this.algorithmBox.TabIndex = 22;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 294);
            this.Controls.Add(this.algorithmBox);
            this.Controls.Add(this.algorithmText);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.minimalError);
            this.Controls.Add(this.minimalErrorText);
            this.Controls.Add(this.learningSpeed);
            this.Controls.Add(this.learningCycles);
            this.Controls.Add(this.learningCyclesText);
            this.Controls.Add(this.learningSpeedText);
            this.Controls.Add(this.thirdtLayerNeurons);
            this.Controls.Add(this.thirdtLayerNeuronsText);
            this.Controls.Add(this.secondtLayerNeurons);
            this.Controls.Add(this.secondtLayerNeuronsText);
            this.Controls.Add(this.firstLayerNeurons);
            this.Controls.Add(this.firstLayerNeuronsText);
            this.Controls.Add(this.hiddenLayersText);
            this.Controls.Add(this.hiddenLayers);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.learnButton);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MultilayerPerceptron";
            ((System.ComponentModel.ISupportInitialize)(this.hiddenLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstLayerNeurons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondtLayerNeurons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdtLayerNeurons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningCycles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimalError)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button learnButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.NumericUpDown hiddenLayers;
        private System.Windows.Forms.Label hiddenLayersText;
        private System.Windows.Forms.Label firstLayerNeuronsText;
        private System.Windows.Forms.NumericUpDown firstLayerNeurons;
        private System.Windows.Forms.NumericUpDown secondtLayerNeurons;
        private System.Windows.Forms.Label secondtLayerNeuronsText;
        private System.Windows.Forms.NumericUpDown thirdtLayerNeurons;
        private System.Windows.Forms.Label thirdtLayerNeuronsText;
        private System.Windows.Forms.Label learningSpeedText;
        private System.Windows.Forms.NumericUpDown learningCycles;
        private System.Windows.Forms.Label learningCyclesText;
        private System.Windows.Forms.NumericUpDown learningSpeed;
        private System.Windows.Forms.NumericUpDown minimalError;
        private System.Windows.Forms.Label minimalErrorText;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem learningDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingDataToolStripMenuItem;
        private System.Windows.Forms.Label algorithmText;
        private System.Windows.Forms.ComboBox algorithmBox;
    }
}

