Imports System.Collections.ObjectModel
Imports System.IO

Public Class FileList

    ''' <summary>
    ''' ファイル一覧
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Files As New ObservableCollection(Of FileListItem)

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="DirectoryPath"></param>
    ''' <remarks></remarks>
    Public Sub New(DirectoryPath As String)
        If Directory.Exists(DirectoryPath) Then
            For Each FileName In Directory.GetFiles(DirectoryPath)
                Me.Files.Add(New FileListItem(FileName))
            Next
        End If
    End Sub

End Class