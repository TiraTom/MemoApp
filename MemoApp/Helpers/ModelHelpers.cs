using MemoApp.Models;
using MemoApp.ViewModels;
using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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
				List<EachTask> eachTaskList = EachTaskModel.GetSpecificDateEachTasks(specificTime);

				foreach (var eachTask in eachTaskList)
				{
					List<TimeInfo> timeInfoList = TimeInfoModel.GetSpecificTaskTimeInfo(eachTask.EachTaskId);

					foreach(TimeInfo timeInfo in timeInfoList)
					{
						Activity activity = new Activity()
						{
							ExactStartTime = timeInfo.Start.LocalDateTime,
							StartTime = timeInfo.Start.LocalDateTime.ToString(" HH : mm "),
							StopTime = timeInfo.Stop == DateTimeOffset.MinValue ? " XX : XX " : timeInfo.Stop.LocalDateTime.ToString(" HH : mm "),
							TaskContent = eachTask.Content,
							EachTaskId = eachTask.EachTaskId
						};

						activityLog.Add(activity);
					}
				}
			}

			return activityLog.OrderBy(log => log.ExactStartTime).ToList();
		}
	}
}
