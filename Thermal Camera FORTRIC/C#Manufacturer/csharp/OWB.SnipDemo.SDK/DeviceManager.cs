using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace OWB.SnipDemo.SDK
{
    public class DeviceManager
    {
        private Camera _camera;

        public DeviceManager()
        {
            _camera = new Camera();
        }

        public bool Login(string ip, string username, string password)
        {
            return _camera.LoginCamera(ip, username, password);
        }

        public bool Logout()
        {
            bool logoutSuccess = false;
            if (_camera.IsConnected)
            {
                _camera.LogoutCamera();
                logoutSuccess = true;
            }

            return logoutSuccess;
        }

        public bool IsConnected => _camera.IsConnected;

        public int Width => _camera.Width;

        public int Height => _camera.Height;

        #region 视频流
        public bool IsPlay()
        {
            return _camera.IsPlay;
        }

        public bool StartStream(DeviceSDKType.LoginType loginType, DeviceSDKType.StreamType streamType)
        {
            return _camera.StartStream(loginType, streamType);
        }

        public void RefreshStream(DeviceSDKType.LoginType loginType, DeviceSDKType.StreamType streamType)
        {
            _camera.RefreshStream(loginType, streamType);
        }

        public void StopStream()
        {
            _camera.StopStream();
        }

        public Image GetH264Frame()
        {
            bool success = _camera.GetH264Frame(out Image img);
            return success ? img : null;
        }

        public Image GetRawFrame()
        {
            Image img = null;
            bool success = _camera.GetRawFrame(out ushort[,] values);
            if (success)
            {
                img = _camera.ToImage(_camera.GetCurrentPlt(), values);
            }

            return img;
        }

        #endregion

        #region 抓图

        public FileManager Snip()
        {
            if (!Directory.Exists(Constants.DefaultIRFolder))
            {
                Directory.CreateDirectory(Constants.DefaultIRFolder);
            }

            string fileName = Path.Combine(Constants.DefaultIRFolder, "IR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");

            IRtekIRFile irFile = new IRtekIRFile();
            try
            {
                ushort[,] rawValues = _camera.Snip();
                DateTime shipTime = DateTime.Now;

                if (irFile.Open(fileName, FileMode.Create))
                {
                    irFile.CameraName = _camera.Model;
                    irFile.CameraSeriesNo = _camera.SeriesNo;
                    irFile.RadiationType = _camera.RadiationType;

                    irFile.Width = _camera.Width;
                    irFile.Height = _camera.Height;

                    irFile.ReflectedTemperature = _camera.ReflectedTemperature;
                    irFile.AtmosphericTemperature = _camera.AtmosphericTemperature;
                    irFile.Distance = _camera.Distance;
                    irFile.Emissivity = _camera.Emissivity;
                    irFile.RelativeHumidity = _camera.RelativeHumidity;
                    irFile.ExternalOpticsTemperature = _camera.ExternalOpticsTemperature;
                    irFile.ExternalOpticsTransmission = _camera.ExternalOpticsTransmission;

                    irFile.ScaleMode = (int)ScaleMode.Auto;

                    irFile.PalName = GetCurrentPalette();
                    irFile.InvertedPalette = false;

                    irFile.SetFactoryLUT(_camera.ADArray, _camera.TArray);

                    irFile.AddFrame(rawValues, shipTime);
                    irFile.SetMarkerList(GetMarkerList());
                    irFile.CoverImage = irFile.IRFile.GetThermalImage();
                    irFile.StartTime = shipTime;
                    irFile.StopTime = shipTime;
                }
            }
            finally
            { }

            return new FileManager(irFile);
        }

        #endregion

        #region 调色板

        public string[] GetAllPalette()
        {
            return _camera.GetPalettes();
        }

        public string GetCurrentPalette()
        {
            return _camera.GetCurrentPlt();
        }

        public bool SetCurrentPalette(string paletteName)
        {
            return _camera.PutCurrentPlt(paletteName);
        }

        #endregion

        #region 测温参数

        public InstrumentJconfig GetTempParameter()
        {
            return _camera.GetInstrumentJconfig();
        }
        public bool SetTempParameter(InstrumentJconfig config)
        {
            return _camera.PutInstrumentJconfig(config);
        }

        #endregion

        #region 手动校准

        public void DoCalibrate()
        {
            _camera.PostCali();
        }

        #endregion

        #region 调焦

        public void NearFocus()
        {
            _camera.PostNearFocus();
        }

        public void FarFocus()
        {
            _camera.PostFarFocus();
        }

        public void AutoFocus()
        {
            _camera.PostIspAF();
        }

        #endregion

        #region 私有方法

        private List<FileSDKType.OWBFileMarker> GetMarkerList()
        {
            List<FileSDKType.OWBFileMarker> fileMarkers = new List<FileSDKType.OWBFileMarker>();
            List<Marker> markerItems = _camera.GetMarkerItems();
            foreach (Marker marker in markerItems)
            {
                if (marker is SpotMarker)
                {
                    SpotMarker spotMarker = marker as SpotMarker;
                    fileMarkers.Add(new FileSDKType.OWBFileSpotMarker(spotMarker.Label, false, spotMarker.Emission, new Point(spotMarker.Point.X, spotMarker.Point.Y), spotMarker.ReflectionTemp, spotMarker.Distance, spotMarker.Offset));
                }
                else if (marker is LineMarker)
                {
                    LineMarker lineMarker = marker as LineMarker;
                    Point[] linePoints = new Point[lineMarker.PointList.Length];
                    for (int i = 0; i < lineMarker.PointList.Length; i++)
                    {
                        linePoints[i] = new Point(lineMarker.PointList[i].X, lineMarker.PointList[i].Y);
                    }
                    fileMarkers.Add(new FileSDKType.OWBFilePolylineMarker(lineMarker.Label, false, lineMarker.Emission, linePoints, lineMarker.ReflectionTemp, lineMarker.Distance, lineMarker.Offset));
                }
                else if (marker is PolygonMarker)
                {
                    PolygonMarker polygonMarker = marker as PolygonMarker;
                    Point[] polygonPoints = new Point[polygonMarker.PointList.Length];
                    for (int i = 0; i < polygonMarker.PointList.Length; i++)
                    {
                        polygonPoints[i] = new Point(polygonMarker.PointList[i].X, polygonMarker.PointList[i].Y);
                    }
                    fileMarkers.Add(new FileSDKType.OWBFilePolylineMarker(polygonMarker.Label, false, polygonMarker.Emission, polygonPoints, polygonMarker.ReflectionTemp, polygonMarker.Distance, polygonMarker.Offset));
                }
            }

            return fileMarkers;
        }

        #endregion
    }

    public enum RadiationType
    {
        SB = 0,
        SH = 1,
    }

    public enum ScaleMode
    {
        Fixed = 1,
        Auto = 2,
        Intelligent = 3,
    }
}
