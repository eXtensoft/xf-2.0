// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// This class...
    /// </summary>
    [Serializable]
    public class SqlHelper
    {

        public static void ExecuteReader(Func<SqlConnection> connectionProvider, 
            Action<SqlCommand> initializeCommand, 
            Action<SqlDataReader> readerRead )
        {
            using (SqlConnection cn = connectionProvider())
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    initializeCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readerRead(reader);
                        }
                    }
                }
            }
        }

        public static List<T> ExecuteReader<T>(Func<SqlConnection> connectionProvider, 
            Action<SqlCommand> initializeCommand, 
            Action<SqlDataReader,
            List<T>> readerRead) where T : class, new()
        {
            List<T> list = new List<T>();
            using (SqlConnection cn = connectionProvider())
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    initializeCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readerRead(reader,list);
                        }
                    }
                }
            }
            return list;
        }

        public static bool TryExecuteReader<T>(Func<SqlConnection> connectionProvider, Action<SqlCommand> initializeCommand, Action<SqlDataReader, List<T>> readerRead, out List<T> list, out string outputMessage) where T : class, new()
        {
            string message = String.Empty;
            bool b = true;
            List<T> internalList = new List<T>();
            using (SqlConnection cn = connectionProvider())
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        initializeCommand(cmd);
                        try
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    while (reader.Read())
                                    {
                                        readerRead(reader, internalList);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    message = String.Format("SqlDataReader Borrower failed: {0}", ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            message = String.Format("SqlCommand.ExecuteReader failed: {0}", ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = String.Format("SqlConnection failed: {0}", ex.Message);
                }
            }
            list = internalList;
            outputMessage = message;
            return b;
        }


        public static void ExecuteReader<T>(Func<SqlConnection> connectionProvider, 
            Action<SqlCommand,T> initializeCommand, 
            Action<SqlDataReader> readerRead,T t)
        {
            using (SqlConnection cn = connectionProvider())
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    initializeCommand(cmd,t);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readerRead(reader);
                        }
                    }
                }
            }

        }


        public static void ExecuteNonQuery<T>( Func<SqlConnection> connectionProvider,
            Action<SqlCommand> initializeCommand, 
            List<T> list, 
            Action<SqlCommand, T> patchCommand)
        {
            using (SqlConnection cn = connectionProvider())
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    initializeCommand(cmd);
                    foreach (var item in list)
                    {
                        patchCommand(cmd, item);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        //public static List<T> ExecuteReader<T>(Func<SqlConnection> provideConnection,Action<SqlCommand> initializeCommand, Func<SqlDataReader, T> readerBorrower)
        //{
        //    List<T> list = new List<T>();
        //    ExecuteReader(provideConnection, initializeCommand, (reader) =>
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(readerBorrower(reader));
        //        }
        //    });
        //    return list;
        //}

        //public static void ExecuteReader(Func<SqlConnection> connectionProvider, Action<SqlCommand> initializeCommand, Action<SqlDataReader> readerBorrower)
        //{
        //    using (SqlConnection cn = connectionProvider.Invoke())
        //    {
        //        cn.Open();
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            initializeCommand(cmd);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                readerBorrower.Invoke(reader);
        //            }
        //        }
        //    }
        //}

    }

}

