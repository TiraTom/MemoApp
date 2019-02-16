using MempApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
	public static class TimeInfoModel
	{
		public static List<TimeInfo> GetSpecifigTaskTimeInfo(string eachTaskId)
		{
			using (var db = new MemoAppContext())
			{
				return db.TimeInfos.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId).ToList();
			}
		}

		public static string RegisterStart(string eachTaskId)
		{
			string msg = null;

			using (var db = new MemoAppContext())
			{
				var selectedTask = db.EachTasks.FirstOrDefault(eachTask => eachTask.EachTaskId == eachTaskId);

				List<TimeInfo> notStoppedInfo = db.TimeInfos.Where(timeInfo => timeInfo.Stop == DateTimeOffset.MinValue).Include(timeInfo => timeInfo.EachTask).ToList();
				TimeInfo doubleStartTask = notStoppedInfo.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId).FirstOrDefault();

				if (doubleStartTask != null)
				{
					msg = "既に開始中タスクです。";
				}
				else
				{
					if (selectedTask.CompleteFlag == true)
					{
						EachTask completeTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
						completeTask.CompleteFlag = false;

						msg = "完了済みのタスクを再度開始しました。終了フラグを取り消しました。";
					}
					else
					{
						TimeInfo startTimeInfo = new TimeInfo()
						{
							Start = DateTimeOffset.Now,
							EachTask = db.EachTasks.FirstOrDefault(eachTask => eachTask.EachTaskId == eachTaskId)
						};
						db.TimeInfos.Add(startTimeInfo);
					}

					notStoppedInfo.ForEach(timeInfo => timeInfo.Stop = DateTimeOffset.Now);

					db.SaveChanges();
				}
			}

			return msg;

		}

		public static string RegisterPause(string eachTaskId)
		{
			string msg = null;

			using (var db = new MemoAppContext())
			{
				EachTask thisTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
				if (thisTask.CompleteFlag == true)
				{
					msg = "すでに完了済みのタスクです。";
				}
				else
				{
					TimeInfo stopTimeInfo = db.TimeInfos.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId && timeInfo.Stop == DateTimeOffset.MinValue).FirstOrDefault();

					if (stopTimeInfo == null)
					{
						msg = "開始されていない　または　すでに一時停止中です。";
					}
					else
					{
						stopTimeInfo.Stop = DateTimeOffset.Now;

						EachTask completeTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
						completeTask.CompleteFlag = true;

						db.SaveChanges();
					}
				}
			}
			return msg;
		}

		public static string RegisterStop(string eachTaskId)
		{
			string msg = null;

			using (var db = new MemoAppContext())
			{
				EachTask thisTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
				if (thisTask.CompleteFlag == true)
				{
					msg = "すでに完了済みのタスクです。";
				}
				else
				{
					TimeInfo stopTimeInfo = db.TimeInfos.Where(timeInfo => timeInfo.EachTask.EachTaskId == eachTaskId && timeInfo.Stop == DateTimeOffset.MinValue).FirstOrDefault();

					if (stopTimeInfo == null)
					{


						msg = "一時停止のまま、タスク完了となりました。";
					}
					else
					{
						stopTimeInfo.Stop = DateTimeOffset.Now;

						EachTask completeTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();
						completeTask.CompleteFlag = true;

						db.SaveChanges();
					}
				}
			}

			return msg;
		}
	}
}
