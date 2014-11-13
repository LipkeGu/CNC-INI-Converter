Imports System.IO
Imports System.Text

Module main
	Dim INI As New INIDatei

	Structure Building
		Dim Adjacent As String
		Dim Armor As String
		Dim canStore As Boolean
		Dim CaptureAble As Boolean
		Dim Cost As String
		Dim CloakDetect As Boolean
		Dim CSellvalue As String
		Dim AppearsNotOnRadar As Boolean
		Dim ID As String
		Dim Name As String
		Dim Stength As String
		Dim Owner As String
		Dim PrimaryWeapon As String
		Dim DefenceBuilding As Boolean
		Dim Superweapon As String
		Dim Power As Integer
		Dim TechLevel As String
		Dim Image As String
		Dim Prereqs As String
		Dim Sight As String
		Dim UncloakRange As String
		Dim isWarFactory As Boolean
		Dim isConYard As Boolean
		Dim isBarracks As Boolean
		Dim isHeliPad As Boolean
		Dim isWall As Boolean
		Dim DeployTo As String
		Dim isSensorArray As Boolean
		Dim isRefinery As Boolean
		Dim isArtillary As Boolean
		Dim isPowerPlant As Boolean
		Dim isTibsilo As Boolean
		Dim isRadar As Boolean
		Dim isEMPulse As Boolean
		Dim StorageAmount As String
		Dim isBaseDefence As Boolean
		Dim StorageType As String
		Dim Insignificant As Boolean
		Dim isTransformer As Boolean
		Dim CivBuilding As Boolean
		Dim Buildable As Boolean
		Dim FreeActor As String
		Dim canRepair As Boolean
		Dim ProvideFreeUnit As Boolean
		Dim hasbib As Boolean
		Dim WasteFacility As Boolean
		Dim dim_x As Integer
		Dim dim_y As Integer


	End Structure

	Structure Infantry

	End Structure

	Structure Vehicle

	End Structure

	Structure AirCraft

	End Structure

	Structure TerainObject

	End Structure

	Dim Buildings As New List(Of Building)
	Dim Infantrys As New List(Of Infantry)
	Dim Vehicles As New List(Of Vehicle)
	Dim Aircrafts As New List(Of AirCraft)
	Dim TerainOpjects As New List(Of TerainObject)
	Dim Rules As String = My.Application.Info.DirectoryPath & "\rules.ini"
	Dim Art As String = My.Application.Info.DirectoryPath & "\art.ini"
	Dim CivilianBuildingsFile As String = "D:\civilian.yaml"
	Dim PlayerBuildingFile As String = "D:\Structures.yaml"

	Sub Main()
		Dim isplayerbuilding As Boolean = True

		Dim Section As String = "BuildingTypes"
		Dim line As String = ""

		INI.Pfad = Rules

		If File.Exists(Rules) Then


			For i As Integer = 0 To 255 Step 1
				Dim _tmp As String
				line = Replace(INI.WertLesen(Section, CStr(i)), ";", "#")
				If line IsNot Nothing Then
					If line.Length > 0 Then
						Dim b As New Building

						With b
							.ID = line
							.Name = INI.WertLesen(.ID, "Name")

							If .ID = "GASILO" Then
								.isTibsilo = True
							End If

							.isConYard = GetSpecitalProperty(INI.WertLesen(.ID, "ConstructionYard"))
							.isWall = GetSpecitalProperty(INI.WertLesen(.ID, "Wall", INI.WertLesen(.ID, "FirestormWall", INI.WertLesen(.ID, "LaserFencePost", INI.WertLesen(.ID, "LaserFence")))))	'
							.CaptureAble = GetSpecitalProperty(INI.WertLesen(.ID, "Captureable", "yes"))
							.isEMPulse = GetSpecitalProperty(INI.WertLesen(.ID, "EMPulseCannon"))
							.isHeliPad = GetSpecitalProperty(INI.WertLesen(.ID, "Helipad"))
							.isRefinery = GetSpecitalProperty(INI.WertLesen(.ID, "Refinery"))
							.isSensorArray = GetSpecitalProperty(INI.WertLesen(.ID, "SensorArray"))
							.isArtillary = GetSpecitalProperty(INI.WertLesen(.ID, "Artillary"))
							.isPowerPlant = GetSpecitalProperty(INI.WertLesen(.ID, "PowerPlant"))
							.isRadar = GetSpecitalProperty(INI.WertLesen(.ID, "Radar"))
							.hasbib = GetSpecitalProperty(INI.WertLesen(.ID, "Bib"))
							.PrimaryWeapon = INI.WertLesen(.ID, "Primary", Nothing)
							.TechLevel = INI.WertLesen(.ID, "TechLevel", "-1")
							.DeployTo = INI.WertLesen(.ID, "Deploysto")
							.WasteFacility = GetSpecitalProperty(INI.WertLesen(.ID, "WEEDER", Nothing))
							.Insignificant = GetSpecitalProperty(INI.WertLesen(.ID, "Insignificant"))
							.AppearsNotOnRadar = GetSpecitalProperty(INI.WertLesen(.ID, "RadarInvisible"))
							.CloakDetect = GetSpecitalProperty(INI.WertLesen(.ID, "Sensors"))
							.canStore = GetSpecitalProperty(INI.WertLesen(.ID, "Storage"))
							.StorageType = INI.WertLesen(.ID, "PipScale", "Tiberium")
							.DeployTo = INI.WertLesen(.ID, "UndeploysInto", Nothing)
							.canRepair = GetSpecitalProperty(INI.WertLesen(.ID, "UnitRepair"))
							.FreeActor = INI.WertLesen(.ID, "FreeUnit", Nothing)
							.Superweapon = INI.WertLesen(.ID, "SuperWeapon", Nothing)
							.PrimaryWeapon = INI.WertLesen(.ID, "Primary", Nothing)
							.isBaseDefence = GetSpecitalProperty(INI.WertLesen(.ID, "IsBaseDefense"))
							If .PrimaryWeapon Is Nothing Then .DefenceBuilding = False




							If .Superweapon Is Nothing Then .DefenceBuilding = False

							If .isConYard Then
								.Buildable = True
							End If

							If .DeployTo Is Nothing Then
								.isTransformer = False
								.Buildable = False
							Else
								.isTransformer = True
								.Buildable = True
								.CivBuilding = False
							End If

							_tmp = INI.WertLesen(.ID, "Factory", "no")

							If _tmp = "InfantryType" Then
								.isBarracks = True
								.isWarFactory = False
								.isConYard = False
								.isHeliPad = False
								.CivBuilding = False
							ElseIf _tmp = "WeaponsFactory" Then
								.isWarFactory = True
								.isBarracks = False
								.isConYard = False
								.isHeliPad = False
								.CivBuilding = False
							ElseIf _tmp = "AircraftType" Then
								.isHeliPad = True
								.isWarFactory = False
								.isBarracks = False
								.isConYard = False
								.CivBuilding = False
							ElseIf _tmp = "VehicleType" Or _tmp = "UnitType" Then
								.isWarFactory = True
								.isBarracks = False
								.isConYard = False
								.isHeliPad = False
								.CivBuilding = False
							End If

							If _tmp = "yes" Or _tmp = "InfantryType" Then
								.isBarracks = True
							Else
								.isBarracks = False
							End If

							.Stength = INI.WertLesen(.ID, "Strength", "100")

							.Owner = INI.WertLesen(.ID, "Owner", Nothing)

							If .Owner IsNot Nothing Then
								.Owner = Replace(Replace(.Owner, ",", ", "), "  ", " ")
							End If

							.Armor = INI.WertLesen(.ID, "Armor", "")

							If .isWall Then
								.Adjacent = "7"
							Else
								.Adjacent = INI.WertLesen(.ID, "Adjacent", "2")
							End If

							.Power = CInt(INI.WertLesen(.ID, "Power", "0"))
							.Sight = INI.WertLesen(.ID, "Sight", "2") & "c0"
							_tmp = INI.WertLesen(.ID, "PowerPlant", "no")

							If .Power > 2 Then
								_tmp = "yes"
							End If

							If _tmp = "yes" Then
								.isPowerPlant = True
							Else
								.isPowerPlant = False
							End If

							.Prereqs = INI.WertLesen(.ID, "Prerequisite", Nothing)

							If Not .Prereqs.Contains(" ") AndAlso .Prereqs.Contains(",") Then
								.Prereqs = Replace(.Prereqs, ",", ", ")
							End If
							.TechLevel = INI.WertLesen(.ID, "TechLevel")
							.Cost = INI.WertLesen(.ID, "Cost", "100")

							If .DeployTo Is Nothing Then
								.isTransformer = False
								.Buildable = True
							Else
								.isTransformer = True
								.Buildable = False
							End If

							_tmp = INI.WertLesen(.ID, "Image", Nothing)

							If _tmp <> .ID AndAlso _tmp.Length > 0 Then
								.Image = _tmp
							Else
								.Image = .ID
							End If

							If b.Image Is Nothing Then
								b.Image = b.ID
							End If

						End With

						Dim __xy() As String

						If b.Image <> b.ID AndAlso b.Image.Length > 0 Then
							If b.ID = "GAPLUG2" Then
								__xy = Getfootprint("GAPLUG")
							End If
						Else
							__xy = Getfootprint(b.ID)
						End If


						b.dim_x = CInt(__xy(0))
						b.dim_y = CInt(__xy(1))

						Buildings.Add(b)
					End If
				End If
			Next

			If File.Exists(PlayerBuildingFile) Then
				File.Delete(PlayerBuildingFile)
			End If

			If File.Exists(CivilianBuildingsFile) Then
				File.Delete(CivilianBuildingsFile)
			End If

			Dim sw As StreamWriter

			If (Buildings.Count > 0) Then
				Dim order As Integer = 1
				For Each entry As Building In Buildings
					Dim factory As Boolean = False
					Dim Unlocker As Boolean = False
					Dim UnlockedType As String = ""

					With entry
						If .Owner IsNot Nothing Then
							If .isArtillary Or .isBarracks Or .isConYard Or .isEMPulse Or .isHeliPad Or .isPowerPlant Or .isRadar Or .isRefinery _
							Or .isSensorArray Or .isTibsilo Or .isWall Or .isWarFactory Then
								.CivBuilding = False
								.Buildable = True

								If .isSensorArray Then
									.CivBuilding = False
									.Buildable = True
								End If

								If .isTransformer = True Then
									.CivBuilding = False
								End If

								If .isTransformer AndAlso .isConYard Then
									.CivBuilding = False
								End If
							End If

							If .TechLevel <> "-1" Then
								.Buildable = True
							End If
						Else
							If .TechLevel = "-1" Then
								.CivBuilding = True
							End If

							If .Buildable = True Then
								.CivBuilding = False
							End If

							If .isTransformer AndAlso .CivBuilding Then
								.CivBuilding = False
							End If
						End If

						If Not .CivBuilding AndAlso .Buildable Then
							Console.WriteLine("Converting [" & CStr(order) & " / " & Buildings.Count & "]: " & entry.Name & " ...")
							isplayerbuilding = True
							sw = New StreamWriter(PlayerBuildingFile, True)
						Else
							isplayerbuilding = False
							Console.WriteLine("Converting [" & CStr(order) & " / " & Buildings.Count & "]: [CIV] " & entry.Name & " ...")
							sw = New StreamWriter(CivilianBuildingsFile, True)
						End If
					End With

					sw.WriteLine(Environment.NewLine & entry.ID & ":")

					If entry.isWall Then
						sw.WriteLine(Chr(9) & "Inherits: ^Wall")
					Else
						sw.WriteLine(Chr(9) & "Inherits: ^Building")
					End If

					sw.WriteLine(Chr(9) & "Valued:")
					sw.WriteLine(Chr(9) & Chr(9) & "Cost: " & entry.Cost)

					sw.WriteLine(Chr(9) & "ToolTip:")
					sw.WriteLine(Chr(9) & Chr(9) & "Name: " & entry.Name)

					Unlocker = factory

					If entry.isWarFactory Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Assembly point for\nvehicle reinforcements")
						factory = True
						Unlocker = True
						UnlockedType = "factory"
					ElseIf entry.isBarracks Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Produces infantry")
						factory = True
						UnlockedType = "Infantry"

					ElseIf entry.isConYard Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Builds base structures.")
						factory = True

					ElseIf entry.WasteFacility Then
						entry.ProvideFreeUnit = True

					ElseIf entry.isRefinery Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Processes raw Tiberium\ninto useable resources")

						entry.canStore = entry.isRefinery

						If entry.canStore Then
							entry.StorageType = "Tiberium"

							If entry.StorageAmount Is Nothing Then
								entry.StorageAmount = "1500"
							End If
						End If

					ElseIf entry.isPowerPlant Or entry.Power > 2 Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Provides power for other structures")
						entry.isPowerPlant = True
						Unlocker = True
						UnlockedType = "anypower"

					ElseIf entry.isHeliPad Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Produces, rearms and\nrepairs helicopters")
						factory = True
						UnlockedType = "Air"
						entry.canRepair = True
					ElseIf entry.isRadar Then
						sw.WriteLine(Chr(9) & Chr(9) & "Description: Provides radar screen")
						Unlocker = True
						UnlockedType = "radar"

					Else
						'	sw.WriteLine(Chr(9) & Chr(9) & "Description:")
						factory = False
					End If

					If Not Unlocker Then
						Unlocker = factory
					End If

					sw.WriteLine(Chr(9) & "Buildable:")

					If entry.isBaseDefence Then
						entry.DefenceBuilding = True
					End If

					If entry.isWall Then
						entry.DefenceBuilding = True
					End If

					If entry.DefenceBuilding = False Then
						sw.WriteLine(Chr(9) & Chr(9) & "Queue: Building")
					Else
						sw.WriteLine(Chr(9) & Chr(9) & "Queue: Defense")
					End If

					If entry.Prereqs IsNot Nothing Then
						If entry.Prereqs.Length > 0 Then
							If entry.Prereqs.ToLower = "power" Then entry.Prereqs = "anypower"
							sw.WriteLine(Chr(9) & Chr(9) & "Prerequisites: " & entry.Prereqs.ToLower)
						End If
					End If

					sw.WriteLine(Chr(9) & Chr(9) & "BuildPaletteOrder: " & CStr(order * 10))
					sw.WriteLine(Chr(9) & "Building:")

					If entry.Adjacent <> "2" Then
						sw.WriteLine(Chr(9) & Chr(9) & "Adjacent: " & entry.Adjacent)
					End If

					' <Phroh|orca> x is no passage, _ is just art that doesnt show up on the placement overlay, = is art that is shown on the placement overlay

					sw.WriteLine(Chr(9) & Chr(9) & "Footprint: " & DrawFootprintMap(entry.dim_x, entry.dim_y))
					sw.WriteLine(Chr(9) & Chr(9) & "Dimensions: " & entry.dim_x & ", " & entry.dim_y)	' Read from Art.ini :)

					If entry.Power <> 0 Then
						sw.WriteLine(Chr(9) & "Power: ")
						sw.WriteLine(Chr(9) & Chr(9) & "Amount: " & CStr(entry.Power))
					End If

					If entry.Armor.Length > 0 Then
						sw.WriteLine(Chr(9) & "Armor:")
						sw.WriteLine(Chr(9) & Chr(9) & "Type: " & entry.Armor)
					End If

					sw.WriteLine(Chr(9) & "Health:")
					sw.WriteLine(Chr(9) & Chr(9) & "HP: " & entry.Stength)

					sw.WriteLine(Chr(9) & "RevealsShroud:")
					If entry.Sight = "0" Then entry.Sight = "2"

					sw.WriteLine(Chr(9) & Chr(9) & "Range: " & entry.Sight)

					If factory Then
						sw.WriteLine(Chr(9) & "ProductionBar:")
						sw.WriteLine(Chr(9) & "PrimaryBuilding:")

						If entry.isWarFactory Then
							sw.WriteLine(Chr(9) & "RallyPoint:")
							sw.WriteLine(Chr(9) & "Production:")
							sw.WriteLine(Chr(9) & Chr(9) & "Produces: Vehicle")
						End If

						If entry.isBarracks Then
							sw.WriteLine(Chr(9) & "RallyPoint:")
							sw.WriteLine(Chr(9) & "Production:")
							sw.WriteLine(Chr(9) & Chr(9) & "Produces: Infantry")
						End If

						If entry.isHeliPad Then
							sw.WriteLine(Chr(9) & "BelowUnits:")
							sw.WriteLine(Chr(9) & "Production:")
							sw.WriteLine(Chr(9) & Chr(9) & "Produces: Air")
						End If

						If entry.isConYard Then
							sw.WriteLine(Chr(9) & "BaseBuilding:")

							sw.WriteLine(Chr(9) & "Production:")
							sw.WriteLine(Chr(9) & Chr(9) & "Produces: Building, Defense")

							sw.WriteLine(Chr(9) & "ProductionBar@Building:")
							sw.WriteLine(Chr(9) & Chr(9) & "ProductionType: Building")

							sw.WriteLine(Chr(9) & "ProductionBar@Defence:")
							sw.WriteLine(Chr(9) & Chr(9) & "ProductionType: Defense")
						End If
					End If

					If Not entry.CaptureAble Then
						sw.WriteLine(Chr(9) & "-Captureable:")
					End If

					If entry.hasbib Then
						sw.WriteLine(Chr(9) & "# Bib:")
					End If

					If entry.AppearsNotOnRadar = True Then
						sw.WriteLine(Chr(9) & "-AppearsOnRadar:")
					End If

					If Unlocker Then
						sw.WriteLine(Chr(9) & "ProvidesCustomPrerequisite:")
						sw.WriteLine(Chr(9) & Chr(9) & "Prerequisite: " & UnlockedType)
					End If

					If entry.isWall Then
						Dim _WallType = "sandbags"

						If entry.ID = "GASAND" Then
							_WallType = "sandbags"
						Else
							_WallType = "wall"
						End If

						sw.WriteLine(Chr(9) & "LineBuild:")
						sw.WriteLine(Chr(9) & Chr(9) & "NodeTypes: " & _WallType)

						sw.WriteLine(Chr(9) & "LineBuildNode:")
						sw.WriteLine(Chr(9) & Chr(9) & "Types: " & _WallType)

						sw.WriteLine(Chr(9) & "RenderBuildingWall:")
						sw.WriteLine(Chr(9) & Chr(9) & "Type: " & _WallType)
					End If

					If entry.CloakDetect Then
						sw.WriteLine(Chr(9) & "DetectCloaked:")
						sw.WriteLine(Chr(9) & Chr(9) & "Range: " & entry.Sight)
					End If

					If entry.isRadar Then
						sw.WriteLine(Chr(9) & "RequiresPower:")
						sw.WriteLine(Chr(9) & "ProvidesRadar:")

						sw.WriteLine(Chr(9) & "Infiltratable:")
						sw.WriteLine(Chr(9) & Chr(9) & "Type: Exploration")
						sw.WriteLine(Chr(9) & "RenderDetectionCircle:")
					End If

					If entry.canRepair Then
						sw.WriteLine(Chr(9) & "RepairsUnits:")
						sw.WriteLine(Chr(9) & "RallyPoint:")
					End If

					If entry.isRefinery Then
						sw.WriteLine(Chr(9) & "TiberiumRefinery:")
						entry.ProvideFreeUnit = True
					End If

					If entry.canStore Then
						sw.WriteLine(Chr(9) & "StoresResources:")

						If entry.StorageType = "Tiberium" Then
							sw.WriteLine(Chr(9) & Chr(9) & "PipColor: Green")
						ElseIf entry.StorageType = "Weed" Then
							sw.WriteLine(Chr(9) & Chr(9) & "PipColor: Orange")
						Else
							sw.WriteLine(Chr(9) & Chr(9) & "PipColor: Green")
						End If

						sw.WriteLine(Chr(9) & Chr(9) & "PipCount: " & CStr((CInt(entry.StorageAmount) / 100)))
						sw.WriteLine(Chr(9) & Chr(9) & "Capacity: " & entry.StorageAmount)
					End If

					If entry.ProvideFreeUnit Then
						sw.WriteLine(Chr(9) & "FreeActor:")
						sw.WriteLine(Chr(9) & Chr(9) & "Actor: " & entry.FreeActor)
					End If

					order = order + 1
					sw.Close()
					sw.Dispose()
				Next

				WriteSequences()

				Buildings.Clear()
			End If
		Else
			Console.WriteLine("cant find Rules.ini and art.ini")
		End If
		Console.ReadLine()
	End Sub

	Function GetSpecitalProperty(ByVal Prop As String) As Boolean
		If Prop IsNot Nothing Then
			If Prop = "yes" Then
				Return True
			Else
				Return False
			End If
		Else
			Return False
		End If
	End Function

	Private Function Getfootprint(ByVal bldif As String) As String()
		Dim ISA As New INIDatei
		Dim _tmp As String = ""

		Dim _D() As String
		ISA.Pfad = Art

		_tmp = ISA.WertLesen(bldif, "Foundation", Nothing)

		If _tmp IsNot Nothing Then
			If _tmp.Length > 0 Then
				_D = _tmp.Split(CChar("x"))
				Return _D
			End If
		End If
	End Function

	Private Function DrawFootprintMap(x As Integer, y As Integer) As String
		Dim _x As String = CStr(x)
		Dim _y As String = CStr(y)

		If _x = "1" And _y = "1" Then
			Return "x"
		ElseIf _x = "2" And _y = "2" Then
			Return "xx xx"
		ElseIf _x = "1" And _y = "2" Then
			Return "xx"
		ElseIf _x = "2" And _y = "1" Then
			Return "xx"
		ElseIf _x = "3" And _y = "3" Then
			Return "xxx xxx xxx"
		ElseIf _x = "2" And _y = "3" Then
			Return "xxx xxx"
		ElseIf _x = "3" And _y = "2" Then
			Return "xxx xxx"
		ElseIf _x = "4" And _y = "3" Then
			Return "xxxx xxxx xxxx"
		ElseIf _x = "3" And _y = "4" Then
			Return "xxxx xxxx xxxx"
		ElseIf _x = "4" And _y = "2" Then
			Return "xxxx xxxx"
		ElseIf _x = "3" And _y = "5" Then
			Return "xxxxx xxxxx xxxxx"
		ElseIf _x = "5" And _y = "3" Then
			Return "xxx xxx xxx xxx xxx"
		ElseIf _x = "2" And _y = "5" Then
			Return "xxxxx xxxxx"
		ElseIf _x = "2" And _y = "6" Then
			Return "xxxxxx xxxxxx"
		ElseIf _x = "2" And _y = "6" Then
			Return "xxxxxx xxxxxx"
		ElseIf _x = "4" And _y = "4" Then
			Return "xxxx xxxx xxxx xxxx"
		ElseIf _x = "3" And _y = "1" Then
			Return "xxx"
		ElseIf _x = "1" And _y = "3" Then
			Return "xxx"
		ElseIf _x = "6" And _y = "4" Then
			Return "xxxxxx xxxxxx xxxxxx xxxxxx"
		Else
			Throw New Exception(_x & "-" & _y)
		End If
	End Function


	Private Sub WriteSequences()
		Dim xr2 As New StreamWriter(My.Application.Info.DirectoryPath & "\civi_squences.yaml")
		xr2.NewLine = Environment.NewLine

		For Each entry As Building In Buildings
			If entry.TechLevel = "-1" Then
				xr2.WriteLine(entry.ID.ToLower & ":")
				xr2.WriteLine(Chr(9) & "idle: " & entry.ID.ToLower)
				xr2.WriteLine(Chr(9) & Chr(9) & "start: 0")
				xr2.WriteLine(Chr(9) & Chr(9) & "ShadowStart: 2")
				xr2.WriteLine("")
			End If
		Next

		xr2.Close()
		xr2.Dispose()
	End Sub






End Module
