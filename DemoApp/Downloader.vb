'**************************************************************'
' FileDownloader Demo                                          '
' By De Dauw Jeroen - jeroendedauw@gmail.com                   '
'**************************************************************'
' Copyright 2009 - BN+ Discussions                             '
' http://code.bn2vs.com                                        '
'**************************************************************'

' This code is avaible at
' > BN+ Discussions: http://code.bn2vs.com/viewtopic.php?t=150
' > The Code Project: http://www.codeproject.com/KB/vb/FileDownloader.aspx

' Dutch support can be found here: http://www.helpmij.nl/forum/showthread.php?t=416568

Option Strict On : Option Explicit On

Imports Bn.Classes
Imports System.IO
Imports Microsoft.WindowsAPICodePack.Taskbar

Public Class Downloader
    Dim p As Process()
    ' Creating a new instance of a FileDownloader
    Private WithEvents downloader As New Bn.Classes.FileDownloader

    ' A simple implementation of setting the directory path, adding files from a textbox and starting the download
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        'TaskbarManager.Instance.SetProgressValue(pBarTotalProgress.Value * 10, pBarTotalProgress.Maximum)
        If File.Exists(Application.StartupPath & "\Scanner\database\bytecode.cvd") Then
            My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\bytecode.cvd", Application.StartupPath & "\Scanner\database\backup\bytecode.cvd", FileIO.UIOption.OnlyErrorDialogs)
        End If
        If File.Exists(Application.StartupPath & "\Scanner\database\daily.cvd") Then
            My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\daily.cvd", Application.StartupPath & "\Scanner\database\backup\daily.cvd", FileIO.UIOption.OnlyErrorDialogs)
        End If
        If File.Exists(Application.StartupPath & "\Scanner\database\main.cvd") Then
            'My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\db\main.cvd", Application.StartupPath & "\Scanner\db\backup\main.cvd", FileIO.UIOption.OnlyErrorDialogs)
        End If

        Dim openFolderDialog As New FolderBrowserDialog
        With downloader
            'If openFolderDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            .Files.Clear()
            .LocalDirectory = Application.StartupPath & "\Scanner\database\downloading"

            For Each path As String In txtFilesToDownload.Lines
                ' The FileInfo structure will parse your path, and split it to the path itself and the file name, which will both be avaible for you
                .Files.Add(New Bn.Classes.FileDownloader.FileInfo(path))
            Next
            .Start()
            'End If
        End With
    End Sub
    Public Sub CheckIfRunning()
        p = Process.GetProcessesByName("Hawk scanner")
        If p.Length > 0 Then
            Try
                If File.Exists(Application.StartupPath & "\Scanner\database\bytecode.cvd") Then
                    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Scanner\database\bytecode.cvd")
                End If
                If File.Exists(Application.StartupPath & "\Scanner\database\daily.cvd") Then
                    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Scanner\database\daily.cvd")
                End If
            Catch ex As Exception

            End Try
            Try
                With downloader

                    .Files.Clear()
                    .LocalDirectory = Application.StartupPath & "\Scanner\database\downloading"

                    For Each path As String In txtFilesToDownload.Lines
                        ' The FileInfo structure will parse your path, and split it to the path itself and the file name, which will both be avaible for you
                        .Files.Add(New Bn.Classes.FileDownloader.FileInfo(path))
                    Next
                    .Start()
                End With
            Catch ex As Exception

            End Try
            'btnStart.PerformClick()
            Timer1.Start()
        Else
            MessageBox.Show("Failed to force update the database! Please launch HawkClam again and try the update process again.", "Force Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End If
    End Sub
    Private Sub btnPauze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
        ' Pause the downloader
        downloader.Pause()
        PictureBox1.Image = My.Resources._01
        Label2.Visible = True
        Label2.Text = "Updates Paused"
    End Sub

    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        ' Resume the downloader
        downloader.Resume()
        PictureBox1.Image = My.Resources._011
        Label2.Visible = False
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        ' Stop the downloader
        ' Note: This will not be instantantanious - the current requests need to be closed down, and the downloaded files need to be deleted
        downloader.Stop()

    End Sub

    ' This event is fired every time the paused or busy state is changed, and used here to set the controls of the interface
    Private Sub downloader_StateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles downloader.StateChanged 'This is equal to: Handles downloader.IsBusyChanged, downloader.IsPausedChanged
        ' Setting the buttons
        btnStart.Enabled = downloader.CanStart
        btnStop.Enabled = downloader.CanStop
        btnPause.Enabled = downloader.CanPause
        btnResume.Enabled = downloader.CanResume

        ' Enabling or disbaling the setting controls
        txtFilesToDownload.ReadOnly = downloader.IsBusy
        cbUseProgress.Enabled = Not downloader.IsBusy
    End Sub

    ' Show a message that the file sizes are being calculated
    ' This is redunant if (like applied here) you show a detailed status when the CalculatingFileSize is fired
    ' Also note that these events will only occur when the total file size is calculated in advance, in other words when the SupportsProgress is set to true
    Private Sub downloader_CalculationFileSizesStarted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.CalculationFileSizesStarted
        lblStatus.Text = "Calculating file sizes..."
    End Sub

    ' Show the progress of file size calculation
    Private Sub downloader_CalculationFileSize(ByVal sender As Object, ByVal fileNumber As Int32) Handles downloader.CalculatingFileSize
        lblStatus.Text = String.Format("Calculating file sizes - file {0} of {1}", fileNumber, downloader.Files.Count)
    End Sub

    ' Occurs every time of block of data has been downloaded, and can be used to display the progress with
    ' Note that you can also create a timer, and display the progress every certain interval
    ' Also note that the progress properties return a size in bytes, which is not really user friendly to display
    '   The FileDownloader class provides shared functions to format these byte amounts to a more readible format, either in binary or decimal notation
    Private Sub downloader_ProgressChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.ProgressChanged
        On Error Resume Next
        pBarFileProgress.Value = CInt(downloader.CurrentFilePercentage)
        lblFileProgressDetails.Text = String.Format("Downloaded {0} of {1} ({2}%)", Bn.Classes.FileDownloader.FormatSizeBinary(downloader.CurrentFileProgress), Bn.Classes.FileDownloader.FormatSizeBinary(downloader.CurrentFileSize), downloader.CurrentFilePercentage) & String.Format(" - {0}/s", Bn.Classes.FileDownloader.FormatSizeBinary(downloader.DownloadSpeed))
        If downloader.SupportsProgress Then
            pBarTotalProgress.Value = CInt(downloader.TotalPercentage)
            lblTotalProgressDetails.Text = String.Format("Downloaded {0} of {1} ({2}%)", Bn.Classes.FileDownloader.FormatSizeBinary(downloader.TotalProgress), Bn.Classes.FileDownloader.FormatSizeBinary(downloader.TotalSize), downloader.TotalPercentage)
        End If
    End Sub

    ' This will be shown when the request for the file is made, before the download starts (or fails)
    Private Sub downloader_FileDownloadAttempting(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.FileDownloadAttempting
        lblStatus.Text = String.Format("Preparing {0}", downloader.CurrentFile.Path)
    End Sub

    ' Display of the file info after the download started
    Private Sub downloader_FileDownloadStarted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.FileDownloadStarted
        Label2.Visible = False
        PictureBox1.Image = My.Resources._011
        lblStatus.Text = String.Format("Downloading {0}", downloader.CurrentFile.Path)
        lblFileSize.Text = String.Format("File size: {0}", Bn.Classes.FileDownloader.FormatSizeBinary(downloader.CurrentFileSize))
        lblTargetPath.Text = String.Format("Saving to {0}\{1}", downloader.LocalDirectory, downloader.CurrentFile.Name)
    End Sub

    ' Display of a completion message, showing the amount of files that has been downloaded.
    ' Note, this does not hold into account any possible failed file downloads
    Private Sub downloader_Completed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.Completed
        lblStatus.Text = String.Format("Download complete, downloaded {0} files.", downloader.Files.Count.ToString)
        Try
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\bytecode.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\bytecode.cvd", Application.StartupPath & "\Scanner\database\bytecode.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\daily.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\daily.cvd", Application.StartupPath & "\Scanner\database\daily.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\main.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\main.cvd", Application.StartupPath & "\Scanner\database\main.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\junk.ndb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\junk.ndb", Application.StartupPath & "\Scanner\database\junk.ndb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\rogue.hdb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\rogue.hdb", Application.StartupPath & "\Scanner\database\rogue.hdb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\foxhole_filename.cdb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\foxhole_filename.cdb", Application.StartupPath & "\Scanner\database\foxhole_filename.cdb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\foxhole_generic.cdb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\foxhole_generic.cdb", Application.StartupPath & "\Scanner\database\foxhole_generic.cdb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\foxhole_js.cdb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\foxhole_js.cdb", Application.StartupPath & "\Scanner\database\foxhole_js.cdb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\badmacro.ndb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\badmacro.ndb", Application.StartupPath & "\Scanner\database\badmacro.ndb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\scam.ndb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\scam.ndb", Application.StartupPath & "\Scanner\database\scam.ndb", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\sanesecurity.ftm") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\sanesecurity.ftm", Application.StartupPath & "\Scanner\database\sanesecurity.ftm", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\sigwhitelist.ign2") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\sigwhitelist.ign2", Application.StartupPath & "\Scanner\database\sigwhitelist.ign2", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\downloading\hawk.hdb") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\downloading\hawk.hdb", Application.StartupPath & "\Scanner\database\hawk.hdb", FileIO.UIOption.OnlyErrorDialogs)
            End If
        Catch ex As Exception

        End Try
        frmMain.Panel1.Visible = False
        Process.Start(Application.StartupPath & "\Scanner\Hawk scanner.exe")

        Application.Exit()
    End Sub

    ' Show a message that the downloads are being canceled - all files downloaded will be deleted and the current ones will be aborted
    Private Sub downloader_CancelRequested(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.CancelRequested
        lblStatus.Text = "Canceling downloads..."
    End Sub

    ' Show a message that the downloads are being canceled - all files downloaded will be deleted and the current ones will be aborted
    Private Sub downloader_DeletingFilesAfterCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.DeletingFilesAfterCancel
        lblStatus.Text = "Canceling downloads - deleting files..."
    End Sub

    ' Show a message saying the downloads have been canceled
    Private Sub downloader_Canceled(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downloader.Canceled
        lblStatus.Text = "Download(s) canceled"
        pBarFileProgress.Value = 0
        pBarTotalProgress.Value = 0
        lblFileProgressDetails.Text = "-"
        lblTotalProgressDetails.Text = "-"
        lblFileSize.Text = "-"
        lblTargetPath.Text = "-"
        frmMain.Panel1.Visible = False
        Try
            If File.Exists(Application.StartupPath & "\Scanner\database\backup\bytecode.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\backup\bytecode.cvd", Application.StartupPath & "\Scanner\database\bytecode.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\backup\daily.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\backup\daily.cvd", Application.StartupPath & "\Scanner\database\daily.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
            If File.Exists(Application.StartupPath & "\Scanner\database\backup\main.cvd") Then
                My.Computer.FileSystem.MoveFile(Application.StartupPath & "\Scanner\database\backup\main.cvd", Application.StartupPath & "\Scanner\database\main.cvd", FileIO.UIOption.OnlyErrorDialogs)
            End If
        Catch ex As Exception

        End Try
    End Sub

    ' Setting the SupportsProgress property - if set to false, no total progress data will be avaible!
    Private Sub cbUseProgress_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUseProgress.CheckedChanged
        downloader.SupportsProgress = cbUseProgress.Checked
    End Sub

    ' Setting the DeleteCompletedFilesAfterCancel property - indicates if the completed files should be deleted after cancellation
    Private Sub cbDeleteCompletedFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDeleteCompletedFiles.CheckedChanged
        downloader.DeleteCompletedFilesAfterCancel = cbDeleteCompletedFiles.Checked
    End Sub

    Private Sub Downloader_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub lblStatus_TextChanged(sender As Object, e As EventArgs) Handles lblStatus.TextChanged

        lblStatus.Left = CInt((frmMain.ClientSize.Width - lblStatus.Width) / 2)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If lblStatus.Text.Contains("Wait") Then
            Timer1.Stop()
            'Threading.Thread.Sleep(5000)

            'MessageBox.Show("Failed to force update the database! Please launch HawkClam again and try the update process again.", "Force Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

        End If
        If lblStatus.Text.Contains("Downloading") Then


        End If
    End Sub
End Class