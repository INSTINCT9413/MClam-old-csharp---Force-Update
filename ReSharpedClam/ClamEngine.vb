Imports System.Runtime.InteropServices
Imports System.Threading
Imports ReSharpedClam.ClamConstants
Imports ReSharpedClam.ClamPInvokes

''' <summary>
''' Manged ClamAV scanning engine.
''' </summary>
''' <remarks>ClamAV main scanning engine. You can create one or more engine at same time, but you might consider memory consumption.</remarks>
Public Class ClamEngine
    Implements IDisposable, IClamEngine

    ''' <summary>ClamAV Engine handle.</summary>
    Protected engineHandle As IntPtr

    Private _cl_engine_compiled As Boolean = False
    Private _cl_engine_db_loaded As Boolean = False

#Region " Properties "
    ''' <summary>
    ''' Check if database is loaded to engine.
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if database is loaded.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsDatabaseLoaded As Boolean Implements IClamEngine.IsDatabaseLoaded
        Get
            Return _cl_engine_db_loaded
        End Get
    End Property

    ''' <summary>
    ''' Check if engine is compiled.
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if engine is compiled.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsEngineCompiled As Boolean Implements IClamEngine.IsEngineCompiled
        Get
            Return _cl_engine_compiled
        End Get
    End Property
#End Region

    Public Sub New()
        engineHandle = cl_engine_new()
    End Sub

#Region " ScanByFile "
    ''' <summary>
    ''' Scans a file.
    ''' </summary>
    ''' <param name="filePath">Full path to file.</param>
    ''' <returns><see cref="IScanResult" /> object contains scan result informations.</returns>
    ''' <remarks>The engine must be loaded and compiled before using this method.</remarks>
    Public Overloads Function ScanByFile(ByVal filePath As String) As IScanResult Implements IClamEngine.ScanByFile
        Return Me.ScanByFile(filePath, ClamScanOptions.CL_SCAN_ARCHIVE)
    End Function

    ''' <summary>
    ''' Scans a file with specified scan options.
    ''' </summary>
    ''' <param name="filePath">Full path to file.</param>
    ''' <param name="scanOptions">Scan option.</param>
    ''' <returns>See this section in <see cref="ScanByFile" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="ScanByFile" /> overloads.</remarks>
    Public Overloads Function ScanByFile(ByVal filePath As String, ByVal scanOptions As ClamScanOptions) As IScanResult Implements IClamEngine.ScanByFile
        If Not _cl_engine_db_loaded Then
            Throw New InvalidOperationException("Database is not loaded.")
        ElseIf Not _cl_engine_compiled Then
            Throw New InvalidOperationException("Engine is not compiled.")
        End If

        Dim tmr As New Stopwatch
        tmr.Start()

        Dim virusName As IntPtr = IntPtr.Zero
        Dim scanned As UInteger = 0
        Dim file_status As Integer = cl_scanfile(filePath, virusName, scanned, engineHandle, CUInt(scanOptions))

        Dim fileSize As Long = 0
        Dim scannedData As New ClamFileSize(scanned)
        Dim virusNameString As String = String.Empty

        If file_status = CL_CLEAN Then
            'The file is clean.
        ElseIf file_status = CL_VIRUS Then
            'The file is malware.
            virusNameString = Marshal.PtrToStringAnsi(virusName)
        Else
            Throw New ClamException(file_status)
        End If

        tmr.Stop()
        Dim elapsedTime = tmr.Elapsed

        Dim result As New ClamScanResult(filePath, scannedData, virusNameString, elapsedTime)
        Return result
    End Function
#End Region

#Region " ScanByDescriptor "
    ''' <summary>
    ''' Scans file using it's file descriptor.
    ''' </summary>
    ''' <param name="fileDescriptor">The file descriptor from open or ws_open.</param>
    ''' <returns><see cref="IScanResult" /> object contains scan result informations.</returns>
    ''' <remarks>
    ''' <para>THIS METHOD ONLY WORKING WHEN USED WITH C/C++ CODE. 
    ''' You must create a C/C++ library to open file descriptor using C/C++ 
    ''' code because File Descriptor is officialy supported by C/C++ Runtime, 
    ''' not by CLR.</para>
    ''' <para>I'm currently working to create Unamanaged Library using C++ 
    ''' to open file and get it's file descriptor.</para> 
    ''' <para>Remember, you can't use ANY .NET Framework internal method to open file descriptor, 
    ''' YOU MUST CREATE YOUR OWN LIBRARY USING C/C++ TO OPEN FILE DESCRIPTOR. 
    ''' .NET Framework FileSystem method like <c>FileOpen</c> will not work.</para>
    ''' </remarks>
    Public Function ScanByDescriptor(ByVal fileDescriptor As Integer) As IScanResult Implements IClamEngine.ScanByDescriptor
        Return Me.ScanByDescriptor(fileDescriptor, ClamScanOptions.CL_SCAN_STDOPT)
    End Function

    ''' <summary>
    ''' Scans file using it's file descriptor with specified scan options.
    ''' </summary>
    ''' <param name="fileDescriptor">The file descriptor from open or ws_open.</param>
    ''' <param name="scanOptions">Scan option.</param>
    ''' <returns>See this section in <see cref="ScanByDescriptor" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="ScanByDescriptor" /> overloads.</remarks>
    Public Function ScanByDescriptor(ByVal fileDescriptor As Integer, ByVal scanOptions As ClamScanOptions) As IScanResult Implements IClamEngine.ScanByDescriptor
        If Not _cl_engine_db_loaded Then
            Throw New InvalidOperationException("Database is not loaded.")
        ElseIf Not _cl_engine_compiled Then
            Throw New InvalidOperationException("Engine is not compiled.")
        End If

        Dim tmr As New Stopwatch
        tmr.Start()

        Dim virusName As IntPtr = IntPtr.Zero
        Dim scanned As UInteger = 0
        Dim file_status As Integer = cl_scandesc(fileDescriptor, virusName, scanned, engineHandle, CUInt(scanOptions))

        Dim fileSize As Long = 0
        Dim scannedData As New ClamFileSize(scanned)
        Dim virusNameString As String = String.Empty

        If file_status = CL_CLEAN Then
            'The file is clean.
        ElseIf file_status = CL_VIRUS Then
            'The file is malware.
            virusNameString = Marshal.PtrToStringAnsi(virusName)
        Else
            Throw New ClamException(file_status)
        End If

        tmr.Stop()
        Dim elapsedTime = tmr.Elapsed

        Dim result As New ClamScanResult("File Descriptor : " & fileDescriptor, scannedData, virusNameString, elapsedTime)
        Return result
    End Function
#End Region

    ''' <summary>
    ''' Load database file(s) (CVD) to this engine.
    ''' </summary>
    ''' <param name="dbPath">Full path to CVD file or directory.</param>
    ''' <returns>Number of loaded sigantures.</returns>
    ''' <remarks>You can't load database after the engine had compiled.</remarks>
    Public Overloads Function LoadDatabase(ByVal dbPath As String) As UInteger Implements IClamEngine.LoadDatabase
        Return Me.LoadDatabase(dbPath, ClamDatabaseOptions.CL_DB_STDOPT)
    End Function

    ''' <summary>
    ''' Load database file(s) (CVDs) to this engine.
    ''' </summary>
    ''' <param name="dbPath">Full path to CVD file or directory.</param>
    ''' <param name="dbOptions">Database loading options.</param>
    ''' <returns>See this section in <see cref="LoadDatabase" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="LoadDatabase" /> overloads.</remarks>
    Public Overloads Function LoadDatabase(ByVal dbPath As String, ByVal dbOptions As ClamDatabaseOptions) As UInteger Implements IClamEngine.LoadDatabase
        If _cl_engine_compiled Then
            Throw New InvalidOperationException("Engine is already compiled. You can't load database into it.")
        End If
        If Not _cl_engine_db_loaded Then
            Dim loadedSignatures As UInteger = 0
            Dim dbPathPointer As IntPtr = Marshal.StringToHGlobalAnsi(dbPath)
            Dim cl_retval As Integer = cl_load(dbPathPointer, engineHandle, loadedSignatures, CUInt(dbOptions))

            _cl_engine_db_loaded = True
            SuccessOrThrow(cl_retval)
            Return loadedSignatures
        Else
            Throw New InvalidOperationException("Database is alredy loaded.")
        End If
    End Function

    ''' <summary>
    ''' Compile engine for scanning purpose.
    ''' </summary>
    ''' <remarks>After the engine is compiled, you can't change anything else (e.g. change settings, load database).</remarks>
    Public Sub CompileEngine() Implements IClamEngine.CompileEngine
        If Not _cl_engine_db_loaded Then
            Throw New InvalidOperationException("Database is not loaded. Engine can't be compiled without database.")
        End If
        If Not _cl_engine_compiled Then
            Dim cl_retval As Integer = cl_engine_compile(engineHandle)
            SuccessOrThrow(cl_retval)
            _cl_engine_compiled = True
        Else
            Throw New InvalidOperationException("Engine is already compiled.")
        End If
    End Sub

    ''' <summary>
    ''' Sets an engine field value.
    ''' </summary>
    ''' <param name="field">Engine field.</param>
    ''' <param name="value">New value to apply.</param>
    ''' <remarks>Call this before compiling the engine. BE CAREFUL WHEN CALLING THIS METHOD. PLEASE REFER TO CLAMAV DOCUMENTATION FOR MORE INFORMATION.</remarks>
    Public Sub SetEngineField(ByVal field As ClamEngineField, ByVal value As Object) Implements IClamEngine.SetEngineField
        If (field = ClamEngineField.CL_ENGINE_PUA_CATEGORIES) OrElse (field = ClamEngineField.CL_ENGINE_TMPDIR) Then
            Dim cl_retval As Integer = cl_engine_set_str(engineHandle, field, value.ToString())
            SuccessOrThrow(cl_retval)
        Else
            'Check if the value is number
            Dim chars As Char() = value.ToString.ToCharArray
            For Each character In chars
                If Not Char.IsDigit(character) Then
                    Throw New ArithmeticException("This field is for number only.")
                End If
            Next
            Erase chars

            Dim cl_retval As Integer = cl_engine_set_num(engineHandle, field, CInt(value))
            SuccessOrThrow(cl_retval)
        End If
    End Sub

    ''' <summary>
    ''' Gets an engine field value.
    ''' </summary>
    ''' <param name="field">Engine field.</param>
    ''' <returns>Value setting from current engine.</returns>
    ''' <remarks>Call this before compiling the engine. BE CAREFUL WHEN CALLING THIS METHOD. PLEASE REFER TO CLAMAV DOCUMENTATION FOR MORE INFORMATION.</remarks>
    Public Function GetEngineField(ByVal field As ClamEngineField) As Object Implements IClamEngine.GetEngineField
        If (field = ClamEngineField.CL_ENGINE_PUA_CATEGORIES) OrElse (field = ClamEngineField.CL_ENGINE_TMPDIR) Then
            Dim cl_retval As Integer = CL_SUCCESS
            Dim field_str As IntPtr = cl_engine_get_str(engineHandle, field, cl_retval)
            SuccessOrThrow(cl_retval)
            Return Marshal.PtrToStringAnsi(field_str)
        Else
            Dim cl_retval As Integer = CL_SUCCESS
            Dim field_int As Integer = cl_engine_get_num(engineHandle, field, cl_retval)
            SuccessOrThrow(cl_retval)
            Return field_int
        End If
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            If _cl_engine_db_loaded Then
                cl_engine_free(engineHandle)
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
