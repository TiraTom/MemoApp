using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.ViewModels
{
	public class LogPageViewModel
	{
		public string MainPageButtonLabel = "Mainに\n戻る";
		public string TitleLabel = "行動ログ";
		public DateTimeOffset LogDate { get; set; } = DateTimeOffset.Now.Date;

	}
}
