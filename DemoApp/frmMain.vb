Option Explicit On
Imports System.Threading
Imports ReSharpedClam
Imports System.IO
Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class frmMain
    Dim p As Process()
    Private Sub ChangeStatusText(ByVal msg As String)
        If lblStatus.InvokeRequired Then
            Dim add_delegate As New Action(Of String)(AddressOf ChangeStatusText)
            Me.Invoke(add_delegate, msg)
        Else
            lblStatus.Text = msg
            lstLog.Items.Add("(" & Now.ToLocalTime & ") " & msg)
        End If
    End Sub

    Private Sub ScanCompleted(ByVal args As Object)
        'Cast 'Object' to 'ClamScanResult'
        Dim scan_result As ClamScanResult = DirectCast(args, ClamScanResult)
        'Change labels text.
        With scan_result
            lblFileName.Text = System.IO.Path.GetFileName(.FullPath)
            lblFullPath.Text = .FullPath
            lblFileSize.Text = .FileSize & " bytes"
            lblScannedData.Text = .ScannedData.ToKbytes() & " KB" 'Convert scanned data to KB (Kbytes)
            lblIsInfected.Text = .IsInfected
            lblVirusName.Text = .VirusName
            lblTimeElapsed.Text = .TimeElapsed.ToString

        End With
        ChangeStatusText("Ready.")
        PictureBox1.Visible = False
    End Sub

    Private Sub ThreadScanFile(ByVal args As Object)
        'Always use 'Using' statement to handle memory leaking
        ChangeStatusText("Creating engine...")
        Using cl_engine As New ClamEngine()
            'Load database
            Dim loadedSignatureCount As UInteger = 0
            'Change this path according your ClamAV Database directory path.
            ChangeStatusText("Loading database...")
            cl_engine.LoadDatabase(Application.StartupPath & "\Scanner\db", loadedSignatureCount)
            ChangeStatusText("Database loaded.")

            'Compile engine
            ChangeStatusText("Compiling engine...")
            cl_engine.CompileEngine()
            ChangeStatusText("Engine compiled.")

            'Scan a file
            'File path stored in 'args' parameter.
            ChangeStatusText("Scan started.")
            Dim scan_result As ClamScanResult = cl_engine.ScanByFile(args.ToString)
            ChangeStatusText("Scan finished.")
            'Invoke 'ScanCompleted' method.
            Me.Invoke(New Action(Of Object)(AddressOf ScanCompleted), scan_result)
        End Using
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFileToScan.Text = ofd.FileName
        End If


    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClamMain.InitializeClamAV()
        Me.Text = "HawkClam " & Application.ProductVersion.ToString
        Button1.PerformClick()

    End Sub

    Private Sub cmdStartScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartScan.Click
        If txtFileToScan.TextLength = 0 Then
            MsgBox("Please select a file.", vbExclamation, "File not selected.")
            Return
        End If
        'Start file scanning.
        resetlabels
        ChangeStatusText("Starting scan...")
        PictureBox1.Visible = True
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ThreadScanFile), txtFileToScan.Text)
    End Sub
    Public Sub resetlabels()
        lblFileName.Text = "Waiting for scan..."
        lblFileSize.Text = "Waiting for scan..."
        lblFullPath.Text = "Waiting for scan..."
        lblIsInfected.Text = "Waiting for scan..."
        lblScannedData.Text = "Waiting for scan..."
        lblStatus.Text = "Waiting for scan..."
        lblTimeElapsed.Text = "Waiting for scan..."
        lblVirusName.Text = "Waiting for scan..."
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dll1234 As New Downloader

        Dim fileinfo As New IO.FileInfo(Application.StartupPath & "\Scanner\database\daily.cvd")
        Dim date1 As DateTime = fileinfo.CreationTime.Date
        Dim date2 As DateTime = DateTime.Now.Date
        'Try
        '    If Directory.Exists(Application.StartupPath & "\Scanner\database\") Then
        '        IO.Directory.Move(Application.StartupPath & "\Scanner\database\", Application.StartupPath & "\Scanner\db\")
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("HawkClam found an old database structure but could not rename the folder. Manually rename the ""database"" folder to ""db"" then relaunch HawkClam", "Database Structure Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Process.Start(Application.StartupPath & "\Scanner\")
        '    Application.Exit()
        'End Try


        Try

                        'Label9.Visible = True
                        'Label8.Visible = True
                        'PictureBox2.Visible = True
                        'PictureBox3.Visible = True
                        'ProgressBar1.Visible = True

                        Panel1.Visible = True
                        BackgroundWorker1.WorkerReportsProgress = True
                        BackgroundWorker1.WorkerSupportsCancellation = True

                        ' Dim gp As GraphicsPath = New GraphicsPath()
                        ' gp.AddEllipse(PictureBox3.DisplayRectangle)
                        ' PictureBox3.Region = New Region(gp)

                        If BackgroundWorker1.IsBusy = False Then
                            'BackgroundWorker1.RunWorkerAsync(10)
                            'Dim dll1234 As New Downloader
                            Panel1.Controls.Add(dll1234)
                            dll1234.Dock = DockStyle.Fill
                            Me.Size = New System.Drawing.Size(Me.Width, 400)
                dll1234.CheckIfRunning()
                Button1.Visible = False
                        End If

                    Catch ex As Exception

                    End Try


    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            'PictureBox3.Image = My.Resources.loading45
            File.Delete(Application.StartupPath & "\Scanner\database\main.cvd")
            'Label9.Text = "Deleting main.cvd"
            'Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
            '(ClientSize.Height - Label9.Height) \ 2 + 120)
            System.Threading.Thread.Sleep(1000)
            File.Delete(Application.StartupPath & "\Scanner\database\daily.cvd")
            'Label9.Text = "Deleting daily.cvd"
            'Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
            '(ClientSize.Height - Label9.Height) \ 2 + 120)
            System.Threading.Thread.Sleep(1000)
            File.Delete(Application.StartupPath & "\Scanner\database\bytecode.cvd")
            'Label9.Text = "Deleting bytecode.cvd"
            'Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
            '(ClientSize.Height - Label9.Height) \ 2 + 120)
            System.Threading.Thread.Sleep(1000)
        Catch ex As Exception

        End Try
        Try
            'PictureBox3.Image = My.Resources._01
            'Label9.Text = "Downloading bytecode.cvd"
            'Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
            '(ClientSize.Height - Label9.Height) \ 2 + 120)
            My.Computer.Network.DownloadFile("http://database.clamav.net/bytecode.cvd", Application.StartupPath & "\Scanner\database\bytecode.cvd")

        Catch ex As Exception

        End Try
        Try
            System.Threading.Thread.Sleep(1000)
            ' Label9.Text = "Downloading daily.cvd"
            'Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
            '(ClientSize.Height - Label9.Height) \ 2 + 120)
            My.Computer.Network.DownloadFile("http://database.clamav.net/daily.cvd", Application.StartupPath & "\Scanner\database\daily.cvd")
        Catch ex As Exception

        End Try
        Dim numToDo As Integer = CInt(e.Argument)

        For n As Integer = 1 To numToDo
            Try
                'Label9.Text = "Downloading main.cvd"
                ' Label9.Location = New Point((ClientSize.Width - Label9.Width) \ 2,
                '(ClientSize.Height - Label9.Height) \ 2 + 120)
                My.Computer.Network.DownloadFile("http://database.clamav.net/main.cvd", Application.StartupPath & "\Scanner\database\main.cvd")

                System.Threading.Thread.Sleep(1000)
            Catch ex As Exception

            End Try

            System.Threading.Thread.Sleep(100)


            BackgroundWorker1.ReportProgress(Convert.ToInt32((n / numToDo) * 100))
        Next



        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MessageBox.Show("The database has been updated successfully!", "Database Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Label8.Visible = False
        'Label9.Visible = False
        'PictureBox2.Visible = False
        'PictureBox3.Visible = False
        'ProgressBar1.Visible = False
        Panel1.Visible = False
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        UpdateProgress(e.ProgressPercentage)
    End Sub
    Public Sub UpdateProgress(pct As Integer)

        ' ToDo: Add error checking 
        'ProgressBar1.Value = ProgressBar1.Maximum
        'ProgressBar1.Value = pct
    End Sub


End Class
