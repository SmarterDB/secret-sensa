namespace OzekiDemoSoftphone.GUI
{
    /// <summary>
    /// Main state of GUI.
    /// </summary>
    /// <remarks>
    ///  The state is calculated from number of phone lines and
    /// active calls, and pushed buttons.
    /// </remarks>
    abstract class GUIState
    {
        /// <summary>
        /// Every state has its form associated with.
        /// </summary>
        protected MainForm ItsViewForm;

        /// <summary>
        /// Creates a state of GUI object.
        /// </summary>
        /// <param name="itsViewForm">Its view form.</param>
        public GUIState(MainForm itsViewForm)
        {
            ItsViewForm = itsViewForm;
        }

        /// <summary>
        /// Pick up has pressed.
        /// </summary>
        public abstract void PickUpPressed();

        /// <summary>
        /// Hang up has pressed.
        /// </summary>
        public abstract void HangUpPressed();

        /// <summary>
        /// Number of active phone lines has changed.
        /// </summary>
        public abstract void ActivePhoneLineNumberHasChanged();

        /// <summary>
        /// Number of active calls has changed.
        /// </summary>
        public abstract void ActivePhoneCallNumberHasChanged();


        public abstract void CameraTestStateChanged(bool isEnable);
    }
}
