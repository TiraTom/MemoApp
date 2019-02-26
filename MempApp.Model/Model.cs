using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MempApp.Model
{
	public class MemoAppContext : DbContext
	{
		public DbSet<EachTask> EachTasks { get; set; }
		public DbSet<Memo> Memos { get; set; }
		public DbSet<TimeInfo> TimeInfos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data source=memoApp.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EachTask>().ToTable("EachTasks").Property(x => x.EachTaskId).IsRequired();
			modelBuilder.Entity<Memo>().ToTable("Memos").Property(x => x.MemoId).IsRequired();
			modelBuilder.Entity<TimeInfo>().ToTable("TimeInfos").Property(x => x.TimeInfoId).IsRequired();
		}
	}

	public class Memo
	{
		public string MemoId { get; set; }
		public DateTimeOffset CreateTime { get; set; }
		public DateTimeOffset UpdateTime { get; set; }
		public string Content { get; set; }
		public EachTask EachTask { get; set; }

	}

	public class EachTask
	{
		public string EachTaskId { get; set; }
		public string Content { get; set; } = default;
		public DateTimeOffset DeadLine { get; set; }
		public DateTimeOffset PlanDate { get; set; }
		public DateTimeOffset RegisteredDate { get; set; }
		public string Type { get; set; } = default;
		public string ParentEachTaskId { get; set; } = default;
		public bool CompleteFlag { get; set; } = false;
		public bool StartedFlag { get; set; } = false;
		public int Rank { get; set; } = default;
		public bool ValidFlag { get; set; } = true;
	}

	public class TimeInfo
	{
		public string TimeInfoId { get; set; }
		public DateTimeOffset Start { get; set; }
		public DateTimeOffset Stop { get; set; }
		public EachTask EachTask { get; set; }
	}
}
