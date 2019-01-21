using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MemoApp.ViewModels
{
	class RegisterPageModelView : Helpers.Observable
	{
		private void TaskRegister(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				var newTask = new EachTask
				{
					this.taskName = NewEachTaskName,
					this.DeadLine = NewEachTaskDeadLine,
					this.PlanDate = NewEachTaskPlanDate,
					this.RegistedDate = DateTime.Now
				};

				db.TaskList.Add(newTask);
				db.SaveChanges();

				TaskList.ItemSource = db.EachTask.ToList();

	}
}
	}
}
