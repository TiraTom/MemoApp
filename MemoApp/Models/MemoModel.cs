using MempApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MemoApp.Models
{
	public static class MemoModel
	{
		public static void Register(string eachTaskId, string memoContent)
		{
			using (var db = new MemoAppContext())
			{
				EachTask targetTask = db.EachTasks.Where(eachTask => eachTask.EachTaskId == eachTaskId).FirstOrDefault();

				if (targetTask == null)
				{
					return;
				}

				Memo sameMemo = db.Memos.Where(memo => memo.EachTask.EachTaskId == eachTaskId).FirstOrDefault();

				if (sameMemo == null)
				{
					Memo newMemo = new Memo
					{
						CreateTime = DateTimeOffset.Now.ToLocalTime(),
						Content = memoContent,
						EachTask = targetTask
					};

					db.Memos.Add(newMemo);
				}
				else
				{
					sameMemo.Content = memoContent;
					sameMemo.UpdateTime = DateTimeOffset.Now.ToLocalTime();
				}

				db.SaveChanges();

			}
		}

		public static List<Memo> GetSpecificDateMemo(DateTimeOffset specificDate)
		{
			using(var db = new MemoAppContext())
			{
				List<Memo> memoList = db.Memos.Where(memo => memo.CreateTime.Date == specificDate.Date).Include(memo => memo.EachTask).ToList();

				return memoList;
			}
		}

		public static string GetSpecificEachTaskMemo(string eachTaskId)
		{
			using (var db = new MemoAppContext())
			{
				return db.Memos.Where(memo => memo.EachTask.EachTaskId == eachTaskId).FirstOrDefault()?.Content;
			}
		}

	}
}
