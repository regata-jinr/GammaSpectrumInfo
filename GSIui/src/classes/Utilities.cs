/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace GSI
{
   internal static class Utilities
   {
        private static string lang;
        public static void ChangeFormLanguage(Form form, string language)
        {
            ConfigurationManager.Language = language;
            lang = language;
            var vers = form.GetType().Assembly.GetName().Version;
            form.Text = $"{ConfigurationManager.config[$"{form.Name}:{lang}"]} - {vers.Major.ToString()}.{vers.Minor.ToString()}.{vers.Build.ToString()}";

            SetLanguageToControls(form.Controls);
        }

        private static void SetLanguageToControls(Control.ControlCollection controls)
        {
            foreach (var cont in controls)
                SetLanguageToObject(cont);
        }

        private static void SetLanguageToObject(object cont)
        {
            switch (cont)
            {
                case GroupBox grpb:
                    grpb.Text = ConfigurationManager.config[$"{grpb.Name}:{lang}"];
                    SetLanguageToControls(grpb.Controls);
                    break;

                case TabControl tbcont:
                    foreach (TabPage page in tbcont.TabPages)
                    {
                        page.Text = ConfigurationManager.config[$"{page.Name}:{lang}"];
                        SetLanguageToControls(page.Controls);
                    }
                    break;

                case DataGridView dgv:
                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = ConfigurationManager.config[$"{col.Name}:{lang}"];
                    break;

                case MenuStrip ms:
                    foreach (ToolStripMenuItem item in ms.Items)
                        SetLanguageToObject(item);
                    break;

                case ToolStripMenuItem tsi:
                    tsi.Text = ConfigurationManager.config[$"{tsi.Name}:{lang}"];
                    foreach (ToolStripMenuItem innerTsi in tsi.DropDownItems)
                        SetLanguageToObject(innerTsi);
                    break;

                default:
                    var getName = cont.GetType().GetProperty("Name").GetGetMethod();
                    var setText = cont.GetType().GetProperty("Text").GetSetMethod();

                    setText.Invoke(cont, new object[] { ConfigurationManager.config[$"{getName.Invoke(cont, null)}:{lang}"] });
                    break;

                case null:
                    throw new ArgumentNullException("Have trying to set language for null control");
            }
        }
    } // internal static class Utilities

    internal class ConfigurationManager
    {
        public static readonly IConfiguration config;

        static ConfigurationManager()
        {
            config = new ConfigurationBuilder().
                           SetBasePath(AppContext.BaseDirectory).
                           AddJsonFile("labels.json").
                           Build();
            _lang = config["language"];
        }
        private static string _lang;
        public static string Language
        {
           get { return _lang; }
           set
            {
                if (value != "rus" && value != "eng")
                    _lang = "eng";
                else
                {
                    if (_lang != value)
                    {
                        var prev = _lang;
                        _lang = value;
                        UpdateConfigFile(prev);
                    }
                }
            }
        }

        private static void UpdateConfigFile(string prev)
        {
            var jsonString = new StringBuilder(File.ReadAllText("labels.json"));
            jsonString.Replace($"\"language\": \"{prev}\"", $"\"language\": \"{_lang}\"");
            File.WriteAllText("labels.json", jsonString.ToString());
        }
    } // internal class ConfigurationManager
} // namespace GSI.Utilities
