using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logs.Info
{
	public class MemoryLogger
	{
		private static readonly MemoryLogger _instance = new MemoryLogger();
		private static readonly string path = "C:\\Users\\drago\\source\\repos\\NewTest\\Logs.Info\\log.json";
		private int _InfoCount;
		private int _WarningCount;
		private int _ErrorCount;

		private MemoryLogger() { }

		public static MemoryLogger GetLogger
		{
			get
			{
				return _instance;
			}
		}

		private List<LogMessage> _logs = new List<LogMessage>();
		public IReadOnlyCollection<LogMessage> Logs => _logs;


		private async void Log(string message, LogType logType)
		{
			var temp = new LogMessage()
			{
				Message = message,
				LogType = logType,
				CreatedAt = DateTime.Now
			};
			_logs.Add(temp);
			await WriteToFileAsync(temp);
		}
		private static async Task WriteToFileAsync(LogMessage logMessage)
		{
			var options = new JsonSerializerOptions { WriteIndented = true };
			var json = JsonSerializer.Serialize(logMessage, options);

			// Check if the file exists, if not, create it
			if (!File.Exists(path))
			{
				using (FileStream fs = File.Create(path))
				{
					byte[] info = new UTF8Encoding(true).GetBytes("[]");
					await fs.WriteAsync(info, 0, info.Length);
				}
			}

			using (StreamWriter sw = new StreamWriter(path, append: true))
			{
				await sw.WriteLineAsync(json);
			}
		}


		public void LogInfo(string message)
		{
			++_InfoCount;
			Log(message, LogType.INFO);
		}
		public void LogError(string message)
		{
			++_ErrorCount;
			Log(message, LogType.ERROR);
		}
		public void LogWarning(string message)
		{
			++_WarningCount;
			Log(message, LogType.WARNING);
		}

		public void ShowLog()
		{

			_logs.ForEach(x => Console.WriteLine(x));
			Console.WriteLine($"-------------------------------");

			Console.WriteLine($"Info ({_InfoCount}), Warning ({_WarningCount}), Error ({_ErrorCount})");
		}
	}
}
