Imports System.Runtime.InteropServices
Imports ReSharpedClam.ClamConstants
Imports ReSharpedClam.ClamPInvokes

''' <summary>
''' Managed ClamAV bindings in .NET Framework.
''' </summary>
''' <remarks>This class only contains Shared methods.</remarks>
Public Class ClamMain

    ''' <summary>
    ''' Initialize ClamAV library (Libclamav).
    ''' </summary>
    ''' <param name="cl_init_option">Initialization option.</param>
    ''' <remarks>Only call this once.</remarks>
    Public Shared Sub InitializeClamAV(Optional ByVal cl_init_option As UInteger = CL_INIT_DEFAULT)
        cl_init(cl_init_option)
    End Sub

    ''' <summary>
    ''' Get ClamAV version.
    ''' </summary>
    ''' <returns>ClamAV version.</returns>
    ''' <remarks>Might this value is hardcoded by ClamAV library.</remarks>
    Public Shared Function GetVersion() As String
        Return Marshal.PtrToStringAnsi(cl_retver())
    End Function
End Class
