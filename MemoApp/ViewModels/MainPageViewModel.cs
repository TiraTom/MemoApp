using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MemoApp.ViewModels
{
	public class MainPageViewModel : Helpers.Observable
	{
		public Views.MainPage View { get; private set; } = null;

		public void Initialize(Views.MainPage mainPage)
		{
			View = mainPage;
		}

		public string TaskChoiceLabel { get; set; } = "タスク選択";
		public string TaskChoicePlaceholder { get; set; } = "タスクを選択してください";
		public string StartLabel { get; set; } = "START";
		public string PauseLabel { get; set; } = "PAUSE";
		public string FinishLabel { get; set; } = "FINISH";
		public string MemoLabel { get; set; } = "メモを残す";
		public string HashButtonLabel { get; set; } = "ハッシュ";
		public string RegisterButtonLabel { get; set; } = "登録";
		public ObservableCollection<EachTask> TaskListData { get; set; } = new ObservableCollection<EachTask>(EachTaskModel.GetSpecificDateEachTasks(DateTimeOffset.Now.Date));
		public List<Memo> MemoListData { get; set; } = default;
		public string RegisterPageButtonLabel { get; set; } = "タスク\n登録";
		public string SelectedEachTaskId { get; set; }
		public string MemoContent { get; set; } = default;


		public void Page_Loaded(object sender, RoutedEventArgs e)
		{

		}

		public void MemoRegister(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(MemoContent))
			{
				MemoModel.Register(SelectedEachTaskId, MemoContent);
			}
		}

		public void TaskStart(object sender, RoutedEventArgs e)
		{
			TimeInfoModel.RegisterStart(SelectedEachTaskId);
		}

		public void TaskPause(object sender, RoutedEventArgs e)
		{
			TimeInfoModel.RegisterPause(SelectedEachTaskId);
		}

		public void TaskStop(object sender, RoutedEventArgs e)
		{
			TimeInfoModel.RegisterStop(SelectedEachTaskId);
		}
	}
}
