using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataReader_EFWheel.Entity;
using DataReader_EFWheel.Tool;

namespace DataReader_EFWheel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int id = 4;

               { 
            var company = SimpleFactory.CreatDataReaderHelper().Read<CompanyModel>(id);
            var user = GenericCache<DataReaderHelper>.GetCache().Read<UserModel>(id);



            var countCompany = SimpleFactory.CreatDataReaderHelper().INSERT<CompanyModel>(company.FirstOrDefault());
            var countUser = GenericCache<DataReaderHelper>.GetCache().INSERT<UserModel>(user.FirstOrDefault());

            //var tempCompany = company.FirstOrDefault();
            //tempCompany.Address = "修改111";
            //var tempUser = user.FirstOrDefault();
            //tempUser.Remark = "修改111";

            //countCompany = SimpleFactory.CreatDataReaderHelper().Update<CompanyModel>(tempCompany);
            //countUser = GenericCache<DataReaderHelper>.GetCache().Update<UserModel>(tempUser);

            //    countCompany =  SimpleFactory.CreatDataReaderHelper().Delete<CompanyModel>(id);
            //    countUser = GenericCache<DataReaderHelper>.GetCache().Delete<UserModel>(id);
            }
               {
                   //测试合并的标识
               }


        }


    }
}
