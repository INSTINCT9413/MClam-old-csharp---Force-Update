''' <summary>
''' File scan result.
''' </summary>
''' <remarks>ClamAV scan result.</remarks>
<Serializable()>
Public Class ClamScanResult
    Implements IScanResult

    Dim infoArr As Object() = Nothing

    ''' <summary>
    ''' Scanned file size, in bytes.
    ''' </summary>
    ''' <returns>Original file size, in bytes.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileSize As Long Implements IScanResult.FileSize
        Get
            Return New IO.FileInfo(FullPath).Length
        End Get
    End Property

    ''' <summary>
    ''' Full path to scanned file.
    ''' </summary>
    ''' <returns>Full path to scanned file.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FullPath As String Implements IScanResult.FullPath
        Get
            Return CStr(infoArr(0))
        End Get
    End Property

    ''' <summary>
    ''' Is the file infected?
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if the file is a malware.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsInfected As Boolean Implements IScanResult.IsInfected
        Get
            Return VirusName.Length > 0
        End Get
    End Property

    ''' <summary>
    ''' Total scanned data.
    ''' </summary>
    ''' <returns>Total scanned data by ClamAV Engine.</returns>
    ''' <remarks>This number is calculated using CL_COUNT_PERCISION.</remarks>
    Public ReadOnly Property ScannedData As ClamFileSize Implements IScanResult.ScannedData
        Get
            Return CType(infoArr(1), ClamFileSize)
        End Get
    End Property

    ''' <summary>
    ''' Virus name, if infected.
    ''' </summary>
    ''' <returns>Virus name.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property VirusName As String Implements IScanResult.VirusName
        Get
            Return CStr(infoArr(2))
        End Get
    End Property

    ''' <summary>
    ''' Time elapsed to scan this file.
    ''' </summary>
    ''' <returns>Time elapsed to scan this file.</returns>
    ''' <remarks>Only to scan this file. Not including database loading, compiling and another process time.</remarks>
    Public ReadOnly Property TimeElapsed As System.TimeSpan Implements IScanResult.TimeElapsed
        Get
            Return CType(infoArr(3), TimeSpan)
        End Get
    End Property

    Friend Sub New(ByVal path As String, ByVal scanned As ClamFileSize, ByVal virName As String, ByVal elapsed As TimeSpan)
        infoArr = New Object() {path, scanned, virName, elapsed}
    End Sub
End Class