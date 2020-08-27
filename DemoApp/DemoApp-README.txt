
                                              DemoApp
                                           ReSharpedClam
                                           =============
                                        Copyright (C) 2015
                                    Created by Fahmi Noor Fiqri

1) HOW TO RUN THIS DEMO
   1. Copy libclamav binaries to app output path.
      a. libclamav binaries contains these files :
	     1) libclamav.dll
		 2) libclamunrar.dll
		 3) libclamunrar_iface.dll
		 4) libeay32.dll
		 5) ssleay32.dll

	  b. Project output paths :
	     1) ../DemoApp/bin/Debug/     --> x86-bit
		 2) ../DemoApp/bin/x68/Debug  --> x64-bit

	  c. Just copy all libclamav binary files to Project output path.

   2. Change Platform option in Configuration Manager.
      a. Click : Build, Configuration Manager.
      b. Change Platform value either to x86 and x64 according to your OS.
      c. Click RUN/DEBUG.

2) POSSIBLE EXCEPTIONS
   1. BadImageFormatException.
      You misplaced ClamAV version to 'Debug' folder. Might you are running
	  32-bit version DemoApp but placed 64-bit version of ClamAV in 'Debug'
	  folder.

	  Resolution.
	  Change DemoApp platform configuration to match ClamAV version platform.

   2. ClamException : "Can't get file status.".
      UAC might be activated on your system.

	  Resolution.
	  Turn off UAC or run this demo As Administrator.

   3. I think there is no more exceptions... :D
                                  