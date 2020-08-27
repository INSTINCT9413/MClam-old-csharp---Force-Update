Imports System.Runtime.InteropServices
Imports System.Net
Imports ReSharpedClam.ClamConstants
Imports ReSharpedClam.ClamPInvokes

''' <summary>
''' Keep your ClamAV database up-to-date.
''' </summary>
''' <remarks>This class in only to check for updates purpose.</remarks>
Public Class Freshclam

    ''' <summary>
    ''' Gets database directory.
    ''' </summary>
    ''' <returns>Full path to database path.</returns>
    ''' <remarks>Hardcoded.</remarks>
    Public Function GetDatabaseDirectory() As String
        Return Marshal.PtrToStringAnsi(cl_retdbdir())
    End Function

    ''' <summary>
    ''' Check database (CVD) file header.
    ''' </summary>
    ''' <param name="cvdPath">Full path to CVD file.</param>
    ''' <returns><c>True</c> if database header is valid, otherwise <c>False</c>.</returns>
    ''' <remarks>This method check CVD file header.</remarks>
    Public Function VerifyDatabase(ByVal cvdPath As String) As Boolean
        Dim cl_retval As Integer = cl_cvdverify(cvdPath)
        SuccessOrThrow(cl_retval)
        Return True
    End Function

    ''' <summary>
    ''' Count signature in database.
    ''' </summary>
    ''' <param name="countOption">Database to count.</param>
    ''' <param name="dbPath">Full path to database directory.</param>
    ''' <returns>Number of loaded sigantures.</returns>
    ''' <remarks>The directory! Not the file.</remarks>
    Public Shared Function CountSignatures(ByVal dbPath As String, ByVal countOption As ClamSignaturesOption) As UInteger
        Dim dbCount As UInteger = 0
        Dim cl_return As Integer = CL_SUCCESS
        If countOption = ClamSignaturesOption.Official Then
            cl_return = cl_countsigs(dbPath, CL_COUNTSIGS_OFFICIAL, dbCount)
        ElseIf countOption = ClamSignaturesOption.Unofficial Then
            cl_return = cl_countsigs(dbPath, CL_COUNTSIGS_UNOFFICIAL, dbCount)
        ElseIf countOption = ClamSignaturesOption.All Then
            cl_return = cl_countsigs(dbPath, CL_COUNTSIGS_ALL, dbCount)
        Else
            Throw New ArgumentOutOfRangeException("countOption")
        End If
        SuccessOrThrow(cl_return)
        Return dbCount
    End Function

    ''' <summary>
    ''' Check for new update.
    ''' </summary>
    ''' <param name="cvdPath">Full path to CVD file.</param>
    ''' <returns><c>Boolean</c> value indicating if new version is available.</returns>
    ''' <remarks>
    ''' <para>This method does not update the database!</para>
    ''' <para>You must use original file name (main.cvd or daily.cvd). 
    ''' If you rename database file name, this method will fail.</para></remarks>
    Public Shared Function IsNewUpdateAvailable(ByVal cvdPath As String) As Boolean
        Dim cl_retval As Integer = CL_SUCCESS

        'Try to parse CVD head
        cl_retval = cl_cvdverify(cvdPath)
        SuccessOrThrow(cl_retval)

        'Get CVD head
        Dim local_cvd_pointer As IntPtr = cl_cvdhead(cvdPath)
        Dim local_cvd_head As cl_cvd = CType(Marshal.PtrToStructure(local_cvd_pointer, GetType(cl_cvd)), cl_cvd)

        Dim local_cvd_time As UInteger = local_cvd_head.stime

        'Free memory
        cl_cvdfree(local_cvd_pointer)
        local_cvd_head = Nothing

        'Get remote CVD version
        Dim reqURL As String = "http://db.clamav.net/" & IO.Path.GetFileName(cvdPath)
        Dim reqHTTP As HttpWebRequest = CType(HttpWebRequest.Create(reqURL), HttpWebRequest)
        With reqHTTP
            .AddRange(0, 512)
        End With
        Dim resHTTP As HttpWebResponse = CType(reqHTTP.GetResponse, HttpWebResponse)

        'Store downloaded bytes into temorary file.
        Dim temp_cvd_path As String = IO.Path.GetTempFileName()
        Using netStream As IO.Stream = resHTTP.GetResponseStream()
            If netStream.Length < 512 Then
                Using fs As New IO.FileStream(temp_cvd_path, IO.FileMode.Create, IO.FileAccess.Write)
                    Dim buffer() As Byte
                    ReDim buffer(CInt(netStream.Length))

                    netStream.Write(buffer, 0, buffer.Length)
                    fs.Write(buffer, 0, buffer.Length)
                End Using
            Else
                Throw New InsufficientMemoryException("Not enough data to parse CVD Head.")
            End If
        End Using

        'Get CVD head
        Dim remote_cvd_pointer As IntPtr = cl_cvdhead(temp_cvd_path)
        Dim remote_cvd_head As cl_cvd = CType(Marshal.PtrToStructure(remote_cvd_pointer, GetType(cl_cvd)), cl_cvd)

        Dim remote_cvd_time As UInteger = remote_cvd_head.stime

        'Free memory
        cl_cvdfree(remote_cvd_pointer)
        remote_cvd_head = Nothing

        'Compare two version
        Return remote_cvd_time > local_cvd_time
    End Function
End Class
