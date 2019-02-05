using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
	public class EachTaskModel
	{
		public bool IsOver(EachTask eachTask)
		{
			return eachTask.DeadLine.CompareTo(DateTime.Now) == 0 ? false : true;
		}

		public enum TaskType
		{
			Classification,	Item
		}



		async public Task<int> RegisterTask(EachTask newTask)
		{
			using (var db = new MemoAppContext())
			{

				db.EachTasks.Add(newTask);
				return await db.SaveChangesAsync();
			}
		}
	}
}
