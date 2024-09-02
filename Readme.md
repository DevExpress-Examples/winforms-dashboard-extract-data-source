<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/197570851/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T828595)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Dashboard for WinForms - Dashboard with Extract Data Source

This example shows how to create the Extract data source, replace existing dashboard data sources with Extract data sources and update the Extract data file.

The [DashboardViewer](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer) loads a dashboard. The dashboard is initially bound to the Microsoft SQL Server database file (.mdf) with the [DashboardSqlDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource). 

Click the _Create Extract Data Source_ button to create the [DashboardExtractDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource) based on the original data source, add the newly created data source to the dashboard, iterate over dashboard items to change the data source settings, and finally save the modified dashboard and the Extract data file. 

Click the _Update Extract Data Source_ button to load current data to the Extract data file bound to the dashboard.

Click the _Async Update Extract Data Sources_ button to asynchronously update all extract data sources bound to the dashboard.

![screenshot](/images/screenshot.png)

## Files to Review
* [Form1.cs](./CS/ExtractDataSourceExample/Form1.cs) (VB: [Form1.vb](./VB/ExtractDataSourceExample/Form1.vb))

## API

* [Dashboard.DataSources](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.Dashboard.DataSources) property
* [DashboardExtractDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource) class
* [DashboardExtractDataSource.ExtractSourceOptions](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource.ExtractSourceOptions) property
* [DashboardExtractDataSource.UpdateExtractFile](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource.UpdateExtractFile) method
* [DashboardViewer.UpdateExtractDataSourcesAsync](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExtractDataSource.UpdateExtractDataSourcesAsync)
* [DashboardViewer.CustomizeDashboardTitle](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.CustomizeDashboardTitle) event

## Documentation

- [Extract Data Source](https://docs.devexpress.com/Dashboard/115900)
- [Provide Data](https://docs.devexpress.com/Dashboard/16914)

## More Examples

* [WPF Dashboard - How to Update the Extract Data File](https://github.com/DevExpress-Examples/wpf-dashboard-how-to-update-extract-data-source-file)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-dashboard-extract-data-source&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-dashboard-extract-data-source&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
