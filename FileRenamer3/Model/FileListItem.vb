Imports System.ComponentModel

Public Class FileListItem
    Implements INotifyPropertyChanged

    ''' <summary>
    ''' プロパティ変更通知イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    ''' <summary>
    ''' ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private _FileName As String = String.Empty
    Public Property FileName As String
        Get
            Return Me._FileName
        End Get
        Set(value As String)
            If Me._FileName <> value Then
                Me._FileName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FileName"))
            End If
            If Me.FileName = Me.NewFileName Then
                Me.RowColor = New SolidColorBrush(Colors.Transparent)
            Else
                Me.RowColor = New SolidColorBrush(Colors.Pink)
            End If
        End Set
    End Property

    ''' <summary>
    ''' 新ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private _NewFileName As String = String.Empty
    Public Property NewFileName As String
        Get
            Return Me._NewFileName
        End Get
        Set(value As String)
            If Me._NewFileName <> value Then
                Me._NewFileName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NewFileName"))
            End If
            If Me.FileName = Me.NewFileName Then
                Me.RowColor = New SolidColorBrush(Colors.Transparent)
            Else
                Me.RowColor = New SolidColorBrush(Colors.Pink)
            End If
        End Set
    End Property

    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Status As String = String.Empty
    Public Property Status As String
        Get
            Return Me._Status
        End Get
        Set(value As String)
            If Me._Status <> value Then
                Me._Status = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Status"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' 行の背景色
    ''' </summary>
    ''' <remarks></remarks>
    Private _RowColor As New SolidColorBrush(Colors.Transparent)
    Public Property RowColor As SolidColorBrush
        Get
            Return Me._RowColor
        End Get
        Set(value As SolidColorBrush)
            If Me._RowColor IsNot value Then
                Me._RowColor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("RowColor"))
            End If
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks></remarks>
    Public Sub New(FileName As String)
        FileName = System.IO.Path.GetFileName(FileName)
        Me.FileName = FileName
        Me.NewFileName = FileName
    End Sub
End Class