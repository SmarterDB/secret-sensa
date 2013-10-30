namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// State for incomming call.
    /// </summary>
    class IncommingCall : DefaultStateTransition
    {
        /// <summary>
        /// Creates state for incoming call.
        /// </summary>
        /// <param name="itsForm">Its view form.</param>
        public IncommingCall(MainForm itsForm)
            : base(itsForm)
        {
            itsForm.EnablePickUpButton();
            itsForm.EnableHangUpButton();
            itsForm.DisableNumkeys();
            itsForm.DisableAdapterSettings();
            itsForm.DisableTransfer();
            ItsViewForm.tbPhoneStatus.Text = "Incomming call!";
        }

        /// <summary>
        /// Accepts incoming call.
        /// </summary>
        public override void PickUpPressed()
        {
            ItsViewForm.AcceptIncomingCall();
            ChangeState();
            ItsViewForm.ItsDisplay.Text = string.Empty;
        }

        /// <summary>
        /// Rejects the incoming call.
        /// </summary>
        public override void HangUpPressed()
        {
            ItsViewForm.RejectIncomingCall();
            ChangeState();
            ItsViewForm.ItsDisplay.Text = string.Empty;
        }

        /// <summary>
        /// Nothing changes here, the user must decide between reject or accept the incoming call.
        /// </summary>
        public override void ActivePhoneLineNumberHasChanged()
        {
            /* Nothing changes here, the user must decide between reject or accept the incoming call.
             * */
        }

        /// <summary>
        /// Nothing changes here, the user must decide between reject or accept the incoming call.
        /// </summary>
        public override void ActivePhoneCallNumberHasChanged()
        {
            /* Nothing changes here, the user must decide between reject or accept the incoming call.
             * */
        }

    }
}
