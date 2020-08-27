Imports ReSharpedClam.ClamConstants

Module modExtensions

    ''' <summary>
    ''' Check if return code is an error and throws it.
    ''' </summary>
    ''' <param name="code">Return value from ClamAV.</param>
    ''' <remarks>Error message gathered from ClamAV.</remarks>
    Public Sub SuccessOrThrow(ByVal code As Integer)
        If code <> CL_SUCCESS Then
            Throw New ClamException(code)
        End If
    End Sub
End Module
