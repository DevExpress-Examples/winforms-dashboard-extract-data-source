Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System.IO
Imports System.Linq

Namespace ExtractDataSourceExample
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
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
			dashboardViewer1.ReloadData()
			For Each ds In dashboardViewer1.Dashboard.DataSources.Where(Function(d) TypeOf d Is DashboardExtractDataSource)
				CType(ds, DashboardExtractDataSource).UpdateExtractFile()
			Next ds
		End Sub

		Private Sub DashboardViewer1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
			Dim itemUpdate As New DashboardToolbarItem(Sub(args) UpdateExtract()) With {.Caption = "Update Extract Data Source"}
			e.Items.Add(itemUpdate)

			Dim itemSave As New DashboardToolbarItem(Sub(args) CreateExtractAndSave()) With {.Caption = "Create Extract Data Source"}
			e.Items.Insert(0,itemSave)
		End Sub
	End Class
End Namespace
