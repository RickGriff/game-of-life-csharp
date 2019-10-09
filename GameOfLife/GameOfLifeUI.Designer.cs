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
			this.dataGridView.Location = new System.Drawing.Point(190, 63);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersWidth = 62;
			this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView.RowTemplate.Height = 28;
			this.dataGridView.Size = new System.Drawing.Size(555, 562);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
			this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 1000;
			// 
			// cyclesLabel
			// 
			this.cyclesLabel.AutoSize = true;
			this.cyclesLabel.Location = new System.Drawing.Point(814, 63);
			this.cyclesLabel.Name = "cyclesLabel";
			this.cyclesLabel.Size = new System.Drawing.Size(72, 20);
			this.cyclesLabel.TabIndex = 1;
			this.cyclesLabel.Text = "Cycles: 0";
			this.cyclesLabel.Click += new System.EventHandler(this.cyclesLabel_Click);
			// 
			// startGameButton
			// 
			this.startGameButton.Location = new System.Drawing.Point(31, 173);
			this.startGameButton.Name = "startGameButton";
			this.startGameButton.Size = new System.Drawing.Size(133, 47);
			this.startGameButton.TabIndex = 2;
			this.startGameButton.Text = "Start Game";
			this.startGameButton.UseVisualStyleBackColor = true;
			this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
			// 
			// selectCellsButton
			// 
			this.selectCellsButton.Location = new System.Drawing.Point(31, 266);
			this.selectCellsButton.Name = "selectCellsButton";
			this.selectCellsButton.Size = new System.Drawing.Size(133, 89);
			this.selectCellsButton.TabIndex = 3;
			this.selectCellsButton.Text = "Select Cells";
			this.selectCellsButton.UseVisualStyleBackColor = true;
			this.selectCellsButton.Click += new System.EventHandler(this.selectCellsButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Location = new System.Drawing.Point(773, 173);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(113, 47);
			this.resetButton.TabIndex = 4;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_click);
			// 
			// setRandomCellsButton
			// 
			this.setRandomCellsButton.Location = new System.Drawing.Point(773, 266);
			this.setRandomCellsButton.Name = "setRandomCellsButton";
			this.setRandomCellsButton.Size = new System.Drawing.Size(113, 89);
			this.setRandomCellsButton.TabIndex = 5;
			this.setRandomCellsButton.Text = "Set Random Cells";
			this.setRandomCellsButton.UseVisualStyleBackColor = true;
			this.setRandomCellsButton.Click += new System.EventHandler(this.setRandomCellsButton_Click);
			// 
			// GameOfLifeUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 798);
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
	}
}

