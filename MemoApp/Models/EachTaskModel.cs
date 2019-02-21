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



		async public static Task<int> RegisterTaskAsync(EachTask newTask)
		{
			using (var db = new MemoAppContext())
			{

				db.EachTasks.Add(newTask);
				return await db.SaveChangesAsync();
			}
		}

		public static int RegisterTask(EachTask newTask)
		{
			using (var db = new MemoAppContext())
			{
				db.EachTasks.Add(newTask);
				return db.SaveChanges();
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
				return db.EachTasks.ToList().FindAll(eachTask => eachTask.PlanDate.Date == specificDate.Date);
			}
		}
	}
}
