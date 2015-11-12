Imports System.IO

Public Class MainWindow

#Region "プロパティ"

    ''' <summary>
    ''' ファイル一覧
    ''' </summary>
    ''' <remarks></remarks>
    Private _FileLists As New FileList

    ''' <summary>
    ''' ディレクトリパス
    ''' </summary>
    ''' <remarks></remarks>
    Private _DirectoryPath As String

    ''' <summary>
    ''' 現在のタブインデックス
    ''' </summary>
    ''' <remarks></remarks>
    Private _CurrentTabIndex As Integer

#End Region

#Region "メニュー"

    ''' <summary>
    ''' メニュー再読込押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuReload_Click(sender As Object, e As RoutedEventArgs) Handles MenuReload.Click

        ' ディレクトリの存在確認
        If Directory.Exists(Me.TxtDirectoryPath.Text) Then

            Me._DirectoryPath = Me.TxtDirectoryPath.Text

        Else

            Me._DirectoryPath = String.Empty

        End If

        ' コントロールの初期化
        Me.InitInputControl()

        ' ディレクトリの設定、読込
        Me.SetDirectoryPath()
    End Sub

    ''' <summary>
    ''' メニューフォルダを開く、参照ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuOpen_Click(sender As Object, e As RoutedEventArgs) Handles MenuOpen.Click

        ' フォルダ選択ダイアログを開く
        Dim fbd As New System.Windows.Forms.FolderBrowserDialog

        ' 選択されたかどうか
        If fbd.ShowDialog() = Forms.DialogResult.OK Then

            ' ディレクトリの存在確認
            If Directory.Exists(fbd.SelectedPath) Then

                ' ディレクトリ設定
                Me._DirectoryPath = fbd.SelectedPath

                ' コントロールの初期化
                Me.InitInputControl()

                ' ディレクトリの設定、読込
                Me.SetDirectoryPath()
            End If
        End If
    End Sub

    ''' <summary>
    ''' メニュー入力欄初期化押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuInputClear_Click(sender As Object, e As RoutedEventArgs) Handles MenuInputClear.Click
        ' ディレクトリ初期化
        Me._DirectoryPath = String.Empty

        ' コントロールの初期化
        Me.InitInputControl()

        ' ディレクトリの設定、読込
        Me.SetDirectoryPath()
    End Sub

    ''' <summary>
    ''' メニュー終了押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuEnd_Click(sender As Object, e As RoutedEventArgs) Handles MenuEnd.Click

        ' アプリケーションを終了する
        My.Application.Shutdown()
    End Sub

    ''' <summary>
    ''' メニューバージョン情報押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuVersion_Click(sender As Object, e As RoutedEventArgs) Handles MenuVersion.Click

        ' バージョンを表示する
        MessageBox.Show(String.Concat("FileRenamaer3", vbCrLf, My.Application.Info.Version.ToString), "バージョン情報")
    End Sub

#End Region

#Region "ファイルリスト"

    ''' <summary>
    ''' 再読込ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnReload_Click(sender As Object, e As RoutedEventArgs) Handles BtnReload.Click

        ' ディレクトリの存在確認
        If Directory.Exists(Me.TxtDirectoryPath.Text) Then

            Me._DirectoryPath = Me.TxtDirectoryPath.Text

        Else

            Me._DirectoryPath = String.Empty

        End If

        ' コントロールの初期化
        Me.InitInputControl()

        ' ディレクトリの設定、読込
        Me.SetDirectoryPath()
    End Sub

    ''' <summary>
    ''' メニューフォルダを開く、参照ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOpen_Click(sender As Object, e As RoutedEventArgs) Handles BtnOpen.Click

        ' フォルダ選択ダイアログを開く
        Dim fbd As New System.Windows.Forms.FolderBrowserDialog

        ' 選択されたかどうか
        If fbd.ShowDialog() = Forms.DialogResult.OK Then

            ' ディレクトリの存在確認
            If Directory.Exists(fbd.SelectedPath) Then

                ' ディレクトリ設定
                Me._DirectoryPath = fbd.SelectedPath

                ' コントロールの初期化
                Me.InitInputControl()

                ' ディレクトリの設定、読込
                Me.SetDirectoryPath()
            End If
        End If
    End Sub

    ''' <summary>
    ''' ディレクトリパス設定テキストボックスのファイルやフォルダのドラッグ時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TxtDirectoryPath_PreviewDragOver(sender As Object, e As Windows.DragEventArgs) Handles TxtDirectoryPath.PreviewDragOver

        ' ファイルやフォルダのドラッグか
        If e.Data.GetData(System.Windows.DataFormats.FileDrop) IsNot Nothing Then

            ' カーソルを変更する
            e.Effects = Windows.DragDropEffects.Copy

        Else

            ' カーソルを変更しない
            e.Effects = Windows.DragDropEffects.None

        End If

        ' 以降イベントを再度起こさない
        e.Handled = True
    End Sub

    ''' <summary>
    ''' ディレクトリパス設定テキストボックスのファイルやフォルダのドロップ時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TxtDirectoryPath_Drop(sender As Object, e As Windows.DragEventArgs) Handles TxtDirectoryPath.Drop

        ' ドロップされたファイル全てのファイルパスを取得する
        Dim files As String() = CType(e.Data.GetData(System.Windows.DataFormats.FileDrop), String())

        ' ファイルパスが取得できたかどうか
        If files IsNot Nothing Then

            ' ファイルパスからフォルダかファイルを判断
            If Directory.Exists(files(0)) Then

                ' ディレクトリ
                Me._DirectoryPath = files(0)

            ElseIf File.Exists(files(0)) Then

                ' ファイルパス
                Me._DirectoryPath = Path.GetDirectoryName(files(0))

            Else

                ' 何が何だか分からない
                Me._DirectoryPath = String.Empty

            End If

            ' コントロールの初期化
            Me.InitInputControl()

            ' ディレクトリの設定、読込
            Me.SetDirectoryPath()
        End If
    End Sub

