Public Class Yaml

	Public Function YamlKey(ByVal Key As String, ByVal value As String, Optional tabs As String = "") As String
		Return tabs & Key & ":" & value & Environment.NewLine
	End Function

End Class
