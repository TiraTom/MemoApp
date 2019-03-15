using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MemoApp.Models
{
	public static class EachTaskModel
	{

		public enum TaskType
		{
			Classification,	Item
		}

		public static EachTask GetEachTask(string targetTaskId)
		{
			EachTask targetTask = new EachTask();

			using (var db = new MemoAppContext())
			{
				targetTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == targetTaskId).FirstOrDefault();
			}
			return targetTask;
		}

		public static bool IsSpecificDateTask(string eachTaskId, DateTimeOffset specificDateTime)
		{
			EachTask targetTask = GetEachTask(eachTaskId);

			return targetTask?.PlanDate.Date == specificDateTime.Date ? true : false;
		}


		async public static Task<int> RegisterTaskAsync(EachTask newTask)
		{
			using (var db = new MemoAppContext())
			{
				db.EachTasks.Add(newTask);
				return await db.SaveChangesAsync();
			}
		}

		public static string RegisterTask(EachTask newTask)
		{
			using (var db = new MemoAppContext())
			{
				db.EachTasks.Add(newTask);
				db.SaveChanges();
				return GetEachTaskId(newTask);
			}
		}

		public static List<EachTask> GetSpecificDateEachTasks(DateTimeOffset specificDate)
		{
			if(specificDate == null)
			{
				return new List<EachTask>();
			}

			using (var db = new MemoAppContext())
			{
				return db.EachTasks.ToList()
							.FindAll(eachTask => eachTask.PlanDate.Date == specificDate.Date 
												&& string.IsNullOrWhiteSpace(eachTask.ParentEachTaskId)
												&& eachTask.ValidFlag == true)
							.OrderBy(eachTask => eachTask.Rank)
							.ToList();
			}
		}

		public static List<EachTask> GetSpecificTaskSmallTasks(string eachTaskId)
		{
			if (string.IsNullOrWhiteSpace(eachTaskId))
			{
				return new List<EachTask>();
			}

			using (var db = new MemoAppContext())
			{
				return db.EachTasks.Where(eachTask => eachTask.ParentEachTaskId == eachTaskId && eachTask.ValidFlag == true)
									.OrderBy(eachTask => eachTask.Rank).ToList();
			}
		}

		public static string GetEachTaskId(EachTask targetTask)
		{
			using (var db = new MemoAppContext())
			{
				EachTask existEachTask = db.EachTasks.Where(eachTask =>
											eachTask.Content == targetTask.Content 
											&& eachTask.PlanDate.Date == targetTask.PlanDate.Date 
											&& eachTask.ParentEachTaskId == targetTask.ParentEachTaskId)
											.FirstOrDefault();

				return existEachTask?.EachTaskId ?? string.Empty;
			}
		}

		async public static Task UpdateTaskRankAsync(string newTaskId, int rank)
		{
			using (var db = new MemoAppContext())
			{
				EachTask targetTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == newTaskId).FirstOrDefault();
				targetTask.Rank = rank;
				await db.SaveChangesAsync();
			}
		}

		public static void ChangeValidFlag(string targetTaskId, bool flagValue)
		{
			using (var db = new MemoAppContext())
			{
				EachTask targetTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == targetTaskId).FirstOrDefault();
				targetTask.ValidFlag = flagValue;
				db.SaveChanges();
			}
		}
	}
}