#End Region

#Region "タブ"

    ''' <summary>
    ''' タブ選択時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabFunc_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles TabFunc.SelectionChanged

        ' タブの移動があったかどうか
        If Me.TabFunc.SelectedIndex <> Me._CurrentTabIndex Then

            ' コントロールの初期化
            Me.InitInputControl()

            ' ディレクトリの設定、読込
            Me.SetDirectoryPath()

            ' 現在のタブを更新
            Me._CurrentTabIndex = Me.TabFunc.SelectedIndex

            ' 画像変換タブであれば、変更後ファイル名の変更を不可にする
            If TabFunc.SelectedIndex = 2 Then

                Me.ColNewFileName.IsReadOnly = True

            Else

                Me.ColNewFileName.IsReadOnly = False
            End If
        End If
    End Sub

#End Region

#Region "ファイル名変更"

    ''' <summary>
    ''' 置換系テキストボックス変更時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TxtFileRename_TextChanged(sender As Object, e As RoutedEventArgs) Handles TxtFileRenameRepStr.TextChanged, TxtFileRenameStr.TextChanged

        ' 置換を仮実行する
        Me.SetFileNameFileRename()
    End Sub

    ''' <summary>
    ''' 確定ボタン押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnFileRenameOK_Click(sender As Object, e As RoutedEventArgs) Handles BtnFileRenameOK.Click

        ' 置換を実行する
        Me.RenameFile()
    End Sub

    ''' <summary>
    ''' 置換を仮実行する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFileNameFileRename()

        For Each FileListItem In Me._FileLists.Files

            With FileListItem

                Try
                    ' 置換文字が空であるかどうか
                    If Me.TxtFileRenameRepStr.Text = String.Empty Then

                        ' 元のファイル名を設定
                        .NewFileName = .FileName

                    Else

                        ' 置換を実行したファイル名を設定
                        .NewFileName = .FileName.Replace(Me.TxtFileRenameRepStr.Text, Me.TxtFileRenameStr.Text)

                    End If

                    ' ステータスを初期化
                    .Status = String.Empty

                Catch ex As Exception

                    ' エラー詳細をステータスに設定
                    .Status = ex.Message
                End Try
            End With
        Next
    End Sub

#End Region

#Region "ファイル名変更2"

    ''' <summary>
    ''' 置換2系テキストボックス変更時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FileRename2_TextChanged(sender As Object, e As RoutedEventArgs) Handles TxtFileRename2RegStr.TextChanged, TxtFileRename2Str.TextChanged

        ' 置換2を仮実行する
        Me.SetFileNameFileRename2()
    End Sub

    ''' <summary>
    ''' ヘルプボタン押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnFileRename2Help_Click(sender As Object, e As RoutedEventArgs) Handles BtnFileRename2Help.Click

        ' ヘルプ画面を開く
        Dim win As New WinFileRename2Help With {.Owner = Me}

        win.Show()
    End Sub

    ''' <summary>
    ''' 確定ボタン押下時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnFileRename2OK_Click(sender As Object, e As RoutedEventArgs) Handles BtnFileRename2OK.Click

        ' 置換2を実行する
        Me.RenameFile()
    End Sub

    ''' <summary>
    ''' 置換2を仮実行する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFileNameFileRename2()

        ' エラー発生時は置換2処理は中断するがステータスは空欄で更新する為のフラグ
        Dim IsEnd As Boolean = False

        For Each FileListItem In Me._FileLists.Files

            With FileListItem

                Try

                    ' 終了判定
                    If Not IsEnd Then

                        ' 置換文字が空であるかどうか
                        If Me.TxtFileRename2RegStr.Text = String.Empty Then

                            ' 元のファイル名を設定
                            .NewFileName = .FileName

                        Else

                            ' 置換を実行したファイル名を設定
                            .NewFileName = System.Text.RegularExpressions.Regex.Replace(.FileName, Me.TxtFileRename2RegStr.Text, Me.TxtFileRename2Str.Text)

                        End If

                    End If

                    ' ステータスを初期化
                    .Status = String.Empty

                Catch ex As ArgumentException

                    ' 引数例外、以降の処理は中断する
                    IsEnd = True

                    ' エラー詳細をステータスに設定
                    .Status = String.Concat(ex.Message, vbCrLf, "以降の処理を中断します。")

                Catch ex As Exception

                    ' エラー詳細をステータスに設定
                    .Status = ex.Message
                End Try
            End With
        Next
    End Sub

