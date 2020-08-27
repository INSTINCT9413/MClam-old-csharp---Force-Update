Imports System.Runtime.InteropServices
Imports ReSharpedClam.ClamConstants

''' <summary>
''' P/Invoke to ClamAV Library.
''' </summary>
''' <remarks></remarks>
Public Class ClamPInvokes
    ''' <summary>
    ''' Initialize ClamAV.
    ''' </summary>
    ''' <param name="initoptions">Optional initialization opetion.</param>
    ''' <returns>Error code (if an error occured).</returns>
    ''' <remarks>Just call this once.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_init", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_init(ByVal initoptions As UInteger) As Integer
    End Function

    ''' <summary>
    ''' Gets ClamAV version.
    ''' </summary>
    ''' <returns>Handle to ClamAV version string.</returns>
    ''' <remarks>The return value is handle. Use <c>Marshal.PtrToStringAnsi</c> to read String from this handle.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_retver", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_retver() As IntPtr
    End Function

    ''' <summary>
    ''' Gets error message from error code.
    ''' </summary>
    ''' <param name="clerror">Error code.</param>
    ''' <returns>Error message from specified error code.</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_strerror", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_strerror(ByVal clerror As Integer) As IntPtr
    End Function

#Region " Engine Related "
    ''' <summary>
    ''' Create new engine.
    ''' </summary>
    ''' <returns>Handle to new ClamAV Engine.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_new", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_engine_new() As IntPtr
    End Function

    ''' <summary>
    ''' Free ClamAV Engine (engine created using <see cref="cl_engine_new">cl_engine_new</see>).
    ''' </summary>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_free", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_engine_free(ByVal engine As IntPtr) As Integer
    End Function

    ''' <summary>
    ''' Scans a file.
    ''' </summary>
    ''' <param name="filename">Full path to file.</param>
    ''' <param name="virname">Virus name (if infected).</param>
    ''' <param name="scanned">Total scanned data (use <c>CL_COUNT_PRECISION</c> to calculate the size).</param>
    ''' <param name="engine">ClamAV Engine to scan the file.</param>
    ''' <param name="scanoptions">Scan options.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_scanfile", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_scanfile(<InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal filename As String, ByRef virname As IntPtr, ByRef scanned As UInteger, ByVal engine As IntPtr, ByVal scanoptions As UInteger) As Integer
    End Function

    ''' <summary>
    ''' Scans a file using it's File Descriptor.
    ''' </summary>
    ''' <param name="desc">The File Descriptor.</param>
    ''' <param name="virname">Virus name (if infected).</param>
    ''' <param name="scanned">Total scanned data (use <c>CL_COUNT_PRECISION</c> to calculate the size).</param>
    ''' <param name="engine">ClamAV Engine to scan the file.</param>
    ''' <param name="scanoptions">Scan options.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_scandesc")> _
    Public Shared Function cl_scandesc(ByVal desc As Integer, ByRef virname As IntPtr, ByRef scanned As UInteger, ByVal engine As IntPtr, ByVal scanoptions As UInteger) As Integer
    End Function

    ''' <summary>
    ''' Compiles ClamAV engine.
    ''' </summary>
    ''' <param name="engine">Handle to ClamAV engine.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_compile", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_engine_compile(ByVal engine As System.IntPtr) As Integer
    End Function

    ''' <summary>
    ''' Loads ClamAV Database (CVD) to ClamAV Engine.
    ''' </summary>
    ''' <param name="path">Full path to file or folder contains CVD File.</param>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <param name="signo">Number of loaded signatures.</param>
    ''' <param name="dboptions">Database loading options.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_load", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_load(<InAttribute()> ByVal path As IntPtr, ByVal engine As IntPtr, ByRef signo As UInteger, ByVal dboptions As UInteger) As Integer
    End Function

    ''' <summary>
    ''' Sets setting in ClamAV Engine (NUMERICAL FIELD ONLY).
    ''' </summary>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <param name="field">ClamAV Engine field.</param>
    ''' <param name="num">Your setting value.</param>
    ''' <returns>Please refer to ClamAV Documentation.</returns>
    ''' <remarks>Please refer to ClamAV Documentation.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_set_num")> _
    Public Shared Function cl_engine_set_num(ByRef engine As IntPtr, ByVal field As ClamEngineField, ByVal num As Integer) As Integer
    End Function

    ''' <summary>
    ''' Gets engine numerical setting field value.
    ''' </summary>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <param name="field">ClamAV Engine field.</param>
    ''' <param name="err">Error code (if an error occured).</param>
    ''' <returns>Engine setting field value.</returns>
    ''' <remarks>NUMERICAL FIELD ONLY!</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_get_num")> _
    Public Shared Function cl_engine_get_num(ByRef engine As IntPtr, ByVal field As ClamEngineField, ByRef err As Integer) As Integer
    End Function

    ''' <summary>
    ''' Sets engine setting field value.
    ''' </summary>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <param name="field">ClamAV Engine field.</param>
    ''' <param name="str">Setting field value.</param>
    ''' <returns>Error code (if an error occured).</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_set_str")> _
    Public Shared Function cl_engine_set_str(ByRef engine As IntPtr, ByVal field As ClamEngineField, <InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal str As String) As Integer
    End Function

    ''' <summary>
    ''' Gets engine setting field value.
    ''' </summary>
    ''' <param name="engine">ClamAV Engine handle.</param>
    ''' <param name="field">ClamAV Engine field.</param>
    ''' <param name="err">Error code (if an error occured).</param>
    ''' <returns>Engine setting field value.</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_engine_get_str")> _
    Public Shared Function cl_engine_get_str(ByRef engine As IntPtr, ByVal field As ClamEngineField, ByRef err As Integer) As IntPtr
    End Function
