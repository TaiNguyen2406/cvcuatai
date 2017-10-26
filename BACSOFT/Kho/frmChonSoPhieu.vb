Public Class frmChonSoPhieu 

    Private Sub gdvCT_RowClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles gdvCT.RowClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Me.Tag = "mDHCanNhap" Then
            TrangThai.isUpdate = True
            fCNNhapKho = New frmCNNhapKho
            fCNNhapKho.PhieuNK = gdvCT.GetFocusedRowCellValue("SoPhieu")
            fCNNhapKho.Tag = Me.Tag
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                fCNNhapKho.btCal.Enabled = False
                fCNNhapKho.btGhi.Enabled = False
                fCNNhapKho.btChuyenXK.Enabled = False
                fCNNhapKho.mNhapKho.Enabled = False
            End If
            fCNNhapKho.ShowDialog()
        Else
            TrangThai.isUpdate = True
            fCNXuatKho = New frmCNXuatKho
            fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieu")
            fCNXuatKho.Tag = Me.Tag
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                fCNXuatKho.btCal.Enabled = False
                fCNXuatKho.btGhi.Enabled = False
                fCNXuatKho.btChuyenXK.Enabled = False
                fCNXuatKho.mXuatKho.Enabled = False
            End If
            fCNXuatKho.ShowDialog()
        End If
        
    End Sub
End Class