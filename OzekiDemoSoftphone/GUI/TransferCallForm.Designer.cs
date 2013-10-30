namespace OzekiDemoSoftphone
{
    partial class TransferCallForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.radioBtnBlindTransfer = new System.Windows.Forms.RadioButton();
            this.pnlBlindTransfer = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxTransferTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAttendedTransfer = new System.Windows.Forms.Panel();
            this.cmbBoxActiveCalls = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioBtnAttendedTransfer = new System.Windows.Forms.RadioButton();
            this.pnlBlindTransfer.SuspendLayout();
            this.pnlAttendedTransfer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(121, 248);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // radioBtnBlindTransfer
            // 
            this.radioBtnBlindTransfer.AutoSize = true;
            this.radioBtnBlindTransfer.Checked = true;
            this.radioBtnBlindTransfer.Location = new System.Drawing.Point(5, 4);
            this.radioBtnBlindTransfer.Name = "radioBtnBlindTransfer";
            this.radioBtnBlindTransfer.Size = new System.Drawing.Size(86, 17);
            this.radioBtnBlindTransfer.TabIndex = 14;
            this.radioBtnBlindTransfer.TabStop = true;
            this.radioBtnBlindTransfer.Text = "Blind transfer";
            this.radioBtnBlindTransfer.UseVisualStyleBackColor = true;
            this.radioBtnBlindTransfer.CheckedChanged += new System.EventHandler(this.radioBtnBlindTransfer_CheckedChanged);
            // 
            // pnlBlindTransfer
            // 
            this.pnlBlindTransfer.Controls.Add(this.label2);
            this.pnlBlindTransfer.Controls.Add(this.tbxTransferTo);
            this.pnlBlindTransfer.Controls.Add(this.label1);
            this.pnlBlindTransfer.Location = new System.Drawing.Point(5, 27);
            this.pnlBlindTransfer.Name = "pnlBlindTransfer";
            this.pnlBlindTransfer.Size = new System.Drawing.Size(274, 100);
            this.pnlBlindTransfer.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Example: 873 or 873@192.168.112.1";
            // 
            // tbxTransferTo
            // 
            this.tbxTransferTo.Location = new System.Drawing.Point(7, 69);
            this.tbxTransferTo.Name = "tbxTransferTo";
            this.tbxTransferTo.Size = new System.Drawing.Size(256, 20);
            this.tbxTransferTo.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "Please specify the phone number or the SIP URI where you want to transfer the cur" +
    "rent call.";
            // 
            // pnlAttendedTransfer
            // 
            this.pnlAttendedTransfer.Controls.Add(this.cmbBoxActiveCalls);
            this.pnlAttendedTransfer.Controls.Add(this.label3);
            this.pnlAttendedTransfer.Enabled = false;
            this.pnlAttendedTransfer.Location = new System.Drawing.Point(5, 156);
            this.pnlAttendedTransfer.Name = "pnlAttendedTransfer";
            this.pnlAttendedTransfer.Size = new System.Drawing.Size(274, 86);
            this.pnlAttendedTransfer.TabIndex = 16;
            // 
            // cmbBoxActiveCalls
            // 
            this.cmbBoxActiveCalls.FormattingEnabled = true;
            this.cmbBoxActiveCalls.Location = new System.Drawing.Point(7, 49);
            this.cmbBoxActiveCalls.Name = "cmbBoxActiveCalls";
            this.cmbBoxActiveCalls.Size = new System.Drawing.Size(253, 21);
            this.cmbBoxActiveCalls.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(267, 32);
            this.label3.TabIndex = 14;
            this.label3.Text = "Please select the other call where want to transfer the current call.";
            // 
            // radioBtnAttendedTransfer
            // 
            this.radioBtnAttendedTransfer.AutoSize = true;
            this.radioBtnAttendedTransfer.Location = new System.Drawing.Point(5, 134);
            this.radioBtnAttendedTransfer.Name = "radioBtnAttendedTransfer";
            this.radioBtnAttendedTransfer.Size = new System.Drawing.Size(106, 17);
            this.radioBtnAttendedTransfer.TabIndex = 17;
            this.radioBtnAttendedTransfer.Text = "Attended transfer";
            this.radioBtnAttendedTransfer.UseVisualStyleBackColor = true;
            this.radioBtnAttendedTransfer.CheckedChanged += new System.EventHandler(this.radioBtnAttendedTransfer_CheckedChanged);
            // 
            // TransferCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 276);
            this.Controls.Add(this.radioBtnAttendedTransfer);
            this.Controls.Add(this.pnlAttendedTransfer);
            this.Controls.Add(this.pnlBlindTransfer);
            this.Controls.Add(this.radioBtnBlindTransfer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TransferCallForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer Call";
            this.pnlBlindTransfer.ResumeLayout(false);
            this.pnlBlindTransfer.PerformLayout();
            this.pnlAttendedTransfer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton radioBtnBlindTransfer;
        private System.Windows.Forms.Panel pnlBlindTransfer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxTransferTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlAttendedTransfer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioBtnAttendedTransfer;
        private System.Windows.Forms.ComboBox cmbBoxActiveCalls;

    }
}