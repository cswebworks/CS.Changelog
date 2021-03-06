﻿using System;

namespace CS.Changelog.Utils
{

	/// <summary>
	/// <see cref="Console"/> extensions for logging, writing in color.
	/// </summary>
	/// <remarks>Some part should be move to a more console-centric lib.</remarks>
	public static partial class ConsoleExtensions
	{
		/// <summary>The default verbosity</summary>
		public const LogLevel DefaultVerbosity =
#if DEBUG
		 LogLevel.Debug;
#else
		LogLevel.Info;
#endif

		/// <summary>The verbosity, by deafult uses <see cref="DefaultVerbosity"/>.</summary>
		public static LogLevel Verbosity { get; set; } = DefaultVerbosity;

		/// <summary>Dumps the specified <paramref name="trash"/> to a console window, either for development purposes or to use the programs' verbosity.</summary>
		/// <param name="trash">The 'trash', the object or text to dump.</param>
		/// <param name="loglevel">The loglevel.</param>
		/// <param name="color">The color in which to write to the console.</param>
		public static void Dump(this object trash, LogLevel loglevel = LogLevel.Info, System.ConsoleColor? color = null)
		{
			if (loglevel <= Verbosity)
				using ((ConsoleColor)color.GetValueOrDefault(loglevel.ToConsoleColor()))
					Console.WriteLine($"{trash}");
		}

		/// <summary>Converts <paramref name="level"/> to <see cref="System.ConsoleColor"/>.</summary>
		/// <param name="level">The level.</param>
		/// <returns>A <see cref="System.ConsoleColor"/> for the specified <paramref name="level"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">level</exception>
		public static System.ConsoleColor ToConsoleColor(this LogLevel level)
		{
			switch (level)
			{
				case LogLevel.Error:
					return System.ConsoleColor.Red;
				case LogLevel.Warning:
					return System.ConsoleColor.Yellow;
				case LogLevel.Info:
					return System.ConsoleColor.White;
				case LogLevel.Debug:
					return System.ConsoleColor.Gray;
				default:
					throw new ArgumentOutOfRangeException(nameof(level), level, $"{level} is not a valid {typeof(LogLevel).Name}");
			}
		}
	}
}