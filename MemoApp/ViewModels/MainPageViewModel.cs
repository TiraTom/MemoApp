using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MemoApp.ViewModels
{
	public class MainPageViewModel : Helpers.Observable
	{
		public Views.MainPage View { get; private set; } = null;

		public void Initialize(Views.MainPage mainPage)
		{
			View = mainPage;
		}

		public void Page_Loaded(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				TaskListData = db.EachTasks.ToList();
			}
		}

		public string TaskChoiceLabel { get; set; } = "タスク選択";
		public string StartLabel { get; set; } = "START";
		public string PauseLabel { get; set; } = "PAUSE";
		public string FinishLabel { get; set; } = "FINISH";
		public string MemoLabel { get; set; } = "メモを残す";
		public string HashButtonLabel { get; set; } = "ハッシュ";
		public string RegisterButtonLabel { get; set; } = "登録";
		public List<EachTask> TaskListData { get; set; } = default;
		public List<HashItem> HashItemListData { get; set; } = default;
		public List<Memo> MemoListData { get; set; } = default;

		public void ShowHashList(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				HashItemListData = db.HashItems.ToList();
			}
		}

		public void MemoRegister(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				Memo newMemo = new Memo
				{
					CreateTime = DateTime.Now,
					Content = MemoLabel
				};

				db.Memos.Add(newMemo);
				db.SaveChanges();

				MemoListData = db.Memos.ToList();

			}
		}
	}
}
