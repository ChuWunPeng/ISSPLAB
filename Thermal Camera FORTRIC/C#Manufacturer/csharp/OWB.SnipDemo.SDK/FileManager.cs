using System;
using System.Drawing;
using System.Collections.Generic;

namespace OWB.SnipDemo.SDK
{
    public class FileManager
    {
        private IRtekIRFile _irFile;
        public FileManager(IRtekIRFile irFile)
        {
            _irFile = irFile;
        }

        public int Width => _irFile.Width;

        public int Height => _irFile.Height;

        /// <summary>
        /// 保存图片至指定的文件夹
        /// </summary>
        /// <param name="filename">保存文件的全路径和名称：D:\IRFile\aaa.jpg</param>
        /// <returns></returns>
        public bool SaveAs(string filename)
        {
            return _irFile.SaveAs(filename);
        }

        /// <summary>
        /// 获取调色板图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetPaletteImage()
        {
            return _irFile.IRFile.GetPaletteImage();
        }

        /// <summary>
        /// 获取整幅图像的温度数组
        /// </summary>
        /// <returns></returns>
        public float[,] GetTemperature()
        {
            Rectangle rectangle = new Rectangle(0, 0, _irFile.Width, _irFile.Height);

            LocalThermalParams thermalParams = new LocalThermalParams
            {
                LocalParams = true,
                Emissivity = _irFile.Emissivity,
                Distance = _irFile.Distance,
                ReflTemp = _irFile.ReflectedTemperature
            };

            float[] values = _irFile.GetValues(rectangle, thermalParams);

            float[,] tempValues = new float[Width, Height];

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    tempValues[x, y] = values[index++];
                }
            }

            return tempValues;
        }

        /// <summary>
        /// 获取所有的测温对象
        /// </summary>
        /// <returns></returns>
        public List<FileSDKType.OWBFileMarker> GetMarkerList()
        {
            return _irFile.GetMarkerList();
        }

        /// <summary>
        /// 设置测温对象
        /// </summary>
        /// <param name="markerList"></param>
        public void SetMarkerList(List<FileSDKType.OWBFileMarker> markerList)
        {
            _irFile.SetMarkerList(markerList);
        }
    }
}
