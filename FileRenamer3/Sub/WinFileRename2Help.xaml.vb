Public Class WinFileRename2Help

#Region "プロパティ"

    ''' <summary>
    ''' 現在の行数
    ''' </summary>
    ''' <remarks></remarks>
    Private _RowCount As Integer = 0

    ''' <summary>
    ''' 終了時にイベントハンドラを削除しなければならないハイパーリンク
    ''' </summary>
    ''' <remarks></remarks>
    Private _RemoveHandleHyperlink As New List(Of Hyperlink)

#End Region

#Region "イベント"

    ''' <summary>
    ''' フォームロード時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WinFileRename2Help_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ' 文章を追加する
        Me.SetStrRow("^", ":", "先頭を表します。")
        Me.SetStrRow("$", ":", "文末を表します。")
        Me.SetStrRow("[]", ":", "括弧内のいずれか1文字を表します。")
        Me.SetStrRow("{n}", ":", "直前の文字n個の文字を表します。")
        Me.SetStrRow("{n,m}", ":", "直前の文字n～m個の文字を表します。")
        Me.SetStrRow(".", ":", "1文字を表します。")
        Me.SetStrRow("?", ":", "0～1文字を表します。")
        Me.SetStrRow("*", ":", "直前の文字0個以上の文字を表します。")
        Me.SetStrRow("()", ":", "括弧内の文字を記憶し、$1、$2等の文字で置換文字内で使用できます。")
        Me.SetStrRow("\", ":", "特殊な記号らの直前に付けることで文字として扱います。")
        Me.SetLinkRow("詳しくはこちら", "http://msdn.microsoft.com/ja-jp/library/cc392020.aspx")
    End Sub

    ''' <summary>
    ''' ハイパーリンク押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Hyperlink_Click(sender As Object, e As RoutedEventArgs)

        Try

            ' 指定URLをブラウザで開く
            System.Diagnostics.Process.Start(DirectCast(sender, Hyperlink).NavigateUri.AbsoluteUri)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' フォームクローズ時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WinFileRename2Help_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        For Each item In Me._RemoveHandleHyperlink

            RemoveHandler item.Click, AddressOf Hyperlink_Click

        Next
    End Sub

#End Region

#Region "共通"

    ''' <summary>
    ''' 文字行を追加する
    ''' </summary>
    ''' <param name="col0"></param>
    ''' <param name="col1"></param>
    ''' <param name="col2"></param>
    ''' <remarks></remarks>
    Private Sub SetStrRow(col0 As String, col1 As String, col2 As String)

        Dim rd As New RowDefinition
        Me.GrdMain.RowDefinitions.Add(rd)

        Dim lbl0 As New Label
        With lbl0
            .Content = col0
            .SetValue(Grid.RowProperty, Me._RowCount)
            .SetValue(Grid.ColumnProperty, 0)
        End With
        Me.GrdMain.Children.Add(lbl0)

        Dim lbl1 As New Label
        With lbl1
            .Content = col1
            .SetValue(Grid.RowProperty, Me._RowCount)
            .SetValue(Grid.ColumnProperty, 1)
        End With
        Me.GrdMain.Children.Add(lbl1)

        Dim lbl2 As New Label
        With lbl2
            .Content = col2
            .SetValue(Grid.RowProperty, Me._RowCount)
            .SetValue(Grid.ColumnProperty, 2)
        End With

        ' 行追加
        Me.GrdMain.Children.Add(lbl2)

        Me._RowCount += 1
    End Sub

    ''' <summary>
    ''' ハイパーリンク行を追加する
    ''' </summary>
    ''' <param name="disp"></param>
    ''' <param name="link"></param>
    ''' <remarks></remarks>
    Private Sub SetLinkRow(disp As String, link As String)

        Dim rd As New RowDefinition
        Me.GrdMain.RowDefinitions.Add(rd)

        Dim hy As New Hyperlink
        With hy
            .Inlines.Add(New Run(disp))
            .NavigateUri = New Uri(link)
            AddHandler .Click, AddressOf Hyperlink_Click
            Me._RemoveHandleHyperlink.Add(hy)
        End With

        Dim tb As New TextBlock
        With tb
            .Inlines.Add(hy)
            .Margin = New Thickness(5)
            .SetValue(Grid.RowProperty, Me._RowCount)
            .SetValue(Grid.ColumnProperty, 0)
            .SetValue(Grid.ColumnSpanProperty, 3)
        End With

        ' 行追加
        Me.GrdMain.Children.Add(tb)

        Me._RowCount += 1
    End Sub

#End Region

End Class
