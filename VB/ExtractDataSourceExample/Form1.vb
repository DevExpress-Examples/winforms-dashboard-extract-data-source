Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System.Linq
Imports System.Windows.Forms

Namespace ExtractDataSourceExample
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			AddHandler dashboardViewer1.CustomizeDashboardTitle, AddressOf DashboardViewer1_CustomizeDashboardTitle
			dashboardViewer1.LoadDashboard("DashboardTest.xml")
		End Sub

		Private Delegate Sub SafeUpdate(ByVal file As String)
		Private Sub CreateExtractAndSave()
			Dim dsCollection As New DataSourceCollection()
			dsCollection.AddRange(dashboardViewer1.Dashboard.DataSources.Where(Function(d) Not (TypeOf d Is DashboardExtractDataSource)))
			For Each ds In dsCollection
					Dim extractDataSource As New DashboardExtractDataSource()
					extractDataSource.ExtractSourceOptions.DataSource = ds

					If TypeOf ds Is DashboardSqlDataSource Then
						extractDataSource.ExtractSourceOptions.DataMember = DirectCast(ds, DashboardSqlDataSource).Queries(0).Name
					End If

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

		Private Sub UpdateExtractAsync()
			dashboardViewer1.UpdateExtractDataSourcesAsync(Sub(a, b)
				OnDataReady(a)
			End Sub, Sub(a, __)
				MessageBox.Show($"File {a} updated ")
End Sub)
		End Sub

		Private Sub OnDataReady(ByVal fileName As String)
			dashboardViewer1.Invoke(New SafeUpdate(AddressOf UpdateViewer), New Object() { fileName })
		End Sub
		Private Sub UpdateViewer(ByVal fileName As String)
			MessageBox.Show($"Data for the file {fileName} is ready ")
			dashboardViewer1.ReloadData()
		End Sub

		Private Sub DashboardViewer1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
			Dim itemUpdate As New DashboardToolbarItem(Sub(args) UpdateExtract()) With {.Caption = "Update Extract Data Source"}
			e.Items.Add(itemUpdate)

			Dim itemUpdateAsync As New DashboardToolbarItem(Sub(args) UpdateExtractAsync()) With {.Caption = "Async Update Extract Data Sources"}
			e.Items.Add(itemUpdateAsync)

			Dim itemSave As New DashboardToolbarItem(Sub(args) CreateExtractAndSave()) With {.Caption = "Create Extract Data Source"}
			e.Items.Insert(0,itemSave)
		End Sub
	End Class
End Namespace
