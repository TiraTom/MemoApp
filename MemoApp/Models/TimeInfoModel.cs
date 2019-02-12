using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
	public static class TimeInfoModel
	{
		public static TimeSpan ElapsedTime(TimeInfo timeInfo)
		{
			return (timeInfo.Start != null && timeInfo.Stop != null) ? timeInfo.Stop.Subtract(timeInfo.Start) : new TimeSpan(0);
		}

		public static void RegisterStart(string eachTaskId)
		{
			using (var db = new MemoAppContext())
			{
				TimeInfo startTimeInfo = new TimeInfo()
				{
					Start = DateTimeOffset.Now,
					EachTask = db.EachTasks.FirstOrDefault(eachTask => eachTask.EachTaskId == eachTaskId)
				};
				db.TimeInfos.Add(startTimeInfo);

				List<TimeInfo> notStoppedInfo = db.TimeInfos.Where(timeInfo => timeInfo.Stop == null).ToList();
				notStoppedInfo.ForEach(timeInfo => timeInfo.Stop = DateTimeOffset.Now);

				db.SaveChanges();
			}
		}

		public static void RegisterPause(string eachTaskId)
		{
			using (var db = new MemoAppContext())
			{
				TimeInfo stopTimeInfo = db.TimeInfos.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId && timeInfo.Stop == DateTimeOffset.MinValue).FirstOrDefault();

				if (stopTimeInfo == null)
				{
					// すでに終わったタスクについて終了を押したパターン
					// 始めてないのに終わらしちゃったパターン
					// なんかメッセージ出したい
				}
				else
				{
					stopTimeInfo.Stop = DateTimeOffset.Now;
				}

				db.SaveChanges();
			}
		}

		public static void RegisterStop(string eachTaskId)
		{
			using (var db = new MemoAppContext())
			{
				TimeInfo stopTimeInfo = db.TimeInfos.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId && timeInfo.Stop == DateTimeOffset.MinValue).FirstOrDefault();

				if (stopTimeInfo == null)
				{
					// 一時停止のまま終了を押したパターン
					// 始めてないのに終わらしちゃったパターン
					// なんかメッセージ出したい
				}
				else
				{
					RegisterPause(eachTaskId);
				}

				EachTask completeTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
				completeTask.CompleteFlag = true;

				db.SaveChanges();
			}
		}
	}
}
