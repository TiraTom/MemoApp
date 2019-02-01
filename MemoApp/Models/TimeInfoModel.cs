using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
	public class TimeInfoModel
	{
		public TimeSpan ElapsedTime(TimeInfo timeInfo)
		{
			return (timeInfo.Start != null && timeInfo.Stop != null) ? timeInfo.Stop.Subtract(timeInfo.Start) : new TimeSpan(0);
		}

	}
}
