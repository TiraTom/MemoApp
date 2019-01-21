using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MempApp.Model
{
	public class MemoAppContext : DbContext
	{
		public DbSet<HashItem> HashItemList {get; set;}
		public DbSet<EachTask> TaskList { get; set; }
		public DbSet<Memo> MemoList { get; set; }
	}

	public class Memo
	{
		public string ID { get; }
		public DateTime CreateTime { get; set; }
		public DateTime UpdateTime { get; set; }
		public string Content { get; set; }
	}
	public class HashItem
	{
		public string ID { get; }
		public string Name { get; set; } = default;
		public DateTime UsedTime { get; set; } = default;
	}
	public class EachTask
	{
		public string ID { get; }
		private string taskName = string.Empty;
		public List<TimeInfo> TimeInfoList { get; set; }
		public DateTime DeadLine { get; set; } = default;
		public DateTime PlanDate { get; set; } = default;

		public string TaskName
		{
			get { return this.taskName; }
			set { this.taskName = value; }
		}

		public class TimeInfo
		{
			private DateTime start;
			private DateTime stop;

			public DateTime Start { get; set; }
			public DateTime Stop { get; set; }
			public TimeSpan ElapsedTime()
			{
				return (Start != null && Stop != null) ? Stop.Subtract(Start) : new TimeSpan(0);
			}
		}

		public bool IsOver()
		{
			return DeadLine.CompareTo(DateTime.Now) == 0 ? false : true;
		}
	}
}
