
''' <summary>
''' Deafult ClamAV Engine base.
''' </summary>
''' <remarks></remarks>
Public Interface IClamEngine
    ''' <summary>
    ''' Scans a file.
    ''' </summary>
    ''' <param name="filePath">Full path to file.</param>
    ''' <returns><see cref="IScanResult" /> object contains scan result informations.</returns>
    ''' <remarks>The engine must be loaded and compiled before using this method.</remarks>
    Overloads Function ScanByFile(ByVal filePath As String) As IScanResult
    ''' <summary>
    ''' Scans a file with specified scan options.
    ''' </summary>
    ''' <param name="filePath">Full path to file.</param>
    ''' <param name="scanOptions">Scan option.</param>
    ''' <returns>See this section in <see cref="ScanByFile" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="ScanByFile" /> overloads.</remarks>
    Overloads Function ScanByFile(ByVal filePath As String, ByVal scanOptions As ClamScanOptions) As IScanResult

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
    Function ScanByDescriptor(ByVal fileDescriptor As Integer) As IScanResult
    ''' <summary>
    ''' Scans file using it's file descriptor with specified scan options.
    ''' </summary>
    ''' <param name="fileDescriptor">The file descriptor from open or ws_open.</param>
    ''' <param name="scanOptions">Scan option.</param>
    ''' <returns>See this section in <see cref="ScanByDescriptor" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="ScanByDescriptor" /> overloads.</remarks>
    Function ScanByDescriptor(ByVal fileDescriptor As Integer, ByVal scanOptions As ClamScanOptions) As IScanResult

    ''' <summary>
    ''' Load database file(s) (CVD) to this engine.
    ''' </summary>
    ''' <param name="dbPath">Full path to CVD file or directory.</param>
    ''' <returns>Number of loaded sigantures.</returns>
    ''' <remarks>You can't load database after the engine had compiled.</remarks>
    Function LoadDatabase(ByVal dbPath As String) As UInteger
    ''' <summary>
    ''' Load database file(s) (CVDs) to this engine.
    ''' </summary>
    ''' <param name="dbPath">Full path to CVD file or directory.</param>
    ''' <param name="dbOptions">Database loading options.</param>
    ''' <returns>See this section in <see cref="LoadDatabase" /> overloads.</returns>
    ''' <remarks>See this section in <see cref="LoadDatabase" /> overloads.</remarks>
    Function LoadDatabase(ByVal dbPath As String, ByVal dbOptions As ClamDatabaseOptions) As UInteger

    ''' <summary>
    ''' Sets an engine field value.
    ''' </summary>
    ''' <param name="field">Engine field.</param>
    ''' <param name="value">New value to apply.</param>
    ''' <remarks>Call this before compiling the engine. BE CAREFUL WHEN CALLING THIS METHOD. PLEASE REFER TO CLAMAV DOCUMENTATION FOR MORE INFORMATION.</remarks>
    Sub SetEngineField(ByVal field As ClamEngineField, ByVal value As Object)
    ''' <summary>
    ''' Gets an engine field value.
    ''' </summary>
    ''' <param name="field">Engine field.</param>
    ''' <returns>Setting field value.</returns>
    ''' <remarks>Call this before compiling the engine. BE CAREFUL WHEN CALLING THIS METHOD. PLEASE REFER TO CLAMAV DOCUMENTATION FOR MORE INFORMATION.</remarks>
    Function GetEngineField(ByVal field As ClamEngineField) As Object

    ''' <summary>
    ''' Compile engine for scanning purpose.
    ''' </summary>
    ''' <remarks>After the engine is compiled, you can't change anything else (e.g. change settings, load database).</remarks>
    Sub CompileEngine()

    ''' <summary>
    ''' Check if database is loaded to engine.
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if database is loaded.</returns>
    ''' <remarks></remarks>
    ReadOnly Property IsDatabaseLoaded As Boolean
    ''' <summary>
    ''' Check if engine is compiled.
    ''' </summary>
    ''' <returns><c>Boolean</c> value indicating if engine is compiled.</returns>
    ''' <remarks></remarks>
    ReadOnly Property IsEngineCompiled As Boolean
End Interface
