''' <summary>
''' ClamAV engine scan options.
''' </summary>
''' <remarks>Use <see cref="ClamScanOptions.CL_SCAN_STDOPT" /> if you not sure.</remarks>
<Flags()> _
Public Enum ClamScanOptions
    ''' <summary>
    ''' Use it alone if you want to disable support for special files.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_RAW = 0
    ''' <summary>
    ''' This flag enables transparent scanning of various archive formats.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_ARCHIVE = 1
    ''' <summary>
    ''' Enable support for mail files.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_MAIL = 2
    ''' <summary>
    ''' Enables support for OLE2 containers (used by MS Office and .msi files).
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_OLE2 = 4
    ''' <summary>
    ''' With this flag the library will mark encrypted archives as viruses (Encrypted.Zip, Encrypted.RAR).
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_BLOCKENCRYPTED = 8
    ''' <summary>
    ''' This flag enables HTML normalisation (including ScrEnc decryption).
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_HTML = 16
    ''' <summary>
    ''' This flag enables deep scanning of Portable Executable files and allows libclamav to unpack executables compressed with run-time unpackers.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_PE = 32
    ''' <summary>
    ''' libclamav will try to detect broken executables andmark themas Broken.Executable.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_BLOCKBROKEN = 64
    ''' <summary>
    ''' Undocumented flag in ClamAV Documentation.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_MAILURL = 128
    ''' <summary>
    ''' Undocumented flag in ClamAV Documentation.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_BLOCKMAX = 256
    ''' <summary>
    ''' Enable algorithmic detection of viruses.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_ALGORITHMIC = 512
    ''' <summary>
    ''' Phishing module: always block SSL mismatches in URLs.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_PHISHING_BLOCKSSL = 2048
    ''' <summary>
    ''' Phishing module: always block cloaked URLs.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_PHISHING_BLOCKCLOAK = 4096
    ''' <summary>
    ''' Enable support for ELF files.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_ELF = 8192
    ''' <summary>
    ''' Enables scanning within PDF files.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_PDF = 16384
    ''' <summary>
    ''' Enable the DLP module which scans for credit card and SSN numbers.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_STRUCTURED = 32768
    ''' <summary>
    ''' Search for SSNs formatted as xx-yy-zzzz.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_STRUCTURED_SSN_NORMAL = 65536
    ''' <summary>
    ''' Search for SSNs formatted as xxyyzzzz.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_STRUCTURED_SSN_STRIPPED = 131072
    ''' <summary>
    ''' Scan RFC1341 messages split over many emails. You will need to periodically
    ''' clean up $TemporaryDirectory/clamav-partial directory (or use CleanClamavTempoary).
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_PARTIAL_MESSAGE = 262144
    ''' <summary>
    ''' Allow heuristic match to take precedence. When enabled, if a heuristic scan (such
    ''' as phishingScan) detects a possible virus/phish it will stop scan immediately. Rec-ommended, saves CPU scan-time. When disabled, virus/phish detected by heuris-tic scans will be reported only at the end of a scan. If an archive contains both a
    ''' heuristically detected virus/phishing, and a real malware, the real malware will be
    ''' reported.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_HEURISTIC_PRECEDENCE = 524288
    ''' <summary>
    ''' OLE2 containers, which contain VBA macros will be marked infected (Heuris-tics.OLE2.ContainsMacros).
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_BLOCKMACROS = 1048576
    ''' <summary>
    ''' Undocumented flag in ClamAV Documentation.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_ALLMATCHES = 2097152
    ''' <summary>
    ''' Enables scanning within SWF files, notably compressed SWF.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_SWF = 4194304
    ''' <summary>
    ''' Standard scan option recormended by ClamAV Team.
    ''' </summary>
    ''' <remarks></remarks>
    CL_SCAN_STDOPT = CL_SCAN_ARCHIVE Or CL_SCAN_MAIL Or CL_SCAN_OLE2 Or CL_SCAN_PDF Or CL_SCAN_HTML Or CL_SCAN_PE Or CL_SCAN_ALGORITHMIC Or CL_SCAN_ELF Or CL_SCAN_SWF
End Enum