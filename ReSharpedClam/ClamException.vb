Imports System.Runtime.InteropServices
Imports ReSharpedClam.ClamConstants
Imports ReSharpedClam.ClamPInvokes

''' <summary>
''' Represents errors that occur during ClamAV execution.
''' </summary>
''' <remarks>This exception is usually thrown from ClamAV library.</remarks>
Public Class ClamException
    Inherits Exception

    ''' <param name="errCode">ClamAV Return code.</param>
    Public Sub New(ByVal errCode As Integer)
        MyBase.New(Marshal.PtrToStringAnsi(cl_strerror(errCode)))
    End Sub

End Class
