using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OzekiDemoSoftphone.Softphone;

namespace OzekiDemoSoftphone.GUI
{
    public partial class ForwardCallForm : Form
    {

        private SoftphoneEngine softphoneEngine;

        public ForwardCallForm(SoftphoneEngine softphoneEngine)
        {
            this.softphoneEngine = softphoneEngine;
            InitializeComponent();
            tbxForwardTo.Text = softphoneEngine.ForwardCallTo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            softphoneEngine.Forwarding = false;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxForwardTo.Text))
            {
                MessageBox.Show("Please give a SIP phone number or SIP uri to forward the incoming calls.");
                return;
            }
            softphoneEngine.ForwardCallTo = tbxForwardTo.Text;
            softphoneEngine.Forwarding = true;
            this.Close();
        }
    }
}
