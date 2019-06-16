Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://www.vensoft.co.za/WebService1")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Email
    Inherits System.Web.Services.WebService
    <WebMethod()> _
    Public Function Email(ByVal Permission As String, ByVal EMailTo As String, ByVal Msg As String, Subject As String) As String
        If Permission.ToString <> "jakwes" Then
            Return "Authentication did not pass"
            Exit Function
        ElseIf EMailTo.ToString = "" Then
            Return "No EmailTo Specified"
            Exit Function
        ElseIf Msg.ToString = "" Then
            Return "No Message specified"
            Exit Function
        ElseIf Subject.ToString = "" Then
            Return "No subject specified"
            Exit Function
        Else
            Try
                Dim client As New SmtpClient()
                Dim sendTo As New MailAddress(EMailTo.ToString)
                Dim from As MailAddress = New MailAddress("admin@vensoft.co.za")
                Dim message As New MailMessage(from, sendTo)
                message.IsBodyHtml = True
                message.Subject = Subject.ToString
                message.Body = Msg.ToString
                Dim basicAuthenticationInfo As New System.Net.NetworkCredential("admin@vensoft.co.za", "audi7807")
                client.Host = "mail.edraw.co.za"
                client.UseDefaultCredentials = False
                client.Credentials = basicAuthenticationInfo
                'For smtp config
                'google use EnableSsl=True and Port = 587
                client.EnableSsl = True
                client.Port = 25
                client.Send(message)
            Catch ex As Exception
                Return ex.Message
            End Try

        End If
        Return "Email Sent to " & EMailTo
    End Function

End Class