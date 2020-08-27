<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.cmdStartScan = New System.Windows.Forms.Button()
        Me.txtFileToScan = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTimeElapsed = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblVirusName = New System.Windows.Forms.Label()
        Me.lblIsInfected = New System.Windows.Forms.Label()
        Me.lblScannedData = New System.Windows.Forms.Label()
        Me.lblFileSize = New System.Windows.Forms.Label()
        Me.lblFullPath = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lstLog = New System.Windows.Forms.ListBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(576, 11)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(109, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "Select file..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'cmdStartScan
        '
        Me.cmdStartScan.Location = New System.Drawing.Point(576, 122)
        Me.cmdStartScan.Name = "cmdStartScan"
        Me.cmdStartScan.Size = New System.Drawing.Size(109, 49)
        Me.cmdStartScan.TabIndex = 1
        Me.cmdStartScan.Text = "Start Scan"
        Me.cmdStartScan.UseVisualStyleBackColor = True
        '
        'txtFileToScan
        '
        Me.txtFileToScan.Location = New System.Drawing.Point(12, 12)
        Me.txtFileToScan.Name = "txtFileToScan"
        Me.txtFileToScan.ReadOnly = True
        Me.txtFileToScan.Size = New System.Drawing.Size(557, 20)
        Me.txtFileToScan.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTimeElapsed)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblVirusName)
        Me.GroupBox1.Controls.Add(Me.lblIsInfected)
        Me.GroupBox1.Controls.Add(Me.lblScannedData)
        Me.GroupBox1.Controls.Add(Me.lblFileSize)
        Me.GroupBox1.Controls.Add(Me.lblFullPath)
        Me.GroupBox1.Controls.Add(Me.lblFileName)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(558, 114)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Scan Information"
        '
        'lblTimeElapsed
        '
        Me.lblTimeElapsed.AutoSize = True
        Me.lblTimeElapsed.Location = New System.Drawing.Point(94, 94)
        Me.lblTimeElapsed.Name = "lblTimeElapsed"
        Me.lblTimeElapsed.Size = New System.Drawing.Size(16, 13)
        Me.lblTimeElapsed.TabIndex = 13
        Me.lblTimeElapsed.Text = "..."
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(456, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Time elapsed"
        '
        'lblVirusName
        '
        Me.lblVirusName.AutoSize = True
        Me.lblVirusName.Location = New System.Drawing.Point(94, 81)
        Me.lblVirusName.Name = "lblVirusName"
        Me.lblVirusName.Size = New System.Drawing.Size(16, 13)
        Me.lblVirusName.TabIndex = 11
        Me.lblVirusName.Text = "..."
        '
        'lblIsInfected
        '
        Me.lblIsInfected.AutoSize = True
        Me.lblIsInfected.Location = New System.Drawing.Point(94, 68)
        Me.lblIsInfected.Name = "lblIsInfected"
        Me.lblIsInfected.Size = New System.Drawing.Size(16, 13)
        Me.lblIsInfected.TabIndex = 10
        Me.lblIsInfected.Text = "..."
        '
        'lblScannedData
        '
        Me.lblScannedData.AutoSize = True
        Me.lblScannedData.Location = New System.Drawing.Point(94, 55)
        Me.lblScannedData.Name = "lblScannedData"
        Me.lblScannedData.Size = New System.Drawing.Size(16, 13)
        Me.lblScannedData.TabIndex = 9
        Me.lblScannedData.Text = "..."
        '
        'lblFileSize
        '
        Me.lblFileSize.AutoSize = True
        Me.lblFileSize.Location = New System.Drawing.Point(94, 42)
        Me.lblFileSize.Name = "lblFileSize"
        Me.lblFileSize.Size = New System.Drawing.Size(16, 13)
        Me.lblFileSize.TabIndex = 8
        Me.lblFileSize.Text = "..."
        '
        'lblFullPath
        '
        Me.lblFullPath.AutoSize = True
        Me.lblFullPath.Location = New System.Drawing.Point(94, 29)
        Me.lblFullPath.Name = "lblFullPath"
        Me.lblFullPath.Size = New System.Drawing.Size(16, 13)
        Me.lblFullPath.TabIndex = 7
        Me.lblFullPath.Text = "..."
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(94, 16)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(16, 13)
        Me.lblFileName.TabIndex = 6
        Me.lblFileName.Text = "..."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 81)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Virus name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Is infected?"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Scanned size"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "File size"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Full path"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File name"
        '
        'ofd
        '
        Me.ofd.DefaultExt = "*.*"
        Me.ofd.Filter = "All files|*.*"
        Me.ofd.Title = "Select file to scan."
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 41)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(41, 13)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Ready."
        '
        'lstLog
        '
        Me.lstLog.FormattingEnabled = True
        Me.lstLog.Location = New System.Drawing.Point(12, 174)
        Me.lstLog.Name = "lstLog"
        Me.lstLog.Size = New System.Drawing.Size(673, 108)
        Me.lstLog.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(576, 86)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Update Database"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.Label7.Location = New System.Drawing.Point(573, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Update Before Scanning"
        Me.Label7.Visible = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(697, 380)
        Me.Panel1.TabIndex = 15
        Me.Panel1.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(697, 380)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lstLog)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtFileToScan)
        Me.Controls.Add(Me.cmdStartScan)
        Me.Controls.Add(Me.cmdBrowse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hawk Clam"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents cmdStartScan As System.Windows.Forms.Button
    Friend WithEvents txtFileToScan As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblVirusName As System.Windows.Forms.Label
    Friend WithEvents lblIsInfected As System.Windows.Forms.Label
    Friend WithEvents lblScannedData As System.Windows.Forms.Label
    Friend WithEvents lblFileSize As System.Windows.Forms.Label
    Friend WithEvents lblFullPath As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTimeElapsed As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lstLog As System.Windows.Forms.ListBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As Panel
End Class
