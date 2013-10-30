using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace OzekiDemoSoftphone.GUI
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            //this.Text = String.Format("About {0}", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                string v = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return v.Substring(0,v.LastIndexOf("."));
            }
        }

        public string BulildVersion
        {
            get
            {
                string v = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return v.Substring(v.LastIndexOf(".")+1);
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        
        private void Bt_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            Rtb_About.LinkPattern = @"((([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                            +
                            @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                            +
                            @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4}))|www.\w+\.\w+|http://\w+\.[\w+,\,\-,+,_,\.,/]+";

            Rtb_About.Text = AssemblyProduct +
                             @"

Version: " + AssemblyVersion + @"
Build version: " + BulildVersion +
                             @"

This software is the copyrighted work of Ozeki.
Use of the software is governed by the terms of the end user license agreement, which is included in the software.

Internet: http://www.voip-sip-sdk.com
E-mail: info@ozeki.hu

© Copyright 2000-2012. Ozeki Informatics Ltd. All rights reserved.";

        }

        private void Rtb_About_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText.Contains("@"))
                Process.Start("mailto:"+e.LinkText);
            else
                Process.Start(e.LinkText);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
