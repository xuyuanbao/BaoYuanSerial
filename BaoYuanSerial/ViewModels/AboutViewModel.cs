using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BaoYuanSerial.ViewModels
{
    public class AboutViewModel:ViewModelBase
    {

        public AboutViewModel()
        {
            
        }

        
        private string _Title = "BaoYuanSerial";
        public string Title {
            get => _Title;
            set
            {
                _Title = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Version = "Version:1.0.1";
        public string Version
        {
            get => _Version;
            set
            {
                _Version = value;
                this.RaisePropertyChanged();
            }
        }
        private string _Blog = "Email:xuyuanbaoxyb@163.com\r\nWeChat/Phone:18694923164\r\nQQ:416315797\r\n\r\nBlog: https://blog.csdn.net/lvyiwuhen\r\nGitHub: https://github.com/xuyuanbao/BaoYuanSerial\r\n";
        public string Blog
        {
            get => _Blog;
            set
            {
                _Blog = value;
                this.RaisePropertyChanged();
            }
        }
        private string _Email = "Email:xuyuanbaoxyb@163.com";
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                this.RaisePropertyChanged();
            }
        }
        private string _Provide = "Provide fully customized software solutions.";
        public string Provide
        {
            get => _Provide;
            set
            {
                _Provide = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
