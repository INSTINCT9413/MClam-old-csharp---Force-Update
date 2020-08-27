
                                           ReSharpedClam
                                           =============
                                        Copyright (C) 2015
                                    Created by Fahmi Noor Fiqri
							

1) LICENSE
   THIS WORK IS LICENSED UNDER CreativeCommons Attribution ShareAlike 4.0 Unported License.
   THIS PROGRAM IS PRODUCED WITHOUT ANY WARRANTY AND NOT GUARANTEE.
   YOU MAY USE IT FOR COMMERCIAL, WITHOUT ANY CHARGES. 
   PLEASE GIVE CREDITS IN YOUR APPLICATION ABOUT THIS LIBRARY.

2) CHANGELOG
   v1.0.00
   -Initial release

   v1.2.8.1
   -ALL CODE RECODED!
   -Change name from 'SharpClam' to 'ReSharpedClam'.
   -Change .NET Framework from v4.0 to v2.0 for maximum compatibility.
   -Supports with ClamAV 0.98.7 (x86 and x64).
   -New 'Freshclam' class to check updates.
   -Added full code documentation.

3) HOW TO USE
   This simple Console Application will describe how to use ReSharpedClam.

   ---START OF CODE--------------------------------------------------------------------------------
   Imports ReSharpedClam

   Module Module1
       Sub Main()
           'First, initialize ClamAV.
           ClamMain.InitializeClamAV()

           Console.WriteLine("ClamAV initialized.")

           'Always use 'Using' statement to handle memory leaking
           Using cl_engine As New ClamEngine()
               'Load database
                Dim loadedSignatureCount As UInteger = 0
               'Change this path according your ClamAV Database directory path
               cl_engine.LoadDatabase("C:\clamav\database", loadedSignatureCount)

               Console.WriteLine()
               Console.WriteLine("Database loading completed.")
               Console.WriteLine("Loaded signatures : " & loadedSignatureCount)

               'Compile engine
               cl_engine.CompileEngine()

               Console.WriteLine()
               Console.WriteLine("Engine compiled successfully.")

               'Scan a file
               'Change to your file path to scan
               Dim scan_result As ClamScanResult = cl_engine.ScanByFile("C:\example.exe")
               'Print all scan informations
               Console.WriteLine()
               Console.WriteLine("Scan completed.")
               Console.WriteLine("===============")

               Console.WriteLine("File name : " & IO.Path.GetFileName(scan_result.FullPath))
               Console.WriteLine("Full path : " & scan_result.FullPath)
               Console.WriteLine("File size : " & scan_result.FileSize)
               Console.WriteLine()
               Console.WriteLine("Is infected : " & scan_result.IsInfected)
               Console.WriteLine("Virus name : " & scan_result.VirusName)
               Console.WriteLine("Scanned data : " & scan_result.ScannedData.ToKbytes & " Kbytes")
               Console.WriteLine("Time elapsed : " & scan_result.TimeElapsed.ToString)
           End Using
           'Pause exit
           Console.Read()
       End Sub
   End Module
   ---END OF CODE----------------------------------------------------------------------------------
   #RUN CONDITION
   1. Libclamav files are placed in same directory with application executable path.
   2. Place right ClamAV version in application directory.
   3. Be aware placing x64 and x86 bits version of ClamAV and project Configuration.
   
4) SUPPORT
   You can contact me from my Facebook or post in GitHub.
   I can't always reply your question because of my school activity.

   Hope this library is useful for you...