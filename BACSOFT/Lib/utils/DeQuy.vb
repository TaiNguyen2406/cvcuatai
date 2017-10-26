Namespace Utils
    Public Class DeQuy
        Public Shared Function deQuycmb(ByVal tb As DataTable, Optional ByVal nullStr As String = "-Không thuộc nhóm nào-", Optional ByVal nullTile As Boolean = True) As DataTable
            Dim tb2 As DataTable = tb.Copy
            tb2.Rows.Clear()
            If nullTile Then
                Dim dr As DataRow = tb2.NewRow
                dr("ID") = DBNull.Value
                dr("tenNhom") = nullStr
                dr("ID_Cha") = DBNull.Value
                tb2.Rows.Add(dr)
            End If

            For i As Integer = 0 To tb.Rows.Count - 1
                If tb.Rows(i)("ID_Cha") Is DBNull.Value Then
                    Dim r As DataRow = tb2.NewRow
                    r("ID") = tb.Rows(i)("ID")
                    r("tenNhom") = tb.Rows(i)("tenNhom")
                    r("ID_Cha") = tb.Rows(i)("ID_Cha")
                    tb2.Rows.Add(r)
                    deQuy(tb, tb.Rows(i)("ID"), 1, tb2)
                End If
            Next
            Return tb2
        End Function

        Protected Shared Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
            For i As Integer = 0 To tb.Rows.Count - 1
                If tb.Rows(i)("ID_Cha") Is DBNull.Value Then Continue For
                If tb.Rows(i)("ID_Cha") = idCha Then
                    Dim strTen As String = ""
                    For j As Integer = 0 To level - 1
                        strTen &= "-- "
                    Next
                    strTen = " " & strTen & tb.Rows(i)("tenNhom")
                    Dim r As DataRow = tb2.NewRow
                    r("ID") = tb.Rows(i)("ID")
                    r("tenNhom") = strTen
                    r("ID_Cha") = tb.Rows(i)("ID_Cha")
                    tb2.Rows.Add(r)
                    deQuy(tb, tb.Rows(i)("ID"), level + 1, tb2)
                End If
            Next
        End Sub

        Public Shared Function deQuycmb2(ByVal tb As DataTable) As DataTable
            Dim tb2 As DataTable = tb.Copy
            tb2.Rows.Clear()
            For i As Integer = 0 To tb.Rows.Count - 1
                If tb.Rows(i)("ID_Cha") Is DBNull.Value Then
                    Dim r As DataRow = tb2.NewRow
                    r("ID") = tb.Rows(i)("ID")
                    r("tenNhom") = tb.Rows(i)("tenNhom")
                    r("ID_Cha") = tb.Rows(i)("ID_Cha")
                    tb2.Rows.Add(r)
                    deQuy2(tb, tb.Rows(i)("ID"), 1, tb2)
                End If
            Next
            Return tb2
        End Function

        Protected Shared Sub deQuy2(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
            For i As Integer = 0 To tb.Rows.Count - 1
                If tb.Rows(i)("ID_Cha") Is DBNull.Value Then Continue For
                If tb.Rows(i)("ID_Cha") = idCha Then
                    Dim r As DataRow = tb2.NewRow
                    r("ID") = tb.Rows(i)("ID")
                    r("tenNhom") = tb.Rows(i)("tenNhom")
                    r("ID_Cha") = tb.Rows(i)("ID_Cha")
                    tb2.Rows.Add(r)
                    deQuy2(tb, tb.Rows(i)("ID"), level + 1, tb2)
                End If
            Next
        End Sub
    End Class
End Namespace