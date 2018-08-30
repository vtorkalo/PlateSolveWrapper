using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PlateSolveWrapper
{
    public class SettingsProvider
    {
        private readonly string _settingsFileName;

        public SettingsProvider(string settingsFileName)
        {
            _settingsFileName = settingsFileName;
        }

        public Settings ReadSettings()
        {
            Settings settings = null;

            if (File.Exists(_settingsFileName))
            {
                var text = File.ReadAllText(_settingsFileName);
                settings = DeserializeSettings(text);
                if (settings == null)
                {
                    settings = new Settings();
                }
            }
            else
            {
                settings = new Settings();
            }

            return settings;
        }

        public void SaveSettings(Settings settings)
        {
            var text = SerializeSettings(settings);
            File.WriteAllText(_settingsFileName, text);
        }

        private Settings DeserializeSettings(string serialized)
        {
            var type = typeof(Settings);
            var serializer = new XmlSerializer(type);
            Settings result = null;

            using (TextReader reader = new StringReader(serialized))
            {
                result = (Settings)serializer.Deserialize(reader);
            }
            return result;
        }

        private string SerializeSettings(Settings settings)
        {
            XmlSerializer xsSubmit = new XmlSerializer(settings.GetType());
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, settings);
                    xml = sww.ToString();
                }
            }

            return xml;
        }
    }
}
