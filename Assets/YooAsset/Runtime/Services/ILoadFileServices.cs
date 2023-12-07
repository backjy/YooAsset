using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

namespace YooAsset
{
    public interface ILoadFileServices
    {
        /// <summary>
        /// �ж��ļ��Ƿ����
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Exists( string filePath);

        /// <summary>
        /// ��ȡ�ļ����� byte[]
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public byte[] ReadAllBytes( string filePath );

        /// <summary>
        /// ��ȡ�ļ����� string
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ReadAllText( string filePath );

        /// <summary>
        /// д��bytes
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public void WriteAllBytes( string filePath, byte[] data );

        /// <summary>
        /// д���ı�
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public void WriteAllText( string filePath, string text );

        /// <summary>
        /// ��ȡ�ļ���С
        /// </summary>
        /// <param name="filePath"></param>
        public long GetFileSize( string filePath );
    }

    internal class DefaultLoadFileServices : ILoadFileServices
    {
        public bool Exists(string filePath)
        {
            return File.Exists( filePath);
        }

        public long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        public byte[] ReadAllBytes(string filePath)
        {
            if (File.Exists(filePath) == false)
                return null;
            return File.ReadAllBytes(filePath);
        }

        public string ReadAllText(string filePath)
        {
            if (File.Exists(filePath) == false)
                return string.Empty;
            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        public void WriteAllBytes(string filePath, byte[] data)
        {
            // �����ļ���·��
            CreateFileDirectory(filePath);

            File.WriteAllBytes(filePath, data);
        }

        public void WriteAllText(string filePath, string content)
        {
            // �����ļ���·��
            CreateFileDirectory(filePath);
            //����д��BOM���
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// �����ļ����ļ���·��
        /// </summary>
        public static void CreateFileDirectory(string filePath)
        {
            // ��ȡ�ļ����ļ���·��
            string directory = Path.GetDirectoryName(filePath);
            if (Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);
        }
    }
}

