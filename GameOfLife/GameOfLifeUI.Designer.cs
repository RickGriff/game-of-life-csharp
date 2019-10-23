namespace GameOfLife
{
	partial class GameOfLifeUI
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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.cyclesLabel = new System.Windows.Forms.Label();
			this.startGameButton = new System.Windows.Forms.Button();
			this.selectCellsButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.setRandomCellsButton = new System.Windows.Forms.Button();
			this.chooseColourListBox = new System.Windows.Forms.ListBox();
			this.chooseColourLabel = new System.Windows.Forms.Label();
			this.chooseGameDropdown = new System.Windows.Forms.ComboBox();
			this.chooseGameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToResizeColumns = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Location = new System.Drawing.Point(362, 77);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersWidth = 62;
			this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView.RowTemplate.Height = 28;
			this.dataGridView.Size = new System.Drawing.Size(600, 600);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 1000;
			// 
			// cyclesLabel
			// 
			this.cyclesLabel.AutoSize = true;
			this.cyclesLabel.Location = new System.Drawing.Point(1116, 54);
			this.cyclesLabel.Name = "cyclesLabel";
			this.cyclesLabel.Size = new System.Drawing.Size(72, 20);
			this.cyclesLabel.TabIndex = 1;
			this.cyclesLabel.Text = "Cycles: 0";
			// 
			// startGameButton
			// 
			this.startGameButton.Location = new System.Drawing.Point(151, 284);
			this.startGameButton.Name = "startGameButton";
			this.startGameButton.Size = new System.Drawing.Size(133, 47);
			this.startGameButton.TabIndex = 2;
			this.startGameButton.Text = "Start Game";
			this.startGameButton.UseVisualStyleBackColor = true;
			this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
			// 
			// selectCellsButton
			// 
			this.selectCellsButton.Location = new System.Drawing.Point(1081, 153);
			this.selectCellsButton.Name = "selectCellsButton";
			this.selectCellsButton.Size = new System.Drawing.Size(133, 89);
			this.selectCellsButton.TabIndex = 3;
			this.selectCellsButton.Text = "Set Custom Cells";
			this.selectCellsButton.UseVisualStyleBackColor = true;
			this.selectCellsButton.Click += new System.EventHandler(this.selectCellsButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Location = new System.Drawing.Point(171, 432);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(113, 47);
			this.resetButton.TabIndex = 4;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_click);
			// 
			// setRandomCellsButton
			// 
			this.setRandomCellsButton.Location = new System.Drawing.Point(1081, 432);
			this.setRandomCellsButton.Name = "setRandomCellsButton";
			this.setRandomCellsButton.Size = new System.Drawing.Size(133, 96);
			this.setRandomCellsButton.TabIndex = 5;
			this.setRandomCellsButton.Text = "Set Random Cells";
			this.setRandomCellsButton.UseVisualStyleBackColor = true;
			this.setRandomCellsButton.Click += new System.EventHandler(this.setRandomCellsButton_Click);
			// 
			// chooseColourListBox
			// 
			this.chooseColourListBox.FormattingEnabled = true;
			this.chooseColourListBox.ItemHeight = 20;
			this.chooseColourListBox.Location = new System.Drawing.Point(1081, 284);
			this.chooseColourListBox.Name = "chooseColourListBox";
			this.chooseColourListBox.Size = new System.Drawing.Size(133, 104);
			this.chooseColourListBox.TabIndex = 6;
			this.chooseColourListBox.SelectedIndexChanged += new System.EventHandler(this.chooseColourListBox_SelectedIndexChanged);
			// 
			// chooseColourLabel
			// 
			this.chooseColourLabel.AutoSize = true;
			this.chooseColourLabel.Location = new System.Drawing.Point(1081, 261);
			this.chooseColourLabel.Name = "chooseColourLabel";
			this.chooseColourLabel.Size = new System.Drawing.Size(107, 20);
			this.chooseColourLabel.TabIndex = 7;
			this.chooseColourLabel.Text = "Choose State";
			// 
			// chooseGameDropdown
			// 
			this.chooseGameDropdown.FormattingEnabled = true;
			this.chooseGameDropdown.Location = new System.Drawing.Point(52, 153);
			this.chooseGameDropdown.Name = "chooseGameDropdown";
			this.chooseGameDropdown.Size = new System.Drawing.Size(232, 28);
			this.chooseGameDropdown.TabIndex = 8;
			this.chooseGameDropdown.SelectedIndexChanged += new System.EventHandler(this.chooseGameDropdown_SelectedIndexChanged);
			// 
			// chooseGameLabel
			// 
			this.chooseGameLabel.AutoSize = true;
			this.chooseGameLabel.Location = new System.Drawing.Point(127, 121);
			this.chooseGameLabel.Name = "chooseGameLabel";
			this.chooseGameLabel.Size = new System.Drawing.Size(157, 20);
			this.chooseGameLabel.TabIndex = 9;
			this.chooseGameLabel.Text = "Choose Game Rules";
			// 
			// GameOfLifeUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1292, 790);
			this.Controls.Add(this.chooseGameLabel);
			this.Controls.Add(this.chooseGameDropdown);
			this.Controls.Add(this.chooseColourLabel);
			this.Controls.Add(this.chooseColourListBox);
			this.Controls.Add(this.setRandomCellsButton);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.selectCellsButton);
			this.Controls.Add(this.startGameButton);
			this.Controls.Add(this.cyclesLabel);
			this.Controls.Add(this.dataGridView);
			this.Name = "GameOfLifeUI";
			this.Text = "Game Of Life";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Label cyclesLabel;
		private System.Windows.Forms.Button startGameButton;
		private System.Windows.Forms.Button selectCellsButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button setRandomCellsButton;
		private System.Windows.Forms.ListBox chooseColourListBox;
		private System.Windows.Forms.Label chooseColourLabel;
		private System.Windows.Forms.ComboBox chooseGameDropdown;
		private System.Windows.Forms.Label chooseGameLabel;
	}
}

