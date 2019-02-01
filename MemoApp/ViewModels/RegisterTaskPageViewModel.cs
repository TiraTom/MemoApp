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
		public string RegisterButtonLabel { get; set; } = "Register";
		public string TaskData { get; set; } = default;

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

				foreach (string eachTaskString in taskStrings.GetRange(1, taskStrings.Count - 1))
				{
					EachTask newTask = new EachTask()
					{
						Content = eachTaskString,
						RegisteredDate = DateTime.Now
					};

					EachTaskModel eachTaskModel = new EachTaskModel();
					await eachTaskModel.RegisterTask(newTask);
				}

			}
		}
	}
}
