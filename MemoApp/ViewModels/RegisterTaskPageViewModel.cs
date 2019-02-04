using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static MemoApp.Models.EachTaskModel;

namespace MemoApp.ViewModels
{
	public class RegisterTaskPageViewModel : Helpers.Observable
	{
		public Views.RegisterTaskPage View { get; private set; } = null;
		public string TitleLabel { get; set; } = "本日のタスク";
		public string RegisterButtonLabel { get; set; } = "登録";
		public string TaskData { get; set; } = default;
		public string MainPageButtonLabel { get; set; } = "戻る";
		public string TaskDataPlaceHolder { get; set; } = "タスク入力欄\n\n「#」から始まる行は中タスク、「-」で始まる行は中タスクに属する小タスクとして認識されます。\n\n入力例）\n#カップラーメンを作る\n-ふたを開ける\n-お湯を注ぐ\n-３分待つ\n";
		public DateTimeOffset PlanDate {get; set;} = DateTimeOffset.Now ;

		public void Initialize(Views.RegisterTaskPage registerTaskPage)
		{
			View = registerTaskPage;
		}

		async public void RegisterTaskData(object sender, RoutedEventArgs e)
		{
			TaskData = "\n" + TaskData;

			string[] largeTaskStrings = TaskData.Split("\n#");

			foreach (string eachLargeTask in largeTaskStrings)
			{
				List<string> taskStrings = TaskData.Split("\n-").ToList();

				string classificationTask = taskStrings[0];

				EachTask newClassificationTask = new EachTask()
				{
					PlanDate = this.PlanDate,
					Content = classificationTask,
					RegisteredDate = DateTime.Now,

				};
				EachTaskModel eachTaskModel = new EachTaskModel();
				await eachTaskModel.RegisterTask(newClassificationTask);

				foreach (string eachTaskString in taskStrings.GetRange(1, taskStrings.Count - 1))
				{
					EachTask newTask = new EachTask()
					{
						Content = eachTaskString,
						RegisteredDate = DateTimeOffset.Now
					};
					await eachTaskModel.RegisterTask(newTask);
				}
			}
		}
	}
}
