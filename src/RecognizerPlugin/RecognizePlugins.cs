using McMaster.NETCore.Plugins;
using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecognizerPlugin
{
    public partial class RecognizePlugins : RecognizeFileExt
    {
        public RecognizePlugins()
        {
            LoadPlugins();
        }
        private void LoadPlugins() { 
            var loaders = new List<PluginLoader>();
            var pluginsDir = Path.Combine(AppContext.BaseDirectory, "plugins");
            foreach (var dir in Directory.GetDirectories(pluginsDir))
            {
                var dirName = Path.GetFileName(dir);
                var pluginDll = Path.Combine(dir, dirName + ".dll");
                if (File.Exists(pluginDll))
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(
                        pluginDll,
                        config => config.PreferSharedTypes = true
                        );
                    
                    loaders.Add(loader);
                }
            }

            // Create an instance of plugin types
            foreach (var loader in loaders)
            {
                var ass = loader.LoadDefaultAssembly();
                
                var types = ass.GetTypes();
                var names = types.Select(it => it.Name).OrderBy(ut => ut).ToArray();
                using (var c = new CurDir(Path.GetDirectoryName(ass.Location)))
                {
                    foreach (var pluginType in types
                        .Where(t =>
                        typeof(RecognizeFileExt).IsAssignableFrom(t)
                        && !t.IsAbstract
                        && t.IsPublic
                        ))
                    {

                        // This assumes the implementation of IPlugin has a parameterless constructor
                        var plugin = Activator.CreateInstance(pluginType) as RecognizeFileExt;
                        this.recognizes.AddRange(plugin.recognizes);
                    }

                    foreach (var pluginType in types
                        .Where(t =>
                        typeof(IRecognize).IsAssignableFrom(t)
                        && !t.IsAbstract
                        && t.IsPublic
                        ))
                    {
                        // This assumes the implementation of IPlugin has a parameterless constructor
                        var plugin = Activator.CreateInstance(pluginType) as IRecognize;
                        this.recognizes.Add(plugin);
                    }
                }
            }
        }
    }
}
