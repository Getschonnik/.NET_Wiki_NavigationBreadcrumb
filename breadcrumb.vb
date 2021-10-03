Imports System.Text.RegularExpressions
Namespace navigation
    Module breadcrumb
        Private bcn_startPosX As Integer = 10
        Private bcn_startPosY As Integer = 10
        Private bcn_delimiter As String = ">"
        Private bcn_delimiterSpace As Integer = 1
        Private bcn_parentControl As Control
        Private bcn_removeDelimiterOnFirstPosition As Boolean = False
        Private bcn_removeSpecifiedPath As String = ""
        Friend Sub setBreadcrumbNavigation(ByVal path As String, ByVal addToControl As Control, Optional removeSpecifiedPath As String = "")
            If removeSpecifiedPath <> "" Then
                bcn_removeSpecifiedPath = removeSpecifiedPath
                bcn_removeDelimiterOnFirstPosition = True
            End If
            bcn_parentControl = addToControl
            bcn_parentControl.Controls.Clear()
            bcn_addControls(path, bcn_removeSpecifiedPath)
            If bcn_removeDelimiterOnFirstPosition Then
                bnc_remoteDelimiterAsFirst(bcn_parentControl)
            End If
        End Sub
        Private Function bcn_getLastestControlPosX(ByVal ctr As Control)
            Dim ctrCount As Integer = ctr.Controls.Count - 1
            Dim latestCtrPosX = ctr.Controls.Item(ctrCount)
            Dim output = Math.Round(latestCtrPosX.Location.X + latestCtrPosX.Width)
            Return output
        End Function
        Private Function rewritePath(ByVal path As String, ByVal rewriteTo As String)
            Dim xSplit = Regex.Split(path, Regex.Escape(rewriteTo))
            Return xSplit(0) + "\" + rewriteTo
        End Function
        Private Sub bcn_addControls(ByVal path As String, ByVal removeSpecifiedPath As String)
            If removeSpecifiedPath.Length > 0 Then
                Dim newPath = Regex.Split(path, Regex.Escape(bcn_removeSpecifiedPath))
                If newPath.Count > 1 Then
                    path = newPath(1)
                End If
            End If
            Dim pSplit = Regex.Split(path, Regex.Escape("\"))
            For i As Integer = 0 To pSplit.Count - 1
                If i = 0 Then
                    bcn_setControl(pSplit(i), path, bcn_startPosX, bcn_startPosY)
                Else
                    If pSplit(i).Length > 0 Then
                        bcn_setControl(bcn_delimiter.ToString, "", bcn_getLastestControlPosX(bcn_parentControl) + bcn_delimiterSpace, bcn_startPosY)
                        bcn_setControl(pSplit(i), rewritePath(path, pSplit(i)), bcn_getLastestControlPosX(bcn_parentControl) + bcn_delimiterSpace, bcn_startPosY)
                    End If
                End If
            Next
        End Sub
        Private Sub bcn_setControl(ByVal name As String, ByVal path As String, ByVal posX As Integer, ByVal posY As Integer)
            Dim lbl As New Label
            lbl.Text = name
            If name <> bcn_delimiter Then
                lbl.Tag = path
                lbl.Cursor = Cursors.Hand
                AddHandler lbl.Click, AddressOf bnc_NavigationClicked
            End If
            lbl.AutoSize = True
            lbl.Location = New Drawing.Point(posX, posY)
            If bcn_parentControl.InvokeRequired Then
                bcn_parentControl.Invoke(Sub()
                                             bcn_parentControl.Controls.Add(lbl)
                                         End Sub)
            Else
                bcn_parentControl.Controls.Add(lbl)
            End If
        End Sub
        Private Sub bnc_remoteDelimiterAsFirst(ByVal ctr As Control)
            Dim subPosition As Integer = ctr.Controls.Item(1).Width
            ctr.Controls.RemoveAt(1)
            Dim ctrCount = ctr.Controls.Count
            For i As Integer = 0 To ctrCount - 1
                If i = 0 Then

                End If
                ctr.Controls.Item(i).Location = New Point(ctr.Controls.Item(i).Location.X - subPosition, ctr.Controls.Item(i).Location.Y)
            Next
        End Sub
        Friend Sub bnc_NavigationClicked(sender As Object, e As EventArgs)
            Dim lbl As New Label
            lbl = sender
            setBreadcrumbNavigation(lbl.Tag, bcn_parentControl, bcn_removeSpecifiedPath)
        End Sub
    End Module
End Namespace
