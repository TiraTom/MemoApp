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

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				TaskList.ItemSource = db.TaskList.ToList();
			}
		}

		public string TaskChoiceLabel { get; set; } = "タスク選択";
		public string StartLabel { get; set; } = "START";
		public string PauseLabel { get; set; } = "PAUSE";
		public string FinishLabel { get; set; } = "FINISH";
		public string MemoLabel { get; set; } = "メモを残す";
		public string HashButtonLabel { get; set; } = "ハッシュ";
		public string RegisterButtonLabel { get; set; } = "登録";

		public void ShowHashList(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				//HashItemList.ItemSource = db.HashItemList.ToList();
			}
		}

		private void MemoRegister(object sender, RoutedEventArgs e)
		{
			using (var db = new MemoAppContext())
			{
				Memo newMemo = new Memo
				{
					this.CreateTime = DateTime.Now,
					this.Content = MemoLabell 
				};

				db.MemoList.Add(newMemo);
				db.SaveChanges();

				MemoList.ItemSource = db.MemoList.ToList();

			}
		}
	}
