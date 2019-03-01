using MemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MemoApp.ViewModels
{
    public class CommonViewModel　
    {
		public string MainPageButtonLabel = "[メ]";
		public string RegisterPageButtonLabel { get; set; } = "[タ]";
		public string LogPageButtonLabel { get; set; } = "[ロ]";
		public string ConfigPageButtonLabel { get; set; } = "[コ]";



	}
}
