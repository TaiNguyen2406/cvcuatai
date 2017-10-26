Imports Microsoft.VisualBasic.CompilerServices
Imports System.Collections.Generic

Namespace Utils

    Public Class StringHelper
        Public Shared Function DOC_SO_VN(ByVal so As Double, ByVal dvt As String, ByVal sole As String) As String
            Dim i1 As Integer = 0

            Dim string1 As String = Nothing
            Dim string2 As String = Nothing
            Dim string3 As String = Nothing
            Dim string4 As String = Nothing
            Dim string5 As String = Nothing
            Dim string6 As String = Nothing
            Dim string7 As String = Nothing
            Dim string8 As String = Nothing
            Dim string9 As String = Nothing
            Dim string10 As String = Nothing

            Dim i2 As Integer = 0
            Dim string11 As String = Nothing
            Dim i3 As Integer = 0

            Dim string12 As String = Nothing
            Dim string13 As String = Nothing
            Dim string14 As String = Nothing
            Dim string15 As String = Nothing
            Dim stringArray1 As String() = Nothing

            string1 = "1một   2hai   3ba    4bốn   5năm   6sáu   7bẩy   8tám   9chín  #tỉ    $triệu %ngàn  &      'xu    "
            string2 = "trăm"
            string3 = "mươi"
            string4 = "mười"
            string5 = "lăm"
            string6 = "mốt"
            string7 = "lẻ"
            string8 = "chẵn"
            string9 = "không"
            string10 = so.ToString().Trim()
            i2 = Strings.InStr(1, string10, ".", CompareMethod.Binary)
            If i2 > 0 Then
                string11 = string10.Substring(CInt(i2 + 1))
                i3 = (2 - string11.Length)
                i1 = 1
                While (i1 <= i3)
                    string10 = (string10 & "0")
                    i1 += 1
                End While
            Else
                string10 = (string10 & ".00")
            End If
            string10 = Strings.Right((Strings.Space(15) & string10), 15)
            i1 = 0
            string12 = StringType.FromObject((If((so = 0.0), (string9 & " " & dvt & ".!"), "")))
            GoTo L_05A5
L_05A1:


            If True Then
            End If
            i1 += 1
