Imports System.ComponentModel
Imports System.ServiceProcess
Imports System.Threading

Public Class frmMain

    ' Application Variables
    Dim XAMPP_ROOT As String = "C:\xampp\"
    Dim XPM_ROOT As String = Application.StartupPath & "\"

    ' Service Declarations
    Dim ApacheService As String = "Apache2.4"
    Dim MySQLService As String = "mysql"

    ' Re-usable variables
    Dim restartSrv As String = ""
    Dim projectName As String = ""
    Dim projectConfFile As String = ""

    ' Create termination lock
    Dim TerminationLock As Boolean = False

    ' Set variable for folder to delete, if any
    Dim FolderDelete As String = ""

    ' Initialize the WindowsHost Class
    Dim WindowsHostSession As New WindowsHost()

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Determine XAMPP Root
        If My.Computer.FileSystem.FileExists(XPM_ROOT & "root.txt") Then
            XAMPP_ROOT = My.Computer.FileSystem.ReadAllText(XPM_ROOT & "root.txt")
        End If

        If XAMPP_ROOT.EndsWith("\") = False Then
            XAMPP_ROOT = XAMPP_ROOT & "\"
        End If

        ' Let's see if XAMPP is installed.
        If My.Computer.FileSystem.DirectoryExists(XAMPP_ROOT) = False Then
            MessageBox.Show("XAMPP is not installed on this system. If you have a different location for XAMPP, create a root.txt file in the XMP folder that contains the absolute path to your xampp folder." & vbNewLine & vbNewLine & "We are currently looking at: " & XAMPP_ROOT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        ' Generate the vhosts folder
        If My.Computer.FileSystem.DirectoryExists(XAMPP_ROOT & "apache\conf\vhosts") = False Then
            ' Create
            My.Computer.FileSystem.CreateDirectory(XAMPP_ROOT & "apache\conf\vhosts")
            ' Rewrite the httpd-vhosts.conf file
            Dim vhdata As String = $"
NameVirtualHost *:80
<VirtualHost *:80>
	ServerAdmin root@localhost
	DocumentRoot ""{XAMPP_ROOT}htdocs""
	ServerName localhost
	ErrorLog ""logs/localhost-error.log""
	CustomLog ""logs/localhost-access.log"" common
</VirtualHost>

IncludeOptional conf/vhosts/*.conf"
            My.Computer.FileSystem.WriteAllText(XAMPP_ROOT & "apache\conf\extra\httpd-vhosts.conf", vhdata, False)
        End If

        ' Generate the logs folder
        If My.Computer.FileSystem.DirectoryExists(XAMPP_ROOT & "logs") = False Then
            ' Create
            My.Computer.FileSystem.CreateDirectory(XAMPP_ROOT & "logs")
        End If

        ' Start Timer
        refreshList.Start()

        ' Initialize
        For Each file As String In IO.Directory.GetFiles(XAMPP_ROOT & "apache\conf\vhosts", "*.*")
            lvProjects.Items.Add(file)
        Next
    End Sub

    Private Sub bwServiceRestart_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwServiceRestart.DoWork
        ' Toggle the Termination Lock
        TerminationLock = True
        For Each s As ServiceController In ServiceController.GetServices()
            If s.ServiceName = restartSrv Then
                tslStatus.Text = "Restarting service " & restartSrv & "..."
                If s.Status = ServiceControllerStatus.Running Then
                    s.Stop()
                    s.WaitForStatus(ServiceControllerStatus.Stopped)
                    s.Start()
                    s.WaitForStatus(ServiceControllerStatus.Running)
                ElseIf s.Status = ServiceControllerStatus.Stopped Then
                    s.Start()
                    s.WaitForStatus(ServiceControllerStatus.Running)
                End If
            End If
        Next
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        ' Disable All Controls
        btnCreate.Enabled = False
        btnDelete.Enabled = False
        txtProjectName.ReadOnly = True
        chkLaravel.Enabled = False
        chkRunLaravelNew.Enabled = False

        ' Validate
        projectName = txtProjectName.Text()
        If System.Text.RegularExpressions.Regex.IsMatch(projectName, "^[a-z0-9]+$") = False Then
            MessageBox.Show("Invalid project name. XMP only accepts alphanumeric characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnCreate.Enabled = True
            btnDelete.Enabled = True
            txtProjectName.ReadOnly = False
            chkLaravel.Enabled = True
            If chkLaravel.Checked Then
                chkRunLaravelNew.Enabled = True
            End If
            Exit Sub
        End If

        projectConfFile = XAMPP_ROOT & "apache\conf\vhosts\" & projectName & ".local.conf"
        If My.Computer.FileSystem.FileExists(projectConfFile) Then
            btnCreate.Enabled = True
            btnDelete.Enabled = True
            txtProjectName.ReadOnly = False
            chkLaravel.Enabled = True
            If chkLaravel.Checked Then
                chkRunLaravelNew.Enabled = True
            End If
            MessageBox.Show("A project of the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ' Write
        tslStatus.Text = "Writing configuration..."
        Dim drAppend = ""
        If chkLaravel.Checked Then
            drAppend = "\public"
        End If
        Dim vh As String = $"
<VirtualHost *:80>
	ServerAdmin root@{projectName}.local
	DocumentRoot ""{XAMPP_ROOT}htdocs\{projectName}{drAppend}""
	ServerName {projectName}.local
	ErrorLog ""{XAMPP_ROOT}logs\localhost-error.log""
	CustomLog ""{XAMPP_ROOT}logs\localhost-access.log"" common
</VirtualHost>"
        My.Computer.FileSystem.WriteAllText(projectConfFile, vh, False)

        ' Add to HOSTS File
        tslStatus.Text = "Adding to HOSTS file..."
        WindowsHostSession.AddHostMap(New HostMap(projectName & ".local", System.Net.IPAddress.Parse("127.0.0.1")))

        ' Do we run laravel new?
        If chkRunLaravelNew.Checked = True Then
            If chkLaravel.Checked = True Then
                tslStatus.Text = "Creating Laravel application..."
                bwRunLaravel.RunWorkerAsync()
                While bwRunLaravel.IsBusy
                    Thread.Sleep(200)
                    Application.DoEvents()
                End While
            End If
        End If

        ' Restart Apache
        restartSrv = "Apache2.4"
        bwServiceRestart.RunWorkerAsync()


        lvProjects.Items.Clear()
        Dim AllowedExtension As String = "conf"
        For Each file As String In IO.Directory.GetFiles(XAMPP_ROOT & "apache\conf\vhosts", "*.*")
            lvProjects.Items.Add(file)
        Next

        ' Done
        MessageBox.Show("Project created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnCreate.Enabled = True
        btnDelete.Enabled = True
        txtProjectName.ReadOnly = False
        chkLaravel.Enabled = True
        If chkLaravel.Checked Then
            chkRunLaravelNew.Enabled = True
        End If
    End Sub

    Private Sub bwServiceRestart_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwServiceRestart.RunWorkerCompleted
        tslStatus.Text = "Ready"
        ' Unlock termination
        TerminationLock = False
    End Sub

    Private Sub bwRunLaravel_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwRunLaravel.DoWork
        Process.Start("cmd.exe", "/c cd " & XAMPP_ROOT & "htdocs\ && laravel new " & projectName)
    End Sub

    Private Sub refreshList_Tick(sender As Object, e As EventArgs) Handles refreshList.Tick
        lvProjects.Items.Clear()
        Dim AllowedExtension As String = "conf"
        For Each file As String In IO.Directory.GetFiles(XAMPP_ROOT & "apache\conf\vhosts", "*.*")
            lvProjects.Items.Add(file)
        Next
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lvProjects.SelectedItem = "" Then
            MessageBox.Show("Select a project to delete first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim result As Integer = MessageBox.Show("Do you also want to delete the project folder?", "Delete Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If result = DialogResult.Cancel Then
            ' Exit Sub
            Exit Sub
        ElseIf result = DialogResult.No Then
            My.Computer.FileSystem.DeleteFile(lvProjects.SelectedItem)
        ElseIf result = DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(lvProjects.SelectedItem)
            ' Dim projdir As String = XAMPP_ROOT & "htdocs\" & lvProjects.SelectedItem().ToString().Replace(XAMPP_ROOT & "apache\conf\vhosts", "").ToString().Replace(".local.conf", "")
            FolderDelete = XAMPP_ROOT & "htdocs\" & lvProjects.SelectedItem().ToString().Replace(XAMPP_ROOT & "apache\conf\vhosts", "").ToString().Replace(".local.conf", "")
            If My.Computer.FileSystem.DirectoryExists(FolderDelete) Then
                bwDeleteFolder.RunWorkerAsync()
            End If
        End If

        ' Remove from HOSTS File
        Dim dom As String = lvProjects.SelectedItem().ToString().Replace(XAMPP_ROOT & "apache\conf\vhosts\", "").ToString().Replace(".local.conf", ".local")
        WindowsHostSession.DeleteHostMapByDomain(dom)

        ' Restart Apache
        restartSrv = "Apache2.4"
        bwServiceRestart.RunWorkerAsync()


        lvProjects.Items.Clear()
        Dim AllowedExtension As String = "conf"
        For Each file As String In IO.Directory.GetFiles(XAMPP_ROOT & "apache\conf\vhosts", "*.*")
            lvProjects.Items.Add(file)
        Next

        MessageBox.Show("Project deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub chkLaravel_CheckedChanged(sender As Object, e As EventArgs) Handles chkLaravel.CheckedChanged
        If chkLaravel.Checked Then
            chkRunLaravelNew.Enabled = True
        Else
            chkRunLaravelNew.Checked = False
            chkRunLaravelNew.Enabled = False
        End If
    End Sub

    Private Sub lblCredit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCredit.LinkClicked
        Process.Start("https://github.com/liamdemafelix")
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If TerminationLock Then
            MessageBox.Show("Please wait for pending operations to finish before closing the application.", "Pending Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
        End If
    End Sub

    Private Sub bwDeleteFolder_DoWork(sender As Object, e As DoWorkEventArgs) Handles bwDeleteFolder.DoWork
        My.Computer.FileSystem.DeleteDirectory(FolderDelete, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Private Sub bwDeleteFolder_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bwDeleteFolder.RunWorkerCompleted
        ' Reset the variable value
        FolderDelete = ""
    End Sub
End Class
