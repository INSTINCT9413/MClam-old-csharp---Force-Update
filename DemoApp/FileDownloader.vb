'**************************************************************'
' BN+ Framework                                                '
' By De Dauw Jeroen - jeroendedauw@gmail.com                   '
' Based on code by Carmine_XX                                  '
'**************************************************************'
' Namespace Bn.Classes                                         '
' Class FileDownloader v1.0.3 - May 2009                       '
'**************************************************************'
' Copyright 2009 - BN+ Discussions                             '
' http://code.bn2vs.com                                        '
'**************************************************************'

' Author notes
' > Although this class is part of BN+ Framework, it can be used as a stand alone without modifications

' This code is avaible at
' > BN+ Discussions: http://code.bn2vs.com/viewtopic.php?t=150
' > The Code Project: http://www.codeproject.com/KB/vb/FileDownloader.aspx

' C#.Net implementation avaible at
' > BN+ Discussions: http://code.bn2vs.com/viewtopic.php?t=153
' > The Code Project: http://www.codeproject.com/KB/cs/BackgroundFileDownloader.aspx

' Dutch support can be found here: http://www.helpmij.nl/forum/showthread.php?t=416568

Option Strict On : Option Explicit On

Imports System.ComponentModel
Imports System.IO
Imports System.Net

Namespace Bn.Classes

#Region "Public Class FileDownloader"
    ''' <summary>Class for downloading files in the background that supports info about their progress, the total progress, cancellation, pausing, and resuming. The downloads will run on a separate thread so you don't have to worry about multihreading yourself. </summary>
    ''' <remarks>Class FileDownloader v1.0.3, by De Dauw Jeroen - May 2009</remarks>
    Public Class FileDownloader
        Inherits System.Object
        Implements IDisposable

#Region "Nested types"

#Region "Public Structure FileInfo"
        ''' <summary>Simple structure for managing file info</summary>
        Public Structure FileInfo
            ''' <summary>The complete path of the file (directory + filename)</summary>
            Public Path As String
            ''' <summary>The name of the file</summary>
            Public Name As String

            ''' <summary>Create a new instance of FileInfo</summary>
            ''' <param name="path">The complete path of the file (directory + filename)</param>
            Public Sub New(ByVal path As String)
                Me.Path = path
                Me.Name = Me.Path.Split("/"c)(Me.Path.Split("/"c).Length - 1)
            End Sub
        End Structure
#End Region

#Region "Private Enum [Event]"
        ''' <summary>Holder for events that are triggered in the background worker but need to be fired in the main thread</summary>
        Private Enum [Event]
            CalculationFileSizesStarted

            FileSizesCalculationComplete
            DeletingFilesAfterCancel

            FileDownloadAttempting
            FileDownloadStarted
            FileDownloadStopped
            FileDownloadSucceeded

            ProgressChanged
        End Enum
#End Region

