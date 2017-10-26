Imports System.Data.SqlClient
Imports System.Configuration

Public Class DbWebBaoAn


#Region "# SQLAuthenticationType #"
    Public Enum SQLAuthenticationType
        WindowsAuthentication = 0
        SQLServerAuthentication = 1
    End Enum
#End Region

#Region "# Property SQL Server  #"

    Private _ServerName As String = ""
    Private _DatabaseName As String = ""
    Private _SqlUserName As String = ""
    Private _SqlPassword As String = ""
    Private _Authentication As SQLAuthenticationType = SQLAuthenticationType.SQLServerAuthentication
    Private _SQLConnectionString As String = ""
    Private _objSqlConn As SqlConnection
    Private _LoiNgoaiLe As String = ""
    Public _strSQL As String = ""

    Public Property ServerName() As String
        Get
            Return _ServerName
        End Get
        Set(ByVal value As String)
            _ServerName = value
        End Set
    End Property

    Public Property DatabaseName() As String
        Get
            Return _DatabaseName
        End Get
        Set(ByVal value As String)
            _DatabaseName = value
        End Set
    End Property

    Public Property SqlUserName() As String
        Get
            Return _SqlUserName
        End Get
        Set(ByVal value As String)
            _SqlUserName = value
        End Set
    End Property

    Public Property SqlPassword() As String
        Get
            Return _SqlPassword
        End Get
        Set(ByVal value As String)
            _SqlPassword = value
        End Set
    End Property

    Public Property Authentication() As SQLAuthenticationType
        Get
            Return _Authentication
        End Get
        Set(ByVal value As SQLAuthenticationType)
            _Authentication = value
        End Set
    End Property

    Public Property SQLConnectionString() As String
        Get
            Return _SQLConnectionString
        End Get
        Set(ByVal value As String)
            _SQLConnectionString = value
        End Set
    End Property

    Public Property LoiNgoaiLe() As String
        Get
            Return _LoiNgoaiLe
        End Get
        Set(ByVal value As String)
            _LoiNgoaiLe = value
        End Set
    End Property

    Public Property objSqlConn() As SqlConnection
        Get
            Return _objSqlConn
        End Get
        Set(ByVal value As SqlConnection)
            _objSqlConn = value
        End Set
    End Property

#End Region

#Region "# Sub New -#"
    Protected Sub New(ByVal strServerName As String, ByVal strDatabaseName As String, ByVal strUserNameSQL As String, ByVal strPasswordSQL As String)
        '_ServerName = strServerName
        '_DatabaseName = strDatabaseName
        '_UserNameSQL = strUserNameSQL
        '_PasswordSQL = strPasswordSQL
        '_Authentication = SQLAuthenticationType.SQLServerAuthentication
    End Sub
    Public Sub New()
        _objSqlParamtter = New List(Of SqlParameter)
        _objSqlParamtterWhere = New List(Of SqlParameter)
    End Sub
    Protected Overridable Overloads Sub Dispose()
        Try
            _objSqlConn.Dispose()
            If Not _objSqlConnWithTrans Is Nothing Then
                _objSqlConnWithTrans.Dispose()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Overrides Sub Finalize()
        Dispose()
    End Sub
#End Region

#Region "# getSQLConnectionString #"
    Public Function getSQLConnectionString() As String
        'Return ConfigurationManager.ConnectionStrings("strChuoiKetNoi").ConnectionString
        Return "Data Source=baoanjsc.com.vn;Initial Catalog=nhbaosov_webbaoan;User ID=nhbao_adminbaoan;Password=p@ssw0rd123qwe"
        'Return "Data Source=192.168.1.109\SQL2005;Initial Catalog=WebBaoAn.User ID=webmaster;Password=adminbaoan"
    End Function
#End Region

