using MemoApp.Helpers;
using MemoApp.Models;
using MempApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using static MemoApp.Views.LogPage;

namespace MemoApp.ViewModels
{
	public class LogPageViewModel
	{

		public string LogLabel { get; } = "ログ";
		public string MemoLabel { get; } = "メモ";
		public string TitleLabel { get; } = "行動ログ";
		public string TaskChoicePlaceholder { get; } = "すべてのタスク";
		public ObservableCollection<EachTask> TaskListData { get; set; } = new ObservableCollection<EachTask>(EachTaskModel.GetSpecificDateEachTasks(DateTimeOffset.UtcNow));
		public CalendarDatePicker logDateCalendarDatePicker { get; set; } = new CalendarDatePicker();
		public ObservableCollection<Activity> ActivityLog { get; set; } = new ObservableCollection<Activity>(ModelsHelpers.GetSpecificDateActivityLog(DateTimeOffset.Now.ToLocalTime()));
		public ObservableCollection<Note> NoteList { get; set; } = new ObservableCollection<Note>(ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(DateTimeOffset.Now.ToLocalTime())));
		public Note Note { get; set; }
		public Activity Activity { get; set; }

		public DateTimeOffset? _logDate;
		public DateTimeOffset LogDate {
			get { return this._logDate ?? DateTimeOffset.Now.ToLocalTime(); }
			set {
				this._logDate = value;
				ShorOtherDateLog();
			}

		}
		public string SelectedEachTaskId { get; set; } = default;

		public static List<Note> ChangeMemoListToNoteList(List<Memo> memoList)
		{
			List<Note> noteList = new List<Note>();

			foreach (Memo memo in memoList ?? new List<Memo>())
			{
				Note note = new Note()
				{
					TaskContent = memo.EachTask?.Content,
					Memo = memo.Content,
					EachTaskId = memo.EachTask.EachTaskId
				};
				noteList.Insert(0, note);
			}
			return noteList;
		}

		public void PageLoaded()
		{
		}

		private void AddAllTask()
		{
			if (TaskListData.Count > 1)
			{
				if (!TaskListData.Any(eachTask => eachTask.Content == "全て"))
				{

					EachTask all = new EachTask()
					{
						EachTaskId = null,
						Content = "全て"
					};

					TaskListData.Insert(0, all);
				}
			}
		}

		public void ShowSpecificTaskLog()
		{
			ActivityLog.Clear();
			List<Activity> SpecificDateActivityList = ModelsHelpers.GetSpecificDateActivityLog(LogDate);
			NoteList.Clear();
			List<Note> SpecificDateNote = ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(LogDate));

			if (SelectedEachTaskId != null)
			{
				SpecificDateActivityList = SpecificDateActivityList.Where(activity => activity.EachTaskId == SelectedEachTaskId).ToList();
				SpecificDateNote = SpecificDateNote.Where(note => note.EachTaskId == SelectedEachTaskId).ToList();
			}

			SpecificDateActivityList.ForEach(activity => ActivityLog.Add(activity));
			SpecificDateNote.ForEach(note => NoteList.Add(note));
		}


		public void ShorOtherDateLog()
		{

			TaskListData.Clear();

			List<EachTask> specificTaskList = EachTaskModel.GetSpecificDateEachTasks(LogDate);
			specificTaskList.ForEach(eachTask => TaskListData.Add(eachTask));
			AddAllTask();

			ActivityLog.Clear();
			ModelsHelpers.GetSpecificDateActivityLog(LogDate).ForEach(activity => ActivityLog.Add(activity));

			NoteList.Clear();
			ChangeMemoListToNoteList(MemoModel.GetSpecificDateMemo(LogDate)).ForEach(memo => NoteList.Add(memo));
		}

	}


	public class Note
	{
		public string TaskContent { get; set; } = default;
		public string Memo { get; set; } = default;
		public string EachTaskId { get; set; } = default;
	}

	public class Activity
	{
		public DateTimeOffset ExactStartTime { get; set; } = default;
		public string StartTime { get; set; } = default;
		public string StopTime { get; set; } = default;
		public string TaskContent { get; set; } = default;
		public string EachTaskId { get; set; }
	}
}