L_05A5:


            If True Then
            End If
            If (i1 < 5) And (so <> 0.0) Then
                If Not ((StringType.StrCmp(Strings.Mid(string10, CInt((i1 * 3) + 1), 3), "   ", False) <> 0) And (StringType.StrCmp(Strings.Mid(string10, CInt((i1 * 3) + 1), 3), "000", False) <> 0)) Then
                    GoTo L_05A1
                ElseIf i1 <> 4 Then
                    string13 = Strings.Mid(string10, CInt((i1 * 3) + 1), 1)
                    string14 = Strings.Mid(string10, CInt((i1 * 3) + 2), 1)
                    string15 = Strings.Mid(string10, CInt((i1 * 3) + 3), 1)
                    string12 = StringType.FromObject(ObjectType.AddObj(StringType.FromObject(ObjectType.AddObj(StringType.FromObject(ObjectType.AddObj(string12, (If((StringType.StrCmp(string13, "0", False) > 0), (" " & Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string13, CompareMethod.Binary) + 1), 6)) & " " & string2), (If((StringType.StrCmp(string12, "", False) <> 0), (" " & string9 & " " & string2), "")))))), (If((StringType.StrCmp(string14, "0", False) = 0), (If(((StringType.StrCmp(string15, "0", False) <> 0) And (i1 <> 4)), (" " & string7), "")), (If((StringType.StrCmp(string14, "1", False) > 0), (" " & Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string14, CompareMethod.Binary) + 1), 6)) & " " & string3), (If((StringType.StrCmp(string14, "1", False) = 0), (" " & string4), "")))))))), (If(((StringType.StrCmp(string15, "5", False) = 0) And (StringType.StrCmp(string14, "0", False) > 0)), (" " & string5), (If(((StringType.StrCmp(string15, "1", False) = 0) And (StringType.StrCmp(string14, "1", False) > 0)), (" " & string6), (If((StringType.StrCmp(string15, "0", False) <> 0), (" " & Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string15, CompareMethod.Binary) + 1), 6))), ""))))))))
                    If i1 <> 3 Then
                        string12 = StringType.FromObject(ObjectType.AddObj(string12, (If((((StringType.StrCmp(string13, "0", False) <> 0) Or (StringType.StrCmp(string14, "0", False) <> 0)) Or (StringType.StrCmp(string15, "0", False) <> 0)), (" " & Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, StringType.FromChar(Strings.Chr(CInt(i1 + 35))), CompareMethod.Binary) + 1), 6))), ""))))
                    End If
                    GoTo L_05A1
                Else
                    string12 = (string12 & " " & dvt)
                    If StringType.StrCmp(Strings.Mid(string10, CInt((i1 * 3) + 2), 3), "00", False) <> 0 Then
                        string13 = string10.Substring(CInt((i1 * 3) + 2), 1)
                        string14 = string10.Substring(CInt((i1 * 3) + 3), 1)
                        If StringType.StrCmp(string13, "0", False) <> 0 Then
                            string12 = StringType.FromObject(ObjectType.AddObj(StringType.FromObject(ObjectType.AddObj((string12 & " "), (If((StringType.StrCmp(string13, "1", False) > 0), (Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string13, CompareMethod.Binary) + 1), 6)) & " " & string3), string4)))), (If((StringType.StrCmp(string14, "0", False) = 0), DirectCast("", Object), ObjectType.AddObj(" ", (If(((StringType.StrCmp(string14, "5", False) = 0) And (StringType.StrCmp(string13, "0", False) > 0)), string5, (If(((StringType.StrCmp(string14, "1", False) = 0) And (StringType.StrCmp(string13, "1", False) > 0)), string6, Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string14, CompareMethod.Binary) + 1), 6)))))))))))
                        Else
                            stringArray1 = New String() {string12, " ", string7, " ", Strings.Trim(Strings.Mid(string1, CInt(Strings.InStr(1, string1, string14, CompareMethod.Binary) + 1), 6))}
                            string12 = String.Concat(stringArray1)
                        End If
                        string12 = (string12 & " " & sole)
                    Else
                        string12 = (string12 & " " & string8)
                    End If
                End If
            End If
            string12 = string12.Trim()
            Return (string12.Substring(0, 1).ToUpper() & string12.Substring(1, CInt(string12.Length - 1)))
        End Function

        Public Shared Function USD2String(ByVal Tien As Double, Optional ByVal TienTe As String = "dollars") As String
            Dim Toread, sp, rr, donvi, hchuc, khung, chuoi, nhom, word As String
            Dim x, y, z, w As Integer
            If Tien = 0 Then
                Toread = "None"
            Else
                sp = Space(1)
                rr = Space(0)
                donvi = rr
                hchuc = rr
                khung = rr
                donvi = donvi + "one      two      three    four     "
                donvi = donvi + "five     six      seven    eight    "
                donvi = donvi + "nine     ten      eleven   twelve   "
                donvi = donvi + "thirteen fourteen fifteen  sixteen  "
                donvi = donvi + "seventeen eighteen nineteen "
                hchuc = hchuc + "twenty   thirty   forty    fifty    "
                hchuc = hchuc + "sixty    seventy  eighty   ninety   "
                khung = khung + "billion  milion   thousand " & TienTe & "  cents    "
                If Tien < 0 Then
                    Toread = "Minus "
                Else
                    Toread = rr
                End If
                chuoi = Format(Math.Abs(Tien), "############.00")
                chuoi = Right(Space(12) + chuoi, 15)
                For i As Integer = 1 To 5
                    nhom = Mid(chuoi, i * 3 - 2, 3)
                    If nhom <> Space(3) Then
                        Select Case nhom
                            Case "000"
                                If i = 4 And Math.Abs(Tien) > 1 Then
                                    word = TienTe & " "
                                Else
                                    word = rr
                                End If
                            Case ".00"
                                word = ""
                            Case Else
                                x = Val(Left(nhom, 1))
                                y = Val(Mid(nhom, 2, 1))
                                z = Val(Right(nhom, 1))
                                w = Val(Right(nhom, 2))
                                If x = 0 Then
                                    word = rr
                                Else
                                    word = Trim(Mid(donvi, x * 9 - 8, 9)) + " hundred "
                                    If w > 0 And w < 21 Then
                                        word = word + "and "
                                    End If
                                End If
                                If i = 5 And Math.Abs(Tien) > 1 Then
                                    word = "and " + word
                                End If
                                If w < 20 And w > 0 Then
                                    word = word + Trim(Mid(donvi, w * 9 - 8, 9)) + sp
                                Else
                                    If w >= 20 Then
                                        word = word + Trim(Mid(hchuc, (y - 1) * 9 - 8, 9)) + sp
                                        If z > 0 Then
                                            word = word + Trim(Mid(donvi, z * 9 - 8, 9)) + sp
                                        End If
                                    End If
                                End If
                                word = word + Trim(Mid(khung, i * 9 - 8, 9)) + sp
                        End Select
                        Toread = Toread + word
                    End If
                Next i
            End If
            Dim str() As Char = Toread.ToCharArray
            str(0) = Char.ToUpper(str(0))
            Return New String(str)
            'Toread = Toread.Replace("Dollars", "dollars")
            'Toread = Toread.Replace("Cents", "cents")
            'Toread = Toread.Replace("Hundred ", "hundred and ")
            'Toread = Toread.Replace("Thousand ", "thousand and ")
            'Toread = Toread.Replace("and and", "and")
            'Toread = Toread.Replace("and dollars", " dollars") & "./."
            'Toread = Toread.Replace("and one hundred", " one hundred")
            'Toread = Toread.Replace("and two hundred", " two hundred")
            'Toread = Toread.Replace("and three hundred", " three hundred")
            'Toread = Toread.Replace("and four hundred", " four hundred")
            'Toread = Toread.Replace("and five hundred", " five hundred")
            'Toread = Toread.Replace("and six hundred", " six hundred")
            'Toread = Toread.Replace("and seven hundred", " seven hundred")
            'Toread = Toread.Replace("and eight hundred", " eight hundred")
            'Toread = Toread.Replace("and nine hundred", " nine hundred")

            'Return Toread
        End Function

        Public Shared Function Number2String_Eng(ByVal Tien As Double) As String
            Dim Toread, sp, rr, donvi, hchuc, khung, chuoi, nhom, word As String
            Dim x, y, z, w As Integer
            If Tien = 0 Then
                Toread = "None"
            Else
                sp = Space(1)
                rr = Space(0)
                donvi = rr
                hchuc = rr
                khung = rr
                donvi = donvi + "one      two      three    four     "
                donvi = donvi + "five     six      seven    eight    "
                donvi = donvi + "nine     ten      eleven   twelve   "
                donvi = donvi + "thirteen fourteen fifteen  sixteen  "
                donvi = donvi + "seventeen eighteen nineteen "
                hchuc = hchuc + "twenty   thirty   forty    fifty    "
                hchuc = hchuc + "sixty    seventy  eighty   ninety   "
                khung = khung + "billion  milion   thousand dong     "
                If Tien < 0 Then
                    Toread = "Minus "
                Else
                    Toread = rr
                End If
                chuoi = Format(Math.Abs(Tien), "############.00")
                chuoi = Right(Space(12) + chuoi, 15)
                For i As Integer = 1 To 5
                    nhom = Mid(chuoi, i * 3 - 2, 3)
                    If nhom <> Space(3) Then
                        Select Case nhom
                            Case "000"
                                If i = 4 And Math.Abs(Tien) > 1 Then
                                    word = "dong "
                                Else
                                    word = rr
                                End If
                            Case ".00"
                                word = ""
                            Case Else
                                x = Val(Left(nhom, 1))
                                y = Val(Mid(nhom, 2, 1))
                                z = Val(Right(nhom, 1))
                                w = Val(Right(nhom, 2))
                                If x = 0 Then
                                    word = rr
                                Else
                                    word = Trim(Mid(donvi, x * 9 - 8, 9)) + " hundred "
                                    If w > 0 And w < 21 Then
                                        word = word + "and "
                                    End If
                                End If
                                'If i = 5 And Math.Abs(Tien) > 1 Then
                                '    word = "and " + word
                                'End If
                                If w < 20 And w > 0 Then
                                    word = word + Trim(Mid(donvi, w * 9 - 8, 9)) + sp
                                Else
                                    If w >= 20 Then
                                        word = word + Trim(Mid(hchuc, (y - 1) * 9 - 8, 9)) + sp
                                        If z > 0 Then
                                            word = word + Trim(Mid(donvi, z * 9 - 8, 9)) + sp
                                        End If
                                    End If
                                End If
                                word = word + Trim(Mid(khung, i * 9 - 8, 9)) + sp
                        End Select
                        Toread = Toread + word
                    End If
                Next i
            End If
            Dim str() As Char = Toread.ToCharArray
            str(0) = Char.ToUpper(str(0))
            Return New String(str)
            'Toread = Toread.Replace("Dollars", "dollars")
            'Toread = Toread.Replace("Cents", "cents")
            'Toread = Toread.Replace("Hundred ", "hundred and ")
            'Toread = Toread.Replace("Thousand ", "thousand and ")
            'Toread = Toread.Replace("and and", "and")
            'Toread = Toread.Replace("and dollars", " dollars") & "./."
            'Toread = Toread.Replace("and one hundred", " one hundred")
            'Toread = Toread.Replace("and two hundred", " two hundred")
            'Toread = Toread.Replace("and three hundred", " three hundred")
            'Toread = Toread.Replace("and four hundred", " four hundred")
            'Toread = Toread.Replace("and five hundred", " five hundred")
            'Toread = Toread.Replace("and six hundred", " six hundred")
            'Toread = Toread.Replace("and seven hundred", " seven hundred")
            'Toread = Toread.Replace("and eight hundred", " eight hundred")
            'Toread = Toread.Replace("and nine hundred", " nine hundred")

            'Return Toread
        End Function

        Public Shared Function VIE2String(ByVal num As Double, ByVal lamtron As Boolean, ByVal unitMeasure As String, ByVal sole As String, ByVal sophay As String, ByVal SoSoLe As Integer) As String
            Dim string1 As String = Nothing
            Dim i2 As Integer = 0
            If lamtron Then
                num = System.Math.Round(num)
            End If
            Dim double1 As Double = Conversion.Int(num)
            Dim double2 As Double = Math.Round((num - Conversion.Int(num)), SoSoLe)
            Dim i1 As Integer = SoSoLe
            i2 = 1
            While (i2 <= i1)
                double2 *= 10.0
                i2 += 1
            End While
            Dim string2 As String = StringHelper.DOC_SO_VN(double1, unitMeasure, sole)
            If double2 > 0.0 Then
                string2 = Strings.Replace(string2, "chẵn", "", 1, -1, CompareMethod.Binary)
                string2 = string2.Replace(unitMeasure, "")
                string2 = string2.Trim()
                string1 = Strings.Replace(StringHelper.DOC_SO_VN(double2, sole, ""), "chẵn", "", 1, -1, CompareMethod.Binary)
                string1 = string1.ToLower()
                string1 = string1.Replace(sole, "")
                string1 = sophay & " " & string1
                string1 = string1.Trim() & " " & unitMeasure.Trim()
                Return (string2 & " " & string1)
            End If
            string1 = ""
            Return (string2 & string1)
        End Function

        Public Shared Function TCVN3ToUNICODE(ByVal str As String) As String
            Dim string2 As String = Nothing
            Dim i1 As Integer = 0
            Dim string3 As String = Nothing
            Dim i2 As Integer = 0
            Dim char1 As Char = ControlChars.NullChar
            Dim char2 As Char = ControlChars.NullChar
            Dim char3 As Char = ControlChars.NullChar
            Dim char4 As Char = ControlChars.NullChar
            Dim char5 As Char = ControlChars.NullChar
            Dim char6 As Char = ControlChars.NullChar
            Dim char7 As Char = ControlChars.NullChar
            Dim char8 As Char = ControlChars.NullChar
            Dim char9 As Char = ControlChars.NullChar
            Dim char10 As Char = ControlChars.NullChar
            Dim char11 As Char = ControlChars.NullChar
            Dim char12 As Char = ControlChars.NullChar
            Dim char13 As Char = ControlChars.NullChar
            Dim char14 As Char = ControlChars.NullChar
            Dim char15 As Char = ControlChars.NullChar
            Dim char16 As Char = ControlChars.NullChar
            Dim char17 As Char = ControlChars.NullChar
            Dim char18 As Char = ControlChars.NullChar
            Dim char19 As Char = ControlChars.NullChar
            Dim char20 As Char = ControlChars.NullChar
            Dim char21 As Char = ControlChars.NullChar
            Dim char22 As Char = ControlChars.NullChar
            Dim char23 As Char = ControlChars.NullChar
            Dim char24 As Char = ControlChars.NullChar
            Dim char25 As Char = ControlChars.NullChar
            Dim char26 As Char = ControlChars.NullChar
            Dim char27 As Char = ControlChars.NullChar
            Dim char28 As Char = ControlChars.NullChar
            Dim char29 As Char = ControlChars.NullChar
            Dim char30 As Char = ControlChars.NullChar
            Dim char31 As Char = ControlChars.NullChar
            Dim char32 As Char = ControlChars.NullChar
            Dim char33 As Char = ControlChars.NullChar
            Dim char34 As Char = ControlChars.NullChar
            Dim char35 As Char = ControlChars.NullChar
            Dim char36 As Char = ControlChars.NullChar
            Dim char37 As Char = ControlChars.NullChar
            Dim char38 As Char = ControlChars.NullChar
            Dim char39 As Char = ControlChars.NullChar
            Dim char40 As Char = ControlChars.NullChar
            Dim char41 As Char = ControlChars.NullChar
            Dim char42 As Char = ControlChars.NullChar
            Dim char43 As Char = ControlChars.NullChar
            Dim char44 As Char = ControlChars.NullChar
            Dim char45 As Char = ControlChars.NullChar
            Dim char46 As Char = ControlChars.NullChar
            Dim char47 As Char = ControlChars.NullChar
            Dim char48 As Char = ControlChars.NullChar
            Dim char49 As Char = ControlChars.NullChar
            Dim char50 As Char = ControlChars.NullChar
            Dim char51 As Char = ControlChars.NullChar
            Dim char52 As Char = ControlChars.NullChar
            Dim char53 As Char = ControlChars.NullChar
            Dim char54 As Char = ControlChars.NullChar
            Dim char55 As Char = ControlChars.NullChar
            Dim char56 As Char = ControlChars.NullChar
            Dim dictionary1 As Dictionary(Of [String], Int32) = DirectCast(Nothing, Dictionary(Of [String], Int32))
            Dim string1 As String = ""
            i1 = 0
            While (i1 < str.Length)
                string2 = str.Substring(i1, 1)
                If (InlineAssignHelper(string3, string2)) IsNot Nothing Then
                    If dictionary1 Is Nothing Then
                        dictionary1 = New Dictionary(Of [String], Int32)(86)
                        dictionary1.Add("a", 0)
                        dictionary1.Add("?", 1)
                        dictionary1.Add("?", 2)
                        dictionary1.Add("¶", 3)
                        dictionary1.Add("?", 4)
                        dictionary1.Add("?", 5)
                        dictionary1.Add("?", 6)
                        dictionary1.Add("?", 7)
                        dictionary1.Add("?", 8)
                        dictionary1.Add("?", 9)
                        dictionary1.Add("?", 10)
                        dictionary1.Add("?", 11)
                        dictionary1.Add("©", 12)
                        dictionary1.Add("?", 13)
                        dictionary1.Add("?", 14)
                        dictionary1.Add("?", 15)
                        dictionary1.Add("?", 16)
                        dictionary1.Add("?", 17)
                        dictionary1.Add("e", 18)
                        dictionary1.Add("?", 19)
                        dictionary1.Add("?", 20)
                        dictionary1.Add("?", 21)
                        dictionary1.Add("?", 22)
                        dictionary1.Add("?", 23)
                        dictionary1.Add("?", 24)
                        dictionary1.Add("?", 25)
                        dictionary1.Add("?", 26)
                        dictionary1.Add("?", 27)
                        dictionary1.Add("?", 28)
                        dictionary1.Add("?", 29)
                        dictionary1.Add("o", 30)
                        dictionary1.Add("?", 31)
                        dictionary1.Add("?", 32)
                        dictionary1.Add("?", 33)
                        dictionary1.Add("?", 34)
                        dictionary1.Add("?", 35)
                        dictionary1.Add("?", 36)
                        dictionary1.Add("?", 37)
                        dictionary1.Add("?", 38)
                        dictionary1.Add("?", 39)
                        dictionary1.Add("?", 40)
                        dictionary1.Add("?", 41)
                        dictionary1.Add("?", 42)
                        dictionary1.Add("?", 43)
                        dictionary1.Add("?", 44)
                        dictionary1.Add("?", 45)
                        dictionary1.Add("?", 46)
                        dictionary1.Add("?", 47)
                        dictionary1.Add("i", 48)
                        dictionary1.Add("?", 49)
                        dictionary1.Add("?", 50)
                        dictionary1.Add("?", 51)
                        dictionary1.Add("?", 52)
                        dictionary1.Add("?", 53)
                        dictionary1.Add("u", 54)
                        dictionary1.Add("?", 55)
                        dictionary1.Add("?", 56)
                        dictionary1.Add("?", 57)
                        dictionary1.Add("?", 58)
                        dictionary1.Add("?", 59)
                        dictionary1.Add("?", 60)
                        dictionary1.Add("?", 61)
                        dictionary1.Add("?", 62)
                        dictionary1.Add("?", 63)
                        dictionary1.Add("?", 64)
                        dictionary1.Add("?", 65)
                        dictionary1.Add("y", 66)
                        dictionary1.Add("?", 67)
                        dictionary1.Add("?", 68)
                        dictionary1.Add("?", 69)
                        dictionary1.Add("?", 70)
                        dictionary1.Add("?", 71)
                        dictionary1.Add("®", 72)
                        dictionary1.Add("A", 73)
                        dictionary1.Add("?", 74)
                        dictionary1.Add("?", 75)
                        dictionary1.Add("E", 76)
                        dictionary1.Add("?", 77)
                        dictionary1.Add("O", 78)
                        dictionary1.Add("?", 79)
                        dictionary1.Add("?", 80)
                        dictionary1.Add("I", 81)
                        dictionary1.Add("U", 82)
                        dictionary1.Add("¦", 83)
                        dictionary1.Add("Y", 84)
                        dictionary1.Add("§", 85)
                    End If
                    If dictionary1.TryGetValue(string3, i2) Then
                        Select Case i2
                            Case 0

                                If True Then
                                    char1 = "a"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 1

                                If True Then
                                    char2 = "?"c
                                    string2 = char2.ToString()
                                    Exit Select
                                End If
                            Case 2

                                If True Then
                                    char3 = "?"c
                                    string2 = char3.ToString()
                                    Exit Select
                                End If
                            Case 3

                                If True Then
                                    char4 = "?"c
                                    string2 = char4.ToString()
                                    Exit Select
                                End If
                            Case 4

                                If True Then
                                    char5 = "?"c
                                    string2 = char5.ToString()
                                    Exit Select
                                End If
                            Case 5

                                If True Then
                                    char6 = "?"c
                                    string2 = char6.ToString()
                                    Exit Select
                                End If
                            Case 6

                                If True Then
                                    char7 = "?"c
                                    string2 = char7.ToString()
                                    Exit Select
                                End If
                            Case 7

                                If True Then
                                    char8 = "?"c
                                    string2 = char8.ToString()
                                    Exit Select
                                End If
                            Case 8

                                If True Then
                                    char9 = "?"c
                                    string2 = char9.ToString()
                                    Exit Select
                                End If
                            Case 9

                                If True Then
                                    char10 = "?"c
                                    string2 = char10.ToString()
                                    Exit Select
                                End If
                            Case 10

                                If True Then
                                    char11 = "?"c
                                    string2 = char11.ToString()
                                    Exit Select
                                End If
                            Case 11

                                If True Then
                                    char12 = "?"c
                                    string2 = char12.ToString()
                                    Exit Select
                                End If
                            Case 12

                                If True Then
                                    char13 = "?"c
                                    string2 = char13.ToString()
                                    Exit Select
                                End If
                            Case 13

                                If True Then
                                    char14 = "?"c
                                    string2 = char14.ToString()
                                    Exit Select
                                End If
                            Case 14

                                If True Then
                                    char15 = "?"c
                                    string2 = char15.ToString()
                                    Exit Select
                                End If
                            Case 15

                                If True Then
                                    char16 = "?"c
                                    string2 = char16.ToString()
                                    Exit Select
                                End If
                            Case 16

                                If True Then
                                    char17 = "?"c
                                    string2 = char17.ToString()
                                    Exit Select
                                End If
                            Case 17

                                If True Then
                                    char18 = "?"c
                                    string2 = char18.ToString()
                                    Exit Select
                                End If
                            Case 18

                                If True Then
                                    char19 = "e"c
                                    string2 = char19.ToString()
                                    Exit Select
                                End If
                            Case 19

                                If True Then
                                    char20 = "?"c
                                    string2 = char20.ToString()
                                    Exit Select
                                End If
                            Case 20

                                If True Then
                                    char21 = "?"c
                                    string2 = char21.ToString()
                                    Exit Select
                                End If
                            Case 21

                                If True Then
                                    char22 = "?"c
                                    string2 = char22.ToString()
                                    Exit Select
                                End If
                            Case 22

                                If True Then
                                    char23 = "?"c
                                    string2 = char23.ToString()
                                    Exit Select
                                End If
                            Case 23

                                If True Then
                                    char24 = "?"c
                                    string2 = char24.ToString()
                                    Exit Select
                                End If
                            Case 24

                                If True Then
                                    char25 = "?"c
                                    string2 = char25.ToString()
                                    Exit Select
                                End If
                            Case 25

                                If True Then
                                    char26 = "?"c
                                    string2 = char26.ToString()
                                    Exit Select
                                End If
                            Case 26

                                If True Then
                                    char27 = "?"c
                                    string2 = char27.ToString()
                                    Exit Select
                                End If
                            Case 27

                                If True Then
                                    char28 = "?"c
                                    string2 = char28.ToString()
                                    Exit Select
                                End If
                            Case 28

                                If True Then
                                    char29 = "?"c
                                    string2 = char29.ToString()
                                    Exit Select
                                End If
                            Case 29

                                If True Then
                                    char30 = "?"c
                                    string2 = char30.ToString()
                                    Exit Select
                                End If
                            Case 30

                                If True Then
                                    char31 = "o"c
                                    string2 = char31.ToString()
                                    Exit Select
                                End If
                            Case 31

                                If True Then
                                    char32 = "?"c
                                    string2 = char32.ToString()
                                    Exit Select
                                End If
                            Case 32

                                If True Then
                                    char33 = "?"c
                                    string2 = char33.ToString()
                                    Exit Select
                                End If
                            Case 33

                                If True Then
                                    char34 = "?"c
                                    string2 = char34.ToString()
                                    Exit Select
                                End If
                            Case 34

                                If True Then
                                    char35 = "?"c
                                    string2 = char35.ToString()
                                    Exit Select
                                End If
                            Case 35

                                If True Then
                                    char36 = "?"c
                                    string2 = char36.ToString()
                                    Exit Select
                                End If
                            Case 36

                                If True Then
                                    char37 = "?"c
                                    string2 = char37.ToString()
                                    Exit Select
                                End If
                            Case 37

                                If True Then
                                    char38 = "?"c
                                    string2 = char38.ToString()
                                    Exit Select
                                End If
                            Case 38

                                If True Then
                                    char39 = "?"c
                                    string2 = char39.ToString()
                                    Exit Select
                                End If
                            Case 39

                                If True Then
                                    char40 = "?"c
                                    string2 = char40.ToString()
                                    Exit Select
                                End If
                            Case 40

                                If True Then
                                    char41 = "?"c
                                    string2 = char41.ToString()
                                    Exit Select
                                End If
                            Case 41

                                If True Then
                                    char42 = "?"c
                                    string2 = char42.ToString()
                                    Exit Select
                                End If
                            Case 42

                                If True Then
                                    char43 = "?"c
                                    string2 = char43.ToString()
                                    Exit Select
                                End If
                            Case 43

                                If True Then
                                    char44 = "?"c
                                    string2 = char44.ToString()
                                    Exit Select
                                End If
                            Case 44

                                If True Then
                                    char45 = "?"c
                                    string2 = char45.ToString()
                                    Exit Select
                                End If
                            Case 45

                                If True Then
                                    char46 = "?"c
                                    string2 = char46.ToString()
                                    Exit Select
                                End If
                            Case 46

                                If True Then
                                    char47 = "?"c
                                    string2 = char47.ToString()
                                    Exit Select
                                End If
                            Case 47

                                If True Then
                                    char48 = "?"c
                                    string2 = char48.ToString()
                                    Exit Select
                                End If
                            Case 48

                                If True Then
                                    char49 = "i"c
                                    string2 = char49.ToString()
                                    Exit Select
                                End If
                            Case 49

                                If True Then
                                    char50 = "?"c
                                    string2 = char50.ToString()
                                    Exit Select
                                End If
                            Case 50

                                If True Then
                                    char51 = "?"c
                                    string2 = char51.ToString()
                                    Exit Select
                                End If
                            Case 51

                                If True Then
                                    char52 = "?"c
                                    string2 = char52.ToString()
                                    Exit Select
                                End If
                            Case 52

                                If True Then
                                    char53 = "?"c
                                    string2 = char53.ToString()
                                    Exit Select
                                End If
                            Case 53

                                If True Then
                                    char54 = "?"c
                                    string2 = char54.ToString()
                                    Exit Select
                                End If
                            Case 54

                                If True Then
                                    char55 = "u"c
                                    string2 = char55.ToString()
                                    Exit Select
                                End If
                            Case 55

                                If True Then
                                    char56 = "?"c
                                    string2 = char56.ToString()
                                    Exit Select
                                End If
                            Case 56

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 57

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 58

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 59

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 60

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 61

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 62

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 63

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 64

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 65

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 66

                                If True Then
                                    char1 = "y"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 67

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 68

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 69

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 70

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 71

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 72

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 73

                                If True Then
                                    char1 = "A"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 74

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 75

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 76

                                If True Then
                                    char1 = "E"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 77

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 78

                                If True Then
                                    char1 = "O"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 79

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 80

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 81

                                If True Then
                                    char1 = "I"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 82

                                If True Then
                                    char1 = "U"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 83

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 84

                                If True Then
                                    char1 = "Y"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                            Case 85

                                If True Then
                                    char1 = "?"c
                                    string2 = char1.ToString()
                                    Exit Select
                                End If
                        End Select
                    End If
                End If
                string1 = (string1 & string2)
                i1 += 1
            End While
            Return string1
        End Function

        Private Shared Function InlineAssignHelper(Of T)(ByRef target As t, ByVal value As t) As t
            target = value
            Return value
        End Function

    End Class

End Namespace