#Region "# AddParameter #"
    Private _objSqlParamtter As List(Of SqlParameter)
    Private _objSqlParamtterWhere As List(Of SqlParameter)
    Public ReadOnly Property objSqlParamtter() As List(Of SqlParameter)
        Get
            Return _objSqlParamtter
        End Get
    End Property
    Public ReadOnly Property objSqlParamtterWhere() As List(Of SqlParameter)
        Get
            Return _objSqlParamtterWhere
        End Get
    End Property
    Public Sub AddParameter(ByVal parmName As String, ByVal parmValue As Object)
        If Not parmValue Is Nothing Then
            _objSqlParamtter.Add(New SqlParameter(parmName, parmValue))
        Else
            _objSqlParamtter.Add(New SqlParameter(parmName, DBNull.Value))
        End If
    End Sub
    Public Sub AddParameterWhere(ByVal parmName As String, ByVal parmValue As Object)
        If Not parmValue Is Nothing Then
            _objSqlParamtterWhere.Add(New SqlParameter(parmName, parmValue))
        Else
            _objSqlParamtterWhere.Add(New SqlParameter(parmName, DBNull.Value))
        End If
    End Sub
#End Region

#Region "# Transaction #"
    Private _objTransaction As SqlTransaction = Nothing
    Private _objSqlConnWithTrans As SqlConnection = Nothing
    Public Function BeginTransaction() As Boolean
        _objSqlConnWithTrans = New SqlConnection(getSQLConnectionString)
        Try
            _objSqlConnWithTrans.Open()
            _objTransaction = _objSqlConnWithTrans.BeginTransaction
            Return True
        Catch ex As Exception
            _LoiNgoaiLe = ex.Message
            Return False
        End Try
    End Function

    Public Function ComitTransaction() As Boolean
        Try
            If _objSqlConnWithTrans Is Nothing Then Return False
            _objTransaction.Commit()
            If _objSqlConnWithTrans.State <> ConnectionState.Closed Then
                _objSqlConnWithTrans.Close()
                _objTransaction = Nothing
                _objSqlConnWithTrans = Nothing
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RollBackTransaction() As Boolean
        Try
            If _objSqlConnWithTrans Is Nothing Then Return False
            _objTransaction.Rollback()
            If _objSqlConnWithTrans.State <> ConnectionState.Closed Then
                _objSqlConnWithTrans.Close()
                _objTransaction = Nothing
                _objSqlConnWithTrans = Nothing
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "# ExecuteSQLDataTable #"
    Public Function ExecuteSQLDataTable(ByVal sSQL As String) As DataTable
        Dim dt As New DataTable
        Dim _sqlCmd As New SqlCommand
        Try
            _objSqlConn = New SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sSQL
            _sqlCmd.Connection = _objSqlConn
            _objSqlConn.Open()
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Dim da As New SqlDataAdapter(_sqlCmd)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            _LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
                _objSqlConn.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "# ExecuteSQLDataSet #"
    Public Function ExecuteSQLDataSet(ByVal sSQL As String) As DataSet
        Dim ds As New DataSet
        Dim _sqlCmd As New SqlCommand
        Try
            _objSqlConn = New SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sSQL
            _sqlCmd.Connection = _objSqlConn
            _objSqlConn.Open()
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Dim da As New SqlDataAdapter(_sqlCmd)
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            _LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
                _objSqlConn.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "# ExecuteSQLNonQuery #"
    Public Function ExecuteSQLNonQuery(ByVal sSQL As String) As Object
        Dim _sqlCmd As New SqlCommand
        Try
            If _objSqlConnWithTrans Is Nothing Then
                _objSqlConn = New SqlConnection(getSQLConnectionString)
                _sqlCmd.Connection = _objSqlConn
                _objSqlConn.Open()
            Else
                _sqlCmd.Connection = _objSqlConnWithTrans
                _sqlCmd.Transaction = _objTransaction
            End If
            _sqlCmd.CommandText = sSQL
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Return CType(_sqlCmd.ExecuteNonQuery(), Integer)
        Catch ex As Exception
            LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConnWithTrans Is Nothing Then
                If _objSqlConn.State <> ConnectionState.Closed Then
                    _objSqlConn.Close()
                    _objSqlConn.Dispose()
                End If
            End If
        End Try
    End Function
#End Region

