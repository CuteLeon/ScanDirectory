Public Class Form1
    Dim WriteText As IO.StreamWriter

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WriteText = New IO.StreamWriter(Application.StartupPath & "\目录扫描-" & My.Computer.Clock.TickCount.ToString & ".txt")
        ScanDirectoryWithChild("F:\Visual Basic .NET\工程\ScanDirectory")
        WriteText.Close()
        WriteText.Dispose()
        End
    End Sub

    ''' <summary>
    ''' 对目标目录遍历扫描（包含子目录）
    ''' </summary>
    ''' <param name="Path">扫描的路径</param>
    ''' <returns>返回目录的大小</returns>
    Private Function ScanDirectoryWithChild(ByVal Path As String) As ULong
        Dim FoldersSize As ULong = 0
        Dim FilesSize As ULong = 0
        '递归扫描目录保存进文件
        On Error Resume Next '对某些目录权限不足会出错
        Dim Directorys() As String = System.IO.Directory.GetDirectories(Path)
        Dim Files() As String = System.IO.Directory.GetFiles(Path)
        For Index As Integer = 0 To Directorys.Length - 1
            '显示文件夹，对文件夹进行递归扫描
            WriteText.WriteLine(Directorys(Index) & "\")
            FoldersSize += ScanDirectoryWithChild(Directorys(Index))
        Next
        For Index As Integer = 0 To Files.Length - 1
            '显示文件，并格式化显示文件大小
            Dim TempFileInfo = New IO.FileInfo(Files(Index))
            FoldersSize += TempFileInfo.Length
            FilesSize += TempFileInfo.Length
            WriteText.WriteLine(Files(Index) & " /[" & FormatSize(TempFileInfo.Length) & "]")
        Next
        WriteText.WriteLine(Path & "\ >>>" & vbCrLf &
                            "  >>> [目录总大小： " & FormatSize(FoldersSize) & "] >>>" & vbCrLf &
                            "  >>> [根文件大小： " & FormatSize(FilesSize) & "] >>>" & vbCrLf &
                            "————————————————")
        Return FoldersSize
    End Function

    ''' <summary>
    ''' 对目标目录扫描（不包含子目录）
    ''' </summary>
    ''' <param name="Path">扫描的目录</param>
    Private Sub ScanDirectoryWithoutChild(ByVal Path As String)
        '扫描目录保存进文件，不对子目录进行递归
        On Error Resume Next '对某些目录权限不足会出错
        Dim Directorys() As String = System.IO.Directory.GetDirectories(Path)
        Dim Files() As String = System.IO.Directory.GetFiles(Path)
        For Index As Integer = 0 To Directorys.Length - 1
            '显示文件夹
            WriteText.WriteLine(Directorys(Index) & "\")
        Next
        For Index As Integer = 0 To Files.Length - 1
            '显示文件，并格式化显示文件大小
            Dim TempFileInfo = New IO.FileInfo(Files(Index))
            WriteText.WriteLine(Files(Index) & " /[" & FormatSize(TempFileInfo.Length) & "]")
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
