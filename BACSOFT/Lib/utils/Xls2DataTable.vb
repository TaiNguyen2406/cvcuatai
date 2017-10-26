Imports System.Data.OleDb
Namespace Utils
    Public Class exportXLS2DataTable
        Public Shared Function getDataTableFromXLS(ByVal fileName As String, ByVal ext As String, ByVal sheetName As String) As DataTable
            Dim cnstr As String = ""
            Dim tb As New DataTable

            Try
                Select Case ext
                    Case ".xls"
                        cnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'"
                    Case ".xlsx"
                        cnstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'"
                End Select
                cnstr = String.Format(cnstr, fileName)
                Dim connExcel As New OleDbConnection(cnstr)
                connExcel.Open()



                Dim cmdExcel As New OleDbCommand("SELECT * FROM [" + sheetName + "$]", connExcel)
                Dim oda As New OleDbDataAdapter()
                oda.SelectCommand = cmdExcel
                oda.Fill(tb)
                connExcel.Close()
                oda = Nothing
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

            Return tb

        End Function

        Public Shared Function getDataTableFromXLSS_SpreadsheetGear(range As SpreadsheetGear.IRange, flags As SpreadsheetGear.Data.GetDataFlags) As DataTable
            ' Get a reference to the worksheet.
            Dim worksheet As SpreadsheetGear.IWorksheet = range.Worksheet

            ' Get a reference to all the worksheet cells.
            Dim cells As SpreadsheetGear.IRange = worksheet.Cells

            ' Get a reference to the advanced API.
            Dim values As SpreadsheetGear.Advanced.Cells.IValues = DirectCast(worksheet, SpreadsheetGear.Advanced.Cells.IValues)

            ' Create a new DataTable.
            Dim dataTable As New DataTable()

            ' Determine the row and column coordinates of the range.
            Dim row1 As Integer = range.Row
            Dim col1 As Integer = range.Column
            Dim rowCount As Integer = range.RowCount
            Dim colCount As Integer = range.ColumnCount
            Dim row2 As Integer = row1 + rowCount - 1
            Dim col2 As Integer = col1 + colCount - 1
            Dim row As Integer = row1

            ' If the first row is not used for column headers...
            If (flags And SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders) <> 0 Then
                ' Create columns using simple column names.
                For col As Integer = col1 To col2
                    Dim colName As String = "Column" + (col - col1 + 1)
                    dataTable.Columns.Add(colName)
                Next
            Else
                ' Create columns using the first row in the range for column names.
                For col As Integer = col1 To col2
                    ' Use the IRange API to get formatted text.
                    Dim colName As String = cells(row, col).Text
                    Select Case colName
                        Case "STT"
                            dataTable.Columns.Add(colName, GetType(System.Int32))
                        Case "Thoi_Gian"
                            dataTable.Columns.Add(colName, GetType(System.DateTime))
                        Case "So_Tien"
                            dataTable.Columns.Add(colName, GetType(System.Double))
                        Case "Phieu_TC"
                            dataTable.Columns.Add(colName, GetType(System.Double))
                        Case Else
                            dataTable.Columns.Add(colName, GetType(System.String))
                    End Select

                Next
                row += 1
            End If

            ' If the DataTable column data types should be set...
            'If (flags And SpreadsheetGear.Data.GetDataFlags.NoColumnTypes) = 0 AndAlso row <= row2 Then
            '    For col As Integer = col1 To col2
            '        ' Get a reference to the DataTable column.
            '        Dim dataCol As System.Data.DataColumn = dataTable.Columns(col - col1)

            '        ' If formatted text is to be used for all cell values...
            '        If (flags And SpreadsheetGear.Data.GetDataFlags.FormattedText) <> 0 Then
            '            ' Set the data type to a string.
            '            dataCol.DataType = GetType(String)
            '        Else
            '            ' Set the data type based on the type of data in the cell.
            '            '
            '            ' Note that this will cause problems if a column does not contain
            '            ' consistent data types - for example a column of formulas where
            '            ' the first is numeric but one of the following is an error.
            '            If dataCol.DataType Is GetType(DateTime) Then Continue For
            '            Dim value As SpreadsheetGear.Advanced.Cells.IValue = values(row, col)
            '            If value IsNot Nothing Then
            '                Select Case value.Type
            '                    Case SpreadsheetGear.Advanced.Cells.ValueType.Number
            '                        dataCol.DataType = GetType(Double)
            '                        Exit Select
            '                    Case SpreadsheetGear.Advanced.Cells.ValueType.Text, SpreadsheetGear.Advanced.Cells.ValueType.[Error]
            '                        dataCol.DataType = GetType(String)
            '                        Exit Select
            '                    Case SpreadsheetGear.Advanced.Cells.ValueType.Logical
            '                        dataCol.DataType = GetType(Boolean)
            '                        Exit Select
            '                End Select
            '            End If
            '        End If
            '    Next
            'End If

            ' If formatted text is to be used for all cell values...
            If (flags And SpreadsheetGear.Data.GetDataFlags.FormattedText) <> 0 Then
                ' Create the row data as an array of strings.
                Dim rowData As String() = New String(colCount - 1) {}
                While row <= row2
                    ' If the row is not hidden...
                    If Not cells(row, 0).Rows.Hidden Then
                        For col As Integer = col1 To col2
                            ' Use the IRange API to get formatted text.
                            Dim text As String = cells(row, col).Text
                            rowData(col - col1) = text
                        Next

                        ' Add a new row using the array of formatted strings.
                        dataTable.Rows.Add(rowData)
                    End If
                    row += 1
                End While
            Else
                ' Create the row data as an array of objects.
                Dim rowData As Object() = New Object(colCount - 1) {}
                While row <= row2
                    ' If the row is not hidden...
                    If Not cells(row, 0).Rows.Hidden Then
                        For col As Integer = col1 To col2
                            ' Use the advanced API to get the raw data values.
                            Dim value As SpreadsheetGear.Advanced.Cells.IValue = values(row, col)
                            Dim obj As Object = Nothing
                            If value IsNot Nothing Then
                                Select Case value.Type
                                    Case SpreadsheetGear.Advanced.Cells.ValueType.Number
                                        If cells(0, col).Text = "Thoi_Gian" Then
                                            obj = DateTime.FromOADate(value.Number)
                                        Else
                                            obj = value.Number
                                        End If

                                        Exit Select
                                    Case SpreadsheetGear.Advanced.Cells.ValueType.Text
                                        obj = value.Text
                                        Exit Select
                                    Case SpreadsheetGear.Advanced.Cells.ValueType.Logical
                                        obj = value.Logical
                                        Exit Select
                                        'Case (SpreadsheetGear.Advanced.Cells.ValueType.)
                                        '    obj = value.Logical
                                        '    Exit Select
                                    Case SpreadsheetGear.Advanced.Cells.ValueType.[Error]
                                        ' This will create problems if it is a column type
                                        ' of double or bool.
                                        obj = "#" + value.[Error].ToString().ToUpper() + "!"
                                        Exit Select
                                End Select
                            End If
                            rowData(col - col1) = obj
                        Next

                        ' Add a new row using the array of objects.
                        dataTable.Rows.Add(rowData)
                    End If
                    row += 1
                End While
            End If

            ' Return the DataTable.
            Return dataTable
        End Function

    End Class
End Namespace