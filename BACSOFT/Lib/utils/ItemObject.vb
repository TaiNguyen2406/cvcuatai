Namespace Utils

    Public Class ItemObject
        Private _value As Object
        Private _name As String

        Sub New(ByVal value As Object, ByVal text As String)
            _value = value
            _name = text
        End Sub
        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal value As Object)
                _value = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return _name
        End Function
    End Class

End Namespace