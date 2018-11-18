' Class for Handling HOSTS file operations
' Taken from https://stackoverflow.com/a/33896056

Public Class WindowsHost
    Public ReadOnly Location As String
    Private FullMap As List(Of HostMap)
    Public Sub New()
        Location = Environment.SystemDirectory & "\drivers\etc\hosts"
        If Not System.IO.File.Exists(Location) Then
            Throw New System.IO.FileNotFoundException("Host File Was Not Found", Location)
        End If
        FullMap = LoadCurrentMap()
    End Sub
    Public Function Count() As Integer
        Return FullMap.Count
    End Function
    Public Function Item(ByVal index As Integer) As HostMap
        Return New HostMap(FullMap.Item(index))
    End Function
    Public Sub AddHostMap(ByVal NewMap As HostMap)
        FullMap = LoadCurrentMap()
        FullMap.Add(NewMap)
        SaveData()
    End Sub
    Public Sub DeleteHostMapByDomain(ByVal dom As String)
        FullMap = LoadCurrentMap()
        Dim Reall As Integer = 0
        For i As Integer = 0 To FullMap.Count - 1 Step 1
            If FullMap.Item(Reall).domain = dom Then
                FullMap.RemoveAt(Reall)
            Else
                Reall += 1
            End If
        Next
        SaveData()
    End Sub
    Public Sub DeleteHostMapByIp(ByVal ip As System.Net.IPAddress)
        FullMap = LoadCurrentMap()
        Dim Reall As Integer = 0
        For i As Integer = 0 To FullMap.Count - 1 Step 1
            If FullMap.Item(Reall).Ip.Equals(ip) Then
                FullMap.RemoveAt(Reall)
                Reall += 1
            End If
        Next
        SaveData()
    End Sub
    Public Sub UpdateData()
        FullMap = LoadCurrentMap()
    End Sub
    Private Function LoadCurrentMap() As List(Of HostMap)
        Dim FileStream As New System.IO.StreamReader(Location)
        Dim Lines() As String = FileStream.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        FileStream.Close()
        Dim Lst As New List(Of HostMap)
        For Each line As String In Lines
            If Not line.Contains("#") Then
                Dim LineData() As String = line.Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
                If LineData.Length = 2 Then
                    Try
                        Dim temp As New HostMap(LineData(1), System.Net.IPAddress.Parse(LineData(0)))
                        Lst.Add(temp)
                    Catch ex As Exception
                    End Try
                End If
            End If
        Next
        Return Lst
    End Function
    Private Sub SaveData()
        Dim Data As String = "# Windows Host Generate" & vbNewLine & "# Time: " & Now.ToString
        For Each Map As HostMap In FullMap
            Data = Data & vbNewLine & Map.ToString
        Next
        Dim w As New System.IO.StreamWriter(Location)
        w.Write(Data)
        w.Close()
    End Sub
End Class
Public Class HostMap
    Public domain As String
    Public Ip As System.Net.IPAddress
    Public Sub New(ByVal _dom As String, ByVal _ip As System.Net.IPAddress)
        domain = _dom
        Ip = _ip
    End Sub
    Public Sub New(ByRef map As HostMap)
        domain = map.domain
        Ip = map.Ip
    End Sub
    Public Overrides Function ToString() As String
        Return Ip.ToString & "       " & domain
    End Function
End Class