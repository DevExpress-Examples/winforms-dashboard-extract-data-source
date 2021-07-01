using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using System.Linq;
using System.Windows.Forms;

namespace ExtractDataSourceExample {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {
        public Form1() {
            InitializeComponent();
            dashboardViewer1.CustomizeDashboardTitle += DashboardViewer1_CustomizeDashboardTitle;
            dashboardViewer1.LoadDashboard("DashboardTest.xml");
        }

        delegate void SafeUpdate(string file);
        private void CreateExtractAndSave() {
            DataSourceCollection dsCollection = new DataSourceCollection();
            dsCollection.AddRange(dashboardViewer1.Dashboard.DataSources.Where(d => !(d is DashboardExtractDataSource)));
            foreach (var ds in dsCollection) {
                    DashboardExtractDataSource extractDataSource = new DashboardExtractDataSource();
                    extractDataSource.ExtractSourceOptions.DataSource = ds;

                    if (ds is DashboardSqlDataSource)
                        extractDataSource.ExtractSourceOptions.DataMember = ((DashboardSqlDataSource)(ds)).Queries[0].Name;

                    extractDataSource.FileName = "Extract_" + ds.Name + ".dat";
                    extractDataSource.UpdateExtractFile();
                    foreach (DataDashboardItem item in dashboardViewer1.Dashboard.Items)
                        if (item.DataSource == ds)
                            item.DataSource = extractDataSource;
            }
            dashboardViewer1.Dashboard.DataSources.RemoveRange(dsCollection);
            dashboardViewer1.Dashboard.SaveToXml("Dashboard_Extract.xml");
        }

        private void UpdateExtract() {
            dashboardViewer1.ReloadData();
            foreach (var ds in dashboardViewer1.Dashboard.DataSources.Where(d => d is DashboardExtractDataSource)) {
                ((DashboardExtractDataSource)ds).UpdateExtractFile();
            }
        }

        private void UpdateExtractAsync() {
            dashboardViewer1.UpdateExtractDataSourcesAsync((a, b) => { OnDataReady(a); }, (a, __) => { 
                MessageBox.Show($"File {a} updated "); 
            });
        }

        void OnDataReady(string fileName) {
            dashboardViewer1.Invoke(new SafeUpdate(UpdateViewer), new object[] { fileName });
        }
        void UpdateViewer(string fileName) {
            MessageBox.Show($"Data for the file {fileName} is ready ");
            dashboardViewer1.ReloadData();
        }

        private void DashboardViewer1_CustomizeDashboardTitle(object sender, CustomizeDashboardTitleEventArgs e) {
            DashboardToolbarItem itemUpdate = new DashboardToolbarItem((args) => UpdateExtract()) {
				Caption = "Update Extract Data Source",
			};
            e.Items.Add(itemUpdate);

            DashboardToolbarItem itemUpdateAsync = new DashboardToolbarItem((args) => UpdateExtractAsync()) {
				Caption = "Async Update Extract Data Sources",
			};
            e.Items.Add(itemUpdateAsync);

            DashboardToolbarItem itemSave = new DashboardToolbarItem((args) => CreateExtractAndSave()) {
                Caption = "Create Extract Data Source",
            };
            e.Items.Insert(0,itemSave);
        }
    }
}
