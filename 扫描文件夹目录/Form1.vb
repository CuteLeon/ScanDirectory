Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ScanDirectory("d:\")
    End Sub

    Private Sub ScanDirectory(ByVal path As String)
        On Error Resume Next '对某些目录权限不足会出错
        Dim Directorys() As String = System.IO.Directory.GetDirectories(path)
        Dim Files() As String = System.IO.Directory.GetFiles(path)
        For Index As Integer = 0 To Directorys.Length - 1
            '显示文件夹，并对文件夹进行递归扫描
            Debug.Print(Directorys(Index) & "\")
            ScanDirectory(Directorys(Index))
        Next
        For Index As Integer = 0 To Files.Length - 1
            '显示文件，并格式化显示文件大小
            Dim TempFileInfo = New IO.FileInfo(Files(Index))
            Debug.Print(Files(Index) & " /[" & FormatSize(TempFileInfo.Length) & "]")
        Next
    End Sub

    Private Function FormatSize(ByVal ByteCount As Long) As String
        '格式化文件大小
        If ByteCount < 1024 Then
            Return ByteCount & " Byte"
        ElseIf ByteCount < 1048576 Then
            Return [String].Format("{0:n} KB", ByteCount / 1024)
        ElseIf ByteCount < 1073741824 Then
            Return [String].Format("{0:n} MB", ByteCount / 1048576)
        Else
            Return [String].Format("{0:n} GB", ByteCount / 1073741824)
        End If
    End Function
End Class
