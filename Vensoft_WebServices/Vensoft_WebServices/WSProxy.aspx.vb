Imports System.Net.Mail

Public Class WSProxy
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("EmailTO") = ""
        Session("Subject") = ""
        Session("Msg") = ""
        Session("Permission") = ""
        Session("EmailTO") = Request.QueryString("EmailTO")
        Session("Subject") = Request.QueryString("Subject")
        Session("Msg") = Request.QueryString("Msg")
        Session("Permission") = Request.QueryString("Permission")

        If Session("EmailTO") <> "" Then
            If Session("Subject") <> "" Then
                If Session("Msg") <> "" Then
                    If Session("Permission") <> "" Then
                        If Session("Permission") = "jakwes" Then
                            Try
                                Dim a As String = Session("Msg").ToString, b As String(), C As String
                                Dim i As Integer = 0
                                b = a.Split("|")
                                C = "<html>"
                                For Each b(i) In b
                                    C = C & "<p>" & b(i).Replace("|", "") & "</p>"
                                Next
                                C = C & "<html>"

                                Dim client As New SmtpClient()
                                Dim sendTo As New MailAddress(Session("EMailTo").ToString)
                                Dim from As MailAddress = New MailAddress("admin@vensoft.co.za")
                                Dim message As New MailMessage(from, sendTo)
                                message.IsBodyHtml = True
                                message.Subject = Session("Subject").ToString
                                message.Body = C.ToString
                                Dim basicAuthenticationInfo As New System.Net.NetworkCredential("admin@vensoft.co.za", "audi7807")
                                client.Host = "mail.vensoft.co.za"
                                client.UseDefaultCredentials = False
                                client.Credentials = basicAuthenticationInfo
                                'For smtp config
                                'google use EnableSsl=True and Port = 587
                                client.EnableSsl = False
                                client.Port = 25
                                client.Send(message)
                                Response.Write("Successsfully sent email!!")
                            Catch ex As Exception
                                Response.Write(ex.Message)
                            End Try
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class