Namespace Utils
    Public Class TrangThai
        Private _isAddnew As Boolean
        Public Property isAddNew() As Boolean
            Get
                Return _isAddnew
            End Get
            Set(ByVal value As Boolean)
                _isAddnew = value
                _isUpdate = Not value
                _isCopy = Not value
            End Set
        End Property
        Private _isUpdate As Boolean
        Public Property isUpdate() As Boolean
            Get
                Return _isUpdate
            End Get
            Set(ByVal value As Boolean)
                _isUpdate = value
                _isAddnew = Not value
                _isCopy = Not value
            End Set
        End Property
        Private _isCopy As Boolean
        Public Property isCopy() As Boolean
            Get
                Return _isCopy
            End Get
            Set(ByVal value As Boolean)
                _isCopy = value
                _isAddnew = Not value
                _isUpdate = Not value
            End Set
        End Property

        Public Sub New()
        End Sub
    End Class
End Namespace