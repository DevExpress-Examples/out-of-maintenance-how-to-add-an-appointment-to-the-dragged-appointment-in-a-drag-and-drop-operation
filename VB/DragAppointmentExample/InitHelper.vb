Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text

Namespace CodeExampleTemplate
    Friend Class InitHelper
        Private Property Storage() As SchedulerStorage
        Public Shared RandomInstance As New Random()

        Private CustomResourceCollection As New List(Of CustomResource)()
        Private CustomEventList As New List(Of CustomAppointment)()

        Public Sub New(ByVal currentStorage As SchedulerStorage)
            Storage = currentStorage
        End Sub

        Public Function InitResources() As List(Of CustomResource)
        Dim mappings As ResourceMappingInfo = Me.Storage.Resources.Mappings
            mappings.Id = "ResID"
            mappings.Caption = "Name"
            mappings.Color = "ResColor"

            CustomResourceCollection.Add(CreateCustomResource(1, "Max Fowler", Color.PowderBlue))
            CustomResourceCollection.Add(CreateCustomResource(2, "Nancy Drewmore", Color.PaleVioletRed))
            CustomResourceCollection.Add(CreateCustomResource(3, "Pak Jang", Color.PeachPuff))

            Return CustomResourceCollection
        End Function

        Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal resColor As Color) As CustomResource
            Dim cr As New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            cr.ResColor = resColor
            Return cr
        End Function

        Public Function InitAppointments() As List(Of CustomAppointment)
            Dim mappings As AppointmentMappingInfo = Me.Storage.Appointments.Mappings
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"

            GenerateEvents(CustomEventList, 3)
            CreateSpecialAppointment(CustomEventList)

            Return CustomEventList
        End Function

        Private Sub GenerateEvents(ByVal eventList As List(Of CustomAppointment), ByVal count As Integer)
            For i As Integer = 0 To count - 1
                Dim c_Resource As CustomResource = CustomResourceCollection(i)
                Dim subjPrefix As String = c_Resource.Name & "'s "
                eventList.Add(CreateEvent(subjPrefix & "meeting", "The meeting will be held in the Conference Room", c_Resource.ResID, 2, 5))
                eventList.Add(CreateEvent(subjPrefix & "travel", "Book a hotel in advance", c_Resource.ResID, 3, 6))
                eventList.Add(CreateEvent(subjPrefix & "phone call", "Important phone call", c_Resource.ResID, 0, 10))
            Next i
        End Sub

        Private Sub CreateSpecialAppointment(ByVal eventList As List(Of CustomAppointment))
                Dim apt As New CustomAppointment()
                apt.Subject = "Finalize"
                apt.OwnerId = CustomResourceCollection(0).ResID
                apt.StartTime = Date.Today.AddHours(20)
                apt.EndTime = apt.StartTime.AddHours(1)
                apt.Status = 2
                apt.Label = 1
                eventList.Add(apt)
        End Sub

        Private Function CreateEvent(ByVal subject As String, ByVal additionalInfo As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomAppointment
            Dim apt As New CustomAppointment()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 8
            apt.StartTime = Date.Today.AddHours(9) + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, 180)))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function
    End Class
End Namespace
