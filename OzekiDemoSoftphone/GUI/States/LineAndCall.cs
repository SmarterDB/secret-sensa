namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// There are some lines and some calls.
    /// </summary>
    class LineAndCall : DefaultStateTransition
    {
        /// <summary>
        /// State representing multiple lines and multiple calls.
        /// </summary>
        /// <param name="itsForm">Its form view.</param>
        public LineAndCall(MainForm itsForm)
            : base(itsForm)
        {
            itsForm.EnableHangUpButton();
            itsForm.EnableNumkeys();
            itsForm.EnablePickUpButton();

            itsForm.EnableDNDButton();
            itsForm.EnableHoldButton(false);
            itsForm.EnableKeepAliveBox();

            itsForm.EnableAudioFileButtons();
            itsForm.EnableAutoAnswerButton();
            itsForm.DisableRedialButton();

            itsForm.DisableAdapterSettings();
            SetActiveLineAndCallToFirstOnes();
            itsForm.tbDialNumber.ReadOnly = false;

            itsForm.AcceptOnPickUpButton();
            itsForm.EnableVideoControlButton();
            itsForm.EnableTransfer();
        }

        private void SetActiveLineAndCallToFirstOnes()
        {
            //TODO!!!
            if (ItsViewForm.NumberOfActivePhoneCalls == 1)
                ItsViewForm.SetActiveCallToFirstOne();

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
            SetActiveLineAndCallToFirstOnes();

            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls > 0 &&
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
            SetActiveLineAndCallToFirstOnes();

            /* If the condition remains the same, keep this state as an active state.
             * */
            if (ItsViewForm.NumberOfActivePhoneCalls > 0 &&
                ItsViewForm.NumberOfActivePhoneLines > 0)
                return;

            /* Otherwise alter the state.
             * */
            ChangeState();
        }

        public override void CameraTestStateChanged(bool isEnable)
        {
            if (isEnable)
            {
                ItsViewForm.DisableVideoControlButton();
            }
            else
            {
                ItsViewForm.EnableVideoControlButton();
            }

        }
    }
}
