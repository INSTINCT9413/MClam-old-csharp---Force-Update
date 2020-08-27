Imports System.Runtime.InteropServices

''' <summary>
''' Provides constants from ClamAV libraries.
''' </summary>
''' <remarks>Please edit this class before using this library wrapper.</remarks>
Public Class ClamConstants
    ''' <summary>
    ''' Change this constant value according to your libclamav.dll location.
    ''' </summary>
    ''' <remarks>The path can be Relative or Absolute.</remarks>
    Public Const libclamav_Path As String = "libclamav.dll"

    'DON'T EDIT CONSTANTS INSIDE THIS REGION!!
    'IT'S CLAMAV INTERNAL CONSTANTS!!
#Region " ClamAV Constants "
    ''' <summary>ClamAV default return value.</summary>
    Public Const CL_SUCCESS As Integer = 0

    ''' <summary>Count official signatures only.</summary>
    Public Const CL_COUNTSIGS_OFFICIAL As Integer = 1
    ''' <summary>Count unofficial signatures only.</summary>
    Public Const CL_COUNTSIGS_UNOFFICIAL As Integer = 2
    ''' <summary>Count both official and unofficial signatures.</summary>
    Public Const CL_COUNTSIGS_ALL As Integer = (CL_COUNTSIGS_OFFICIAL Or CL_COUNTSIGS_UNOFFICIAL)

    ''' <summary>Default ClamAV initialization parameter.</summary>
    Public Const CL_INIT_DEFAULT As Integer = 0

    ''' <summary>Indicates the file is clean.</summary>
    Public Const CL_CLEAN As Integer = 0
    ''' <summary>Indicates the file is malware.</summary>
    Public Const CL_VIRUS As Integer = 1

    ''' <summary>ClamAV default count precision.</summary>
    Public Const CL_COUNT_PRECISION As Integer = 4096
#End Region

End Class
