using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MempApp.Model
{
	public class MemoAppContext : DbContext
	{
		public DbSet<HashItem> HashItems { get; set; }
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
			modelBuilder.Entity<HashItem>().ToTable("HashItems").Property(x => x.HashItemId).IsRequired();
			modelBuilder.Entity<TimeInfo>().ToTable("TimeInfos").Property(x => x.TimeInfoId).IsRequired();
		}
	}

	public class Memo
	{
		public string MemoId { get; set; }
		public DateTimeOffset CreateTime { get; set; }
		public DateTimeOffset UpdateTime { get; set; }
		public string Content { get; set; }
		public virtual EachTask EachTask { get; set; }

	}
	public class HashItem
	{
		public string HashItemId { get; set; }
		public string Name { get; set; } = default;
		public DateTimeOffset UsedTime { get; set; } = default;
	}
	public class EachTask
	{
		public string EachTaskId { get; set; }
		public string Content { get; set; } = default;
		public DateTimeOffset DeadLine { get; set; } = default;
		public DateTimeOffset PlanDate { get; set; } = default;
		public DateTimeOffset RegisteredDate { get; set; } = default;
		public string Type { get; set; } = default;
		public string ParentEachTaskId { get; set; } = null;
	}

	public class TimeInfo
	{
		public string TimeInfoId { get; set; }
		public DateTimeOffset Start { get; set; }
		public DateTimeOffset Stop { get; set; }
		public virtual EachTask EachTask { get; set; }
	}
}
