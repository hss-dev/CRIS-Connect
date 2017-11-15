namespace CRISConnectDemo
{
    partial class DemoForm
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
            this.incomingGroupBox = new System.Windows.Forms.GroupBox();
            this.outgoingGroupBox = new System.Windows.Forms.GroupBox();
            this.incomingJsonPayloadGroupBox = new System.Windows.Forms.GroupBox();
            this.incomingImplementationGroupBox = new System.Windows.Forms.GroupBox();
            this.outgoingImplementationGroupBox = new System.Windows.Forms.GroupBox();
            this.showRequestButton = new System.Windows.Forms.Button();
            this.clearDisplayButton = new System.Windows.Forms.Button();
            this.outgoingJsonPayloadGroupBox = new System.Windows.Forms.GroupBox();
            this.sendToCrisReportingButton = new System.Windows.Forms.Button();
            this.incomingTextBox = new System.Windows.Forms.TextBox();
            this.implementationTextBox = new System.Windows.Forms.TextBox();
            this.mapToJsonPayloadTextBox = new System.Windows.Forms.TextBox();
            this.incomingGroupBox.SuspendLayout();
            this.outgoingGroupBox.SuspendLayout();
            this.incomingJsonPayloadGroupBox.SuspendLayout();
            this.incomingImplementationGroupBox.SuspendLayout();
            this.outgoingImplementationGroupBox.SuspendLayout();
            this.outgoingJsonPayloadGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // incomingGroupBox
            // 
            this.incomingGroupBox.Controls.Add(this.incomingImplementationGroupBox);
            this.incomingGroupBox.Controls.Add(this.incomingJsonPayloadGroupBox);
            this.incomingGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incomingGroupBox.Location = new System.Drawing.Point(12, 12);
            this.incomingGroupBox.Name = "incomingGroupBox";
            this.incomingGroupBox.Size = new System.Drawing.Size(464, 622);
            this.incomingGroupBox.TabIndex = 0;
            this.incomingGroupBox.TabStop = false;
            this.incomingGroupBox.Text = "Incoming";
            // 
            // outgoingGroupBox
            // 
            this.outgoingGroupBox.Controls.Add(this.outgoingImplementationGroupBox);
            this.outgoingGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outgoingGroupBox.Location = new System.Drawing.Point(482, 12);
            this.outgoingGroupBox.Name = "outgoingGroupBox";
            this.outgoingGroupBox.Size = new System.Drawing.Size(460, 622);
            this.outgoingGroupBox.TabIndex = 1;
            this.outgoingGroupBox.TabStop = false;
            this.outgoingGroupBox.Text = "Outgoing";
            // 
            // incomingJsonPayloadGroupBox
            // 
            this.incomingJsonPayloadGroupBox.Controls.Add(this.incomingTextBox);
            this.incomingJsonPayloadGroupBox.Location = new System.Drawing.Point(6, 51);
            this.incomingJsonPayloadGroupBox.Name = "incomingJsonPayloadGroupBox";
            this.incomingJsonPayloadGroupBox.Size = new System.Drawing.Size(452, 233);
            this.incomingJsonPayloadGroupBox.TabIndex = 0;
            this.incomingJsonPayloadGroupBox.TabStop = false;
            this.incomingJsonPayloadGroupBox.Text = "JSON Payload";
            // 
            // incomingImplementationGroupBox
            // 
            this.incomingImplementationGroupBox.Controls.Add(this.implementationTextBox);
            this.incomingImplementationGroupBox.Location = new System.Drawing.Point(6, 290);
            this.incomingImplementationGroupBox.Name = "incomingImplementationGroupBox";
            this.incomingImplementationGroupBox.Size = new System.Drawing.Size(452, 326);
            this.incomingImplementationGroupBox.TabIndex = 1;
            this.incomingImplementationGroupBox.TabStop = false;
            this.incomingImplementationGroupBox.Text = "Implementation";
            // 
            // outgoingImplementationGroupBox
            // 
            this.outgoingImplementationGroupBox.Controls.Add(this.sendToCrisReportingButton);
            this.outgoingImplementationGroupBox.Controls.Add(this.outgoingJsonPayloadGroupBox);
            this.outgoingImplementationGroupBox.Controls.Add(this.clearDisplayButton);
            this.outgoingImplementationGroupBox.Controls.Add(this.showRequestButton);
            this.outgoingImplementationGroupBox.Location = new System.Drawing.Point(6, 51);
            this.outgoingImplementationGroupBox.Name = "outgoingImplementationGroupBox";
            this.outgoingImplementationGroupBox.Size = new System.Drawing.Size(448, 565);
            this.outgoingImplementationGroupBox.TabIndex = 0;
            this.outgoingImplementationGroupBox.TabStop = false;
            this.outgoingImplementationGroupBox.Text = "Implementation";
            // 
            // showRequestButton
            // 
            this.showRequestButton.Location = new System.Drawing.Point(6, 32);
            this.showRequestButton.Name = "showRequestButton";
            this.showRequestButton.Size = new System.Drawing.Size(436, 40);
            this.showRequestButton.TabIndex = 0;
            this.showRequestButton.Text = "Show Request";
            this.showRequestButton.UseVisualStyleBackColor = true;
            this.showRequestButton.Click += new System.EventHandler(this.showRequestButton_Click);
            // 
            // clearDisplayButton
            // 
            this.clearDisplayButton.Location = new System.Drawing.Point(6, 78);
            this.clearDisplayButton.Name = "clearDisplayButton";
            this.clearDisplayButton.Size = new System.Drawing.Size(436, 40);
            this.clearDisplayButton.TabIndex = 1;
            this.clearDisplayButton.Text = "Clear Display";
            this.clearDisplayButton.UseVisualStyleBackColor = true;
            this.clearDisplayButton.Click += new System.EventHandler(this.clearDisplayButton_Click);
            // 
            // outgoingJsonPayloadGroupBox
            // 
            this.outgoingJsonPayloadGroupBox.Controls.Add(this.mapToJsonPayloadTextBox);
            this.outgoingJsonPayloadGroupBox.Location = new System.Drawing.Point(6, 139);
            this.outgoingJsonPayloadGroupBox.Name = "outgoingJsonPayloadGroupBox";
            this.outgoingJsonPayloadGroupBox.Size = new System.Drawing.Size(436, 298);
            this.outgoingJsonPayloadGroupBox.TabIndex = 2;
            this.outgoingJsonPayloadGroupBox.TabStop = false;
            this.outgoingJsonPayloadGroupBox.Text = "Map to JSON Payload";
            // 
            // sendToCrisReportingButton
            // 
            this.sendToCrisReportingButton.Location = new System.Drawing.Point(6, 519);
            this.sendToCrisReportingButton.Name = "sendToCrisReportingButton";
            this.sendToCrisReportingButton.Size = new System.Drawing.Size(436, 40);
            this.sendToCrisReportingButton.TabIndex = 3;
            this.sendToCrisReportingButton.Text = "Send to CRIS Reporting";
            this.sendToCrisReportingButton.UseVisualStyleBackColor = true;
            this.sendToCrisReportingButton.Click += new System.EventHandler(this.sendToCrisReportingButton_Click);
            // 
            // incomingTextBox
            // 
            this.incomingTextBox.Enabled = false;
            this.incomingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incomingTextBox.Location = new System.Drawing.Point(6, 25);
            this.incomingTextBox.Multiline = true;
            this.incomingTextBox.Name = "incomingTextBox";
            this.incomingTextBox.Size = new System.Drawing.Size(440, 202);
            this.incomingTextBox.TabIndex = 0;
            // 
            // implementationTextBox
            // 
            this.implementationTextBox.Enabled = false;
            this.implementationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.implementationTextBox.Location = new System.Drawing.Point(6, 25);
            this.implementationTextBox.Multiline = true;
            this.implementationTextBox.Name = "implementationTextBox";
            this.implementationTextBox.Size = new System.Drawing.Size(440, 295);
            this.implementationTextBox.TabIndex = 0;
            // 
            // mapToJsonPayloadTextBox
            // 
            this.mapToJsonPayloadTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapToJsonPayloadTextBox.Location = new System.Drawing.Point(6, 25);
            this.mapToJsonPayloadTextBox.Multiline = true;
            this.mapToJsonPayloadTextBox.Name = "mapToJsonPayloadTextBox";
            this.mapToJsonPayloadTextBox.Size = new System.Drawing.Size(424, 267);
            this.mapToJsonPayloadTextBox.TabIndex = 0;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 646);
            this.Controls.Add(this.outgoingGroupBox);
            this.Controls.Add(this.incomingGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DemoForm";
            this.Text = "CRIS Connect Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DemoForm_FormClosing);
            this.incomingGroupBox.ResumeLayout(false);
            this.outgoingGroupBox.ResumeLayout(false);
            this.incomingJsonPayloadGroupBox.ResumeLayout(false);
            this.incomingJsonPayloadGroupBox.PerformLayout();
            this.incomingImplementationGroupBox.ResumeLayout(false);
            this.incomingImplementationGroupBox.PerformLayout();
            this.outgoingImplementationGroupBox.ResumeLayout(false);
            this.outgoingJsonPayloadGroupBox.ResumeLayout(false);
            this.outgoingJsonPayloadGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox incomingGroupBox;
        private System.Windows.Forms.GroupBox outgoingGroupBox;
        private System.Windows.Forms.GroupBox incomingImplementationGroupBox;
        private System.Windows.Forms.GroupBox incomingJsonPayloadGroupBox;
        private System.Windows.Forms.GroupBox outgoingImplementationGroupBox;
        private System.Windows.Forms.Button sendToCrisReportingButton;
        private System.Windows.Forms.GroupBox outgoingJsonPayloadGroupBox;
        private System.Windows.Forms.Button clearDisplayButton;
        private System.Windows.Forms.Button showRequestButton;
        private System.Windows.Forms.TextBox implementationTextBox;
        private System.Windows.Forms.TextBox incomingTextBox;
        private System.Windows.Forms.TextBox mapToJsonPayloadTextBox;
    }
}

