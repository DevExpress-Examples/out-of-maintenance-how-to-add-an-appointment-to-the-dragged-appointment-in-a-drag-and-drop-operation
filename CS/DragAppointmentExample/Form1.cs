using DevExpress.XtraScheduler;
using System;
using System.Windows.Forms;

namespace CodeExampleTemplate {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.schedulerControl1.Start = DateTime.Now.Date;
            this.schedulerControl1.DayView.TopRowTime = new TimeSpan(9, 0, 0);
            #region #events
            // Subscribe to events
            this.schedulerControl1.AppointmentDrag += SchedulerControl_AppointmentDrag;
            this.schedulerControl1.AdditionalAppointmentsDrag += SchedulerControl1_AdditionalAppointmentsDrag;
            #endregion #events
        }

        private void Form1_Load(object sender, EventArgs e) {
            InitHelper helper = new InitHelper(this.schedulerStorage1);

            schedulerStorage1.BeginUpdate();
            try {
                schedulerStorage1.Resources.DataSource = helper.InitResources();
                schedulerStorage1.Appointments.DataSource = helper.InitAppointments();
            }
            finally {
                schedulerStorage1.EndUpdate();
            }
            schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
        }
        #region #eventhandlers
        private void SchedulerControl_AppointmentDrag(object sender, AppointmentDragEventArgs e) {
            // Find a special appointment among visible appointments.
            AppointmentBaseCollection visbileApts = schedulerControl1.ActiveView.GetAppointments();
            Appointment apt = visbileApts.Find(item => (item.Subject == "Finalize" && item.ResourceId.Equals(e.HitResource.Id)));
            if (apt == null) {
                e.AdditionalAppointments.Clear();
            }
            else {
                if (e.SourceAppointment != apt) {
                    // If the dragged appointment overlaps with the special appointment,
                    // add the special appointment to the AdditionalAppointments collection
                    // to drag along with the initial appointment.
                    // The AdditionalAppointmentsDrag event will occur.
                    if (e.EditedAppointment.End > apt.Start)
                        e.AdditionalAppointments.Add(apt);
                    else
                        e.AdditionalAppointments.Clear();
                }
            }
        }

        private void SchedulerControl1_AdditionalAppointmentsDrag(object sender, AdditionalAppointmentsDragEventArgs e) {
            if (e.AdditionalAppointmentInfos.Count <= 0)
                return;
            AppointmentDragInfo aptInfo = e.AdditionalAppointmentInfos[0];
            // If the end of the main dragged appointment becomes greater than the start of the special appointment,
            // set the start of the special appointment being dragged to the end of the main dragged appointment.
            // It prevents overlapping appointments.
            if (e.PrimaryAppointmentInfos[0].EditedAppointment.End > aptInfo.SourceAppointment.Start)
                aptInfo.EditedAppointment.Start = e.PrimaryAppointmentInfos[0].EditedAppointment.End;
        }
        #endregion #eventhandlers
    }
}
