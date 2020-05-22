using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PortTunneling
{
 	public abstract class AppConfiguration
	{
        private readonly string _name;
        private readonly string _filePath;
		private readonly Type _baseType;
        private readonly PropertyInfo[] _properties;

		public AppConfiguration()
		{
			_baseType = GetType();
			_name = _baseType.Name;
			_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _name + ".json");
			_properties = _baseType.GetProperties();
			_baseType.GetFields();
		}

		public void Initialize()
		{
			object baseObj = null;
			using (var fstream = File.Open(_filePath, FileMode.OpenOrCreate))
			using (var reader = new StreamReader(fstream))
			{
				if (fstream.Length > 0)
				{
					try
					{
						var ser = new JsonSerializer();
						baseObj = ser.Deserialize(reader, _baseType);
					}
					catch { return; }
				}
			}

			if (baseObj != null)
			{
				for (int i = 0; i < _properties.Length; i++)
					_properties[i].SetValue(this, _properties[i].GetValue(baseObj));
			}
		}

		public void Save()
		{
			using (FileStream fs = File.Open(_filePath, FileMode.Create))
			using (var sw = new StreamWriter(fs))
			using (JsonWriter jw = new JsonTextWriter(sw))
			{
				jw.Formatting = Formatting.Indented;
				var serializer = new JsonSerializer();
				serializer.TypeNameHandling = TypeNameHandling.Auto;
				serializer.Serialize(jw, this, _baseType);
			}
		}
	}
}
