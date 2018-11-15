<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/DragAppointmentExample/Form1.cs) (VB: [Form1.vb](./VB/DragAppointmentExample/Form1.vb))
<!-- default file list end -->
# How to add an appointment to the dragged appointment in a drag-and-drop operation


This example illustrates how to implement the following requirements.<br>There is a special appointment with the subject "Finalize". When any appointment located before a special appointment and associated with the same resource is being dragged forward in time, the special appointment must always follow the "dragged" appointment. That is, when an end-user drags an appointment and its end time becomes equal to a start time of a special appointment, the special appointment must be dragged with the primary appointment synchronously. If an end-user presses ESC, both appointments return to the initial state.<br>To accomplish this,  handle the <a href="http://help.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerControl_AppointmentDragtopic">AppointmentDrag</a> event and add a special appointment to the <a href="http://help.devexpress.com/#CoreLibraries/DevExpressXtraSchedulerAppointmentDragEventArgs_AdditionalAppointmentstopic">AppointmentDragEventArgs.AdditionalAppointments</a> collection when appointments should be dragged synchronously. Subscribe to the <a href="http://help.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerControl_AdditionalAppointmentsDragtopic">AdditionalAppointmentsDrag</a> event to modify the special appointment's start so that it equals the end of the primarily dragged appointment.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-add-an-appointment-to-the-dragged-appointment-in-a-drag-and-drop-operation-t445652/16.2.3+/media/15857b48-a036-11e6-80bf-00155d62480c.png">

<br/>


