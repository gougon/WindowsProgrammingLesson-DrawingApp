﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class GoogleDriveHelper
    {
        private GoogleDriveService _service;

        public GoogleDriveHelper()
        {
            _service = new GoogleDriveService(Constant.FORM_APPLICATION_NAME, Constant.CLIENT_SECRET_FILE_NAME);
        }

        // save
        public void Save(List<Shape> shapes)
        {
            SaveFile(shapes, System.Environment.CurrentDirectory);
            UploadShapesInformation(Constant.FILE_NAME, System.Environment.CurrentDirectory + Constant.BACK_SLASH + Constant.FILE_NAME);
        }

        // 儲存 shapes information 成 json file
        private void SaveFile(List<Shape> shapes, string directory)
        {
            ShapesConverter converter = new ShapesConverter();
            converter.ConvertToText(shapes);
            string text = converter.Text;
            string currentDirectory = directory;
            string filePath = Constant.FILE_NAME;
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, false);
            file.Write(text);
            file.Close();
            file.Dispose();
        }

        // 上傳 json file 到雲端
        private void UploadShapesInformation(string fileName, string filePath)
        {
            DeleteFile(fileName);
            _service.UploadFile(filePath, Constant.CONTENT_TYPE);
        }

        // 刪除重複的檔案
        private void DeleteFile(string fileName)
        {
            List<Google.Apis.Drive.v2.Data.File> files = _service.ListRootFileAndFolder();
            FindRepeatFileAndDelete(files, Constant.FILE_NAME);
        }

        // 遍歷 files 並刪除符合名稱的 file
        private void FindRepeatFileAndDelete(List<Google.Apis.Drive.v2.Data.File> files, string fileName)
        {
            foreach (Google.Apis.Drive.v2.Data.File file in files)
            {
                _service.DeleteDuplicatedFile(file, fileName);
            }
        }

        // load
        public void Load(List<Shape> shapes)
        {
            DownloadFile();
            LoadInformationToShapes(shapes);
        }

        // 下載 json 檔
        private void DownloadFile()
        {
            List<Google.Apis.Drive.v2.Data.File> files = _service.ListRootFileAndFolder();
            FindRepeatFileAndDownload(files, Constant.FILE_NAME);
        }

        // 遍歷 files 並下載符合名稱的 file
        private void FindRepeatFileAndDownload(List<Google.Apis.Drive.v2.Data.File> files, string fileName)
        {
            foreach (Google.Apis.Drive.v2.Data.File file in files)
            {
                if (file.Title == fileName)
                {
                    _service.DownloadFile(file, System.Environment.CurrentDirectory);
                }
            }
        }

        // 將 json 資訊讀入 shapes
        private void LoadInformationToShapes(List<Shape> shapes)
        {
            shapes.Clear();
            string text = ReadFile(Constant.FILE_NAME);
            ShapesConverter converter = new ShapesConverter();
            converter.ConvertToShapes(text);
            List<Shape> textToShapes = converter.Shapes;
            foreach (Shape shape in textToShapes)
            {
                shapes.Add(shape.Clone() as Shape);
            }
        }

        // 讀取 json file
        private string ReadFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
    }
}
