Imports BACSOFT.Db.SqlHelper

Public Class frmCNThamSoTinhTon

    Private Sub frmCNThamSoTinhTon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sql As String = ""

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=21 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('CanTon_SoLanXuat',0,21)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=22 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('CanTon_SoKH',0,22)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=23 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('TonMin_XXX',0,23)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=24 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('TonMin_YYY',0,24)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=25 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('TonMin_MauSoChia',0,25)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=26 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_BoiSoTonMin',0,26)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=27 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_SLXuat',0,27)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=28 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_SoKH',0,28)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=29 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_XuatMax',0,29)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=30 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_XuatMax',0,30)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=31 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_XuatMax',0,31)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=32 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_XuatMax',0,32)"
        sql &= " END"

        sql &= " if not exists(Select ISNULL(Diem,0) FROM tblTuDien WHERE Loai=33 )"
        sql &= " Begin"
        sql &= " INSERT INTO tblTuDien (NoiDung,Diem,Loai)"
        sql &= " Values('DatMin_XuatMax',0,33)"
        sql &= " END"

        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        sql = ""

        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.CanTon_SoLanXuat
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.CanTon_SoKH
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_XXX
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_YYY
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_MauSoChia
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_MauSoChia
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ3
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ3
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ2
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ2
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ1
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ1
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_XuatMax

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            tbCanTon_SoLanXuat.EditValue = ds.Tables(0).Rows(0)(0)
            tbCanTon_SoKH.EditValue = ds.Tables(1).Rows(0)(0)
            tbTonMin_XXX.EditValue = ds.Tables(2).Rows(0)(0)
            tbTonMin_YYY.EditValue = ds.Tables(3).Rows(0)(0)
            tbTonMin_MSChia.EditValue = ds.Tables(4).Rows(0)(0)
            tbDatMin_MauSoChia.EditValue = ds.Tables(5).Rows(0)(0)
            tbDatMin_SLXuatBTMOQ3.EditValue = ds.Tables(6).Rows(0)(0)
            tbDatMin_SoKHMOQ3.EditValue = ds.Tables(7).Rows(0)(0)
            tbDatMin_SLXuatBTMOQ2.EditValue = ds.Tables(8).Rows(0)(0)
            tbDatMin_SoKHMOQ2.EditValue = ds.Tables(9).Rows(0)(0)
            tbDatMin_SLXuatBTMOQ1.EditValue = ds.Tables(10).Rows(0)(0)
            tbDatMin_SoKHMOQ1.EditValue = ds.Tables(11).Rows(0)(0)
            tbDatMin_XuatMax.EditValue = ds.Tables(12).Rows(0)(0)
        End If

    End Sub


    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        Dim Sql As String = ""

        Sql &= " UPDATE tblTuDien SET Diem=@CanTon_SoLanXuat WHERE Loai = " & LoaiTuDien.CanTon_SoLanXuat
        Sql &= " UPDATE tblTuDien SET Diem=@CanTon_SoKH WHERE Loai = " & LoaiTuDien.CanTon_SoKH
        Sql &= " UPDATE tblTuDien SET Diem=@TonMin_XXX WHERE Loai = " & LoaiTuDien.TonMin_XXX
        Sql &= " UPDATE tblTuDien SET Diem=@TonMin_YYY WHERE Loai = " & LoaiTuDien.TonMin_YYY
        Sql &= " UPDATE tblTuDien SET Diem=@TonMin_MSChia WHERE Loai = " & LoaiTuDien.TonMin_MauSoChia
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_MauSoChia WHERE Loai = " & LoaiTuDien.DatMin_MauSoChia
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SLXuatMOQ3 WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ3
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SoKHMOQ3 WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ3
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SLXuatMOQ2 WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ2
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SoKHMOQ2 WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ2
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SLXuatMOQ1 WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ1
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_SoKHMOQ1 WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ1
        Sql &= " UPDATE tblTuDien SET Diem=@DatMin_XuatMax WHERE Loai = " & LoaiTuDien.DatMin_XuatMax
        AddParameter("@CanTon_SoLanXuat", tbCanTon_SoLanXuat.EditValue)
        AddParameter("@CanTon_SoKH", tbCanTon_SoKH.EditValue)
        AddParameter("@TonMin_XXX", tbTonMin_XXX.EditValue)
        AddParameter("@TonMin_YYY", tbTonMin_YYY.EditValue)
        AddParameter("@TonMin_MSChia", tbTonMin_MSChia.EditValue)
        AddParameter("@DatMin_MauSoChia", tbDatMin_MauSoChia.EditValue)
        AddParameter("@DatMin_SLXuatMOQ3", tbDatMin_SLXuatBTMOQ3.EditValue)
        AddParameter("@DatMin_SoKHMOQ3", tbDatMin_SoKHMOQ3.EditValue)
        AddParameter("@DatMin_SLXuatMOQ2", tbDatMin_SLXuatBTMOQ2.EditValue)
        AddParameter("@DatMin_SoKHMOQ2", tbDatMin_SoKHMOQ2.EditValue)
        AddParameter("@DatMin_SLXuatMOQ1", tbDatMin_SLXuatBTMOQ1.EditValue)
        AddParameter("@DatMin_SoKHMOQ1", tbDatMin_SoKHMOQ1.EditValue)
        AddParameter("@DatMin_XuatMax", tbDatMin_XuatMax.EditValue)


        If ExecuteSQLNonQuery(Sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

End Class