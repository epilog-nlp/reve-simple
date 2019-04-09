/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT

Original source licence:
This source file is under MIT License (MIT)
Copyright (c) 2016 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;
using System.Diagnostics;

namespace REvE.Configuration
{
    /// <summary>
    /// Configuration Utility Methods extracted from Shared.Core.Common.auxfunc by Mihaela Iridon
    /// </summary>
    public static class ConfigUtil
    {
        /// <summary>
        /// Delegate that manages access to the <see cref="System.Configuration.ConfigurationManager.AppSettings"/> dictionary.
        /// </summary>
        public static Func<string, string> AppSettingStr =>
            k => System.Configuration.ConfigurationManager.AppSettings[k];

        /// <summary>
        /// Retrieves the application configuration value associated with the provided <paramref name="key"/>, or <c>default</c> if not found.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the value should be converted to.</typeparam>
        /// <param name="key">The application configuration key to retrieve the value for.</param>
        /// <returns>The application configuration value associated with the provided <paramref name="key"/>, or <c>default</c> if not found.</returns>
        [DebuggerStepThrough]
        public static T AppSetting<T>(string key) =>
            ReadSetting(key, () => default(T));

        /// <summary>
        /// Retrieves the application configuration value associated with the provided <paramref name="key"/> or the provided <paramref name="defaultValue"/> if not found.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the value should be converted to. Implied from <paramref name="defaultValue"/>.</typeparam>
        /// <param name="key">The application configuration key to retrieve the value for.</param>
        /// <param name="defaultValue">The default value to return if the key isn't found.</param>
        /// <returns>The application configuration value associated with the provided <paramref name="key"/> or the provided <paramref name="defaultValue"/> if not found.</returns>
        [DebuggerStepThrough]
        public static T AppSetting<T>(string key, T defaultValue) =>
            ReadSetting(key, () => defaultValue);

        private static T ReadSetting<T>(string key, Func<T> defaultFunc)
        {
            var s = AppSettingStr(key);
            if (string.IsNullOrEmpty(s))
                return defaultFunc();

            var o = (T)Convert.ChangeType(s, typeof(T));
            return o;
        }
    }
}
