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
using Windows.UI.Xaml.Controls;
using static MemoApp.Views.LogPage;

namespace MemoApp.ViewModels
{
	public class LogPageViewModel
	{
		public string TitleLabel = "行動ログ";
		public ObservableCollection<EachTask> TaskListData { get; set; } = new ObservableCollection<EachTask>(EachTaskModel.GetSpecificDateEachTasks(DateTimeOffset.Now));
		public string TaskChoicePlaceholder { get; set; } = "すべてのタスク";
		public string SelectedEachTaskId { get; set; }
		public CalendarDatePicker LogDateCalendarDatePicker { get; set; } = new CalendarDatePicker();
		public ObservableCollection<Activity> ActivityLog { get; set; } = new ObservableCollection<Activity>(ModelsHelpers.GetSpecificDateActivityLog(DateTimeOffset.Now));
		public ObservableCollection<Note> NoteList { get; set; } = new ObservableCollection<Note>(ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(DateTimeOffset.Now)));
		public Note Note { get; set; }
		public Activity Activity { get; set; }
		public DateTimeOffset LogDate { get; set; } = DateTimeOffset.Now.Date;


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

		public void ShorOtherDateLog()
		{
			ActivityLog = new ObservableCollection<Activity>(ModelsHelpers.GetSpecificDateActivityLog(Convert.ToDateTime(LogDate.Date)));
			NoteList = new ObservableCollection<Note>(ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(Convert.ToDateTime(LogDate.Date))));
		}

	}


	public class Note
	{
		public string TaskContent { get; set; } = default;
		public string Memo { get; set; } = default;
	}

	public class Activity
	{
		public DateTimeOffset ExactStartTime { get; set; } = default;
		public string StartTime { get; set; } = default;
		public string StopTime { get; set; } = default;
		public string TaskContent { get; set; } = default;
	}



}
