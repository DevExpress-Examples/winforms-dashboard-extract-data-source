Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System.IO
Imports System.Linq

Namespace ExtractDataSourceExample
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			AddHandler dashboardViewer1.ConfigureDataConnection, AddressOf DashboardViewer1_ConfigureDataConnection
			AddHandler dashboardViewer1.CustomizeDashboardTitle, AddressOf DashboardViewer1_CustomizeDashboardTitle
			dashboardViewer1.LoadDashboard("DashboardTest.xml")
		End Sub

		Private Sub CreateExtractAndSave()
			Dim dsCollection As New DataSourceCollection()
			dsCollection.AddRange(dashboardViewer1.Dashboard.DataSources.Where(Function(d) Not (TypeOf d Is DashboardExtractDataSource)))
			For Each ds In dsCollection
					Dim extractDataSource As New DashboardExtractDataSource()
					extractDataSource.ExtractSourceOptions.DataSource = ds

					If TypeOf ds Is DashboardSqlDataSource Then
						extractDataSource.ExtractSourceOptions.DataMember = DirectCast(ds, DashboardSqlDataSource).Queries(0).Name
					End If
					' If the data source is an Entity Framework data source, specify the ExtractSourceOptions.DataMember.

					extractDataSource.FileName = "Extract_" & ds.Name & ".dat"
					extractDataSource.UpdateExtractFile()
					For Each item As DataDashboardItem In dashboardViewer1.Dashboard.Items
						If item.DataSource Is ds Then
							item.DataSource = extractDataSource
						End If
					Next item
			Next ds
			dashboardViewer1.Dashboard.DataSources.RemoveRange(dsCollection)
			dashboardViewer1.Dashboard.SaveToXml("Dashboard_Extract.xml")
		End Sub

		Private Sub UpdateExtract()
			Dim dashboard As New Dashboard()
			dashboard.LoadFromXDocument(dashboardViewer1.Dashboard.SaveToXDocument())
			For Each ds In dashboard.DataSources.Where(Function(d) TypeOf d Is DashboardExtractDataSource)
				Dim extractDsTempSource As DashboardExtractDataSource = TryCast(ds, DashboardExtractDataSource)
				extractDsTempSource.FileName &= "_updated"
				extractDsTempSource.UpdateExtractFile()
			Next ds
			dashboard.Dispose()
			dashboardViewer1.ReloadData()
		End Sub

		Private Sub DashboardViewer1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
			Dim itemUpdate As New DashboardToolbarItem(Sub(args) UpdateExtract()) With {.Caption = "Update Extract Data Source"}
			e.Items.Add(itemUpdate)

			Dim itemSave As New DashboardToolbarItem(Sub(args) CreateExtractAndSave()) With {.Caption = "Create Extract Data Source"}
			e.Items.Insert(0,itemSave)
		End Sub


		Private Sub DashboardViewer1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DashboardConfigureDataConnectionEventArgs)
            If TypeOf e.ConnectionParameters Is ExtractDataSourceConnectionParameters Then
                Dim connParams As ExtractDataSourceConnectionParameters = TryCast(e.ConnectionParameters, ExtractDataSourceConnectionParameters)
                Dim current As String = connParams.FileName
                Dim updated As String = connParams.FileName & "_updated"
                If File.Exists(updated) Then
                    File.Delete(current)
                    File.Copy(updated, current)
                    File.Delete(updated)
                End If
            End If
        End Sub
	End Class
End Namespace
