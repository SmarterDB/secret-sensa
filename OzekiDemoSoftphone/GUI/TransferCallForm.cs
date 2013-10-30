using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OzekiDemoSoftphone.PM.Data;
using OzekiDemoSoftphone.GUI;

namespace OzekiDemoSoftphone
{
    public partial class TransferCallForm : Form
    {
        public TransferCallForm(List<PhoneCallInfo> phoneCalls)
        {
            InitializeComponent();

            cmbBoxActiveCalls.DataSource = phoneCalls;
            TransferMode = TransferMode.None;

        }

        public string BlindTransferTarget { get; private set; }
        public PhoneCallInfo AttendedTransferTarget { get; private set; }
        public TransferMode TransferMode { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (pnlBlindTransfer.Enabled)
            {
                if (!string.IsNullOrEmpty(tbxTransferTo.Text))
                {
                    BlindTransferTarget = tbxTransferTo.Text;
                    TransferMode = TransferMode.Blind;
                }

            }
            else
            {
                AttendedTransferTarget = cmbBoxActiveCalls.SelectedItem as PhoneCallInfo;

                if (AttendedTransferTarget != null)
                    TransferMode = TransferMode.Attended;
            }


            Close();
        }

        private void radioBtnBlindTransfer_CheckedChanged(object sender, EventArgs e)
        {
            pnlBlindTransfer.Enabled = radioBtnBlindTransfer.Checked;
            pnlAttendedTransfer.Enabled = !radioBtnBlindTransfer.Checked;
        }

        private void radioBtnAttendedTransfer_CheckedChanged(object sender, EventArgs e)
        {
            pnlBlindTransfer.Enabled = !radioBtnAttendedTransfer.Checked;
            pnlAttendedTransfer.Enabled = radioBtnAttendedTransfer.Checked;
        }




    }
}