#End Region

#Region " Freshclam Related "
    ''' <summary>
    ''' ClamAV cl_cvd structure.
    ''' </summary>
    ''' <remarks>For more information about this structure, see ClamAV Documentation.</remarks>
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure cl_cvd
        <MarshalAs(UnmanagedType.LPStr)> _
        Public time As String
        Public version As UInteger
        Public sigs As UInteger
        Public fl As UInteger
        <MarshalAs(UnmanagedType.LPStr)> _
        Public md5 As String
        <MarshalAs(UnmanagedType.LPStr)> _
        Public dsig As String
        <MarshalAs(UnmanagedType.LPStr)> _
        Public builder As String
        Public stime As UInteger
    End Structure

    ''' <summary>
    ''' Count number of signatures.
    ''' </summary>
    ''' <param name="path">Full path to database directory.</param>
    ''' <param name="countoptions">Count options.</param>
    ''' <param name="sigs">Number of loaded signatures.</param>
    ''' <returns>Error code (if an error occured).</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_countsigs", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function cl_countsigs(<InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal path As String, ByVal countoptions As UInteger, ByRef sigs As UInteger) As Integer
    End Function

    ''' <summary>
    ''' Gets ClamAV database header.
    ''' </summary>
    ''' <param name="file">Full path to ClamAV database.</param>
    ''' <returns>Handle to cl_cvd structure.</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_cvdhead")> _
    Public Shared Function cl_cvdhead(<InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal file As String) As IntPtr
    End Function

    ' ''' <summary>
    ' ''' Parse 512-bytes <c>String</c> into cl_cvd structure.
    ' ''' </summary>
    ' ''' <param name="head">512-bytes <c>String</c> into cl_cvd structure.</param>
    ' ''' <returns>Handle to cl_cvd structure.</returns>
    ' ''' <remarks></remarks>
    '<DllImport(libclamav_Path, EntryPoint:="cl_cvdparse")> _
    'Public Shared Function cl_cvdparse(<InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal head As String) As IntPtr
    'End Function

    ''' <summary>
    ''' Verify database file header.
    ''' </summary>
    ''' <param name="file">Full path to ClamAV database.</param>
    ''' <returns>Error code (if an error occured).</returns>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_cvdverify")> _
    Public Shared Function cl_cvdverify(<InAttribute(), MarshalAs(UnmanagedType.LPStr)> ByVal file As String) As Integer
    End Function

    ''' <summary>
    ''' Gets database path.
    ''' </summary>
    ''' <returns>Database path.</returns>
    ''' <remarks>This function also return a handle. Use <c>Marshal.PtrToStringAnsi</c> to get <c>String</c> version.</remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_retdbdir")> _
    Public Shared Function cl_retdbdir() As IntPtr
    End Function

    ''' <summary>
    ''' Free handle created by <see cref="cl_cvdhead" />.
    ''' </summary>
    ''' <param name="cvd">Handle to cl_cvd structure.</param>
    ''' <remarks></remarks>
    <DllImport(libclamav_Path, EntryPoint:="cl_cvdfree")> _
    Public Shared Sub cl_cvdfree(ByRef cvd As IntPtr)
    End Sub
#End Region

End Class
