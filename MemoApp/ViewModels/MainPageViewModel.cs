using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MemoApp.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		public Views.MainPage View { get; private set; } = null;

		public void Initialize(Views.MainPage mainPage)
		{
			View = mainPage;
		}

		public string TaskChoiceLabel { get; } = "タスク選択";
		public string TaskChoicePlaceholder { get; } = "タスクを選択してください";
		public string StartLabel { get; } = "START";
		public string PauseLabel { get; } = "PAUSE";
		public string FinishLabel { get; } = "FINISH";
		public string MemoLabel { get; } = "メモを残す";
		public string RegisterButtonLabel { get; } = "登録";
		public ObservableCollection<TaskDisplayInfo> TaskListData { get; set; } = new ObservableCollection<TaskDisplayInfo>(GetTaskDisplayInfo());
		public List<Memo> MemoListData { get; set; } = default;
		public TaskDisplayInfo TaskDisplayInfo { get; set; }
		public ObservableCollection<SmallTaskInfo> SmallTaskListData { get; set; } = new ObservableCollection<SmallTaskInfo>();
		private ThreadPoolTimer NotifyRestTime;



		public event PropertyChangedEventHandler PropertyChanged;

		public string _selectedEachTaskId;
		public string SelectedEachTaskId
		{
			get { return this._selectedEachTaskId; }
			set
			{
				this._selectedEachTaskId = value;
				MemoContent = MemoModel.GetSpecificEachTaskMemo(SelectedEachTaskId) ?? "";
				SetSmallTaskInfo();
			}
		}

		public string _memoContent;
		public string MemoContent
		{
			get { return this._memoContent; }
			set
			{
				if (this._memoContent == value) { return; }

				this._memoContent = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MemoContent)));
			}
		}

		public string Msg { get; set; }


		public void Page_Loaded(object sender, RoutedEventArgs e)
		{
		}


		public void MemoRegister(object sender, RoutedEventArgs e)
		{
			if (IsEachTaskIdEmpty())
			{
				NotifySystemMessage("タスクを選択してください");
				return;
			}

			if (!string.IsNullOrWhiteSpace(MemoContent))
			{
				MemoModel.Register(SelectedEachTaskId, MemoContent);
			}
		}

		public void TaskStart(object sender, RoutedEventArgs e)
		{
			if (IsEachTaskIdEmpty())
			{
				NotifySystemMessage("タスクを選択してください");
				return;
			}

			Msg = TimeInfoModel.RegisterStart(SelectedEachTaskId);
			if (Msg != null)
			{
				NotifySystemMessage(Msg);
			}

			RefreshTaskListData();

			NotifyRestTime = ThreadPoolTimer.CreatePeriodicTimer((source) =>
			{
				Notification notification = new Notification();
				notification.NotifyRestTime();
			}, TimeSpan.FromMinutes(float.Parse(ConfigModel.GetSpecificConfigValue(ConfigModel.ConfigType.NotificationSpanMinute))));


		}

		public void TaskPause(object sender, RoutedEventArgs e)
		{
			if (IsEachTaskIdEmpty())
			{
				NotifySystemMessage("タスクを選択してください");
				return;
			}

			Msg = TimeInfoModel.RegisterPause(SelectedEachTaskId);
			if (Msg != null)
			{
				NotifySystemMessage(Msg);
			}

			NotifyRestTime.Cancel();
		}

		public void TaskStop(object sender, RoutedEventArgs e)
		{
			if (IsEachTaskIdEmpty())
			{
				NotifySystemMessage("タスクを選択してください");
				return;
			}

			Msg = TimeInfoModel.RegisterStop(SelectedEachTaskId);
			if (Msg != null)
			{
				NotifySystemMessage(Msg);
			}

			RefreshTaskListData();

			NotifyRestTime.Cancel();
		}

		public async void NotifySystemMessage(string msg)
		{
			ContentDialog timeInfoConditionMsg = new ContentDialog
			{
				Title = "MemoAppからのお知らせ",
				Content = msg,
				CloseButtonText = "OK"
			};

			ContentDialogResult result = await timeInfoConditionMsg.ShowAsync();
		}



		public bool IsEachTaskIdEmpty() => SelectedEachTaskId == null ? true : false;


		public static List<TaskDisplayInfo> GetTaskDisplayInfo()
		{
			List<EachTask> specificDateEachTaskList = EachTaskModel.GetSpecificDateEachTasks(DateTimeOffset.Now.LocalDateTime).ToList();

			List<TaskDisplayInfo> result = new List<TaskDisplayInfo>();
			specificDateEachTaskList.ForEach(eachTask =>
			{
				TaskDisplayInfo info = new TaskDisplayInfo
				{
					EachTaskId = eachTask.EachTaskId,
					Content = (eachTask.CompleteFlag ? "✔" : "").PadRight(3) + eachTask.Content
				};
				result.Add(info);
			});

			return result;
		}

		public void RefreshTaskListData()
		{
			TaskListData = new ObservableCollection<TaskDisplayInfo>(GetTaskDisplayInfo());
		}

		public void SetSmallTaskInfo()
		{
			List<EachTask> smallEachTaskList = EachTaskModel.GetSpecificTaskSmallTasks(SelectedEachTaskId);

			SmallTaskListData.Clear();

			smallEachTaskList.ForEach(eachSmallTask =>
			{
				SmallTaskInfo smallTaskInfo = new SmallTaskInfo()
				{
					EachTaskId = eachSmallTask.EachTaskId,
					ParentEachTaskId = SelectedEachTaskId,
					Content = eachSmallTask.Content,
					IsComplete = eachSmallTask.CompleteFlag
				};

				SmallTaskListData.Add(smallTaskInfo);
			});
		}
	}

	public class TaskDisplayInfo
	{
		public string EachTaskId { get; set; } = default;
		public string Content { get; set; } = default;
	}

	public class SmallTaskInfo
	{
		public string EachTaskId { get; set; } = default;
		public string ParentEachTaskId { get; set; } = default;
		public string Content { get; set; } = default;
		public bool IsComplete { get; set; }

		public async void ChangeSmallTaskCompleteFlag()
		{
			await TimeInfoModel.ChangeComplateFlag(EachTaskId, !IsComplete);
		}
	}
}
