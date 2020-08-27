Imports ReSharpedClam.ClamConstants

''' <summary>
''' Size of scanned data, calculated by CL_COUNT_PRECISION.
''' </summary>
''' <remarks>File size measurement using ClamAV calculating rule using <see cref="ClamConstants.CL_COUNT_PRECISION" />.</remarks>
Public Class ClamFileSize
    Dim _size As UInteger = 0

    ''' <summary>Convert current size to MBytes.</summary>
    ''' <returns>Calculated using SIZE * (CL_COUNT_PRECISION / 1024) / 1024 equation.</returns>
    Public Function ToMbytes() As Double
        Return _size * (CL_COUNT_PRECISION / 1024) / 1024
    End Function

    ''' <summary>Convert current size to KBytes.</summary>
    ''' <returns>Calculated using SIZE * (CL_COUNT_PRECISION / 1024) equation.</returns>
    Public Function ToKbytes() As Double
        Return _size * (CL_COUNT_PRECISION / 1024)
    End Function

    ''' <summary>Returns current scanned data size.</summary>
    ''' <returns>Returns original scanned size.</returns>
    Public Function ToBytes() As UInteger
        Return _size
    End Function

    ''' <param name="bytes">Total scanned data.</param>
    Public Sub New(ByVal bytes As UInteger)
        _size = bytes
    End Sub
End Class