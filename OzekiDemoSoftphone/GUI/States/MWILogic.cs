using System.Windows.Forms;
using Ozeki.VoIP.MessageSummary;
using System.Drawing;

namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// Logic for Message Waiting indication.
    /// </summary>
    sealed class MWILogic
    {
        private Label lblMessageWaitingIndication;
        private Color DefaultColor;

        /// <summary>
        /// Constructs an instance of Message Waiting Indication.
        /// </summary>
        /// <param name="lblMWI">The label of the mwi message</param>
        /// <param name="defaultColor">The default background color</param>
        public MWILogic(Label lblMWI, Color defaultColor)
        {
            DefaultColor = defaultColor;
            lblMessageWaitingIndication = lblMWI;
        }

        /// <summary>
        /// Prints the 'No message information text'.
        /// </summary>
        public void Reset()
        {
            lblMessageWaitingIndication.Text = "No message information";
            lblMessageWaitingIndication.BackColor = DefaultColor;
            lblMessageWaitingIndication.ForeColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Changes text of label according to the information provided by the message summary object.
        /// </summary>
        /// <param name="mwi"></param>
        public void ChangeState(VoIPMessageSummary mwi)
        {
            if (!mwi.MessageWaiting)
            {
                NoMessage();
                return;
            }

            int s = 0;
            foreach (var x in mwi.MessageSummaryLines)
                s += x.NewMessages + x.OldMessages;

            if (s == 0)
            {
                NoMessage();
                return;
            }

            NewMessage(s);
        }

        private void NoMessage()
        {
            lblMessageWaitingIndication.Text = "No message";
            lblMessageWaitingIndication.BackColor = DefaultColor;
            lblMessageWaitingIndication.ForeColor = System.Drawing.Color.Black;
        }

        private void NewMessage(int x)
        {
            lblMessageWaitingIndication.Text = x == 1 ? "Unread message" : "Unread messages (" + x + ")";
            lblMessageWaitingIndication.BackColor = System.Drawing.Color.White;
            lblMessageWaitingIndication.ForeColor = System.Drawing.Color.Blue;
        }
    }
}
