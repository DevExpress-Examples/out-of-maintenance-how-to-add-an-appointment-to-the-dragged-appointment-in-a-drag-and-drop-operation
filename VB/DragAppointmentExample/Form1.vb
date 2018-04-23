Imports DevExpress.XtraScheduler
Imports System
Imports System.Windows.Forms

Namespace CodeExampleTemplate
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            Me.schedulerControl1.Start = Date.Now.Date
            Me.schedulerControl1.DayView.TopRowTime = New TimeSpan(9, 0, 0)
'            #Region "#events"
            ' Subscribe to events
            AddHandler Me.schedulerControl1.AppointmentDrag, AddressOf SchedulerControl_AppointmentDrag
            AddHandler Me.schedulerControl1.AdditionalAppointmentsDrag, AddressOf SchedulerControl1_AdditionalAppointmentsDrag
'            #End Region ' #events
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim helper As New InitHelper(Me.schedulerStorage1)

            schedulerStorage1.BeginUpdate()
            Try
                schedulerStorage1.Resources.DataSource = helper.InitResources()
                schedulerStorage1.Appointments.DataSource = helper.InitAppointments()
            Finally
                schedulerStorage1.EndUpdate()
            End Try
            schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
        End Sub
        #Region "#eventhandlers"
        Private Sub SchedulerControl_AppointmentDrag(ByVal sender As Object, ByVal e As AppointmentDragEventArgs)
            ' Find a special appointment among visible appointments.
            Dim visbileApts As AppointmentBaseCollection = schedulerControl1.ActiveView.GetAppointments()
            Dim apt As Appointment = visbileApts.Find(Function(item) (item.Subject = "Finalize" AndAlso item.ResourceId.Equals(e.HitResource.Id)))
            If apt Is Nothing Then
                e.AdditionalAppointments.Clear()
            Else
                If e.SourceAppointment IsNot apt Then
                    ' If the dragged appointment overlaps with the special appointment,
                    ' add the special appointment to the AdditionalAppointments collection
                    ' to drag along with the initial appointment.
                    ' The AdditionalAppointmentsDrag event will occur.
                    If e.EditedAppointment.End > apt.Start Then
                        e.AdditionalAppointments.Add(apt)
                    Else
                        e.AdditionalAppointments.Clear()
                    End If
                End If
            End If
        End Sub

        Private Sub SchedulerControl1_AdditionalAppointmentsDrag(ByVal sender As Object, ByVal e As AdditionalAppointmentsDragEventArgs)
            If e.AdditionalAppointmentInfos.Count <= 0 Then
                Return
            End If
            Dim aptInfo As AppointmentDragInfo = e.AdditionalAppointmentInfos(0)
            ' If the end of the main dragged appointment becomes greater than the start of the special appointment,
            ' set the start of the special appointment being dragged to the end of the main dragged appointment.
            ' It prevents overlapping appointments.
            If e.PrimaryAppointmentInfos(0).EditedAppointment.End > aptInfo.SourceAppointment.Start Then
                aptInfo.EditedAppointment.Start = e.PrimaryAppointmentInfos(0).EditedAppointment.End
            End If
        End Sub
        #End Region ' #eventhandlers
    End Class
End Namespace
