namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// There is no line neither call.
    /// </summary>
    class NoLineAndNoCall : DefaultStateTransition
    {
        /// <summary>
        /// State representing none lines nor calls.
        /// </summary>
        /// <param name="itsForm">Its view form.</param>
        public NoLineAndNoCall(MainForm itsForm)
            : base(itsForm)
        {
            itsForm.DisableHangUpButton();
            itsForm.DisablePickUpButton();
            itsForm.DisableNumkeys();

            itsForm.DisableDNDButton();
            itsForm.DisableHoldButton();
            itsForm.DisableAutoAnswerButton();

            itsForm.DisableKeepAliveBox();
            itsForm.DisableRedialButton();
            itsForm.EnableAdapterSettings();

            itsForm.DisableVideoControlButton();
            itsForm.DisableTransfer();

            itsForm.ItsDisplay.Text = string.Empty;
            itsForm.tbDialNumber.ReadOnly = true;
            itsForm.AcceptOnRegisterButton();
        }

        /// <summary>
        /// If there is neither line nor call, we can't do anything, pushing the button.
        /// </summary>
        public override void PickUpPressed()
        {
            /* If there is neither line nor call, we can't do anything, pushing the button.
             * */
        }

        /// <summary>
        /// If there is neither line nor call, we can't do anything, pushing the button.
        /// </summary>
        public override void HangUpPressed()
        {
            /* If there is neither line nor call, we can't do anything, pushing the button.
             * */
        }

        /// <summary>
        /// It has default behavior.
        /// </summary>
        public override void ActivePhoneLineNumberHasChanged()
        {
            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls == 0 &&
                ItsViewForm.NumberOfActivePhoneLines == 0)
                return;

            /* If has no line at use, reset, the message waiting indication label.
            * */
            ItsViewForm.MWILogic.Reset();

            /* Otherwise alter the state.
             * */
            ChangeState();
        }

        /// <summary>
        /// It has default behavior.
        /// </summary>
        public override void ActivePhoneCallNumberHasChanged()
        {
            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls == 0 &&
                ItsViewForm.NumberOfActivePhoneLines == 0)
                return;

            /* Otherwise alter the state.
             * */
            ChangeState();
        }
    }
}
