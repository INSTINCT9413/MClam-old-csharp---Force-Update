''' <summary>
''' ClamAV database loading options.
''' </summary>
''' <remarks>Use <see cref="ClamDatabaseOptions.CL_DB_STDOPT" /> if you not sure.</remarks>
<Flags()> _
Public Enum ClamDatabaseOptions
    ''' <summary>
    ''' Load phishing signatures.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_PHISHING = 2
    ''' <summary>
    ''' Initialize the phishing detection module and load .wdb and .pdb files.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_PHISHING_URLS = 8
    ''' <summary>
    ''' Load signatures for Potentially Unwanted Applications.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_PUA = 16
    ''' <summary>
    ''' Only load official signatures from digitally signed databases.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_OFFICIAL_ONLY = 4096
    ''' <summary>
    ''' Load bytecode.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_BYTECODE = 8192
    ''' <summary>
    ''' This is an alias for a recommended set of scan options.
    ''' </summary>
    ''' <remarks></remarks>
    CL_DB_STDOPT = CL_DB_PHISHING Or CL_DB_PHISHING_URLS Or CL_DB_BYTECODE
End Enum