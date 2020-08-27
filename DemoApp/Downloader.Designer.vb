<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Downloader
    Inherits System.Windows.Forms.UserControl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Downloader))
        Me.txtFilesToDownload = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblTargetPath = New System.Windows.Forms.Label()
        Me.lblFileSize = New System.Windows.Forms.Label()
        Me.pBarTotalProgress = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pBarFileProgress = New System.Windows.Forms.ProgressBar()
        Me.lblTotalProgressDetails = New System.Windows.Forms.Label()
        Me.lblFileProgressDetails = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnResume = New System.Windows.Forms.Button()
        Me.cbUseProgress = New System.Windows.Forms.CheckBox()
        Me.cbDeleteCompletedFiles = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFilesToDownload
        '
        Me.txtFilesToDownload.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilesToDownload.Location = New System.Drawing.Point(12, 25)
        Me.txtFilesToDownload.Name = "txtFilesToDownload"
        Me.txtFilesToDownload.Size = New System.Drawing.Size(576, 56)
        Me.txtFilesToDownload.TabIndex = 0
        Me.txtFilesToDownload.Text = resources.GetString("txtFilesToDownload.Text")
        Me.txtFilesToDownload.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Files to download"
        Me.Label1.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(220, 176)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(158, 13)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Text = "Please Wait - Contacting Server"
        '
        'lblTargetPath
        '
        Me.lblTargetPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTargetPath.AutoSize = True
        Me.lblTargetPath.Location = New System.Drawing.Point(9, 321)
        Me.lblTargetPath.Name = "lblTargetPath"
        Me.lblTargetPath.Size = New System.Drawing.Size(158, 13)
        Me.lblTargetPath.TabIndex = 3
        Me.lblTargetPath.Text = "Please Wait - Contacting Server"
        Me.lblTargetPath.Visible = False
        '
        'lblFileSize
        '
        Me.lblFileSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFileSize.AutoSize = True
        Me.lblFileSize.Location = New System.Drawing.Point(9, 321)
        Me.lblFileSize.Name = "lblFileSize"
        Me.lblFileSize.Size = New System.Drawing.Size(52, 13)
        Me.lblFileSize.TabIndex = 4
        Me.lblFileSize.Text = "Waiting..."
        '
        'pBarTotalProgress
        '
        Me.pBarTotalProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pBarTotalProgress.Location = New System.Drawing.Point(9, 232)
        Me.pBarTotalProgress.Name = "pBarTotalProgress"
        Me.pBarTotalProgress.Size = New System.Drawing.Size(576, 20)
        Me.pBarTotalProgress.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Total progress"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 275)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "File progress"
        '
        'pBarFileProgress
        '
        Me.pBarFileProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pBarFileProgress.Location = New System.Drawing.Point(9, 291)
        Me.pBarFileProgress.Name = "pBarFileProgress"
        Me.pBarFileProgress.Size = New System.Drawing.Size(576, 10)
        Me.pBarFileProgress.TabIndex = 7
        '
        'lblTotalProgressDetails
        '
        Me.lblTotalProgressDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalProgressDetails.AutoSize = True
        Me.lblTotalProgressDetails.Location = New System.Drawing.Point(9, 255)
        Me.lblTotalProgressDetails.Name = "lblTotalProgressDetails"
        Me.lblTotalProgressDetails.Size = New System.Drawing.Size(135, 13)
        Me.lblTotalProgressDetails.TabIndex = 9
        Me.lblTotalProgressDetails.Text = "Getting files for download..."
        '
        'lblFileProgressDetails
        '
        Me.lblFileProgressDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFileProgressDetails.AutoSize = True
        Me.lblFileProgressDetails.Location = New System.Drawing.Point(9, 304)
        Me.lblFileProgressDetails.Name = "lblFileProgressDetails"
        Me.lblFileProgressDetails.Size = New System.Drawing.Size(73, 13)
        Me.lblFileProgressDetails.TabIndex = 10
        Me.lblFileProgressDetails.Text = "Please Wait..."
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.BackColor = System.Drawing.SystemColors.Control
        Me.btnStart.Location = New System.Drawing.Point(432, 331)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 11
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnPause
        '
        Me.btnPause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPause.Enabled = False
        Me.btnPause.Location = New System.Drawing.Point(432, 331)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(75, 23)
        Me.btnPause.TabIndex = 12
        Me.btnPause.Text = "Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(513, 333)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 14
        Me.btnStop.Text = "Cancel"
        Me.btnStop.UseVisualStyleBackColor = True
        Me.btnStop.Visible = False
        '
        'btnResume
        '
        Me.btnResume.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResume.Enabled = False
        Me.btnResume.Location = New System.Drawing.Point(513, 331)
        Me.btnResume.Name = "btnResume"
        Me.btnResume.Size = New System.Drawing.Size(75, 23)
        Me.btnResume.TabIndex = 13
        Me.btnResume.Text = "Resume"
        Me.btnResume.UseVisualStyleBackColor = True
        '
        'cbUseProgress
        '
        Me.cbUseProgress.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cbUseProgress.AutoSize = True
        Me.cbUseProgress.Checked = True
        Me.cbUseProgress.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbUseProgress.Location = New System.Drawing.Point(479, 339)
        Me.cbUseProgress.Name = "cbUseProgress"
        Me.cbUseProgress.Size = New System.Drawing.Size(154, 17)
        Me.cbUseProgress.TabIndex = 15
        Me.cbUseProgress.Text = "Calculate the total progress"
        Me.cbUseProgress.UseVisualStyleBackColor = True
        Me.cbUseProgress.Visible = False
        '
        'cbDeleteCompletedFiles
        '
        Me.cbDeleteCompletedFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbDeleteCompletedFiles.AutoSize = True
        Me.cbDeleteCompletedFiles.Checked = True
        Me.cbDeleteCompletedFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbDeleteCompletedFiles.Location = New System.Drawing.Point(12, 337)
        Me.cbDeleteCompletedFiles.Name = "cbDeleteCompletedFiles"
        Me.cbDeleteCompletedFiles.Size = New System.Drawing.Size(235, 17)
        Me.cbDeleteCompletedFiles.TabIndex = 16
        Me.cbDeleteCompletedFiles.Text = "Delete complete downloads when cancelled"
        Me.cbDeleteCompletedFiles.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(264, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Please Wait..."
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox1.Image = Global.HawkClam.My.Resources.Resources._58d40bca9e8a2993238b9f18_preloader_circle
        Me.PictureBox1.Location = New System.Drawing.Point(148, -64)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(304, 304)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        '
        'Downloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblTotalProgressDetails)
        Me.Controls.Add(Me.pBarFileProgress)
        Me.Controls.Add(Me.pBarTotalProgress)
        Me.Controls.Add(Me.cbDeleteCompletedFiles)
        Me.Controls.Add(Me.cbUseProgress)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnResume)
        Me.Controls.Add(Me.btnPause)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.lblFileProgressDetails)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblFileSize)
        Me.Controls.Add(Me.lblTargetPath)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFilesToDownload)
        Me.Controls.Add(Me.PictureBox1)
        Me.MinimumSize = New System.Drawing.Size(407, 368)
        Me.Name = "Downloader"
        Me.Size = New System.Drawing.Size(600, 368)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblTargetPath As System.Windows.Forms.Label
    Friend WithEvents lblFileSize As System.Windows.Forms.Label
    Friend WithEvents pBarTotalProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pBarFileProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTotalProgressDetails As System.Windows.Forms.Label
    Friend WithEvents lblFileProgressDetails As System.Windows.Forms.Label
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnResume As System.Windows.Forms.Button
    Friend WithEvents cbUseProgress As System.Windows.Forms.CheckBox
    Friend WithEvents cbDeleteCompletedFiles As System.Windows.Forms.CheckBox
    Public WithEvents btnStart As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents AdobeProgressBar1 As AdobeProgressBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents AdobeProgressBar2 As AdobeProgressBar
    Public WithEvents txtFilesToDownload As RichTextBox
End Class
