using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Cfg;

namespace LeedsSharp.NHib.UI.NHibernate
{
    public static class ConfigHelper
    {
		private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;
		private static readonly string ConfigurationPath = Path.Combine(AppPath, "nh-configuration.serialized");

		internal static readonly string MappingPath = Path.Combine(AppPath, "App_Data", "Mappings");

		public static Configuration TryLoadConfiguration()
        {
	        if (!File.Exists(ConfigurationPath))
				return null;

			using (var stream = File.OpenRead(ConfigurationPath))
	        {
		        return (Configuration)new BinaryFormatter().Deserialize(stream);
	        }
        }

        internal static void PersistConfiguration(Configuration config)
        {
            using (var stream = File.OpenWrite(ConfigurationPath))
			{
				new BinaryFormatter().Serialize(stream, config);
			}
        }
    }

}