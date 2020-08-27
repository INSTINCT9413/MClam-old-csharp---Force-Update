
''' <summary>
''' Standard scan result informations.
''' </summary>
''' <remarks></remarks>
Public Interface IScanResult
    ''' <summary>
    ''' Virus name, if infected.
    ''' </summary>
    ''' <returns>Virus name.</returns>
    ''' <remarks></remarks>
    ReadOnly Property VirusName As String
    ''' <summary>
    ''' Is the file infected?
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if the file is a malware.</returns>
    ''' <remarks></remarks>
    ReadOnly Property IsInfected As Boolean
    ''' <summary>
    ''' Time elapsed to scan this file.
    ''' </summary>
    ''' <returns>Time elapsed to scan this file.</returns>
    ''' <remarks>Only to scan this file. Not including database loading, compiling and another process time.</remarks>
    ReadOnly Property TimeElapsed As TimeSpan
    ''' <summary>
    ''' Total scanned data.
    ''' </summary>
    ''' <returns>Total scanned data by ClamAV Engine.</returns>
    ''' <remarks>This number is calculated using CL_COUNT_PERCISION.</remarks>
    ReadOnly Property ScannedData As ClamFileSize
    ''' <summary>
    ''' Full path to scanned file.
    ''' </summary>
    ''' <returns>Full path to scanned file.</returns>
    ''' <remarks></remarks>
    ReadOnly Property FullPath As String
    ''' <summary>
    ''' Scanned file size, in bytes.
    ''' </summary>
    ''' <returns>Original file size, in bytes.</returns>
    ''' <remarks></remarks>
    ReadOnly Property FileSize As Long
End Interface