#End Region

#Region "画像変換"

    ''' <summary>
    ''' 確定ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnImageConvertOK_Click(sender As Object, e As RoutedEventArgs) Handles BtnImageConvertOK.Click

        ' 画像変換を確定する
        Me.ImageConvert()
    End Sub

    ''' <summary>
    ''' 画像変換系のコンボボックスの選択時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CmbImageConvert_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles CmbImageConvertBefore.SelectionChanged, CmbImageConvertAfter.SelectionChanged

        ' 画像変換を仮実行する
        Me.SetImageConvert()
    End Sub

    ''' <summary>
    ''' 画像変換を仮実行する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageConvert()

        ' エラー発生時は置換2処理は中断するがステータスは空欄で更新する為のフラグ
        Dim IsEnd As Boolean = False

        For Each FileListItem In Me._FileLists.Files

            With FileListItem

                Try

                    ' 置換を実行したファイル名を設定
                    If Not IsEnd Then .NewFileName = System.Text.RegularExpressions.Regex.Replace( _
                        .FileName _
                        , String.Concat(".", DirectCast(Me.CmbImageConvertBefore.SelectedItem, ComboBoxItem).Content.ToString, "$") _
                        , String.Concat(".", DirectCast(Me.CmbImageConvertAfter.SelectedItem, ComboBoxItem).Content.ToString))

                    ' ステータスを初期化
                    .Status = String.Empty

                Catch ex As ArgumentException

                    ' 引数例外、以降の処理は中断する
                    IsEnd = True

                    ' エラー詳細をステータスに設定
                    .Status = String.Concat(ex.Message, vbCrLf, "以降の処理を中断します。")

                Catch ex As Exception

                    ' エラー詳細をステータスに設定
                    .Status = ex.Message
                End Try
            End With
        Next
    End Sub

#End Region

