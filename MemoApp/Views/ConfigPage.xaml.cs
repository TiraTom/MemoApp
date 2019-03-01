using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MemoApp.Views
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class ConfigPage : Page
	{
		public ViewModels.ConfigPageViewModel ViewModel { get; private set; } = new ViewModels.ConfigPageViewModel();
		public ViewModels.CommonViewModel Common { get; private set; } = new ViewModels.CommonViewModel();

		public ConfigPage()
		{
			this.InitializeComponent();
		}

		public void MoveMainPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		public void MoveRegisterTaskPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RegisterTaskPage));
		}

		public void MoveLogPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(LogPage));
		}

		public void MoveConfigPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(ConfigPage));
		}

	}
}
