# WinForms - Dashboard with Extract Data Source

This example demonstrates how to create the Extract data source, replace existing dashboard data sources with Extract data sources and update the Extract data file.

The [DashboardViewer](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer) loads a dashboard. The dashboard is initially bound to the Microsoft SQL Server database file (.mdf) with the [DashboardSqlDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource). 

Click the _Create Extract Data Source_ button to create the [DashboardExtractDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource) based on the original data source, add the newly created data source to the dashboard, iterate over dashboard items to change the data source settings, and finally save the modified dashboard and the Extract data file. 

Click the _Update Extract Data Source_ button to load current data to the Extract data file bound to the dashboard.

Click the _Async Update Extract Data Sources_ button to asynchronously update all extract data sources bound to the dashboard.

![screenshot](/images/screenshot.png). 

API in this example:

* [Dashboard.DataSources](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.Dashboard.DataSources) property
* [DashboardExtractDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource) class
* [DashboardExtractDataSource.ExtractSourceOptions](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource.ExtractSourceOptions) property
* [DashboardExtractDataSource.UpdateExtractFile](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource.UpdateExtractFile) method
* [DashboardViewer.CustomizeDashboardTitle](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.CustomizeDashboardTitle) event

See also:

* [WPF Dashboard - How to Update the Extract Data File](https://github.com/DevExpress-Examples/wpf-dashboard-how-to-update-extract-data-source-file)


