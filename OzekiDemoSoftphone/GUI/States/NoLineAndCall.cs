namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// There is no usable phone line, and there are some ongoing calls.
    /// </summary>
    class NoLineAndCall : DefaultStateTransition
    {
        /// <summary>
        /// State representing none lines and multiple calls.
        /// </summary>
        /// <param name="itsForm">Its form view.</param>
        public NoLineAndCall(MainForm itsForm)
            : base(itsForm)
        {
            itsForm.EnableHangUpButton();
            itsForm.DisableNumkeys();
            itsForm.DisablePickUpButton();

            itsForm.DisableDNDButton();
            itsForm.EnableHoldButton(false);
            itsForm.DisableAutoAnswerButton();

            itsForm.DisableKeepAliveBox();
            itsForm.EnableRedialButton();
            itsForm.DisableAdapterSettings();

            itsForm.DisableTransfer();
            itsForm.ItsDisplay.Text = string.Empty;

            SetActivePhoneCallToFirstOne();
            itsForm.tbDialNumber.ReadOnly = true;
            itsForm.AcceptOnRegisterButton();
        }

        private void SetActivePhoneCallToFirstOne()
        {
            //TODO!!!
            if (ItsViewForm.NumberOfActivePhoneCalls == 1)
                ItsViewForm.SetActiveCallToFirstOne();
        }

        /// <summary>
        /// Neither dial nor pick up
        /// </summary>
        public override void PickUpPressed()
        {
        }

        /// <summary>
        /// Clear the display or hang up a call.
        /// </summary>
        public override void HangUpPressed()
        {
            ClearDisplayOrHangUp();
        }

        /// <summary>
        /// It has default behavior.
        /// </summary>
        public override void ActivePhoneLineNumberHasChanged()
        {
            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls > 0 &&
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
            SetActivePhoneCallToFirstOne();

            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls > 0 &&
                ItsViewForm.NumberOfActivePhoneLines == 0)
                return;

            /* Otherwise alter the state.
             * */
            ChangeState();
        }
    }
}
