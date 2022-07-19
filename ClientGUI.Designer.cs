namespace ClientGUI
{
    partial class GameClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.playerNameTextbox = new System.Windows.Forms.TextBox();
            this.serverTextbox = new System.Windows.Forms.TextBox();
            this.serverLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.heartbeatLabel = new System.Windows.Forms.Label();
            this.incomingPacketsLabel = new System.Windows.Forms.Label();
            this.foodLabel = new System.Windows.Forms.Label();
            this.playerMassLabel = new System.Windows.Forms.Label();
            this.mouseCoordsLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.dataReceivedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerNameLabel.Location = new System.Drawing.Point(265, 230);
            this.playerNameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(206, 45);
            this.playerNameLabel.TabIndex = 0;
            this.playerNameLabel.Text = "Player Name:";
            // 
            // playerNameTextbox
            // 
            this.playerNameTextbox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerNameTextbox.Location = new System.Drawing.Point(493, 225);
            this.playerNameTextbox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.playerNameTextbox.Name = "playerNameTextbox";
            this.playerNameTextbox.PlaceholderText = "Enter your name here!";
            this.playerNameTextbox.Size = new System.Drawing.Size(324, 50);
            this.playerNameTextbox.TabIndex = 1;
            // 
            // serverTextbox
            // 
            this.serverTextbox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.serverTextbox.Location = new System.Drawing.Point(493, 302);
            this.serverTextbox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.serverTextbox.Name = "serverTextbox";
            this.serverTextbox.PlaceholderText = "Enter a server address here!";
            this.serverTextbox.Size = new System.Drawing.Size(324, 50);
            this.serverTextbox.TabIndex = 2;
            this.serverTextbox.Text = "localhost";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.serverLabel.Location = new System.Drawing.Point(265, 307);
            this.serverLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(115, 45);
            this.serverLabel.TabIndex = 3;
            this.serverLabel.Text = "Server:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BackColor = System.Drawing.Color.Red;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(17, 869);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(325, 30);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Messages";
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectButton.Location = new System.Drawing.Point(493, 402);
            this.connectButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(325, 62);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Join Game!";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(1017, 15);
            this.fpsLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(45, 25);
            this.fpsLabel.TabIndex = 6;
            this.fpsLabel.Text = "FPS:";
            // 
            // heartbeatLabel
            // 
            this.heartbeatLabel.AutoSize = true;
            this.heartbeatLabel.Location = new System.Drawing.Point(1017, 55);
            this.heartbeatLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.heartbeatLabel.Name = "heartbeatLabel";
            this.heartbeatLabel.Size = new System.Drawing.Size(54, 25);
            this.heartbeatLabel.TabIndex = 7;
            this.heartbeatLabel.Text = "HPS: ";
            // 
            // incomingPacketsLabel
            // 
            this.incomingPacketsLabel.AutoSize = true;
            this.incomingPacketsLabel.Location = new System.Drawing.Point(1017, 98);
            this.incomingPacketsLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.incomingPacketsLabel.Name = "incomingPacketsLabel";
            this.incomingPacketsLabel.Size = new System.Drawing.Size(107, 25);
            this.incomingPacketsLabel.TabIndex = 8;
            this.incomingPacketsLabel.Text = "Inc Packets: ";
            // 
            // foodLabel
            // 
            this.foodLabel.AutoSize = true;
            this.foodLabel.Location = new System.Drawing.Point(1017, 147);
            this.foodLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.foodLabel.Name = "foodLabel";
            this.foodLabel.Size = new System.Drawing.Size(63, 25);
            this.foodLabel.TabIndex = 9;
            this.foodLabel.Text = "Food: ";
            // 
            // playerMassLabel
            // 
            this.playerMassLabel.AutoSize = true;
            this.playerMassLabel.Location = new System.Drawing.Point(1017, 207);
            this.playerMassLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.playerMassLabel.Name = "playerMassLabel";
            this.playerMassLabel.Size = new System.Drawing.Size(100, 25);
            this.playerMassLabel.TabIndex = 10;
            this.playerMassLabel.Text = "Mass/Size: ";
            // 
            // mouseCoordsLabel
            // 
            this.mouseCoordsLabel.AutoSize = true;
            this.mouseCoordsLabel.Location = new System.Drawing.Point(1017, 260);
            this.mouseCoordsLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.mouseCoordsLabel.Name = "mouseCoordsLabel";
            this.mouseCoordsLabel.Size = new System.Drawing.Size(70, 25);
            this.mouseCoordsLabel.TabIndex = 11;
            this.mouseCoordsLabel.Text = "Mouse:";
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(1017, 302);
            this.positionLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(84, 25);
            this.positionLabel.TabIndex = 12;
            this.positionLabel.Text = "Position: ";
            // 
            // dataReceivedLabel
            // 
            this.dataReceivedLabel.AutoSize = true;
            this.dataReceivedLabel.Location = new System.Drawing.Point(1017, 370);
            this.dataReceivedLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.dataReceivedLabel.Name = "dataReceivedLabel";
            this.dataReceivedLabel.Size = new System.Drawing.Size(132, 25);
            this.dataReceivedLabel.TabIndex = 13;
            this.dataReceivedLabel.Text = "Data Recieved: ";
            // 
            // GameClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1405, 928);
            this.Controls.Add(this.dataReceivedLabel);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.mouseCoordsLabel);
            this.Controls.Add(this.playerMassLabel);
            this.Controls.Add(this.foodLabel);
            this.Controls.Add(this.incomingPacketsLabel);
            this.Controls.Add(this.heartbeatLabel);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.serverTextbox);
            this.Controls.Add(this.playerNameTextbox);
            this.Controls.Add(this.playerNameLabel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "GameClientForm";
            this.Text = "Agar.io 2";
            this.Paint += Draw_Scene;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label playerNameLabel;
        private TextBox playerNameTextbox;
        private TextBox serverTextbox;
        private Label serverLabel;
        private TextBox textBox1;
        private Button connectButton;
        private Label fpsLabel;
        private Label heartbeatLabel;
        private Label incomingPacketsLabel;
        private Label foodLabel;
        private Label playerMassLabel;
        private Label mouseCoordsLabel;
        private Label positionLabel;
        private Label dataReceivedLabel;
    }
}