#Region "Private Enum InvokeType"
        ''' <summary>Holder for the action that needs to be invoked</summary>
        Private Enum InvokeType
            EventRaiser
            FileDownloadFailedRaiser
            CalculatingFileNrRaiser
        End Enum
#End Region

#End Region

#Region "Events"
        ''' <summary>Occurs when the file downloading has started</summary>
        Public Event Started As EventHandler
        ''' <summary>Occurs when the file downloading has been paused</summary>
        Public Event Paused As EventHandler
        ''' <summary>Occurs when the file downloading has been resumed</summary>
        Public Event Resumed As EventHandler
        ''' <summary>Occurs when the user has requested to cancel the downloads</summary>
        Public Event CancelRequested As EventHandler
        ''' <summary>Occurs when the user has requested to cancel the downloads and the cleanup of the downloaded files has started</summary>
        Public Event DeletingFilesAfterCancel As EventHandler
        ''' <summary>Occurs when the file downloading has been canceled by the user</summary>
        Public Event Canceled As EventHandler
        ''' <summary>Occurs when the file downloading has been completed (without canceling it)</summary>
        Public Event Completed As EventHandler
        ''' <summary>Occurs when the file downloading has been stopped by either cancellation or completion</summary>
        Public Event Stopped As EventHandler

        ''' <summary>Occurs when the busy state of the FileDownloader has changed</summary>
        Public Event IsBusyChanged As EventHandler
        ''' <summary>Occurs when the pause state of the FileDownloader has changed</summary>
        Public Event IsPausedChanged As EventHandler
        ''' <summary>Occurs when the either the busy or pause state of the FileDownloader have changed</summary>
        Public Event StateChanged As EventHandler

        ''' <summary>Occurs when the calculation of the file sizes has started</summary>
        Public Event CalculationFileSizesStarted As EventHandler
        ''' <summary>Occurs when the calculation of the file sizes has started</summary>
        Public Event CalculatingFileSize As FileSizeCalculationEventHandler
        ''' <summary>Occurs when the calculation of the file sizes has been completed</summary>
        Public Event FileSizesCalculationComplete As EventHandler

        ''' <summary>Occurs when the FileDownloader attempts to get a web response to download the file</summary>
        Public Event FileDownloadAttempting As EventHandler
        ''' <summary>Occurs when a file download has started</summary>
        Public Event FileDownloadStarted As EventHandler
        ''' <summary>Occurs when a file download has stopped</summary>
        Public Event FileDownloadStopped As EventHandler
        ''' <summary>Occurs when a file download has been completed successfully</summary>
        Public Event FileDownloadSucceeded As EventHandler
        ''' <summary>Occurs when a file download has been completed unsuccessfully</summary>
        Public Event FileDownloadFailed(ByVal sender As Object, ByVal e As Exception)

        ''' <summary>Occurs every time a block of data has been downloaded</summary>
        Public Event ProgressChanged As EventHandler
#End Region

#Region "Fields"
        Public Delegate Sub FileSizeCalculationEventHandler(ByVal sender As Object, ByVal fileNumber As Int32)

        Private WithEvents bgwDownloader As New BackgroundWorker
        Private trigger As New Threading.ManualResetEvent(True)

        ' Preferences
        Private m_supportsProgress, m_deleteCompletedFiles As Boolean
        Private m_packageSize, m_stopWatchCycles As Int32

        ' State
        Private m_disposed As Boolean = False
        Private m_busy, m_paused, m_canceled As Boolean
        Private m_currentFileProgress, m_totalProgress, m_currentFileSize As Int64
        Private m_currentSpeed, m_fileNr As Int32

        ' Data
        Private m_localDirectory As String
        Private m_files As New List(Of FileInfo)
        Private m_totalSize As Int64
#End Region

#Region "Constructors"
        ''' <summary>Create a new instance of a FileDownloader</summary>
        ''' <param name="supportsProgress">Optional. Boolean. Should the FileDownloader support total progress statistics?</param>
        Public Sub New(Optional ByVal supportsProgress As Boolean = False)
            ' Set the bgw properties
            bgwDownloader.WorkerReportsProgress = True
            bgwDownloader.WorkerSupportsCancellation = True

            ' Set the default class preferences
            Me.SupportsProgress = supportsProgress
            Me.PackageSize = 4096
            Me.StopWatchCyclesAmount = 5
            Me.DeleteCompletedFilesAfterCancel = False
        End Sub
#End Region

#Region "Public methods"
        ''' <summary>Start the downloads</summary>
        Public Sub Start()
            Me.IsBusy = True
        End Sub

        ''' <summary>pause the downloads</summary>
        Public Sub Pause()
            Me.IsPaused = True
        End Sub

        ''' <summary>Resume the downloads</summary>
        Public Sub [Resume]()
            Me.IsPaused = False
        End Sub

        ''' <summary>Stop the downloads</summary>
        Public Overloads Sub [Stop]()
            Me.IsBusy = False
        End Sub

        ''' <summary>Stop the downloads</summary>
        ''' <param name="deleteCompletedFiles">Required. Boolean. Indicates wether the complete downloads should be deleted</param>
        Public Overloads Sub [Stop](ByVal deleteCompletedFiles As Boolean)
            Me.DeleteCompletedFilesAfterCancel = deleteCompletedFiles
            Me.Stop()
        End Sub

        ''' <summary>Release the recources held by the FileDownloader</summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>Format an amount of bytes to a more readible notation with binary notation symbols</summary>
        ''' <param name="size">Required. Int64. The raw amount of bytes</param>
        ''' <param name="decimals">Optional. Int32. The amount of decimals you want to have displayed in the notation</param>
        Public Shared Function FormatSizeBinary(ByVal size As Int64, Optional ByVal decimals As Int32 = 2) As String
            ' By De Dauw Jeroen - April 2009 - jeroen_dedauw@yahoo.com
            Dim sizes() As String = {"B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"}
            Dim formattedSize As Double = size
            Dim sizeIndex As Int32 = 0
            While formattedSize >= 1024 And sizeIndex < sizes.Length
                formattedSize /= 1024
                sizeIndex += 1
            End While
            Return Math.Round(formattedSize, decimals).ToString & sizes(sizeIndex)
        End Function

        ''' <summary>Format an amount of bytes to a more readible notation with decimal notation symbols</summary>
        ''' <param name="size">Required. Int64. The raw amount of bytes</param>
        ''' <param name="decimals">Optional. Int32. The amount of decimals you want to have displayed in the notation</param>
        Public Shared Function FormatSizeDecimal(ByVal size As Int64, Optional ByVal decimals As Int32 = 2) As String
            ' By De Dauw Jeroen - April 2009 - jeroen_dedauw@yahoo.com
            Dim sizes() As String = {"B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"}
            Dim formattedSize As Double = size
            Dim sizeIndex As Int32 = 0
            While formattedSize >= 1000 And sizeIndex < sizes.Length
                formattedSize /= 1000
                sizeIndex += 1
            End While
            Return Math.Round(formattedSize, decimals).ToString & sizes(sizeIndex)
        End Function
#End Region

#Region "Private/protected methods"
        Private Sub bgwDownloader_DoWork() Handles bgwDownloader.DoWork
            Dim fileNr As Int32 = 0

            If Me.SupportsProgress Then calculateFilesSize()

            If Not Directory.Exists(Me.LocalDirectory) Then Directory.CreateDirectory(Me.LocalDirectory)

            While fileNr < Me.Files.Count And Not bgwDownloader.CancellationPending
                m_fileNr = fileNr
                downloadFile(fileNr)

                If bgwDownloader.CancellationPending Then
                    fireEventFromBgw([Event].DeletingFilesAfterCancel)
                    cleanUpFiles(If(Me.DeleteCompletedFilesAfterCancel, 0, m_fileNr), If(Me.DeleteCompletedFilesAfterCancel, m_fileNr + 1, 1))
                Else
                    fileNr += 1
                End If
            End While
        End Sub

        Private Sub fireEventFromBgw(ByVal eventName As [Event])
            bgwDownloader.ReportProgress(InvokeType.EventRaiser, eventName)
        End Sub

        Private Sub bwgDownloader_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bgwDownloader.ProgressChanged
            Select Case CType(e.ProgressPercentage, InvokeType)
                Case InvokeType.EventRaiser
                    Select Case CType(e.UserState, [Event])
                        Case [Event].CalculationFileSizesStarted
                            RaiseEvent CalculationFileSizesStarted(Me, New EventArgs)
                        Case [Event].FileSizesCalculationComplete
                            RaiseEvent FileSizesCalculationComplete(Me, New EventArgs)
                        Case [Event].DeletingFilesAfterCancel
                            RaiseEvent DeletingFilesAfterCancel(Me, New EventArgs)

                        Case [Event].FileDownloadAttempting
                            RaiseEvent FileDownloadAttempting(Me, New EventArgs)
                        Case [Event].FileDownloadStarted
                            RaiseEvent FileDownloadStarted(Me, New EventArgs)
                        Case [Event].FileDownloadStopped
                            RaiseEvent FileDownloadStopped(Me, New EventArgs)
                        Case [Event].FileDownloadSucceeded
                            RaiseEvent FileDownloadSucceeded(Me, New EventArgs)
                        Case [Event].ProgressChanged
                            RaiseEvent ProgressChanged(Me, New EventArgs)
                    End Select
                Case InvokeType.FileDownloadFailedRaiser
                    RaiseEvent FileDownloadFailed(Me, CType(e.UserState, Exception))
                Case InvokeType.CalculatingFileNrRaiser
                    RaiseEvent CalculatingFileSize(Me, CInt(e.UserState))
            End Select
        End Sub

        Private Sub cleanUpFiles(Optional ByVal start As Int32 = 0, Optional ByVal length As Int32 = -1)
            Dim last As Int32 = If(length < 0, Me.Files.Count - 1, start + length - 1)
            For fileNr As Int32 = start To last
                Dim fullPath As String = Me.LocalDirectory & "\" & Me.Files(fileNr).Name
                If IO.File.Exists(fullPath) Then IO.File.Delete(fullPath)
            Next
        End Sub

        Private Sub calculateFilesSize()
            fireEventFromBgw([Event].CalculationFileSizesStarted)
            m_totalSize = 0

            For fileNr As Int32 = 0 To Me.Files.Count - 1
                bgwDownloader.ReportProgress(InvokeType.CalculatingFileNrRaiser, fileNr + 1)
                Try
                    Dim webReq As HttpWebRequest = CType(Net.WebRequest.Create(Me.Files(fileNr).Path), HttpWebRequest)
                    Dim webResp As HttpWebResponse = CType(webReq.GetResponse, HttpWebResponse)
                    m_totalSize += webResp.ContentLength
                    webResp.Close()
                Catch ex As Exception
                End Try
            Next
            fireEventFromBgw([Event].FileSizesCalculationComplete)
        End Sub

        Private Sub downloadFile(ByVal fileNr As Int32)
            m_currentFileSize = 0
            fireEventFromBgw([Event].FileDownloadAttempting)

            Dim file As FileInfo = Me.Files(fileNr)
            Dim size As Int64 = 0

            Dim readBytes(Me.PackageSize - 1) As Byte
            Dim currentPackageSize As Int32
            Dim writer As New FileStream(Me.LocalDirectory & "\" & file.Name, IO.FileMode.Create)
            Dim speedTimer As New Stopwatch
            Dim readings As Int32 = 0
            Dim exc As Exception

            Dim webReq As HttpWebRequest
            Dim webResp As HttpWebResponse

            Try
                webReq = CType(Net.WebRequest.Create(Me.Files(fileNr).Path), HttpWebRequest)
                webResp = CType(webReq.GetResponse, HttpWebResponse)

                size = webResp.ContentLength
            Catch ex As Exception
                exc = ex
            End Try

            m_currentFileSize = size
            fireEventFromBgw([Event].FileDownloadStarted)

            If exc IsNot Nothing Then
                bgwDownloader.ReportProgress(InvokeType.FileDownloadFailedRaiser, exc)
            Else
                m_currentFileProgress = 0
                While m_currentFileProgress < size
                    If bgwDownloader.CancellationPending Then
                        speedTimer.Stop()
                        writer.Close()
                        webResp.Close()
                        Exit Sub
                    End If
                    trigger.WaitOne()

                    speedTimer.Start()

                    currentPackageSize = webResp.GetResponseStream().Read(readBytes, 0, Me.PackageSize)
                    m_currentFileProgress += currentPackageSize
                    m_totalProgress += currentPackageSize
                    fireEventFromBgw([Event].ProgressChanged)

                    writer.Write(readBytes, 0, currentPackageSize)
                    readings += 1

                    If readings >= Me.StopWatchCyclesAmount Then
                        m_currentSpeed = CInt(Me.PackageSize * StopWatchCyclesAmount * 1000 / (speedTimer.ElapsedMilliseconds + 1))
                        speedTimer.Reset()
                        readings = 0
                    End If
                End While

                speedTimer.Stop()
                writer.Close()
                webResp.Close()
                fireEventFromBgw([Event].FileDownloadSucceeded)
            End If
            fireEventFromBgw([Event].FileDownloadStopped)
        End Sub

        Private Sub bgwDownloader_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDownloader.RunWorkerCompleted
            Me.IsPaused = False
            m_busy = False

            If Me.HasBeenCanceled Then
                RaiseEvent Canceled(Me, New EventArgs)
            Else
                RaiseEvent Completed(Me, New EventArgs)
            End If

            RaiseEvent Stopped(Me, New EventArgs)
            RaiseEvent IsBusyChanged(Me, New EventArgs)
            RaiseEvent StateChanged(Me, New EventArgs)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not m_disposed Then
                If disposing Then
                    ' Free other state (managed objects)
                    bgwDownloader.Dispose()
                End If
                ' Free your own state (unmanaged objects)
                ' Set large fields to null
                Me.Files = Nothing
            End If
            m_disposed = True
        End Sub
#End Region

#Region "Properties"
        ''' <summary>Gets or sets the list of files to download</summary>
        Public Property Files() As List(Of FileInfo)
            Get
                Return m_files
            End Get
            Set(ByVal value As List(Of FileInfo))
                If Me.IsBusy Then
                    Throw New InvalidOperationException("You can not change the file list during the download")
                Else
                    If Me.Files IsNot value Then m_files = value
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the local directory in which files will be stored</summary>
        Public Property LocalDirectory() As String
            Get
                Return m_localDirectory
            End Get
            Set(ByVal value As String)
                If value <> Me.LocalDirectory Then
                    m_localDirectory = value
                End If
            End Set
        End Property

        ''' <summary>Gets or sets if the FileDownloader should support total progress statistics. Note that when enabled, the FileDownloader will have to get the size of each file before starting to download them, which can delay the operation.</summary>
        Public Property SupportsProgress() As Boolean
            Get
                Return m_supportsProgress
            End Get
            Set(ByVal value As Boolean)
                If Me.IsBusy Then
                    Throw New InvalidOperationException("You can not change the SupportsProgress property during the download")
                Else
                    m_supportsProgress = value
                End If
            End Set
        End Property

        ''' <summary>Gets or sets if when the download process is cancelled the complete downloads should be deleted</summary>
        Public Property DeleteCompletedFilesAfterCancel() As Boolean
            Get
                Return m_deleteCompletedFiles
            End Get
            Set(ByVal value As Boolean)
                m_deleteCompletedFiles = value
            End Set
        End Property

        ''' <summary>Gets or sets the size of the blocks that will be downloaded</summary>
        Public Property PackageSize() As Int32
            Get
                Return m_packageSize
            End Get
            Set(ByVal value As Int32)
                If value > 0 Then
                    m_packageSize = value
                Else
                    Throw New InvalidOperationException("The PackageSize needs to be greather then 0")
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the amount of blocks that need to be downloaded before the progress speed is re-calculated. Note: setting this to a low value might decrease the accuracy</summary>
        Public Property StopWatchCyclesAmount() As Int32
            Get
                Return m_stopWatchCycles
            End Get
            Set(ByVal value As Int32)
                If value > 0 Then
                    m_stopWatchCycles = value
                Else
                    Throw New InvalidOperationException("The StopWatchCyclesAmount needs to be greather then 0")
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the busy state of the FileDownloader</summary>
        Public Property IsBusy() As Boolean
            Get
                Return m_busy
            End Get
            Set(ByVal value As Boolean)
                If Me.IsBusy <> value Then
                    m_busy = value
                    m_canceled = Not value
                    If Me.IsBusy Then
                        m_totalProgress = 0
                        bgwDownloader.RunWorkerAsync()
                        RaiseEvent Started(Me, New EventArgs)
                        RaiseEvent IsBusyChanged(Me, New EventArgs)
                        RaiseEvent StateChanged(Me, New EventArgs)
                    Else
                        m_paused = False
                        bgwDownloader.CancelAsync()
                        RaiseEvent CancelRequested(Me, New EventArgs)
                        RaiseEvent StateChanged(Me, New EventArgs)
                    End If
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the pause state of the FileDownloader</summary>
        Public Property IsPaused() As Boolean
            Get
                Return m_paused
            End Get
            Set(ByVal value As Boolean)
                If Me.IsBusy Then
                    If value <> Me.IsPaused Then
                        m_paused = value
                        If Me.IsPaused Then
                            trigger.Reset()
                            RaiseEvent Paused(Me, New EventArgs)
                        Else
                            trigger.Set()
                            RaiseEvent Resumed(Me, New EventArgs)
                        End If
                        RaiseEvent IsPausedChanged(Me, New EventArgs)
                        RaiseEvent StateChanged(Me, New EventArgs)
                    End If
                End If
            End Set
        End Property

        ''' <summary>Gets if the FileDownloader can start</summary>
        Public ReadOnly Property CanStart() As Boolean
            Get
                Return Not Me.IsBusy
            End Get
        End Property

        ''' <summary>Gets if the FileDownloader can pause</summary>
        Public ReadOnly Property CanPause() As Boolean
            Get
                Return Me.IsBusy And Not Me.IsPaused And Not bgwDownloader.CancellationPending
            End Get
        End Property

        ''' <summary>Gets if the FileDownloader can resume</summary>
        Public ReadOnly Property CanResume() As Boolean
            Get
                Return Me.IsBusy And Me.IsPaused And Not bgwDownloader.CancellationPending
            End Get
        End Property

        ''' <summary>Gets if the FileDownloader can stop</summary>
        Public ReadOnly Property CanStop() As Boolean
            Get
                Return Me.IsBusy And Not bgwDownloader.CancellationPending
            End Get
        End Property

        ''' <summary>Gets the total size of all files together. Only avaible when the FileDownloader suports progress</summary>
        Public ReadOnly Property TotalSize() As Int64
            Get
                If Me.SupportsProgress Then
                    Return m_totalSize
                Else
                    Throw New InvalidOperationException("This FileDownloader that it doesn't support progress. Modify SupportsProgress to state that it does support progress to get the total size.")
                End If
            End Get
        End Property

        ''' <summary>Gets the total amount of bytes downloaded</summary>
        Public ReadOnly Property TotalProgress() As Int64
            Get
                Return m_totalProgress
            End Get
        End Property

        ''' <summary>Gets the amount of bytes downloaded of the current file</summary>
        Public ReadOnly Property CurrentFileProgress() As Int64
            Get
                Return m_currentFileProgress
            End Get
        End Property

        ''' <summary>Gets the total download percentage. Only avaible when the FileDownloader suports progress</summary>
        Public ReadOnly Property TotalPercentage(Optional ByVal decimals As Int32 = 0) As Double
            Get
                If Me.SupportsProgress Then
                    Return Math.Round(Me.TotalProgress / Me.TotalSize * 100, decimals)
                Else
                    Throw New InvalidOperationException("This FileDownloader that it doesn't support progress. Modify SupportsProgress to state that it does support progress.")
                End If
            End Get
        End Property

        ''' <summary>Gets the percentage of the current file progress</summary>
        Public ReadOnly Property CurrentFilePercentage(Optional ByVal decimals As Int32 = 0) As Double
            Get
                Return Math.Round(Me.CurrentFileProgress / Me.CurrentFileSize * 100, decimals)
            End Get
        End Property

        ''' <summary>Gets the current download speed in bytes</summary>
        Public ReadOnly Property DownloadSpeed() As Int32
            Get
                Return m_currentSpeed
            End Get
        End Property

        ''' <summary>Gets the FileInfo object representing the current file</summary>
        Public ReadOnly Property CurrentFile() As FileInfo
            Get
                Return Me.Files(m_fileNr)
            End Get
        End Property

        ''' <summary>Gets the size of the current file in bytes</summary>
        Public ReadOnly Property CurrentFileSize() As Int64
            Get
                Return m_currentFileSize
            End Get
        End Property

        ''' <summary>Gets if the last download was canceled by the user</summary>
        Private ReadOnly Property HasBeenCanceled() As Boolean
            Get
                Return m_canceled
            End Get
        End Property
#End Region

    End Class
#End Region

End Namespace