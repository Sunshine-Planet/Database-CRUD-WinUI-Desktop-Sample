using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public class LoginInfoViewModels :NotificationBase<LoginInfo>
    {
        private LoginInfo _loginInfo;
        public LoginInfoViewModels()
        {
            _loginInfo = new LoginInfo();
        }
        public string UserName
        {
            get { return This.UserName; }
            set { SetProperty(This.UserName, value, () => This.UserName = value); }
        }
        public string Password
        {
            get { return This.Password; }
            set { SetProperty(This.Password, value, () => This.Password = value); }
        }
    }
}
