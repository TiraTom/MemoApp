using MemoApp.Models;
using MemoApp.ViewModels;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MemoApp.ViewModels.LogPageViewModel;

namespace MemoApp.Helpers
{
	public static class ModelsHelpers
	{
		public static List<Activity> GetSpecificDateActivityLog(DateTimeOffset specificTime)
		{
			List<Activity> activityLog = new List<Activity>();

			using (var db = new MemoAppContext())
			{
				List<EachTask> eachTaskList = EachTaskModel.GetSpecificDateEachTasks(specificTime.Date);

				foreach (var eachTask in eachTaskList)
				{
					List<TimeInfo> timeInfoList = TimeInfoModel.GetSpecifigTaskTimeInfo(eachTask.EachTaskId);

					foreach(TimeInfo timeInfo in timeInfoList)
					{
						Activity activity = new Activity()
						{
							StartTime = timeInfo.Start.ToString("hh:mm"),
							StopTime = timeInfo.Stop.ToString("hh:mm"),
							TaskContent = eachTask.Content
						};

						activityLog.Add(activity);
					}
				}
			}
			return activityLog;
		}
	}
}
