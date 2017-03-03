

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.IO;
    using System.Data.SqlTypes;
    //using Microsoft.Win32;

    public static class SqlFileStreamer
    {

        public static bool TryStore(MetaFile metaFile, string connectionString, out int fileId)
        {
            bool b = false;
            fileId = 0;

            const string fileIdParamName = "@fileid";
            const string tagsParamName = "@tags";
            const string nameParamName = "@name";
            const string fileTypeParamName = "@filetype";
            const string filePathParamName = "@filepath";
            const string sizeParamName = "@size";
            const string mimeParamName = "@mime";
            const string extParamName = "@ext";
            const string addedByParamName = "@addedBy";
            const string idParamName = "@id";
            byte[] fileData = File.ReadAllBytes(metaFile.Filepath);

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlTransaction sqlTrans = cn.BeginTransaction();

                    try
                    {
                        using (SqlCommand contextCmd = cn.CreateCommand())
                        using (SqlCommand filestreamCommand = cn.CreateCommand())
                        using (SqlCommand metadataCommand = cn.CreateCommand())
                        {
                            filestreamCommand.CommandType = CommandType.Text;
                            filestreamCommand.CommandText = "insert into [filesystem].[file] ([Data],[FileGuid]) values ((0x),@id) select SCOPE_IDENTITY()";
                            filestreamCommand.Parameters.AddWithValue(idParamName, metaFile.FileGuid);
                            filestreamCommand.Transaction = sqlTrans;


                            contextCmd.CommandType = CommandType.Text;
                            contextCmd.CommandText = "select get_filestream_transaction_context()";
                            contextCmd.Transaction = sqlTrans;
                            // create transaction context
                            object transactionContext = contextCmd.ExecuteScalar();
                            // prepare context to select path
                            contextCmd.CommandText = "select Data.PathName() from [filesystem].[File] where [FileGuid] = " + fileIdParamName;
                            contextCmd.Parameters.AddWithValue(fileIdParamName, metaFile.FileGuid);

                            // empty filestream insert
                            object o = filestreamCommand.ExecuteScalar();


                            if (o != null && Int32.TryParse(o.ToString(), out fileId))
                            {

                                metadataCommand.CommandType = CommandType.Text;
                                metadataCommand.CommandText = "insert into [filesystem].[metadata] ([fileid],[tags],[name],[filetype],[Size],[mime],[extension],[addedby],[filepath]) values (" +
                                fileIdParamName + "," + tagsParamName + "," + nameParamName + "," + fileTypeParamName + "," + sizeParamName + "," + mimeParamName + "," +
                                extParamName + "," + addedByParamName + "," + filePathParamName + ")";
                                metadataCommand.Transaction = sqlTrans;

                                metadataCommand.Parameters.AddWithValue(fileIdParamName, fileId);
                                metadataCommand.Parameters.AddWithValue(tagsParamName, metaFile.TagText);
                                metadataCommand.Parameters.AddWithValue(nameParamName, metaFile.Name);
                                metadataCommand.Parameters.AddWithValue(fileTypeParamName, metaFile.FileType);
                                metadataCommand.Parameters.AddWithValue(sizeParamName, metaFile.Size);
                                metadataCommand.Parameters.AddWithValue(mimeParamName, metaFile.Mime);
                                metadataCommand.Parameters.AddWithValue(extParamName, metaFile.Extension);
                                metadataCommand.Parameters.AddWithValue(addedByParamName, metaFile.AddedBy);
                                metadataCommand.Parameters.AddWithValue(filePathParamName, metaFile.Filepath);

                                // insert file metadata
                                int recordsAffected = metadataCommand.ExecuteNonQuery();
                                // fetch filestream server path
                                string filepathInServer = (string)contextCmd.ExecuteScalar();

                                // write to server filestream
                                SqlFileStream stream = new SqlFileStream(filepathInServer, (byte[])transactionContext, FileAccess.Write);
                                stream.Write(fileData, 0, fileData.Length);
                                stream.Close();
                            }
                        }
                        sqlTrans.Commit();
                        b = true;
                    }
                    catch (Exception ex)
                    {
                        sqlTrans.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return b;
        }

        public static bool TryFetch(int fileId, string connectionString, out byte[] data, out string fileName)
        {
            data = new byte[500000000];
            fileName = String.Empty;
            bool b = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlTransaction trans = cn.BeginTransaction())
                    using (SqlCommand contextCmd = cn.CreateCommand())
                    using (SqlCommand selectCmd = cn.CreateCommand())
                    {
                        contextCmd.Transaction = trans;
                        selectCmd.Transaction = trans;

                        contextCmd.CommandType = CommandType.Text;
                        contextCmd.CommandText = "select get_filestream_transaction_context()";
                        var ctx = contextCmd.ExecuteScalar();

                        const string fileIdParamName = "@fileid";
                        selectCmd.CommandType = CommandType.Text;
                        selectCmd.CommandText = "SELECT f.Data.PathName() AS FilestreamPath, m.Name FROM filesystem.[file] AS f INNER JOIN " +
                         "filesystem.metadata AS m ON f.FileId = m.FileId WHERE (f.FileId = " + fileIdParamName + " )";
                        selectCmd.Parameters.AddWithValue(fileIdParamName, fileId);
                        string serverpath = String.Empty;
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                fileName = reader.GetString(1);
                                serverpath = reader.GetString(0);
                            }

                        }
                        var sqlfilestream = new SqlFileStream(serverpath, (byte[])ctx, FileAccess.Read);
                        sqlfilestream.Seek(0L, SeekOrigin.Begin);
                        var byteAmt = sqlfilestream.Read(data, 0, 500000000);
                        sqlfilestream.Close();
                        trans.Commit();
                        Array.Resize<byte>(ref data, byteAmt);
                        b = true;
                    }
                }
            }
            catch (Exception ex)
            {

                b = false;
            }

            return b;
        }

        //public static void Download(int fileId, string connectionString)
        //{
        //    byte[] fileData;
        //    string fileName;
        //    if (TryFetch(fileId, connectionString, out fileData, out fileName))
        //    {
        //        SaveFileDialog dialog = new SaveFileDialog();
        //        dialog.FileName = fileName;
        //        dialog.Filter = "All files (*.*)|*.*";
        //        dialog.OverwritePrompt = true;
        //        var result = dialog.ShowDialog();
        //        if (result.HasValue && result.Value)
        //        {
        //            File.WriteAllBytes(dialog.FileName, fileData);
        //        }
        //    }
        //}

    }
}