#Region "共通"

    ''' <summary>
    ''' 各種コントロールに初期値を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitInputControl()
        If Me.TxtFileRenameRepStr IsNot Nothing Then Me.TxtFileRenameRepStr.Text = String.Empty
        If Me.TxtFileRenameStr IsNot Nothing Then Me.TxtFileRenameStr.Text = String.Empty
        If Me.TxtFileRename2RegStr IsNot Nothing Then Me.TxtFileRename2RegStr.Text = String.Empty
        If Me.TxtFileRename2Str IsNot Nothing Then Me.TxtFileRename2Str.Text = String.Empty
        If Me.CmbImageConvertBefore IsNot Nothing Then Me.CmbImageConvertBefore.SelectedIndex = 0
        If Me.CmbImageConvertAfter IsNot Nothing Then Me.CmbImageConvertAfter.SelectedIndex = 0
        If Me.ChkImageConverterDeleteOldFile IsNot Nothing Then Me.ChkImageConverterDeleteOldFile.IsChecked = True
    End Sub

    ''' <summary>
    ''' ディレクトリを設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDirectoryPath()
        Me._FileLists = New FileList(Me._DirectoryPath)
        Me.dgdFileList.DataContext = Me._FileLists
        Me.TxtDirectoryPath.Text = Me._DirectoryPath
    End Sub

    ''' <summary>
    ''' ファイル一覧のファイル名を全て変更する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RenameFile()

        For Each FileListItem In Me._FileLists.Files

            With FileListItem

                ' ステータスを初期化
                .Status = String.Empty

                ' 変更後と変更前のファイル名が同名であればスキップ
                If .FileName = .NewFileName Then

                    ' ステータスに詳細を設定
                    .Status = "変更はありません。"

                    ' スキップ
                    Continue For

                End If

                ' 変更前ファイルが存在するかどうか
                If Not File.Exists(Path.Combine(Me._DirectoryPath, .FileName)) Then

                    ' ステータスにエラーを設定
                    .Status = "変更前のファイルが見つかりませんでした。"

                    ' スキップ
                    Continue For

                End If

                ' 変更後ファイルが存在するかどうか
                If File.Exists(Path.Combine(Me._DirectoryPath, .NewFileName)) And .FileName.ToUpper <> .NewFileName.ToUpper Then

                    ' ステータスにエラーを設定
                    .Status = "変更後のファイル名と同名のファイルが存在する為スキップしました。"

                    ' スキップ
                    Continue For

                End If

                ' ファイル名に使用できない文字が存在するかどうか
                If .NewFileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then

                    ' ステータスにエラーを設定
                    .Status = "ファイル名に使用できない文字が含まれている為スキップしました。"

                    ' スキップ
                    Continue For

                End If

                Try

                    ' ファイルを移動
                    File.Move(Path.Combine(Me._DirectoryPath, .FileName), Path.Combine(Me._DirectoryPath, .NewFileName))

                    ' ステータスに設定
                    .Status = "変更しました。"

                Catch ex As Exception

                    ' エラー詳細をステータスに設定
                    .Status = ex.Message
                End Try
            End With
        Next
    End Sub

    ''' <summary>
    ''' 画像置換を実行する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImageConvert()

        For Each FileListItem In Me._FileLists.Files

            With FileListItem

                ' ステータスを初期化
                .Status = String.Empty

                ' 変更後と変更前のファイル名が同名であればスキップ
                If .FileName = .NewFileName Then

                    ' ステータスに詳細を設定
                    .Status = "変更はありません。"

                    ' スキップ
                    Continue For

                End If

                ' 変更前ファイルが存在するかどうか
                If Not File.Exists(Path.Combine(Me._DirectoryPath, .FileName)) Then

                    ' ステータスにエラーを設定
                    .Status = "変換前のファイルが見つかりませんでした。"

                    ' スキップ
                    Continue For

                End If

                ' 変更後ファイルが存在するかどうか
                If File.Exists(Path.Combine(Me._DirectoryPath, .NewFileName)) And .FileName.ToUpper <> .NewFileName.ToUpper Then

                    ' ステータスにエラーを設定
                    .Status = "変換後のファイル名と同名のファイルが存在する為スキップしました。"

                    ' スキップ
                    Continue For

                End If

                ' ファイル名に使用できない文字が存在するかどうか
                If .NewFileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then

                    ' ステータスにエラーを設定
                    .Status = "ファイル名に使用できない文字が含まれている為スキップしました。"

                    ' スキップ
                    Continue For

                End If

                Try

                    Dim wbmp As WriteableBitmap

                    ' メモリへ読込
                    Using mem As New MemoryStream(File.ReadAllBytes(Path.Combine(Me._DirectoryPath, .FileName)))

                        wbmp = New WriteableBitmap(BitmapFrame.Create(mem))

                    End Using

                    Dim enc As BitmapEncoder = Nothing

                    Try

                        ' 変換予定の拡張子を判定
                        Select Case DirectCast(Me.CmbImageConvertAfter.SelectedItem, ComboBoxItem).Content.ToString

                            Case "jpg", "jpeg"

                                ' JPEG
                                enc = New JpegBitmapEncoder

                            Case "bmp"

                                ' BMP
                                enc = New BmpBitmapEncoder

                            Case "png"

                                ' PNG
                                enc = New PngBitmapEncoder

                            Case "tiff"

                                ' TIFF
                                enc = New TiffBitmapEncoder

                            Case "gif"

                                ' GIF
                                enc = New GifBitmapEncoder

                            Case "wmp"

                                ' WMP
                                enc = New WmpBitmapEncoder

                        End Select

                        ' エンコーダーに画像を渡す
                        enc.Frames.Add(BitmapFrame.Create(wbmp))

                        ' ファイルを保存する
                        Using srm As New FileStream(Path.Combine(Me._DirectoryPath, .NewFileName), FileMode.Create)

                            enc.Save(srm)

                        End Using

                        ' 元ファイルの削除
                        If Me.ChkImageConverterDeleteOldFile.IsChecked Then File.Delete(Path.Combine(Me._DirectoryPath, .FileName))

                        ' ステータスに設定
                        .Status = "変換しました。"

                    Catch ex As Exception

                        ' 上層に例外を投げる
                        Throw

                    End Try

                Catch ex As Exception

                    ' エラー詳細をステータスに設定
                    .Status = ex.Message

                End Try
            End With
        Next
    End Sub

#End Region

End Class
