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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.grpNewProject = New System.Windows.Forms.GroupBox()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.chkRunLaravelNew = New System.Windows.Forms.CheckBox()
        Me.chkLaravel = New System.Windows.Forms.CheckBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtProjectName = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ssGeneric = New System.Windows.Forms.StatusStrip()
        Me.tslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.bwServiceRestart = New System.ComponentModel.BackgroundWorker()
        Me.bwRunLaravel = New System.ComponentModel.BackgroundWorker()
        Me.refreshList = New System.Windows.Forms.Timer(Me.components)
        Me.lvProjects = New System.Windows.Forms.ListBox()
        Me.lblCredit = New System.Windows.Forms.LinkLabel()
        Me.bwDeleteFolder = New System.ComponentModel.BackgroundWorker()
        Me.grpNewProject.SuspendLayout()
        Me.ssGeneric.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpNewProject
        '
        Me.grpNewProject.Controls.Add(Me.btnCreate)
        Me.grpNewProject.Controls.Add(Me.chkRunLaravelNew)
        Me.grpNewProject.Controls.Add(Me.chkLaravel)
        Me.grpNewProject.Controls.Add(Me.lblName)
        Me.grpNewProject.Controls.Add(Me.txtProjectName)
        Me.grpNewProject.Location = New System.Drawing.Point(12, 12)
        Me.grpNewProject.Name = "grpNewProject"
        Me.grpNewProject.Size = New System.Drawing.Size(335, 155)
        Me.grpNewProject.TabIndex = 0
        Me.grpNewProject.TabStop = False
        Me.grpNewProject.Text = "New Project"
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(130, 114)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 4
        Me.btnCreate.Text = "&Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'chkRunLaravelNew
        '
        Me.chkRunLaravelNew.AutoSize = True
        Me.chkRunLaravelNew.Enabled = False
        Me.chkRunLaravelNew.Location = New System.Drawing.Point(101, 85)
        Me.chkRunLaravelNew.Name = "chkRunLaravelNew"
        Me.chkRunLaravelNew.Size = New System.Drawing.Size(115, 19)
        Me.chkRunLaravelNew.TabIndex = 3
        Me.chkRunLaravelNew.Text = "&Run 'laravel new'"
        Me.chkRunLaravelNew.UseVisualStyleBackColor = True
        '
        'chkLaravel
        '
        Me.chkLaravel.AutoSize = True
        Me.chkLaravel.Location = New System.Drawing.Point(101, 60)
        Me.chkLaravel.Name = "chkLaravel"
        Me.chkLaravel.Size = New System.Drawing.Size(137, 19)
        Me.chkLaravel.TabIndex = 2
        Me.chkLaravel.Text = "Use &Laravel Template"
        Me.chkLaravel.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(17, 34)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(39, 15)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Name"
        '
        'txtProjectName
        '
        Me.txtProjectName.Location = New System.Drawing.Point(101, 31)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.Size = New System.Drawing.Size(213, 23)
        Me.txtProjectName.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(272, 368)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'ssGeneric
        '
        Me.ssGeneric.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslStatus})
        Me.ssGeneric.Location = New System.Drawing.Point(0, 402)
        Me.ssGeneric.Name = "ssGeneric"
        Me.ssGeneric.Size = New System.Drawing.Size(359, 22)
        Me.ssGeneric.TabIndex = 3
        Me.ssGeneric.Text = "StatusStrip1"
        '
        'tslStatus
        '
        Me.tslStatus.Name = "tslStatus"
        Me.tslStatus.Size = New System.Drawing.Size(39, 17)
        Me.tslStatus.Text = "Ready"
        '
        'bwServiceRestart
        '
        '
        'bwRunLaravel
        '
        '
        'refreshList
        '
        Me.refreshList.Interval = 30000
        '
        'lvProjects
        '
        Me.lvProjects.FormattingEnabled = True
        Me.lvProjects.ItemHeight = 15
        Me.lvProjects.Location = New System.Drawing.Point(12, 173)
        Me.lvProjects.Name = "lvProjects"
        Me.lvProjects.Size = New System.Drawing.Size(335, 184)
        Me.lvProjects.TabIndex = 4
        '
        'lblCredit
        '
        Me.lblCredit.AutoSize = True
        Me.lblCredit.Location = New System.Drawing.Point(9, 372)
        Me.lblCredit.Name = "lblCredit"
        Me.lblCredit.Size = New System.Drawing.Size(188, 15)
        Me.lblCredit.TabIndex = 6
        Me.lblCredit.TabStop = True
        Me.lblCredit.Text = "https://github.com/liamdemafelix"
        '
        'bwDeleteFolder
        '
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 424)
        Me.Controls.Add(Me.lblCredit)
        Me.Controls.Add(Me.lvProjects)
        Me.Controls.Add(Me.ssGeneric)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grpNewProject)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XPM"
        Me.grpNewProject.ResumeLayout(False)
        Me.grpNewProject.PerformLayout()
        Me.ssGeneric.ResumeLayout(False)
        Me.ssGeneric.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents grpNewProject As GroupBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents chkRunLaravelNew As CheckBox
    Friend WithEvents chkLaravel As CheckBox
    Friend WithEvents lblName As Label
    Friend WithEvents txtProjectName As TextBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents ssGeneric As StatusStrip
    Friend WithEvents tslStatus As ToolStripStatusLabel
    Friend WithEvents bwServiceRestart As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwRunLaravel As System.ComponentModel.BackgroundWorker
    Friend WithEvents refreshList As Timer
    Friend WithEvents lvProjects As ListBox
    Friend WithEvents lblCredit As LinkLabel
    Friend WithEvents bwDeleteFolder As System.ComponentModel.BackgroundWorker
End Class
