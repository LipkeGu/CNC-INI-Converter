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

	Sub Main()
		Dim Rules As String = "D:\rules.ini"
		Dim Art As String = "D:\art.ini"
		Dim CivilianBuildingsFile As String = "D:\civilian.yaml"
		Dim PlayerBuildingFile As String = "D:\Structures.yaml"

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

							_tmp = INI.WertLesen(.ID, "Image", "")

							If _tmp <> .ID AndAlso _tmp.Length > 0 Then
								.Image = _tmp
							End If
						End With

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

					sw.WriteLine(Chr(9) & Chr(9) & "Footprint: xxx xxx xxx # dummy")
					sw.WriteLine(Chr(9) & Chr(9) & "Dimensions: ")	' Read from Art.ini :)

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

				Buildings.Clear()
			End If
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

End Module