#Region "# ExecuteSQLScalar #"
    Public Function ExecuteSQLScalar(ByVal sSQL As String) As Object
        Dim _sqlCmd As New SqlCommand
        Try
            If _objSqlConnWithTrans Is Nothing Then
                _objSqlConn = New SqlConnection(getSQLConnectionString)
                _objSqlConn.Open()
                _sqlCmd.Connection = _objSqlConn
            Else
                _sqlCmd.Connection = _objSqlConnWithTrans
                _sqlCmd.Transaction = _objTransaction
            End If
            _sqlCmd.CommandText = sSQL
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Return _sqlCmd.ExecuteScalar()
        Catch ex As Exception
            LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
                _objSqlConn.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "# ExecutePrcDataTable #"
    Public Function ExecutePrcDataTable(ByVal sSQL As String) As DataTable
        Dim dt As New DataTable
        Dim _sqlCmd As New SqlCommand
        Try
            _objSqlConn = New SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sSQL
            _sqlCmd.CommandType = CommandType.StoredProcedure
            _sqlCmd.Connection = _objSqlConn
            _objSqlConn.Open()
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Dim da As New SqlDataAdapter(_sqlCmd)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            _LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
                _objSqlConn.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "# ExecutePrcDataSet #"
    Public Function ExecutePrcDataSet(ByVal sSQL As String) As DataSet
        Dim ds As New DataSet
        Dim _sqlCmd As New SqlCommand
        Try
            _objSqlConn = New SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sSQL
            _sqlCmd.CommandType = CommandType.StoredProcedure
            _sqlCmd.Connection = _objSqlConn
            _objSqlConn.Open()
            For Each p As SqlParameter In _objSqlParamtter
                _sqlCmd.Parameters.Add(p)
            Next
            For Each p As SqlParameter In _objSqlParamtterWhere
                _sqlCmd.Parameters.Add(p)
            Next
            Dim da As New SqlDataAdapter(_sqlCmd)
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            _LoiNgoaiLe = ex.Message
            Return Nothing
        Finally
            _sqlCmd.Dispose()
            _objSqlParamtter.Clear()
            _objSqlParamtterWhere.Clear()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
                _objSqlConn.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "# doInsert #"
    Public Function doInsert(ByVal TenBang As String) As Object
        Dim sql As String = "SET DATEFORMAT DMY "
        Dim str1 As String = "" : Dim str2 As String = ""
        For Each p As SqlClient.SqlParameter In objSqlParamtter
            str1 &= p.ParameterName.Substring(1) & ", "
            str2 &= p.ParameterName & ", "
        Next
        If str1.Length > 2 Then str1 = str1.Substring(0, str1.Length - 2)
        If str2.Length > 2 Then str2 = str2.Substring(0, str2.Length - 2)
        sql &= "INSERT INTO [" & TenBang & "](" & str1 & ") VALUES(" & str2 & "); SELECT SCOPE_IDENTITY();"
        Return ExecuteSQLScalar(sql)
    End Function
#End Region

#Region "# doUpdate #"
    Public Function doUpdate(ByVal TenBang As String, ByVal where As String) As Object
        Dim sql As String = "SET DATEFORMAT DMY UPDATE [" & TenBang & "] "
        Dim str1 As String = ""
        For Each p As SqlClient.SqlParameter In objSqlParamtter
            str1 &= p.ParameterName.Substring(1) & " = " & p.ParameterName & ", "
        Next
        If str1.Length > 2 Then str1 = str1.Substring(0, str1.Length - 2)
        sql &= "SET " & str1 & " "
        sql &= "WHERE " & where & " "
        Return ExecuteSQLNonQuery(sql)
    End Function
#End Region

#Region "# doDelete #"
    Public Function doDelete(ByVal TenBang As String, ByVal where As String) As Object
        Dim sql As String = "DELETE FROM [" & TenBang & "] WHERE " & where
        Return ExecuteSQLNonQuery(sql)
    End Function
#End Region

    Public Class ThamSo


        Private _Ten As String
        Public Property Ten() As String
            Get
                Return _Ten
            End Get
            Set(ByVal value As String)
                _Ten = value
            End Set
        End Property


        Private _GiaTri As Object
        Public Property GiaTri() As Object
            Get
                Return _GiaTri
            End Get
            Set(ByVal value As Object)
                _GiaTri = value
            End Set
        End Property


        Public Sub New(ByVal __Ten As String, ByVal __GiaTri As Object)
            Ten = __Ten
            GiaTri = __GiaTri
        End Sub


    End Class

End Class


