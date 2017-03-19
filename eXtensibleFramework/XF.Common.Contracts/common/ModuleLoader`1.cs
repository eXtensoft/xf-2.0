// <copyright company="eXtensible Solutions, LLC" file="ModuleLoader`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public sealed class ModuleLoader<T> where T : new()
    {
        #region fields

        private Type[] _types = null;

        #endregion fields

        #region properties

        public List<string> Folderpaths { get; set; }

        #endregion

        #region constructors

        public ModuleLoader()
        {
        }

        public ModuleLoader(params Type[] types)
        {
            _types = types;
        }

        #endregion constructors

        #region instance methods

        public bool Load(out T t, IEnumerable<string> folderpaths)
        {
            bool b = false;
            t = new T();
            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                AddCatalogs(catalog, folderpaths);
                AddTypes(catalog);

                using (CompositionContainer container = new CompositionContainer(catalog, true))
                {
                    try
                    {
                        container.ComposeParts(t);
                        b = true;
                        if (eXtensibleConfig.Inform)
                        {
                            EventWriter.Inform("TypeCache loaded successfully");
                        }
                    }
                    catch (System.IO.FileLoadException fileEx)
                    {
                        string message = (fileEx.InnerException != null) ? fileEx.InnerException.Message : fileEx.Message;
                        EventWriter.WriteError(message, SeverityType.Critical);
                        if (eXtensibleConfig.Inform)
                        {
                            EventWriter.Inform(message);
                        }
                    }
                    catch (System.TypeLoadException loadEx)
                    {
                        string message = (loadEx.InnerException != null) ? loadEx.InnerException.Message : loadEx.Message;
                        EventWriter.WriteError(message, SeverityType.Critical);
                        if (eXtensibleConfig.Inform)
                        {
                            EventWriter.Inform(message);
                        }
                    }
                    catch (ReflectionTypeLoadException reflexEx)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in reflexEx.LoaderExceptions)
                        {
                            sb.AppendLine(item.Message);
                        }
                        EventWriter.WriteError(sb.ToString(), SeverityType.Critical);
                        if (eXtensibleConfig.Inform)
                        {
                            EventWriter.Inform(sb.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        EventWriter.WriteError(message, SeverityType.Critical);
                        if (eXtensibleConfig.Inform)
                        {
                            EventWriter.Inform(message);
                        }
                    }
                }
            }
            return b;
        }

        private void AddCatalogs(AggregateCatalog catalog, IEnumerable<string> folderpaths)
        {
            foreach (var path in folderpaths)
            {
                if (Directory.Exists(path))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(path));
                }
            }
        }

        private void AddCatalogsEachDll(AggregateCatalog catalog, IEnumerable<string> folderpaths)
        {
            foreach (var path in folderpaths)
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directorInfo = new DirectoryInfo(path);
                    var fileInfos = directorInfo.GetFileSystemInfos("*.dll");
                    foreach (FileInfo info in fileInfos)
                    {
                        try
                        {
                            var assemblyCatalog = new AssemblyCatalog(Assembly.LoadFile(info.FullName));
                            var parts = assemblyCatalog.Parts.ToArray();
                            catalog.Catalogs.Add(assemblyCatalog);
                        }
                        catch (System.IO.FileLoadException fileEx)
                        {
                            string message = (fileEx.InnerException != null) ? fileEx.InnerException.Message : fileEx.Message;
                            EventWriter.WriteError(fileEx.Message, SeverityType.Critical);
                            if (eXtensibleConfig.Inform)
                            {
                                EventWriter.Inform(message);
                            }
                        }
                        catch (System.TypeLoadException loadEx)
                        {
                            string message = (loadEx.InnerException != null) ? loadEx.InnerException.Message : loadEx.Message;
                            EventWriter.WriteError(message, SeverityType.Critical);
                            if (eXtensibleConfig.Inform)
                            {
                                EventWriter.Inform(message);
                            }
                        }
                        catch (ReflectionTypeLoadException reflexEx)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in reflexEx.LoaderExceptions)
                            {
                                sb.AppendLine(item.Message);
                            }
                            EventWriter.WriteError(sb.ToString(), SeverityType.Critical);
                            if (eXtensibleConfig.Inform)
                            {
                                EventWriter.Inform(sb.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                            EventWriter.WriteError(message, SeverityType.Critical);
                            if (eXtensibleConfig.Inform)
                            {
                                EventWriter.Inform(message);
                            }
                        }
                    }
                }
            }
        }

        private void AddTypes(AggregateCatalog catalog)
        {
            if (_types != null && _types.Length > 0)
            {
                foreach (Type type in _types)
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(type.Assembly));
                }
            }
        }

        private List<string> GenerateFolderpaths()
        {
            List<string> list = new List<string>();
            list.Add(AppDomain.CurrentDomain.BaseDirectory);
            list.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"));
            list.Add(eXtensibleConfig.ModelDataGatewayPlugins);
            return list;
        }

        public bool Load(out T t)
        {
            List<string> folderpaths = Folderpaths != null ? Folderpaths : GenerateFolderpaths();
            return Load(out t, folderpaths);
        }
        //public bool Load(out T t)
        //{
        //    bool b = false;
        //    t = new T();
        //    using (AggregateCatalog catalog = new AggregateCatalog())
        //    {
        //        catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
        //        string webbindirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
                
        //        if (Directory.Exists(webbindirectory))
        //        {
        //            catalog.Catalogs.Add(new DirectoryCatalog(webbindirectory));
        //        }
        //        if (Directory.Exists(eXtensibleConfig.ModelDataGatewayPlugins))
        //        {
        //            catalog.Catalogs.Add(new DirectoryCatalog(eXtensibleConfig.ModelDataGatewayPlugins));
        //        }
        //        if (Directory.Exists(eXtensibleConfig.RemoteProcedureCallPlugins))
        //        {
        //            catalog.Catalogs.Add(new DirectoryCatalog(eXtensibleConfig.RemoteProcedureCallPlugins));
        //        }
        //        if (_types != null && _types.Length > 0)
        //        {
        //            foreach (Type type in _types)
        //            {
        //                catalog.Catalogs.Add(new AssemblyCatalog(type.Assembly));
        //            }
        //        }
        //        using (CompositionContainer container = new CompositionContainer(catalog, true))
        //        {
        //            try
        //            {
        //                container.ComposeParts(t);
        //                b = true;
        //                if (eXtensibleConfig.Inform)
        //                {
        //                    EventWriter.Inform("TypeCache loaded successfully");
        //                }
        //            }
        //            catch (System.IO.FileLoadException fileEx)
        //            {
        //                string message = (fileEx.InnerException != null) ? fileEx.InnerException.Message : fileEx.Message;
        //                EventWriter.WriteError(fileEx.Message, SeverityType.Critical);
        //            }
        //            catch(System.TypeLoadException loadEx)
        //            {
        //                string message = (loadEx.InnerException != null) ? loadEx.InnerException.Message : loadEx.Message;
        //                EventWriter.WriteError(message, SeverityType.Critical);
        //            }
        //            catch (ReflectionTypeLoadException reflexEx)
        //            {
        //                StringBuilder sb = new StringBuilder();
        //                foreach (var item in reflexEx.LoaderExceptions)
        //                {
        //                    sb.AppendLine(item.Message);
        //                }
        //                EventWriter.WriteError(sb.ToString(), SeverityType.Critical);
        //            }
        //            catch (Exception ex)
        //            {
        //                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        //                EventWriter.WriteError(message, SeverityType.Critical);
        //            }
        //        }
        //    }
        //    return b;
        //}

        #endregion instance methods
    }
}
