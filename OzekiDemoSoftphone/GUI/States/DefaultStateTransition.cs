using System;

namespace OzekiDemoSoftphone.GUI.States
{
    /// <summary>
    /// Default state change.
    /// </summary>
    /// <remarks>
    ///  The state change has the same scheme for every state,
    /// and this class also contains some orders, to create
    /// calls, clear displays, and the state change function.
    /// </remarks>
    abstract class DefaultStateTransition : GUIState
    {
        /// <summary>
        /// Represents default state change function.
        /// </summary>
        /// <param name="viewForm">Its view form.</param>
        public DefaultStateTransition(MainForm viewForm)
            : base(viewForm)
        {
            viewForm.GetCallInfo();
        }

        /// <summary>
        /// Creates a call on the selected phone line.
        /// </summary>
        protected void CreateCall()
        {
            /* If the display doesn't contain text, there is no number to dial.
             */
            if (ItsViewForm.ItsDisplay.Text.Length == 0)
                return;

            /* If there is no selected line, it can't call.
             * */
            if(ItsViewForm.SelectedLine == null)
                return;

            string dial = ItsViewForm.ItsDisplay.Text;
            ItsViewForm.ItsDisplay.Text = string.Empty;

            ItsViewForm.CreateCall(dial);
        }

        /// <summary>
        /// Clears the display, or hangs up the call.
        /// </summary>
        /// <remarks>
        ///  If the display contains text is going to be cleared,
        /// otherwise the selected call is going to be hung up.
        /// </remarks>
        protected void ClearDisplayOrHangUp()
        {
            if (ItsViewForm.ItsDisplay.Text.Length != 0)
            {
                ItsViewForm.ItsDisplay.Text = string.Empty;
                return;
            }

            if (ItsViewForm.ItsDisplay.Text.Equals(String.Empty) && ItsViewForm.SelectedCall != null)
            {
                //ItsViewForm.SoftPhoneModel.HangUpCall(ItsViewForm.SelectedCall);
                ItsViewForm.HangUpSelectedCall();
                return;
            }
        }

        /// <summary>
        /// The state change function.
        /// </summary>
        protected void ChangeState()
        {
            if (ItsViewForm.NumberOfActivePhoneLines == 0 && ItsViewForm.NumberOfActivePhoneCalls == 0)
            {
                /* No lines, no calls.
                 * */
                ItsViewForm.SetGUIState(new NoLineAndNoCall(ItsViewForm));
            }

            if (ItsViewForm.NumberOfActivePhoneLines > 0 && ItsViewForm.NumberOfActivePhoneCalls == 0)
            {
                /* There are some lines, and no calls.
                 * */
                ItsViewForm.SetGUIState(new LineAndNoCall(ItsViewForm));
                return;
            }

            if (ItsViewForm.NumberOfActivePhoneLines > 0 && ItsViewForm.NumberOfActivePhoneCalls > 0)
            {
                /* There are some lines and there are some calls.
                 * */
                ItsViewForm.SetGUIState(new LineAndCall(ItsViewForm));
                return;
            }

            if (ItsViewForm.NumberOfActivePhoneLines == 0 && ItsViewForm.NumberOfActivePhoneCalls > 0)
            {
                /*  There are no lines and there are some calls.
                 * This is very strange, but possible, due to
                 * SIP properties, you can simply cancel the registration of a
                 * line phone and keep talking on a call. It's normal.
                 * */
                ItsViewForm.SetGUIState(new NoLineAndCall(ItsViewForm));
                return;
            }
        }

        public override void CameraTestStateChanged(bool isEnable)
        {}

    }
}
