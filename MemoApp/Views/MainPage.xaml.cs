﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace MemoApp.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public ViewModels.MainPageViewModel ViewModel { get; private set; } = new ViewModels.MainPageViewModel();

		public MainPage()
        {
            this.InitializeComponent();
			ApplicationView.PreferredLaunchViewSize = new Size(500, 600);
			ViewModel.Initialize(this);
			this.DataContext = ViewModel;
		}

		public void MoveToRegisterTaskPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RegisterTaskPage));
		}

	}
}
