using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace OWB.SnipDemo.SDK
{
    public class IRtekIRFile : IRFile
    {
        #region Fields
        private string _Name;
        private FileMode _FileMode;

        private CameraInfo _CameraInfo;
        private LensInfo _LensInfo;
        private List<PresetPalette> _PresetPalettes;
        private Palette _PaletteInfo;
        private ThermalParams _ThermalParamInfo;
        private Gain _GainInfo;
        private Reference _ReferenceInfo;
        private Alarm _AlarmInfo;
        private FileLUT _LUTInfo;
        private ViewInfo _ViewInfo;
        private GeoLocation _LocationInfo;

        private int _ImageMode;
        private int _Width;
        private int _Height;

        private IsothermItem _IsothermItem1;
        private IsothermItem _IsothermItem2;
        private IsothermItem _IsothermItem3;

        private DateTime _ShootTime;

        private string _QRCode;

        private string _IRTag;
        private string _Annotation;
        private bool _AttentionFlag;

        #endregion

        #region Constructors
        public IRtekIRFile()
        {
            IRFile = new ThermalImage();
            FileName = string.Empty;
            FileMode = FileMode.Create;
            QRCode = string.Empty;
            LUT = new float[LUT_SIZE];
            for (int i = 0; i < LUT.Length; i++)
            {
                LUT[i] = i;
            }

            _CameraInfo = CameraInfo.DEFAULT;
            _LensInfo = LensInfo.DEFAULT;
            _PaletteInfo = Palette.DEFAULT;
            _ThermalParamInfo = ThermalParams.DEFAULT;
            _GainInfo = Gain.DEFAULT;
            _ReferenceInfo = Reference.DEFAULT;
            _AlarmInfo = Alarm.DEFAULT;
            _ViewInfo = ViewInfo.DEFAULT;
            _LUTInfo = FileLUT.DEFAULT;
            _ViewInfo = ViewInfo.DEFAULT;
            _LocationInfo = GeoLocation.DEFAULT;
            _ShootTime = DateTime.MinValue;

            _IRTag = string.Empty;
            _Annotation = string.Empty;
            _AttentionFlag = false;
        }
        #endregion

        #region Properties
        public ThermalImage IRFile { get; set; }

        private string FileName
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public FileMode FileMode
        {
            get { return _FileMode; }
            private set { _FileMode = value; }
        }

        public override FileSDKType.IRFileType FileType
        {
            get { return FileSDKType.IRFileType.IR_FJPG; }
        }

        public Image CoverImage
        {
            get { return GetCoverImage(); }
            set { SetCoverImage(value); }
        }

        private float[] LUT { get; set; }

        public override string Name
        {
            get { return FileName; }
        }

        public override string CameraName
        {
            get { return _CameraInfo.Model; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _CameraInfo.Model = value;
                        IRFile.SetCameraInfo(_CameraInfo);
                    }
                }
            }
        }

        public override string CameraSeriesNo
        {
            get { return _CameraInfo.SerialNo; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _CameraInfo.SerialNo = value;
                        IRFile.SetCameraInfo(_CameraInfo);
                    }
                }
            }
        }

        public override string Brand
        {
            get { return _CameraInfo.Brand; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _CameraInfo.Brand = value;
                        IRFile.SetCameraInfo(_CameraInfo);
                    }
                }
            }
        }

        public override string Company
        {
            get { return _CameraInfo.Company; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _CameraInfo.Company = value;
                        IRFile.SetCameraInfo(_CameraInfo);
                    }
                }
            }
        }

        public override string LensName
        {
            get { return _LensInfo.Name; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LensInfo.Name = value;
                        IRFile.SetLensInfo(_LensInfo);
                    }
                }
            }
        }

        public override float LensF
        {
            get { return _LensInfo.FocalLength; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LensInfo.FocalLength = value;
                        IRFile.SetLensInfo(_LensInfo);
                    }
                }
            }
        }

        public override float LensD
        {
            get { return 0; }
            set
            {
                //
            }
        }

        public override OWBTemperatureRange TemperatureRange
        {
            get
            {
                if (IRFile != null)
                {
                    TempRange tempRange = IRFile.GetTempRange();
                    return new OWBTemperatureRange(tempRange.Min, tempRange.Max);
                }
                return OWBTemperatureRange.Default;
            }
            set
            {
                //
            }
        }

        public override string PalName
        {
            get
            {
                if (_PresetPalettes != null)
                {
                    return _PresetPalettes.Find(a => a.RefNo == _PaletteInfo.RefNo).Name;
                }
                return string.Empty;
            }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null && _PresetPalettes != null)
                    {
                        _PaletteInfo.RefNo = _PresetPalettes.Find(a => a.Name.Equals(value, StringComparison.OrdinalIgnoreCase)).RefNo;
                        IRFile.SetPalette(_PaletteInfo);
                    }
                }
            }
        }

        public override bool InvertedPalette
        {
            get { return _PaletteInfo.Inverted; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _PaletteInfo.Inverted = value;
                        IRFile.SetPalette(_PaletteInfo);
                    }
                }
            }
        }

        public override int Width
        {
            get { return _Width; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _Width = value;
                        IRFile.SetThermalSize(new Size(value, Height));
                    }
                }
            }
        }

        public override int Height
        {
            get { return _Height; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _Height = value;
                        IRFile.SetThermalSize(new Size(Width, value));
                    }
                }
            }
        }

        public override float ReflectedTemperature
        {
            get { return _ThermalParamInfo.ReflTemp; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.ReflTemp = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float AtmosphericTemperature
        {
            get { return _ThermalParamInfo.AtmTemp; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.AtmTemp = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float Distance
        {
            get { return _ThermalParamInfo.Distance; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.Distance = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float Emissivity
        {
            get { return _ThermalParamInfo.Emissivity; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.Emissivity = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float RelativeHumidity
        {
            get { return _ThermalParamInfo.RelHumidity; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.RelHumidity = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float ExternalOpticsTemperature
        {
            get { return _ThermalParamInfo.ExtOptTemp; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.ExtOptTemp = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override float ExternalOpticsTransmission
        {
            get { return _ThermalParamInfo.ExtOptTrans; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ThermalParamInfo.ExtOptTrans = value;
                        IRFile.SetThermalParams(_ThermalParamInfo);
                    }
                }
            }
        }

        public override int ScaleMode
        {
            get { return (int)_GainInfo.Mode; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _GainInfo.Mode = (GainMode)value;
                        IRFile.SetGain(_GainInfo);
                    }
                }
            }
        }

        public override float ScaleMax
        {
            get { return _GainInfo.Max; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _GainInfo.Max = value;
                        IRFile.SetGain(_GainInfo);
                    }
                }
            }
        }

        public override float ScaleMin
        {
            get { return _GainInfo.Min; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _GainInfo.Min = value;
                        IRFile.SetGain(_GainInfo);
                    }
                }
            }
        }

        public override IsothermItem IsothermItem1
        {
            get { return _IsothermItem1; }
            set { _IsothermItem1 = value; }
        }

        public override IsothermItem IsothermItem2
        {
            get { return _IsothermItem2; }
            set { _IsothermItem2 = value; }
        }

        public override IsothermItem IsothermItem3
        {
            get { return _IsothermItem3; }
            set { _IsothermItem3 = value; }
        }

        public override FileSDKType.OWBRefMarker RefMarker
        {
            get
            {
                FileSDKType.IRDeltaMarkerType type = FileSDKType.IRDeltaMarkerType.None;
                FileSDKType.IRTemperatureType valueType = FileSDKType.IRTemperatureType.None;
                if (_ReferenceInfo.Enabled)
                {
                    if (string.IsNullOrEmpty(_ReferenceInfo.Name))
                    {
                        type = FileSDKType.IRDeltaMarkerType.Temperature;
                        valueType = FileSDKType.IRTemperatureType.None;
                    }
                    else
                    {
                        type = FileSDKType.IRDeltaMarkerType.Marker;
                        valueType = (FileSDKType.IRTemperatureType)_ReferenceInfo.ValueType;
                    }
                }
                return new FileSDKType.OWBRefMarker(type, valueType, _ReferenceInfo.Name, _ReferenceInfo.Temp);
            }
            set
            {
                lock (FileLock)
                {
                    switch (value.DeltaMarkerType)
                    {
                        case FileSDKType.IRDeltaMarkerType.Marker:
                            _ReferenceInfo.Enabled = true;
                            switch (value.TemperatureType)
                            {
                                case FileSDKType.IRTemperatureType.Max:
                                    _ReferenceInfo.ValueType = ValueType.Max;
                                    break;
                                case FileSDKType.IRTemperatureType.Min:
                                    _ReferenceInfo.ValueType = ValueType.Min;
                                    break;
                                case FileSDKType.IRTemperatureType.Avg:
                                    _ReferenceInfo.ValueType = ValueType.Avg;
                                    break;
                            }
                            break;
                        case FileSDKType.IRDeltaMarkerType.Temperature:
                            _ReferenceInfo.Enabled = true;
                            break;
                        case FileSDKType.IRDeltaMarkerType.None:
                            _ReferenceInfo.Enabled = false;
                            break;
                    }
                    _ReferenceInfo.Name = value.Name;
                    _ReferenceInfo.Temp = value.Temperature;
                    IRFile.SetReference(_ReferenceInfo);
                }
            }
        }

        public override DateTime StartTime
        {
            get { return _ShootTime; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ShootTime = value;
                        IRFile.SetShotTime(_ShootTime);
                    }
                }
            }
        }

        public override DateTime StopTime
        {
            get { return _ShootTime; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ShootTime = value;
                        IRFile.SetShotTime(_ShootTime);
                    }
                }
            }
        }

        public override double Longitude
        {
            get { return _LocationInfo.Longitude; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LocationInfo.Longitude = value;
                        IRFile.SetGeoLocation(_LocationInfo);
                    }
                }
            }
        }

        public override double Latitude
        {
            get { return _LocationInfo.Latitude; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LocationInfo.Latitude = value;
                        IRFile.SetGeoLocation(_LocationInfo);
                    }
                }
            }
        }

        public override double Altitude
        {
            get { return _LocationInfo.Altitude; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LocationInfo.Altitude = value;
                        IRFile.SetGeoLocation(_LocationInfo);
                    }
                }
            }
        }

        public override short Direction
        {
            get { return _LocationInfo.Direction; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _LocationInfo.Direction = value;
                        IRFile.SetGeoLocation(_LocationInfo);
                    }
                }
            }
        }

        public override string QRCode
        {
            get { return _QRCode; }
            set { _QRCode = value; }
        }

        public override string IRTag
        {
            get { return _IRTag; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _IRTag = value;
                        //IRFile.SetTagAnnotation(IRTag);
                    }
                }
            }
        }

        public override string Annotation
        {
            get { return _Annotation; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _Annotation = value;
                        IRFile.SetNoteAnnotation(value);
                    }
                }
            }
        }

        public override bool AttentionFlag
        {
            get { return _AttentionFlag; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _AttentionFlag = value;
                        IRFile.SetFavorite(value);
                    }
                }
            }
        }

        public override int ImageMode
        {
            get
            {
                return _ImageMode;
            }
            set
            {
                _ImageMode = value;
                switch (value)
                {
                    case IR_IMAGE:
                        _ViewInfo.Type = ViewType.Thermal;
                        break;
                    case PICTURE_IN_PICTURE:
                        _ViewInfo.Type = ViewType.PicInPic;
                        break;
                    case MIX:
                        _ViewInfo.Type = ViewType.Fusion;
                        break;
                }
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        IRFile.SetViewInfo(_ViewInfo);
                    }
                }
            }
        }

        public override RectangleF PInPRectangleF
        {
            get
            {
                return _ViewInfo.Area;
            }
            set
            {
                if (IRFile != null)
                {
                    _ViewInfo.Area = value;
                    IRFile.SetViewInfo(_ViewInfo);
                }
            }
        }

        public override byte Transparency
        {
            get { return _ViewInfo.Alpha; }
            set
            {
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        _ViewInfo.Alpha = value;
                        IRFile.SetViewInfo(_ViewInfo);
                    }
                }
            }
        }

        public override IsothermItem Threshold
        {
            get
            {
                if (Math.Equals(_ViewInfo.Max, 0) && Math.Equals(_ViewInfo.Min, 0))
                {
                    return IsothermItem.Empty;
                }
                else
                {
                    return new IsothermItem(true, _ViewInfo.Max, _ViewInfo.Min);
                }
            }
            set
            {
                if (value.Enabled)
                {
                    _ViewInfo.Max = value.Max;
                    _ViewInfo.Min = value.Min;
                }
                else
                {
                    _ViewInfo.Max = 0.0f;
                    _ViewInfo.Min = 0.0f;
                }
                lock (FileLock)
                {
                    if (IRFile != null)
                    {
                        IRFile.SetViewInfo(_ViewInfo);
                    }
                }
            }
        }

        public override bool EmbededDCImage
        {
            get { return true; }
            set { }
        }

        public override int FrameIndex
        {
            get { return 0; }
            set { }
        }

        public override bool UseBaseFrame
        {
            get { return false; }
            set { }
        }

        public override int BaseFrameIndex
        {
            get { return 0; }
            set { }
        }

        public override int FrameCount
        {
            get { return 1; }
        }

        public override bool ReadOnly
        {
            get { return false; }
        }

        public override int IRSourceMode
        {
            get { return 1; }
        }
        #endregion

        #region Methods
        public override RadiationType GetRadiation()
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    return (RadiationType)_LUTInfo.RadMethod;
                }
                return RadiationType.SB;
            }
        }

        public void SetRadiation(RadiationType radiation)
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    _LUTInfo.RadMethod = (byte)radiation;
                    IRFile.SetFileLUT(_LUTInfo);
                }
            }
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            _Width = 384;
            _Height = 288;

            _IsothermItem1 = IsothermItem.Empty;
            _IsothermItem2 = IsothermItem.Empty;
            _IsothermItem3 = IsothermItem.Empty;
        }

        public override bool Open(string name, System.IO.FileMode fileMode)
        {
            lock (FileLock)
            {
                FileName = name;
                FileMode = fileMode;
                if (FileMode != System.IO.FileMode.Create && FileMode != System.IO.FileMode.Open)
                {
                    return false;
                }

                if (IsConnected)
                {
                    Close();
                }

                InitProperties();
                bool result = false;
                try
                {
                    switch (FileMode)
                    {
                        case System.IO.FileMode.Create:
                            result = IRFile.Create(name, SnipDemo.SDK.FileType.ThermalPicture);
                            if (result)
                            {
                                CreateProperties();
                            }
                            break;
                        case System.IO.FileMode.Open:
                            result = IRFile.Open(name);
                            if (result)
                            {
                                SyncProperties();
                            }
                            break;
                    }
                    if (result)
                    {
                        IsConnected = true;
                        return true;
                    }
                }
                catch
                {
                    Close();
                }
                return false;
            }
        }

        private void CreateProperties()
        {
            _PresetPalettes = ThermalImage.GetPresetPalettes();
        }

        private void SyncProperties()
        {
            lock (FileLock)
            {
                _CameraInfo = IRFile.GetCameraInfo();
                _LensInfo = IRFile.GetLensInfo();
                _PresetPalettes = ThermalImage.GetPresetPalettes();
                _PaletteInfo = IRFile.GetPalette();
                _Width = IRFile.GetThermalSize().Width;
                _Height = IRFile.GetThermalSize().Height;
                _ThermalParamInfo = IRFile.GetThermalParams();
                _GainInfo = IRFile.GetGain();
                _AlarmInfo = IRFile.GetAlarm();
                _ViewInfo = IRFile.GetViewInfo();
                _LUTInfo = IRFile.GetFileLUT();

                _ShootTime = IRFile.GetShotTime();

                _LocationInfo = IRFile.GetGeoLocation();
                //_IRTag = IRFile.GetTag();

                _Annotation = IRFile.GetNoteAnnotation();
                _AttentionFlag = IRFile.GetFavorite();
                _ViewInfo = IRFile.GetViewInfo();
            }
        }

        public override void Save()
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    IRFile.Save();
                }
            }
        }

        public override bool SaveAs(string fileName)
        {
            lock (FileLock)
            {
                bool success = false;

                if (IRFile != null)
                {
                    success = IRFile.SaveAs(fileName);
                }

                return success;
            }
        }

        public override void Close()
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    IRFile.Close();
                }
            }
        }

        public override float[] GetFactoryLUT()
        {
            float[] lut = new float[LUT_SIZE];
            if (_LUTInfo.ADArray == null || _LUTInfo.ADArray.Length == 0)
            {
                return lut;
            }
            int from = _LUTInfo.ADArray[0];
            int to = _LUTInfo.ADArray[0];
            int adLength = _LUTInfo.ADArray.Length;
            for (int i = 0; i < adLength - 1 && _LUTInfo.ADArray[i] < _LUTInfo.ADArray[i + 1]; i++)
            {
                int tmpFrom = _LUTInfo.ADArray[i];
                int tmpTo = _LUTInfo.ADArray[i + 1];
                to = tmpTo;
                float fromTemp = _LUTInfo.TempArray[i];
                float toTemp = _LUTInfo.TempArray[i + 1];
                for (int j = tmpFrom; j <= tmpTo; j++)
                {
                    lut[j] = fromTemp + (toTemp - fromTemp) * (j - tmpFrom) / (tmpTo - tmpFrom);
                }
            }
            for (int i = 0; i < from; i++)
            {
                lut[i] = lut[from];
            }
            for (int i = to + 1; i < LUT_SIZE; i++)
            {
                lut[i] = lut[to];
            }
            return lut;
        }

        public override void SetFactoryLUT(float[] lut)
        {

        }

        public override ushort[] GetLUTADArray()
        {
            return _LUTInfo.ADArray;
        }

        public override float[] GetLUTTempArray()
        {
            return _LUTInfo.TempArray;
        }

        public override void SetFactoryLUT(ushort[] adArray, float[] tempArray)
        {
            lock (FileLock)
            {
                _LUTInfo.ADArray = adArray;
                _LUTInfo.TempArray = tempArray;
                IRFile.SetFileLUT(_LUTInfo);
            }
        }

        public override byte[] GetSDKParam()
        {
            lock (FileLock)
            {
                return null;
            }
        }

        public void SetSDKParam(byte[] value)
        {
            lock (FileLock)
            {
                //IRFile.SetSDKParam(value);
            }
        }

        public override List<FileSDKType.OWBFileMarker> GetMarkerList()
        {
            lock (FileLock)
            {
                List<FileSDKType.OWBFileMarker> markerList = new List<FileSDKType.OWBFileMarker>();
                foreach (ThermalImage.Marker marker in IRFile.Markers)
                {
                    bool independentEmissivity = marker.ThermalParams.LocalParams;
                    switch (marker.Type)
                    {
                        case MarkerType.Spot:
                            ThermalImage.MarkerSpot spotMarker = marker as ThermalImage.MarkerSpot;
                            FileSDKType.OWBFileSpotMarker spotFileMarker = new FileSDKType.OWBFileSpotMarker(marker.Name, independentEmissivity, marker.ThermalParams.Emissivity, spotMarker.Point, marker.ThermalParams.ReflTemp, marker.ThermalParams.Distance, 0);
                            spotFileMarker.SetTemp(spotMarker.MaxInternal, spotMarker.MinInternal, spotMarker.AvgInternal);
                            markerList.Add(spotFileMarker);
                            break;
                        case MarkerType.Polyline:
                            ThermalImage.MarkerPolyline lineMarker = marker as ThermalImage.MarkerPolyline;
                            FileSDKType.OWBFilePolylineMarker polylinFileMarker = new FileSDKType.OWBFilePolylineMarker(marker.Name, independentEmissivity, marker.ThermalParams.Emissivity, lineMarker.Points, marker.ThermalParams.ReflTemp, marker.ThermalParams.Distance, 0);
                            polylinFileMarker.SetTemp(lineMarker.MaxInternal, lineMarker.MinInternal, lineMarker.AvgInternal);
                            markerList.Add(polylinFileMarker);
                            break;
                        case MarkerType.Polygon:
                            ThermalImage.MarkerPolygon polygonMarker = marker as ThermalImage.MarkerPolygon;
                            FileSDKType.OWBFilePolygonMarker polygonFileMarker = new FileSDKType.OWBFilePolygonMarker(marker.Name, independentEmissivity, marker.ThermalParams.Emissivity, polygonMarker.Points, marker.ThermalParams.ReflTemp, marker.ThermalParams.Distance, 0);
                            polygonFileMarker.SetTemp(polygonMarker.MaxInternal, polygonMarker.MinInternal, polygonMarker.AvgInternal);
                            markerList.Add(polygonFileMarker);
                            break;
                    }
                }
                return markerList;
            }
        }

        public override void SetMarkerList(List<FileSDKType.OWBFileMarker> markerList)
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    IRFile.Markers.Clear(ThermalImage.ClearType.UserMarkers);
                    foreach (FileSDKType.OWBFileMarker marker in markerList)
                    {
                        LocalThermalParams localThermalParams = new LocalThermalParams();
                        localThermalParams.Emissivity = marker.Emissivity;
                        switch (marker.MarkerType)
                        {
                            case FileSDKType.IRMarkerType.Spot:
                                ThermalImage.MarkerSpot spotMarker = new ThermalImage.MarkerSpot(IRFile, marker.Name, localThermalParams, (marker as FileSDKType.OWBFileSpotMarker).Point);
                                IRFile.Markers.Add(spotMarker);
                                break;
                            case FileSDKType.IRMarkerType.Polyline:
                                ThermalImage.MarkerPolyline lineMarker = new ThermalImage.MarkerPolyline(IRFile, marker.Name, localThermalParams, (marker as FileSDKType.OWBFilePolylineMarker).Points);
                                IRFile.Markers.Add(lineMarker);
                                break;
                            case FileSDKType.IRMarkerType.Polygon:
                                ThermalImage.MarkerPolygon polygonMarker = new ThermalImage.MarkerPolygon(IRFile, marker.Name, localThermalParams, (marker as FileSDKType.OWBFilePolygonMarker).Points);
                                IRFile.Markers.Add(polygonMarker);
                                break;
                        }
                    }
                }
            }
        }

        public override bool GetFrame(out ushort[,] values, out DateTime time)
        {
            lock (FileLock)
            {
                return GetFrame(0, out values, out time);
            }
        }

        public override bool GetFrame(int index, out ushort[,] values, out DateTime time)
        {
            lock (FileLock)
            {
                values = IRFile.GetFrame();
                time = _ShootTime;
                return true;
            }
        }

        public override void AddFrame(ushort[,] values, DateTime time)
        {
            lock (FileLock)
            {
                IRFile.SetFrame(values, time);
            }
        }

        private ushort GetADFromTemp(float value)
        {
            ushort low_index = 0;
            ushort high_index = LUT_SIZE - 1;
            while (low_index < high_index)
            {
                ushort mid_index = (ushort)((low_index + high_index) >> 1);
                if (value < LUT[mid_index])
                {
                    high_index = mid_index;
                }
                else
                {
                    low_index = (ushort)(mid_index + 1);
                }
            }

            return --low_index;
        }

        public override Stream GetDCImage()
        {
            MemoryStream stream = new MemoryStream();
            Bitmap dcImage = IRFile.GetVisualImage();
            if (dcImage != null)
            {
                dcImage.Save(stream, ImageFormat.Jpeg);
            }
            return stream;
        }

        public override void SetDCImage(Stream stream)
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    //if (stream != null)
                    //{
                    //    byte[] buf = new byte[stream.Length];
                    //    stream.Read(buf, 0, (int)stream.Length);
                    //    _ViewInfo.DCImage = buf;
                    //    IRFile.SetViewInfo(_ViewInfo);
                    //}
                }
            }
        }

        public override Stream GetLogoImage()
        {
            MemoryStream stream = new MemoryStream();

            if (_CameraInfo.Logo != null)
            {
                _CameraInfo.Logo.Save(stream, ImageFormat.Jpeg);
            }
            return stream;
        }

        public override void SetLogoImage(Stream stream)
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    if (stream != null)
                    {
                        _CameraInfo.Logo = new Bitmap(stream);
                        IRFile.SetCameraInfo(_CameraInfo);
                    }
                }
            }
        }

        public override Stream GetAudio()
        {
            lock (FileLock)
            {
                MemoryStream stream = new MemoryStream();
                AudioAnnotation audio = IRFile.GetAudioAnnotation();
                if (audio.Data != null)
                {
                    stream.Write(audio.Data, 0, audio.Data.Length);
                }
                return stream;
            }
        }

        public override void SetAudio(Stream stream)
        {
            lock (FileLock)
            {
                //if (stream != null)
                //{
                //    byte[] buf = new byte[stream.Length];
                //    stream.Read(buf, 0, (int)stream.Length);
                //    IRFile.SetVoice(VoiceType.MP3, buf);
                //}
            }
        }

        public void SetColorAlarmInfo()
        {
            lock (FileLock)
            {
                //IRFile.SetColorAlarmInfo(_AlarmInfo);
            }
        }

        public override bool BeginRotate()
        {
            return false;
        }

        public override void AddRotateFrame(bool appended, ushort[,] values, DateTime time)
        {

        }

        public override void EndRotate()
        {

        }

        private Image GetCoverImage()
        {
            lock (FileLock)
            {
                return IRFile.GetCoverImage();
            }
        }

        private void SetCoverImage(Image image)
        {
            lock (FileLock)
            {
                if (IRFile != null)
                {
                    if (image != null)
                    {
                        IRFile.SetCoverImage(new Bitmap(image));
                    }
                }
            }
        }

        public override float[] GetLUT()
        {
            return LUT.Clone() as float[];
        }

        public override float[] GetValues(Rectangle rectangle, LocalThermalParams thermalParams)
        {
            return IRFile.GetValues(rectangle, thermalParams);
        }

        #endregion
    }

    public abstract class IRFile : IDisposable
    {
        #region Fields
        protected object FileLock = new object();

        private bool _IsConnected; //连接状态

        public const int LUT_SIZE = 65536;

        public const int SCALE_MODE_FIXED = 1;
        public const int SCALE_MODE_AUTO = 2;
        public const int SCALE_MODE_INTELLIGENT = 3;

        public const int IR_IMAGE = 1;
        public const int DC_IMAGE = 2;
        public const int PICTURE_IN_PICTURE = 3;
        public const int MIX = 4;
        #endregion

        #region Constructors
        public IRFile()
        {
            InitProperties();
        }
        #endregion

        #region Properties
        public abstract FileSDKType.IRFileType FileType { get; }

        public abstract string Name { get; }

        public abstract string CameraName { get; set; }
        public abstract string CameraSeriesNo { get; set; }
        public abstract string Brand { get; set; }
        public abstract string Company { get; set; }

        public abstract string LensName { get; set; }
        public abstract float LensF { get; set; }
        public abstract float LensD { get; set; }
        public abstract OWBTemperatureRange TemperatureRange { get; set; }

        public abstract string PalName { get; set; }

        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public abstract float ReflectedTemperature { get; set; }
        public abstract float AtmosphericTemperature { get; set; }
        public abstract float Distance { get; set; }
        public abstract float Emissivity { get; set; }
        public abstract float RelativeHumidity { get; set; }
        public abstract float ExternalOpticsTemperature { get; set; }
        public abstract float ExternalOpticsTransmission { get; set; }

        public abstract int ScaleMode { get; set; }
        public abstract float ScaleMax { get; set; }
        public abstract float ScaleMin { get; set; }
        public abstract bool InvertedPalette { get; set; }
        public abstract IsothermItem IsothermItem1 { get; set; }
        public abstract IsothermItem IsothermItem2 { get; set; }
        public abstract IsothermItem IsothermItem3 { get; set; }

        public abstract FileSDKType.OWBRefMarker RefMarker { get; set; }

        public abstract DateTime StartTime { get; set; }
        public abstract DateTime StopTime { get; set; }

        public abstract double Longitude { get; set; }
        public abstract double Latitude { get; set; }
        public abstract double Altitude { get; set; }
        public abstract short Direction { get; set; }
        public abstract string QRCode { get; set; }

        public abstract string Annotation { get; set; }

        public abstract bool AttentionFlag { get; set; }

        public abstract string IRTag { get; set; }

        public abstract int ImageMode { get; set; }
        public abstract RectangleF PInPRectangleF { get; set; }
        public abstract byte Transparency { get; set; }
        public abstract IsothermItem Threshold { get; set; }
        public abstract bool EmbededDCImage { get; set; }

        public abstract int FrameIndex { get; set; }
        public abstract bool UseBaseFrame { get; set; }
        public abstract int BaseFrameIndex { get; set; }
        public abstract int FrameCount { get; }
        public virtual int IRSourceMode { get { return 2; } }

        public bool IsConnected
        {
            get { return _IsConnected; }
            protected set { _IsConnected = value; }
        }

        public abstract bool ReadOnly { get; }

        public virtual RadiationType RadiationType { get; set; }
        #endregion

        #region Methods
        public virtual RadiationType GetRadiation()
        {
            RadiationType result = RadiationType.SB;
            return result;
        }
        protected virtual void InitProperties()
        {
            IsConnected = false;
        }

        public abstract bool Open(string name, FileMode fileMode);
        public abstract void Save();
        public abstract bool SaveAs(string fileName);
        public abstract void Close();

        public abstract float[] GetLUT(); //当前LUT表
        public abstract float[] GetFactoryLUT(); //出厂LUT表
        public abstract void SetFactoryLUT(float[] lut); //出厂LUT表

        public virtual ushort[] GetLUTADArray()
        {
            return null;
        }

        public virtual float[] GetLUTTempArray()
        {
            return null;
        }

        public virtual void SetFactoryLUT(ushort[] adArray, float[] tempArray)
        {

        }

        public virtual byte[] GetSDKParam()
        {
            return null;
        }

        public abstract List<FileSDKType.OWBFileMarker> GetMarkerList();
        public abstract void SetMarkerList(List<FileSDKType.OWBFileMarker> markerList);

        public abstract bool GetFrame(out ushort[,] values, out DateTime time);
        public abstract bool GetFrame(int index, out ushort[,] values, out DateTime time);
        public abstract void AddFrame(ushort[,] values, DateTime time);

        public abstract Stream GetDCImage();
        public abstract void SetDCImage(Stream stream);

        public abstract Stream GetLogoImage();
        public abstract void SetLogoImage(Stream stream);

        public abstract Stream GetAudio();
        public abstract void SetAudio(Stream stream);

        public abstract bool BeginRotate();
        public abstract void AddRotateFrame(bool appended, ushort[,] values, DateTime time);
        public abstract void EndRotate();

        public abstract float[] GetValues(Rectangle rectangle, LocalThermalParams thermalParams);

        public virtual void Dispose()
        {

        }
        #endregion
    }

    public struct IsothermItem
    {
        public const float KelvinCelsiurZero = 273.15F;
        public const float KelvinCelsiurTwenty = 293.15F;

        private bool _Enabled;
        private float _Max;
        private float _Min;

        private const float MinSpan = 0.1F;

        public static IsothermItem Empty = new IsothermItem(false, KelvinCelsiurTwenty, KelvinCelsiurZero);

        public IsothermItem(bool enabled, float max, float min)
        {
            _Enabled = enabled;
            if (max < MinSpan)
            {
                max = MinSpan;
            }
            if (max > 5000.0F)
            {
                max = 5000.0F;
            }
            _Max = max;
            if (min < 0.0F)
            {
                min = 0.0F;
            }
            if (min > 5000.0F - MinSpan)
            {
                min = 5000.0F - MinSpan;
            }
            _Min = min;

            if (_Max < _Min + MinSpan)
            {
                throw new InvalidProgramException();
            }
        }

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public float Max
        {
            get { return _Max; }
            set
            {
                if (value < MinSpan)
                {
                    value = MinSpan;
                }
                if (value > 5000.0F)
                {
                    value = 5000.0F;
                }

                _Max = value;

                if (_Min > _Max - MinSpan)
                {
                    _Min = _Max - MinSpan;
                }
            }
        }

        public float Min
        {
            get { return _Min; }
            set
            {
                if (value < 0.0F)
                {
                    value = 0.0F;
                }
                if (value > 5000.0F - MinSpan)
                {
                    value = 5000.0F - MinSpan;
                }

                _Min = value;

                if (_Max < _Min + MinSpan)
                {
                    _Max = _Min + MinSpan;
                }
            }
        }

        public override int GetHashCode()
        {
            return Enabled.GetHashCode() + Max.GetHashCode() + Min.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IsothermItem))
            {
                return false;
            }

            IsothermItem item = (IsothermItem)obj;

            if (Enabled == item.Enabled && Math.Equals(Max, item.Max) && Math.Equals(Min, item.Min))
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(IsothermItem t1, IsothermItem t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(IsothermItem t1, IsothermItem t2)
        {
            return !(t1.Equals(t2));
        }
    }

    public struct OWBTemperatureRange
    {
        #region Fields
        public float RangeMax;
        public float RangeMin;
        public object Tag;
        public static OWBTemperatureRange Default = new OWBTemperatureRange(253.15F, 393.15F);
        #endregion

        #region Constructors
        public OWBTemperatureRange(float rangeMin, float rangeMax)
        {
            RangeMin = rangeMin;
            RangeMax = rangeMax;
            Tag = null;
        }
        #endregion

        #region Methods

        public static bool operator ==(OWBTemperatureRange t1, OWBTemperatureRange t2)
        {
            return Math.Equals(t1.RangeMin, t2.RangeMin) && Math.Equals(t1.RangeMax, t2.RangeMax);
        }

        public static bool operator !=(OWBTemperatureRange t1, OWBTemperatureRange t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            return obj is OWBTemperatureRange && this == (OWBTemperatureRange)obj;
        }

        public override int GetHashCode()
        {
            return RangeMin.GetHashCode();
        }

        #endregion
    }
}
