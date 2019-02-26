using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static MemoApp.Models.EachTaskModel;

namespace MemoApp.ViewModels
{
	public class RegisterTaskPageViewModel : INotifyPropertyChanged
	{
		public Views.RegisterTaskPage View { get; private set; } = null;
		public string TitleLabel { get;  } = "本日のタスク";
		public string RegisterButtonLabel { get;  } = "登録";
		
		public string TaskDataPlaceHolder { get;  } = "タスク入力欄\r\r「#」から始まる行は中タスク、「-」で始まる行は中タスクに属する小タスクとして認識されます。\r\r入力例）\r#カップラーメンを作る\r-ふたを開ける\r-お湯を注ぐ\r-３分待つ\r";
		public string CalenderHeader { get;  } = "Task of date";

		public event PropertyChangedEventHandler PropertyChanged;

		public DateTimeOffset _planDate = DateTimeOffset.Now.ToLocalTime();
		public DateTimeOffset PlanDate {
			get { return this._planDate; }
			set {
				if (this._planDate == value) { return; }
				this._planDate = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlanDate)));
				TaskData = GetTaskData(PlanDate);
			}
		}

		public string _taskData = GetTaskData(DateTimeOffset.Now.ToLocalTime());
		public string TaskData
		{
			get { return this._taskData; }
			set
			{
				if (this._taskData == value) { return; }
				this._taskData = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskData)));
			}
		}

		public void Initialize(Views.RegisterTaskPage registerTaskPage)
		{
			View = registerTaskPage;
		}

		async public void RegisterTaskData(object sender, RoutedEventArgs e)
		{
			// Changes ValidFlag value of All tasks for this plan date to false 
			// If there is a task data in TaskData, the ValidFlag value of the task will change to true after this below processing
			List<EachTask> specificDateClassificationData = GetSpecificDateEachTasks(PlanDate);
			foreach (EachTask classificationTask in specificDateClassificationData)
			{
				EachTaskModel.ChangeValidFlag(classificationTask.EachTaskId, false);
				List<EachTask> smallTaskList = GetSpecificTaskSmallTasks(classificationTask.EachTaskId);
				foreach (EachTask smallTask in smallTaskList)
				{
					EachTaskModel.ChangeValidFlag(smallTask.EachTaskId, false);
				}
			}


			String TaskDataToRegister = "\r" + TaskData;

			TaskDataToRegister = TaskDataToRegister.Replace("\r\r", "\r");

			string[] largeTaskStrings = TaskDataToRegister.Split("\r#");
			int LargetTaskRank = 1;

			foreach (string eachLargeTask in largeTaskStrings)
			{
				if (string.IsNullOrWhiteSpace(eachLargeTask))
				{
					continue;
				}

				List<string> taskStrings = eachLargeTask.Split("\r-").ToList();

				string classificationTask = taskStrings[0].TrimStart();

				EachTask newClassificationTask = new EachTask()
				{
					PlanDate = this.PlanDate,
					Content = classificationTask,
					RegisteredDate = DateTime.Now.ToLocalTime(),
					ParentEachTaskId = string.Empty,
					Rank = LargetTaskRank,
					ValidFlag = true
				};

				// Checks if the task has been already registered by checking EachTaskId
				string classificationId = EachTaskModel.GetEachTaskId(newClassificationTask);
				if (string.IsNullOrWhiteSpace(classificationId))
				{
					classificationId = EachTaskModel.RegisterTask(newClassificationTask);
				}
				else
				{
					await EachTaskModel.UpdateTaskRankAsync(classificationId, LargetTaskRank);
					EachTaskModel.ChangeValidFlag(classificationId, true);
				}

				int smallTaskRank = 1;
				string smallTaskId = string.Empty;

				foreach (string eachTaskString in taskStrings.GetRange(1, taskStrings.Count - 1))
				{
					EachTask newTask = new EachTask()
					{
						PlanDate = this.PlanDate,
						Content = eachTaskString.Substring(1).TrimStart(),
						RegisteredDate = DateTimeOffset.Now.ToLocalTime(),
						ParentEachTaskId = classificationId.ToString(),
						Rank = smallTaskRank,
						ValidFlag = true
					};

					smallTaskId = EachTaskModel.GetEachTaskId(newTask);
					if (string.IsNullOrWhiteSpace(smallTaskId)){
						await EachTaskModel.RegisterTaskAsync(newTask);
					}
					else
					{
						await EachTaskModel.UpdateTaskRankAsync(smallTaskId, smallTaskRank);
						EachTaskModel.ChangeValidFlag(smallTaskId, true);
					}

					smallTaskRank = smallTaskRank + 1;
				}

				LargetTaskRank = LargetTaskRank + 1;

			}
		}

		public static string GetTaskData(DateTimeOffset planDate)
		{
			string taskData = string.Empty;

			List<EachTask> specificDateClassificationData = GetSpecificDateEachTasks(planDate);

			foreach(EachTask classificationTask in specificDateClassificationData)
			{
				taskData = $"{taskData}# {classificationTask.Content}\r";

				List<EachTask> smallTaskList = GetSpecificTaskSmallTasks(classificationTask.EachTaskId);

				foreach(EachTask smallTask in smallTaskList)
				{
					taskData = $"{taskData}- {smallTask.Content}\r";
				}

				taskData = $"{taskData}\r";

			}

			return taskData;
		}
	}
}
