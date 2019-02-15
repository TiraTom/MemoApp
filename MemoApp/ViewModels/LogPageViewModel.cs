using MemoApp.Helpers;
using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static MemoApp.Views.LogPage;

namespace MemoApp.ViewModels
{
	public class LogPageViewModel
	{
		public string MainPageButtonLabel = "Mainに\n戻る";
		public string TitleLabel = "行動ログ";
		public ObservableCollection<EachTask> TaskListData { get; set; } = new ObservableCollection<EachTask>(EachTaskModel.GetSpecificDateEachTasks(DateTimeOffset.Now.Date));
		public string TaskChoicePlaceholder { get; set; } = "すべてのタスク";
		public string SelectedEachTaskId { get; set; }

		public DateTimeOffset LogDate { get; set; } = DateTimeOffset.Now;
		public ObservableCollection<Activity> ActivityLog { get; set; } = new ObservableCollection<Activity>(ModelsHelpers.GetSpecificDateActivityLog(DateTimeOffset.Now));
		public ObservableCollection<Note> NoteList { get; set; } = new ObservableCollection<Note>(ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(DateTimeOffset.Now)));
		public Note Note { get; set; }
		public Activity Activity { get; set; }


		public static List<Note> ChangeMemoListToNoteList(List<Memo> memoList)
		{
			List<Note> noteList = new List<Note>();

			foreach (Memo memo in memoList ?? new List<Memo>())
			{
				Note note = new Note()
				{
					TaskContent = memo.EachTask?.Content,
					Memo = memo.Content
				};
				noteList.Add(note);
			}
			return noteList;
		}

	}


	public class Note
	{
		public string TaskContent { get; set; } = default;
		public string Memo { get; set; } = default;
	}

	public class Activity
	{
		public string StartTime { get; set; } = default;
		public string StopTime { get; set; } = default;
		public string TaskContent { get; set; } = default;
	}


}
