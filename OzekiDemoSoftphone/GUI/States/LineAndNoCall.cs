namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// There are some lines and there isn't any call.
    /// </summary>
    class LineAndNoCall : DefaultStateTransition
    {
        /// <summary>
        /// State representing multiple lines and none calls.
        /// </summary>
        /// <param name="itsForm">Its form view.</param>
        public LineAndNoCall(MainForm itsForm)
            : base(itsForm)
        {
            itsForm.EnableHangUpButton();
            itsForm.EnableNumkeys();
            itsForm.EnablePickUpButton();

            itsForm.EnableDNDButton();
            itsForm.DisableHoldButton();
            itsForm.EnableKeepAliveBox();

            itsForm.EnableAutoAnswerButton();
            itsForm.DisableAudioFileButtons();
            itsForm.EnableRedialButton();

            itsForm.DisableAdapterSettings();
            itsForm.VideoButtonsToNormalState();
            SetActivePhoneLineToFirstOne();

            itsForm.DisableTransfer();
            itsForm.tbDialNumber.ReadOnly = false;
            itsForm.AcceptOnPickUpButton();
        }

        private void SetActivePhoneLineToFirstOne()
        {
            // TODO!!!
            if (ItsViewForm.NumberOfActivePhoneLines == 1)
                ItsViewForm.SetActivePhoneLineToFirstOne();
        }

        /// <summary>
        /// Try to create a call.
        /// </summary>
        public override void PickUpPressed()
        {
            CreateCall();
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
            SetActivePhoneLineToFirstOne();

            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls == 0 &&
                ItsViewForm.NumberOfActivePhoneLines > 0)
                return;

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
                ItsViewForm.NumberOfActivePhoneLines > 0)
                return;

            /* Otherwise alter the state.
             * */
            ChangeState();
        }
    }
}
