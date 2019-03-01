using MempApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
	public static class ConfigModel
	{
		public static List<Config> GetAllConfig()
		{
			using (var db = new MemoAppContext())
			{
				return db.Configs.ToList();
			}
		}

		public static string GetSpecificConfigValue(ConfigType configType)
		{
			using (var db = new MemoAppContext())
			{
				return db.Configs.Where(config => config.ConfigId == configType.ToString()).FirstOrDefault()?.Value;
			}
		}

		public async static Task UpdateSpecificConfig(ConfigType configType, string value)
		{
			using (var db = new MemoAppContext())
			{
				Config targetConfig = db.Configs.Where(config => config.ConfigId == configType.ToString()).FirstOrDefault();

				if (targetConfig == null)
				{
					Config newConfig = new Config()
					{
						ConfigId = configType.ToString(),
						Value = value
					};

					db.Configs.Add(newConfig);
				}
				else
				{
					targetConfig.Value = value;
				}

				await db.SaveChangesAsync();

			}
		}


		public enum ConfigType
		{
			NotificationFlag,
			NotificationSpanMinute
		}
	}
}
