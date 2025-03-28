﻿using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OWB.SnipDemo.SDK
{
    public class ThermalImage : ImageBase
    {
        private const string USER_DATA_KEY_FAVORITE = "FAVORITE";
        private const string USER_DATA_KEY_TAG = "TAG";
        private const string USER_DATA_KEY_GROUPFILES = "GROUPFILES";
        private const string USER_DATA_KEY_TIMERFILE = "TIMERFILE";

        private const string USER_DATA_SEPARATOR = "\n";
        private const string USER_DATA_FAVORITE_YES = "Y";
        private const string USER_DATA_FAVORITE_NO = "N";

        private const string SPOT_PREFIX = "Sp";
        private const string AREA_PREFIX = "Ar";
        private const string POLYLINE_PREFIX = "Li";
        private const string DELTA_PREFIX = "De";

        public event EventHandler ReferenceChanged = null;
        public event MarkerEventHandler MarkerAdded = null;
        public event MarkerEventHandler MarkerChanged = null;
        public event MarkerEventHandler MarkerRenamed = null;
        public event MarkerEventHandler MarkerRemoved = null;
        public event DeltaEventHandler DeltaAdded = null;
        public event DeltaEventHandler DeltaChanged = null;
        public event DeltaEventHandler DeltaRemoved = null;
        public event EventHandler ImageChanged = null;
        public event EventHandler PaletteChanged = null;
        public event EventHandler GainChanged = null;
        public event EventHandler RotationTypeChanged = null;
        public event EventHandler MirrorTypeChanged = null;
        public event EventHandler ViewInfoChanged = null;
        public event EventHandler MappingInfoChanged = null;
        public event EventHandler AlarmChanged = null;

        public ThermalImage()
        {
            IsMarkersSynced = false;
            Markers = new MarkerCollection(this);
            IsDeltasSynced = false;
            Deltas = new DeltaCollection(this);
            ReferenceCache = Reference.DEFAULT;
        }

        private bool IsMarkersSynced { get; set; }

        public MarkerCollection Markers { get; private set; }

        private bool IsDeltasSynced { get; set; }

        public DeltaCollection Deltas { get; private set; }

        private Reference ReferenceCache { get; set; }

        public ThermalParams GetThermalParams()
        {
            if (HImage == IntPtr.Zero)
            {
                return ThermalParams.DEFAULT;
            }

            IRtekFileSDK.ir_file_thermal_params irParams = new IRtekFileSDK.ir_file_thermal_params();
            int result = IRtekFileSDK.ir_file_get_thermal_params(HImage, ref irParams);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                ThermalParams value = new ThermalParams();
                value.ReflTemp = irParams.reflTemp;
                value.AtmTemp = irParams.atmTemp;
                value.Distance = irParams.distance;
                value.RelHumidity = irParams.relHumidity;
                value.Emissivity = irParams.emissivity;
                value.ExtOptTemp = irParams.extOptTemp;
                value.ExtOptTrans = irParams.extOptTrans;
                return value;
            }
            else
            {
                return ThermalParams.DEFAULT;
            }
        }

        public bool SetThermalParams(ThermalParams value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_thermal_params irParams = new IRtekFileSDK.ir_file_thermal_params();
            irParams.reflTemp = value.ReflTemp;
            irParams.atmTemp = value.AtmTemp;
            irParams.distance = value.Distance;
            irParams.relHumidity = value.RelHumidity;
            irParams.emissivity = value.Emissivity;
            irParams.extOptTemp = value.ExtOptTemp;
            irParams.extOptTrans = value.ExtOptTrans;
            int result = IRtekFileSDK.ir_file_set_thermal_params(HImage, ref irParams);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandleThermalParamsChangedEvent();

            Gain gain = GetGain();
            if (gain.Mode == GainMode.Auto)
            {
                HandleGainChangedEvent();
            }

            return true;
        }

        public CameraInfo GetCameraInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return CameraInfo.DEFAULT;
            }

            IRtekFileSDK.ir_file_camera_info irCameraInfo = new IRtekFileSDK.ir_file_camera_info();
            int result = IRtekFileSDK.ir_file_get_camera_info(HImage, ref irCameraInfo);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                CameraInfo value = new CameraInfo();
                value.Model = BytesToString(irCameraInfo.model, Encoding.UTF8);
                value.SerialNo = BytesToString(irCameraInfo.serialNo, Encoding.UTF8);
                value.Brand = BytesToString(irCameraInfo.brand, Encoding.UTF8);
                value.Company = BytesToString(irCameraInfo.company, Encoding.UTF8);
                if (irCameraInfo.logoLength != 0)
                {
                    try
                    {
                        irCameraInfo.logo = Marshal.AllocHGlobal(irCameraInfo.logoLength);
                        IRtekFileSDK.ir_file_get_camera_info(HImage, ref irCameraInfo);
                        byte[] buffer = new byte[irCameraInfo.logoLength];
                        Marshal.Copy(irCameraInfo.logo, buffer, 0, irCameraInfo.logoLength);
                        MemoryStream ms = new MemoryStream(buffer);
                        value.Logo = new Bitmap(ms);
                    }
                    catch (Exception)
                    {
                        value.Logo = null;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irCameraInfo.logo);
                    }
                }
                else
                {
                    value.Logo = null;
                }

                return value;
            }
            else
            {
                return CameraInfo.DEFAULT;
            }
        }

        public bool SetCameraInfo(CameraInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_camera_info irCameraInfo = new IRtekFileSDK.ir_file_camera_info();
            irCameraInfo.model = StringToBytes(value.Model, IRtekFileSDK.MODEL_LEN, Encoding.UTF8);
            irCameraInfo.serialNo = StringToBytes(value.SerialNo, IRtekFileSDK.SERIAL_NO_LEN, Encoding.UTF8);
            irCameraInfo.brand = StringToBytes(value.Brand, IRtekFileSDK.BRAND_LEN, Encoding.UTF8);
            irCameraInfo.company = StringToBytes(value.Company, IRtekFileSDK.COMPANY_LEN, Encoding.UTF8);
            irCameraInfo.logo = IntPtr.Zero;
            irCameraInfo.logoLength = 0;
            if (value.Logo != null)
            {
                MemoryStream ms = new MemoryStream();
                value.Logo.Save(ms, ImageFormat.Jpeg);
                byte[] buffer = ms.GetBuffer();
                try
                {
                    irCameraInfo.logoLength = (int)ms.Length;
                    irCameraInfo.logo = Marshal.AllocHGlobal(irCameraInfo.logoLength);
                    Marshal.Copy(buffer, 0, irCameraInfo.logo, irCameraInfo.logoLength);
                }
                finally
                {
                    Marshal.FreeHGlobal(irCameraInfo.logo);
                }
            }

            int result = IRtekFileSDK.ir_file_set_camera_info(HImage, ref irCameraInfo);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public Bitmap GetCoverImage()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            IRtekFileSDK.ir_file_buffer irBuffer = new IRtekFileSDK.ir_file_buffer();
            IRtekFileSDK.ir_file_get_cover_image(HImage, ref irBuffer);
            if (irBuffer.length != 0)
            {
                try
                {
                    irBuffer.buffer = Marshal.AllocHGlobal(irBuffer.length);
                    IRtekFileSDK.ir_file_get_cover_image(HImage, ref irBuffer);
                    byte[] buffer = new byte[irBuffer.length];
                    Marshal.Copy(irBuffer.buffer, buffer, 0, irBuffer.length);
                    MemoryStream ms = new MemoryStream(buffer);
                    return new Bitmap(ms);
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(irBuffer.buffer);
                }
            }
            else
            {
                return null;
            }
        }

        public bool SetCoverImage(Bitmap value)
        {
            if (HImage == IntPtr.Zero || value == null)
            {
                return false;
            }

            MemoryStream ms = new MemoryStream();
            value.Save(ms, ImageFormat.Jpeg);
            byte[] buffer = ms.GetBuffer();
            IRtekFileSDK.ir_file_buffer irBuffer = new IRtekFileSDK.ir_file_buffer();

            try
            {
                irBuffer.length = (int)ms.Length;
                irBuffer.buffer = Marshal.AllocHGlobal(irBuffer.length);
                Marshal.Copy(buffer, 0, irBuffer.buffer, irBuffer.length);

                int result = IRtekFileSDK.ir_file_set_cover_image(HImage, ref irBuffer);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return false;
                }

                IsModified = true;
            }
            finally
            {
                Marshal.FreeHGlobal(irBuffer.buffer);
            }

            return true;
        }

        public Bitmap GetVisualImage()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            IRtekFileSDK.ir_file_buffer irBuffer = new IRtekFileSDK.ir_file_buffer();
            IRtekFileSDK.ir_file_get_visual_image(HImage, ref irBuffer);
            if (irBuffer.length != 0)
            {
                try
                {
                    irBuffer.buffer = Marshal.AllocHGlobal(irBuffer.length);
                    IRtekFileSDK.ir_file_get_visual_image(HImage, ref irBuffer);
                    byte[] buffer = new byte[irBuffer.length];
                    Marshal.Copy(irBuffer.buffer, buffer, 0, irBuffer.length);
                    MemoryStream ms = new MemoryStream(buffer);
                    return new Bitmap(ms);
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(irBuffer.buffer);
                }
            }
            else
            {
                return null;
            }
        }

        public Bitmap GetPaletteImage()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            try
            {
                int width = 1;
                int height = IRtekFileSDK.COLORS_LEN;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                IRtekFileSDK.ir_file_image irImage = new IRtekFileSDK.ir_file_image();
                irImage.offset = bmpData.Stride - 3 * width;
                irImage.data = bmpData.Scan0;
                IRtekFileSDK.ir_file_get_palette_image(HImage, ref irImage);
                bitmap.UnlockBits(bmpData);
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Bitmap GetThermalImage()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            try
            {
                Size thermalSize = GetThermalSize();
                int width = thermalSize.Width;
                int height = thermalSize.Height;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                IRtekFileSDK.ir_file_image irImage = new IRtekFileSDK.ir_file_image();
                irImage.offset = bmpData.Stride - 3 * width;
                irImage.data = bmpData.Scan0;
                IRtekFileSDK.ir_file_get_thermal_image(HImage, ref irImage);
                bitmap.UnlockBits(bmpData);
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LensInfo GetLensInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return LensInfo.DEFAULT;
            }

            IRtekFileSDK.ir_file_lens_info irLensInfo = new IRtekFileSDK.ir_file_lens_info();
            int result = IRtekFileSDK.ir_file_get_lens_info(HImage, ref irLensInfo);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                LensInfo value = new LensInfo();
                value.Name = BytesToString(irLensInfo.name, Encoding.UTF8);
                value.FocalLength = irLensInfo.focalLength;
                value.HFov = irLensInfo.hfov;
                return value;
            }
            else
            {
                return LensInfo.DEFAULT;
            }
        }

        public bool SetLensInfo(LensInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_lens_info irLensInfo = new IRtekFileSDK.ir_file_lens_info();
            irLensInfo.name = StringToBytes(value.Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            irLensInfo.focalLength = value.FocalLength;
            irLensInfo.hfov = value.HFov;

            int result = IRtekFileSDK.ir_file_set_lens_info(HImage, ref irLensInfo);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public TempRange GetTempRange()
        {
            if (HImage == IntPtr.Zero)
            {
                return TempRange.DEFAULT;
            }

            IRtekFileSDK.ir_file_temp_range irTempRange = new IRtekFileSDK.ir_file_temp_range();
            int result = IRtekFileSDK.ir_file_get_temp_range(HImage, ref irTempRange);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                TempRange value = new TempRange();
                value.Min = irTempRange.min;
                value.Max = irTempRange.max;
                return value;
            }
            else
            {
                return TempRange.DEFAULT;
            }
        }

        public Size GetThermalSize()
        {
            if (HImage == IntPtr.Zero)
            {
                return new Size(0, 0);
            }

            IRtekFileSDK.ir_file_size irSize = new IRtekFileSDK.ir_file_size();
            int result = IRtekFileSDK.ir_file_get_thermal_size(HImage, ref irSize);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                Size value = new Size(irSize.width, irSize.height);
                return value;
            }
            else
            {
                return new Size(0, 0);
            }
        }

        public bool SetThermalSize(Size value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_size irSize = new IRtekFileSDK.ir_file_size();
            irSize.width = (short)value.Width;
            irSize.height = (short)value.Height;

            int result = IRtekFileSDK.ir_file_set_thermal_size(HImage, ref irSize);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public GeoLocation GetGeoLocation()
        {
            if (HImage == IntPtr.Zero)
            {
                return GeoLocation.DEFAULT;
            }

            IRtekFileSDK.ir_file_geo_location irGeoLocation = new IRtekFileSDK.ir_file_geo_location();
            int result = IRtekFileSDK.ir_file_get_geo_location(HImage, ref irGeoLocation);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                GeoLocation value = new GeoLocation();
                value.IsGPSValid = irGeoLocation.isGPSValid == TRUE;
                value.Longitude = irGeoLocation.longitude;
                value.Latitude = irGeoLocation.latitude;
                value.Altitude = irGeoLocation.altitude;
                value.IsCompassValid = irGeoLocation.isCompassValid == TRUE;
                value.Direction = irGeoLocation.direction;
                return value;
            }
            else
            {
                return GeoLocation.DEFAULT;
            }
        }

        public bool SetGeoLocation(GeoLocation value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_geo_location irGeoLocation = new IRtekFileSDK.ir_file_geo_location();
            irGeoLocation.isGPSValid = value.IsGPSValid ? (byte)0x01 : (byte)0x00;
            irGeoLocation.longitude = value.Longitude;
            irGeoLocation.latitude = value.Latitude;
            irGeoLocation.altitude = value.Altitude;
            irGeoLocation.isCompassValid = value.IsCompassValid ? (byte)0x01 : (byte)0x00;
            irGeoLocation.direction = value.Direction;

            int result = IRtekFileSDK.ir_file_set_geo_location(HImage, ref irGeoLocation);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public DateTime GetShotTime()
        {
            if (HImage == IntPtr.Zero)
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }

            ulong lSpan = 0;
            int result = IRtekFileSDK.ir_file_get_shot_time(HImage, ref lSpan);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                DateTime dtStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan tsSpan = new TimeSpan((long)lSpan * TimeSpan.TicksPerMillisecond);
                return (dtStart + tsSpan).ToLocalTime();
            }
            else
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public bool SetShotTime(DateTime value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            DateTime dtStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            ulong lSpan = (ulong)((value - dtStart).Ticks / TimeSpan.TicksPerMillisecond);

            int result = IRtekFileSDK.ir_file_set_shot_time(HImage, lSpan);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public DateTime GetModifiedTime()
        {
            if (HImage == IntPtr.Zero)
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }

            ulong lSpan = 0;
            int result = IRtekFileSDK.ir_file_get_modified_time(HImage, ref lSpan);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                DateTime dtStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan tsSpan = new TimeSpan((long)lSpan * TimeSpan.TicksPerMillisecond);
                return (dtStart + tsSpan).ToLocalTime();
            }
            else
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public AudioAnnotation GetAudioAnnotation()
        {
            if (HImage == IntPtr.Zero)
            {
                return new AudioAnnotation(false, AudioType.Mp3, null);
            }

            IRtekFileSDK.ir_file_audio irAudio = new IRtekFileSDK.ir_file_audio();
            int result = IRtekFileSDK.ir_file_get_audio(HImage, ref irAudio);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    irAudio.buffer = Marshal.AllocHGlobal(irAudio.length);
                    result = IRtekFileSDK.ir_file_get_audio(HImage, ref irAudio);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        bool isValid = irAudio.length != 0;
                        AudioType type = (AudioType)irAudio.type;
                        byte[] buffer = new byte[irAudio.length];
                        Marshal.Copy(irAudio.buffer, buffer, 0, irAudio.length);
                        return new AudioAnnotation(isValid, type, buffer);
                    }
                    else
                    {
                        return new AudioAnnotation(false, AudioType.Mp3, null);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irAudio.buffer);
                }
            }
            else
            {
                return new AudioAnnotation(false, AudioType.Mp3, null);
            }
        }

        public string GetNoteAnnotation()
        {
            if (HImage == IntPtr.Zero)
            {
                return string.Empty;
            }

            IRtekFileSDK.ir_file_buffer irNote = new IRtekFileSDK.ir_file_buffer();
            int result = IRtekFileSDK.ir_file_get_note(HImage, ref irNote);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    irNote.buffer = Marshal.AllocHGlobal(irNote.length);
                    result = IRtekFileSDK.ir_file_get_note(HImage, ref irNote);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        byte[] buffer = new byte[irNote.length];
                        Marshal.Copy(irNote.buffer, buffer, 0, irNote.length);
                        return BytesToString(buffer, Encoding.UTF8);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irNote.buffer);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public bool SetNoteAnnotation(string value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_buffer irNote = new IRtekFileSDK.ir_file_buffer();
            byte[] buffer = StringToBytes(value, Encoding.UTF8);
            try
            {
                irNote.length = buffer.Length;
                irNote.buffer = Marshal.AllocHGlobal(irNote.length);
                Marshal.Copy(buffer, 0, irNote.buffer, irNote.length);
                int result = IRtekFileSDK.ir_file_set_note(HImage, ref irNote);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return false;
                }

                IsModified = true;
            }
            finally
            {
                Marshal.FreeHGlobal(irNote.buffer);
            }

            return true;
        }

        public string[] GetTagAnnotation()
        {
            if (HImage == IntPtr.Zero)
            {
                return new string[0];
            }

            return GetUserData(USER_DATA_KEY_TAG).Split(new string[] { USER_DATA_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool SetTagAnnotation(string[] value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            return SetUserData(USER_DATA_KEY_TAG, string.Join(USER_DATA_SEPARATOR, value));
        }

        public bool GetFavorite()
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            return USER_DATA_FAVORITE_YES.Equals(GetUserData(USER_DATA_KEY_FAVORITE));
        }

        public bool SetFavorite(bool value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            return SetUserData(USER_DATA_KEY_FAVORITE, value ? USER_DATA_FAVORITE_YES : USER_DATA_FAVORITE_NO);
        }

        public string[] GetGroupFiles()
        {
            if (HImage == IntPtr.Zero)
            {
                return new string[0];
            }

            return GetUserData(USER_DATA_KEY_GROUPFILES).Split(new string[] { USER_DATA_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool SetGroupFiles(string[] value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            return SetUserData(USER_DATA_KEY_TAG, string.Join(USER_DATA_SEPARATOR, value));
        }

        public TimerFileInfo GetTimerFileInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            string sTimerFileInfo = GetUserData(USER_DATA_KEY_TIMERFILE);
            if (!string.IsNullOrEmpty(sTimerFileInfo))
            {
                string[] s = sTimerFileInfo.Split(new string[] { USER_DATA_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                return new TimerFileInfo(s[0], Convert.ToInt32(s[1]));
            }
            return null;
        }

        public bool SetTimerFileInfo(TimerFileInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            string sTimerFileInfo = string.Empty;
            if (value != null)
            {
                sTimerFileInfo = string.Format("{0}\n{1:D}", value.GUID, value.No);
            }

            return SetUserData(USER_DATA_KEY_TIMERFILE, sTimerFileInfo);
        }

        public Reference GetReference()
        {
            if (HImage == IntPtr.Zero)
            {
                return Reference.DEFAULT;
            }

            IRtekFileSDK.ir_file_reference irReference = new IRtekFileSDK.ir_file_reference();
            int result = IRtekFileSDK.ir_file_get_reference(HImage, ref irReference);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                Reference reference = new Reference();
                reference.Enabled = irReference.enabled == TRUE;
                reference.Name = BytesToString(irReference.name, Encoding.UTF8);
                switch (irReference.valueType)
                {
                    case IRtekFileSDK.IR_FILE_MAX:
                    default:
                        reference.ValueType = ValueType.Max;
                        break;
                    case IRtekFileSDK.IR_FILE_MIN:
                        reference.ValueType = ValueType.Min;
                        break;
                    case IRtekFileSDK.IR_FILE_AVG:
                        reference.ValueType = ValueType.Avg;
                        break;
                }
                reference.Temp = irReference.temp;
                return reference;
            }
            else
            {
                return Reference.DEFAULT;
            }
        }

        public bool SetReference(Reference value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_reference irReference = new IRtekFileSDK.ir_file_reference();
            irReference.enabled = value.Enabled ? TRUE : FALSE;
            irReference.name = StringToBytes(value.Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            switch (value.ValueType)
            {
                case ValueType.Max:
                default:
                    irReference.valueType = IRtekFileSDK.IR_FILE_MAX;
                    break;
                case ValueType.Min:
                    irReference.valueType = IRtekFileSDK.IR_FILE_MIN;
                    break;
                case ValueType.Avg:
                    irReference.valueType = IRtekFileSDK.IR_FILE_AVG;
                    break;
            }
            irReference.temp = value.Temp;
            int result = IRtekFileSDK.ir_file_set_reference(HImage, ref irReference);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            ReferenceCache = GetReference();
            HandleReferenceChangedEvent();

            return true;
        }

        public Gain GetGain()
        {
            if (HImage == IntPtr.Zero)
            {
                return Gain.DEFAULT;
            }

            IRtekFileSDK.ir_file_gain irGain = new IRtekFileSDK.ir_file_gain();
            int result = IRtekFileSDK.ir_file_get_gain(HImage, ref irGain);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                Gain gain = new Gain();
                switch (irGain.mode)
                {
                    case IRtekFileSDK.IR_FILE_FIXED:
                        gain.Mode = GainMode.Fixed;
                        break;
                    case IRtekFileSDK.IR_FILE_AUTO:
                    default:
                        gain.Mode = GainMode.Auto;
                        break;
                    case IRtekFileSDK.IR_FILE_TOUCH:
                        gain.Mode = GainMode.Touch;
                        break;
                }
                gain.Max = irGain.max;
                gain.Min = irGain.min;
                gain.FixedSpan = irGain.fixedSpan == TRUE ? true : false;
                gain.Span = irGain.span;
                return gain;
            }
            else
            {
                return Gain.DEFAULT;
            }
        }

        public bool SetGain(Gain value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_gain irGain = new IRtekFileSDK.ir_file_gain();
            switch (value.Mode)
            {
                case GainMode.Fixed:
                    irGain.mode = IRtekFileSDK.IR_FILE_FIXED;
                    break;
                case GainMode.Auto:
                default:
                    irGain.mode = IRtekFileSDK.IR_FILE_AUTO;
                    break;
                case GainMode.Touch:
                    irGain.mode = IRtekFileSDK.IR_FILE_TOUCH;
                    break;
            }
            irGain.max = value.Max;
            irGain.min = value.Min;
            irGain.fixedSpan = value.FixedSpan ? TRUE : FALSE;
            irGain.span = value.Span;
            int result = IRtekFileSDK.ir_file_set_gain(HImage, ref irGain);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandleGainChangedEvent();

            return true;
        }

        public Alarm GetAlarm()
        {
            if (HImage == IntPtr.Zero)
            {
                return Alarm.DEFAULT;
            }

            IRtekFileSDK.ir_file_alarm irAlarm = new IRtekFileSDK.ir_file_alarm();
            int result = IRtekFileSDK.ir_file_get_alarm(HImage, ref irAlarm);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                Alarm alarm = new Alarm();
                switch (irAlarm.type)
                {
                    case IRtekFileSDK.IR_FILE_NORMAL:
                    default:
                        alarm.Type = AlarmType.Normal;
                        break;
                    case IRtekFileSDK.IR_FILE_BELOW:
                        alarm.Type = AlarmType.Below;
                        break;
                    case IRtekFileSDK.IR_FILE_ABOVE:
                        alarm.Type = AlarmType.Above;
                        break;
                    case IRtekFileSDK.IR_FILE_BETWEEN:
                        alarm.Type = AlarmType.Between;
                        break;
                    case IRtekFileSDK.IR_FILE_MAGIC_THERMAL:
                        alarm.Type = AlarmType.MagicThermal;
                        break;
                    case IRtekFileSDK.IR_FILE_OUTSIDE:
                        alarm.Type = AlarmType.Outside;
                        break;
                    case IRtekFileSDK.IR_FILE_HUMIDITY:
                        alarm.Type = AlarmType.Humidity;
                        break;
                    case IRtekFileSDK.IR_FILE_INSULATION:
                        alarm.Type = AlarmType.Insulation;
                        break;
                }
                alarm.Custom = irAlarm.custom == TRUE ? true : false;
                alarm.Color = Color.FromArgb((int)irAlarm.color);
                alarm.Max = irAlarm.max;
                alarm.Min = irAlarm.min;
                alarm.RHLimit = irAlarm.rhLimit;
                alarm.IndoorTemp = irAlarm.indoorTemp;
                alarm.OutdoorTemp = irAlarm.outdoorTemp;
                alarm.HeatIndex = irAlarm.heatIndex;
                return alarm;
            }
            else
            {
                return Alarm.DEFAULT;
            }
        }

        public bool SetAlarm(Alarm value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_alarm irAlarm = new IRtekFileSDK.ir_file_alarm();
            switch (value.Type)
            {
                case AlarmType.Normal:
                default:
                    irAlarm.type = IRtekFileSDK.IR_FILE_NORMAL;
                    break;
                case AlarmType.Below:
                    irAlarm.type = IRtekFileSDK.IR_FILE_BELOW;
                    break;
                case AlarmType.Above:
                    irAlarm.type = IRtekFileSDK.IR_FILE_ABOVE;
                    break;
                case AlarmType.Between:
                    irAlarm.type = IRtekFileSDK.IR_FILE_BETWEEN;
                    break;
                case AlarmType.MagicThermal:
                    irAlarm.type = IRtekFileSDK.IR_FILE_MAGIC_THERMAL;
                    break;
                case AlarmType.Outside:
                    irAlarm.type = IRtekFileSDK.IR_FILE_OUTSIDE;
                    break;
                case AlarmType.Humidity:
                    irAlarm.type = IRtekFileSDK.IR_FILE_HUMIDITY;
                    break;
                case AlarmType.Insulation:
                    irAlarm.type = IRtekFileSDK.IR_FILE_INSULATION;
                    break;
            }
            irAlarm.custom = value.Custom ? TRUE : FALSE;
            irAlarm.color = (uint)value.Color.ToArgb();
            irAlarm.max = value.Max;
            irAlarm.min = value.Min;
            irAlarm.rhLimit = value.RHLimit;
            irAlarm.indoorTemp = value.IndoorTemp;
            irAlarm.outdoorTemp = value.OutdoorTemp;
            irAlarm.heatIndex = value.HeatIndex;

            int result = IRtekFileSDK.ir_file_set_alarm(HImage, ref irAlarm);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandleAlarmChangedEvent();

            return true;
        }

        public FileLUT GetFileLUT()
        {
            if (HImage == IntPtr.Zero)
            {
                return FileLUT.DEFAULT;
            }

            IRtekFileSDK.ir_file_lut irFileLUT = new IRtekFileSDK.ir_file_lut();
            int result = IRtekFileSDK.ir_file_get_lut(HImage, ref irFileLUT);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    irFileLUT.adArray = Marshal.AllocHGlobal(irFileLUT.count * Marshal.SizeOf(typeof(ushort)));
                    irFileLUT.tempArray = Marshal.AllocHGlobal(irFileLUT.count * Marshal.SizeOf(typeof(float)));
                    result = IRtekFileSDK.ir_file_get_lut(HImage, ref irFileLUT);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        FileLUT fileLUT = new FileLUT();
                        fileLUT.RadMethod = irFileLUT.radMethod;
                        fileLUT.Count = irFileLUT.count;
                        fileLUT.ADArray = new ushort[fileLUT.Count];
                        fileLUT.TempArray = new float[fileLUT.Count];
                        for (int index = 0; index < fileLUT.Count; index++)
                        {
                            fileLUT.ADArray[index] = (ushort)Marshal.PtrToStructure((IntPtr)((UInt32)irFileLUT.adArray + index * Marshal.SizeOf(typeof(ushort))), typeof(ushort));
                            fileLUT.TempArray[index] = (float)Marshal.PtrToStructure((IntPtr)((UInt32)irFileLUT.tempArray + index * Marshal.SizeOf(typeof(float))), typeof(float));
                        }

                        return fileLUT;
                    }
                    else
                    {
                        return FileLUT.DEFAULT;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irFileLUT.adArray);
                    Marshal.FreeHGlobal(irFileLUT.tempArray);
                }
            }
            else
            {
                return FileLUT.DEFAULT;
            }
        }

        public bool SetFileLUT(FileLUT value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_lut irFileLUT = new IRtekFileSDK.ir_file_lut();
            irFileLUT.radMethod = value.RadMethod;
            irFileLUT.count = (ushort)value.ADArray.Length;
            try
            {
                irFileLUT.adArray = Marshal.AllocHGlobal(irFileLUT.count * Marshal.SizeOf(typeof(ushort)));
                irFileLUT.tempArray = Marshal.AllocHGlobal(irFileLUT.count * Marshal.SizeOf(typeof(float)));
                for (int index = 0; index < irFileLUT.count; index++)
                {
                    Marshal.StructureToPtr(value.ADArray[index], irFileLUT.adArray + index * Marshal.SizeOf(typeof(ushort)), false);
                    Marshal.StructureToPtr(value.TempArray[index], irFileLUT.tempArray + index * Marshal.SizeOf(typeof(float)), false);
                }

                int result = IRtekFileSDK.ir_file_set_lut(HImage, ref irFileLUT);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return false;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(irFileLUT.adArray);
                Marshal.FreeHGlobal(irFileLUT.tempArray);
            }

            IsModified = true;

            return true;
        }

        public Palette GetPalette()
        {
            if (HImage == IntPtr.Zero)
            {
                return Palette.DEFAULT;
            }

            IRtekFileSDK.ir_file_palette irPalette = new IRtekFileSDK.ir_file_palette();
            int result = IRtekFileSDK.ir_file_get_palette(HImage, ref irPalette);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                Palette palette = new Palette();
                palette.RefNo = irPalette.refNo;
                palette.Colors = irPalette.colors;
                palette.Inverted = irPalette.inverted == TRUE ? true : false;
                return palette;
            }
            else
            {
                return Palette.DEFAULT;
            }
        }

        public bool SetPalette(Palette value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_palette irPalette = new IRtekFileSDK.ir_file_palette();
            irPalette.refNo = value.RefNo;
            if (irPalette.refNo == 0xFF)
            {
                irPalette.colors = value.Colors;
            }
            else
            {
                irPalette.colors = new uint[IRtekFileSDK.COLORS_LEN];
            }
            irPalette.inverted = value.Inverted ? TRUE : FALSE;

            int result = IRtekFileSDK.ir_file_set_palette(HImage, ref irPalette);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandlePaletteChangedEvent();

            return true;
        }

        public ViewInfo GetViewInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return ViewInfo.DEFAULT;
            }

            IRtekFileSDK.ir_file_view_info irViewInfo = new IRtekFileSDK.ir_file_view_info();
            int result = IRtekFileSDK.ir_file_get_view_info(HImage, ref irViewInfo);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                ViewInfo viewInfo = new ViewInfo();
                switch (irViewInfo.type)
                {
                    case IRtekFileSDK.IR_FILE_THERMAL:
                    default:
                        viewInfo.Type = ViewType.Thermal;
                        break;
                    case IRtekFileSDK.IR_FILE_PIC_IN_PIC:
                        viewInfo.Type = ViewType.PicInPic;
                        break;
                    case IRtekFileSDK.IR_FILE_BLEND:
                        viewInfo.Type = ViewType.Blend;
                        break;
                    case IRtekFileSDK.IR_FILE_FUSION:
                        viewInfo.Type = ViewType.Fusion;
                        break;
                }
                viewInfo.Max = irViewInfo.max;
                viewInfo.Min = irViewInfo.min;
                viewInfo.Alpha = irViewInfo.alpha;
                IRtekFileSDK.ir_file_rectangle_f area = irViewInfo.area;
                viewInfo.Area = new RectangleF(area.x, area.y, area.width, area.height);
                return viewInfo;
            }
            else
            {
                return ViewInfo.DEFAULT;
            }
        }

        public bool SetViewInfo(ViewInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_view_info irViewInfo = new IRtekFileSDK.ir_file_view_info();
            switch (value.Type)
            {
                case ViewType.Thermal:
                default:
                    irViewInfo.type = IRtekFileSDK.IR_FILE_THERMAL;
                    break;
                case ViewType.PicInPic:
                    irViewInfo.type = IRtekFileSDK.IR_FILE_PIC_IN_PIC;
                    break;
                case ViewType.Blend:
                    irViewInfo.type = IRtekFileSDK.IR_FILE_BLEND;
                    break;
                case ViewType.Fusion:
                    irViewInfo.type = IRtekFileSDK.IR_FILE_FUSION;
                    break;
            }
            irViewInfo.max = value.Max;
            irViewInfo.min = value.Min;
            irViewInfo.alpha = value.Alpha;
            RectangleF area = value.Area;
            IRtekFileSDK.ir_file_rectangle_f irArea = new IRtekFileSDK.ir_file_rectangle_f();
            irArea.x = area.X;
            irArea.y = area.Y;
            irArea.width = area.Width;
            irArea.height = area.Height;
            irViewInfo.area = irArea;

            int result = IRtekFileSDK.ir_file_set_view_info(HImage, ref irViewInfo);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandleViewInfoChangedEvent();

            return true;
        }

        public MappingInfo GetMappingInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return MappingInfo.DEFAULT;
            }

            IRtekFileSDK.ir_file_mapping_info irMappingInfo = new IRtekFileSDK.ir_file_mapping_info();
            int result = IRtekFileSDK.ir_file_get_mapping_info(HImage, ref irMappingInfo);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                MappingInfo mappingInfo = new MappingInfo();
                mappingInfo.VisualSize = new Size(irMappingInfo.visualSize.width, irMappingInfo.visualSize.height);
                mappingInfo.MappingArea = new RectangleF(irMappingInfo.mappingArea.x, irMappingInfo.mappingArea.y, irMappingInfo.mappingArea.width, irMappingInfo.mappingArea.height);
                return mappingInfo;
            }
            else
            {
                return MappingInfo.DEFAULT;
            }
        }

        public bool SetMappingInfo(MappingInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_mapping_info irMappingInfo = new IRtekFileSDK.ir_file_mapping_info();
            Size visualSize = value.VisualSize;
            IRtekFileSDK.ir_file_size irVisualSize = new IRtekFileSDK.ir_file_size();
            irVisualSize.width = (short)visualSize.Width;
            irVisualSize.height = (short)visualSize.Height;
            irMappingInfo.visualSize = irVisualSize;
            RectangleF mappingArea = value.MappingArea;
            IRtekFileSDK.ir_file_rectangle_f irMappingArea = new IRtekFileSDK.ir_file_rectangle_f();
            irMappingArea.x = mappingArea.X;
            irMappingArea.y = mappingArea.Y;
            irMappingArea.width = mappingArea.Width;
            irMappingArea.height = mappingArea.Height;
            irMappingInfo.mappingArea = irMappingArea;

            int result = IRtekFileSDK.ir_file_set_mapping_info(HImage, ref irMappingInfo);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            HandleMappingInfoChangedEvent();

            return true;
        }

        public ZoomInfo GetZoomInfo()
        {
            if (HImage == IntPtr.Zero)
            {
                return ZoomInfo.DEFAULT;
            }

            IRtekFileSDK.ir_file_zoom_info irZoomInfo = new IRtekFileSDK.ir_file_zoom_info();
            int result = IRtekFileSDK.ir_file_get_zoom_info(HImage, ref irZoomInfo);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                ZoomInfo zoomInfo = new ZoomInfo();
                zoomInfo.Enabled = irZoomInfo.enabled == TRUE ? true : false;
                zoomInfo.Location = new PointF(irZoomInfo.location.x, irZoomInfo.location.y);
                zoomInfo.Ratio = irZoomInfo.ratio;
                return zoomInfo;
            }
            else
            {
                return ZoomInfo.DEFAULT;
            }
        }

        public bool SetZoomInfo(ZoomInfo value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_zoom_info irZoomInfo = new IRtekFileSDK.ir_file_zoom_info();
            irZoomInfo.enabled = value.Enabled ? TRUE : FALSE;
            PointF location = value.Location;
            IRtekFileSDK.ir_file_point_f irLocation = new IRtekFileSDK.ir_file_point_f();
            irLocation.x = location.X;
            irLocation.y = location.Y;
            irZoomInfo.location = irLocation;
            irZoomInfo.ratio = value.Ratio;

            int result = IRtekFileSDK.ir_file_set_zoom_info(HImage, ref irZoomInfo);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = true;

            return true;
        }

        public bool Create(string fileName, FileType fileType)
        {
            IntPtr h = IntPtr.Zero;
            int result = IRtekFileSDK.ir_file_create(fileName, (int)fileType, ref h);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                HImage = h;
                return true;
            }
            return false;
        }

        public bool Open(string fileName)
        {
            if (HImage != IntPtr.Zero)
            {
                IsDeltasSynced = false;
                Deltas.Clear();
                IsMarkersSynced = false;
                Markers.Clear();
                ReferenceCache = Reference.DEFAULT;
                IRtekFileSDK.ir_file_close(HImage);
                HImage = IntPtr.Zero;
            }

            IntPtr h = IntPtr.Zero;
            int result = IRtekFileSDK.ir_file_open(fileName, ref h);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                HImage = h;
                IsMarkersSynced = false;
                string[] names = GetMarkerNames();
                for (int i = 0; i < names.Length; i++)
                {
                    string name = names[i];
                    if (string.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    Marker marker = GetMarker(name);
                    if (marker == null)
                    {
                        continue;
                    }

                    Markers.Add(marker);
                }
                IsMarkersSynced = true;

                IsDeltasSynced = false;
                names = GetDeltaNames();
                for (int i = 0; i < names.Length; i++)
                {
                    string name = names[i];
                    if (string.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    Delta delta = GetDelta(name);
                    if (delta == null)
                    {
                        continue;
                    }

                    Deltas.Add(delta);
                }
                IsDeltasSynced = true;

                ReferenceCache = GetReference();

                IsModified = false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Save()
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            int result = IRtekFileSDK.ir_file_save(HImage);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = false;

            return true;
        }

        public bool SaveFJPG()
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            int result = IRtekFileSDK.ir_file_save_fjpg(HImage);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = false;

            return true;
        }

        public bool SaveAs(string fileName)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            int result = IRtekFileSDK.ir_file_save_as(HImage, fileName);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = false;

            return true;
        }

        public bool SaveAsFJPG(string fileName)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            int result = IRtekFileSDK.ir_file_save_as_fjpg(HImage, fileName);
            if (result != IRtekFileSDK.IR_FILE_EC_OK)
            {
                return false;
            }

            IsModified = false;

            return true;
        }

        public void Close()
        {
            if (HImage == IntPtr.Zero)
            {
                return;
            }

            IsMarkersSynced = false;
            Markers.Clear();
            IsDeltasSynced = false;
            Deltas.Clear();
            ReferenceCache = Reference.DEFAULT;
            IRtekFileSDK.ir_file_close(HImage);
            HImage = IntPtr.Zero;
            IsModified = false;
        }

        public string[] GetUserDataKeys()
        {
            if (HImage == IntPtr.Zero)
            {
                return new string[0];
            }

            IRtekFileSDK.ir_file_keys irKeyList = new IRtekFileSDK.ir_file_keys();
            int result = IRtekFileSDK.ir_file_get_keys(HImage, ref irKeyList);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    int bufferSize = irKeyList.count * IRtekFileSDK.KEY_LEN;
                    irKeyList.keys = Marshal.AllocHGlobal(bufferSize);
                    result = IRtekFileSDK.ir_file_get_keys(HImage, ref irKeyList);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        byte[] buffer = new byte[bufferSize];
                        Marshal.Copy(irKeyList.keys, buffer, 0, bufferSize);
                        string[] keys = new string[irKeyList.count];
                        for (int i = 0; i < irKeyList.count; i++)
                        {
                            keys[i] = BytesToString(buffer, i * IRtekFileSDK.KEY_LEN, IRtekFileSDK.KEY_LEN, Encoding.UTF8);
                        }
                        return keys;
                    }
                    else
                    {
                        return new string[0];
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irKeyList.keys);
                }
            }
            else
            {
                return new string[0];
            }
        }

        public string GetUserData(string key)
        {
            if (HImage == IntPtr.Zero)
            {
                return string.Empty;
            }

            IRtekFileSDK.ir_file_kvPair irKVPair = new IRtekFileSDK.ir_file_kvPair();
            irKVPair.key = StringToBytes(key, IRtekFileSDK.KEY_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_get_user_data(HImage, ref irKVPair);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    irKVPair.vBuffer = Marshal.AllocHGlobal(irKVPair.vLength);
                    result = IRtekFileSDK.ir_file_get_user_data(HImage, ref irKVPair);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        byte[] buffer = new byte[irKVPair.vLength];
                        Marshal.Copy(irKVPair.vBuffer, buffer, 0, irKVPair.vLength);
                        return BytesToString(buffer, Encoding.UTF8);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irKVPair.vBuffer);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public bool SetUserData(string key, string value)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_kvPair irKVPair = new IRtekFileSDK.ir_file_kvPair();
            irKVPair.key = StringToBytes(key, IRtekFileSDK.KEY_LEN, Encoding.UTF8);
            byte[] buffer = StringToBytes(value, Encoding.UTF8);
            irKVPair.vLength = buffer.Length;
            try
            {
                irKVPair.vBuffer = Marshal.AllocHGlobal(irKVPair.vLength);
                Marshal.Copy(buffer, 0, irKVPair.vBuffer, irKVPair.vLength);
                int result = IRtekFileSDK.ir_file_set_user_data(HImage, ref irKVPair);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return false;
                }

                IsModified = true;
            }
            finally
            {
                Marshal.FreeHGlobal(irKVPair.vBuffer);
            }

            return true;
        }

        public ushort[,] GetFrame()
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            int width = GetThermalSize().Width;
            int height = GetThermalSize().Height;
            ushort[,] values = new ushort[width, height];
            IRtekFileSDK.ir_file_frame frame = new IRtekFileSDK.ir_file_frame();
            try
            {
                int size = Marshal.SizeOf(typeof(ushort));
                frame.buffer = Marshal.AllocHGlobal(size * width * height);

                int result = IRtekFileSDK.ir_file_get_frame(HImage, 0, ref frame);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return null;
                }

                unsafe
                {
                    ushort* sPtr = (ushort*)(frame.buffer);
                    int i = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            values[x, y] = sPtr[i++];
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(frame.buffer);
            }
            return values;
        }

        public bool SetFrame(ushort[,] value, DateTime time)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            int width = GetThermalSize().Width;
            int height = GetThermalSize().Height;
            int size = Marshal.SizeOf(typeof(ushort));

            IRtekFileSDK.ir_file_frame frame = new IRtekFileSDK.ir_file_frame();
            frame.length = (uint)(width * height);
            frame.timeStamp = (ulong)time.Ticks;
            try
            {
                frame.buffer = Marshal.AllocHGlobal(size * width * height);
                unsafe
                {
                    ushort* sPtr = (ushort*)(frame.buffer);
                    int i = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            sPtr[i++] = value[x, y];
                        }
                    }
                }
                int result = IRtekFileSDK.ir_file_append_frame(HImage, ref frame);
                if (result != IRtekFileSDK.IR_FILE_EC_OK)
                {
                    return false;
                }
                return true;
            }
            finally
            {
                Marshal.FreeHGlobal(frame.buffer);
            }
        }

        public float GetValueAt(Point point)
        {
            LocalThermalParams thermalParams = new LocalThermalParams();
            thermalParams.LocalParams = false;
            thermalParams.Emissivity = 1.0F;
            thermalParams.Distance = 1.0F;
            thermalParams.ReflTemp = 293.15F;

            return GetValueAt(point, thermalParams);
        }

        public float GetValueAt(Point point, LocalThermalParams thermalParams)
        {
            if (HImage == IntPtr.Zero)
            {
                return 0.0F;
            }

            IRtekFileSDK.ir_file_temp_point irTempPoint = new IRtekFileSDK.ir_file_temp_point();
            IRtekFileSDK.ir_file_point irPoint = new IRtekFileSDK.ir_file_point();
            irPoint.x = (short)point.X;
            irPoint.y = (short)point.Y;
            irTempPoint.point = irPoint;
            irTempPoint.localParams = thermalParams.LocalParams ? TRUE : FALSE;
            irTempPoint.reflTemp = thermalParams.ReflTemp;
            irTempPoint.distance = thermalParams.Distance;
            irTempPoint.emissivity = thermalParams.Emissivity;
            int result = IRtekFileSDK.ir_file_get_temp_point(HImage, ref irTempPoint);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                return irTempPoint.temp;
            }
            else
            {
                return 0.0F;
            }
        }

        public float[] GetValues(Rectangle rectangle)
        {
            LocalThermalParams thermalParams = new LocalThermalParams();
            thermalParams.LocalParams = false;
            thermalParams.Emissivity = 1.0F;
            thermalParams.Distance = 1.0F;
            thermalParams.ReflTemp = 293.15F;

            return GetValues(rectangle, thermalParams);
        }

        public float[] GetValues(Rectangle rectangle, LocalThermalParams thermalParams)
        {
            if (HImage == IntPtr.Zero)
            {
                return new float[] { };
            }

            IRtekFileSDK.ir_file_temp_area irTempRectangle = new IRtekFileSDK.ir_file_temp_area();
            IRtekFileSDK.ir_file_rectangle irRectangle = new IRtekFileSDK.ir_file_rectangle();
            irRectangle.x = (short)rectangle.X;
            irRectangle.y = (short)rectangle.Y;
            irRectangle.width = (short)rectangle.Width;
            irRectangle.height = (short)rectangle.Height;
            irTempRectangle.rect = irRectangle;
            irTempRectangle.localParams = thermalParams.LocalParams ? TRUE : FALSE;
            irTempRectangle.reflTemp = thermalParams.ReflTemp;
            irTempRectangle.distance = thermalParams.Distance;
            irTempRectangle.emissivity = thermalParams.Emissivity;

            try
            {
                int count = irRectangle.width * irRectangle.height;
                irTempRectangle.temps = Marshal.AllocHGlobal(count * sizeof(float));
                int result = IRtekFileSDK.ir_file_get_temp_area(HImage, ref irTempRectangle);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    float[] buffer = new float[count];
                    Marshal.Copy(irTempRectangle.temps, buffer, 0, count);
                    return buffer;
                }
                else
                {
                    return new float[] { };
                }
            }
            finally
            {
                Marshal.FreeHGlobal(irTempRectangle.temps);
            }
        }

        public void Rotate(RotateType rotation)
        {
            if (HImage == IntPtr.Zero)
            {
                return;
            }

            int irRotation = IRtekFileSDK.IR_FILE_ROTATE_90;
            switch (rotation)
            {
                case RotateType.Rotate90:
                default:
                    irRotation = IRtekFileSDK.IR_FILE_ROTATE_90;
                    break;
                case RotateType.Rotate180:
                    irRotation = IRtekFileSDK.IR_FILE_ROTATE_180;
                    break;
                case RotateType.Rotate270:
                    irRotation = IRtekFileSDK.IR_FILE_ROTATE_270;
                    break;
            }

            int result = IRtekFileSDK.ir_file_set_rotation(HImage, irRotation);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                IsModified = true;

                HandleRotationTypeChangedEvent();
            }
        }

        public void Mirror(MirrorType mirror)
        {
            if (HImage == IntPtr.Zero)
            {
                return;
            }

            int irMirror = IRtekFileSDK.IR_FILE_MIRROR_HORIZONTAL;
            switch (mirror)
            {
                case MirrorType.MirrorHorizontal:
                default:
                    irMirror = IRtekFileSDK.IR_FILE_MIRROR_HORIZONTAL;
                    break;
                case MirrorType.MirrorVertical:
                    irMirror = IRtekFileSDK.IR_FILE_MIRROR_VERTICAL;
                    break;
            }

            int result = IRtekFileSDK.ir_file_set_mirror(HImage, irMirror);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                IsModified = true;

                HandleMirrorTypeChangedEvent();
            }
        }

        private string[] GetMarkerNames()
        {
            if (HImage == IntPtr.Zero)
            {
                return new string[0];
            }

            IRtekFileSDK.ir_file_marker_names irMarkerNameList = new IRtekFileSDK.ir_file_marker_names();
            int result = IRtekFileSDK.ir_file_get_marker_names(HImage, ref irMarkerNameList);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    int bufferSize = irMarkerNameList.count * IRtekFileSDK.NAME_LEN;
                    irMarkerNameList.names = Marshal.AllocHGlobal(bufferSize);
                    result = IRtekFileSDK.ir_file_get_marker_names(HImage, ref irMarkerNameList);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        byte[] buffer = new byte[bufferSize];
                        Marshal.Copy(irMarkerNameList.names, buffer, 0, bufferSize);
                        string[] markerNames = new string[irMarkerNameList.count];
                        for (int i = 0; i < irMarkerNameList.count; i++)
                        {
                            markerNames[i] = BytesToString(buffer, i * IRtekFileSDK.NAME_LEN, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                        }
                        return markerNames;
                    }
                    else
                    {
                        return new string[0];
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irMarkerNameList.names);
                }
            }
            else
            {
                return new string[0];
            }
        }

        private string[] GetDeltaNames()
        {
            if (HImage == IntPtr.Zero)
            {
                return new string[0];
            }

            IRtekFileSDK.ir_file_delta_names irDeltaNameList = new IRtekFileSDK.ir_file_delta_names();
            int result = IRtekFileSDK.ir_file_get_delta_names(HImage, ref irDeltaNameList);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    int bufferSize = irDeltaNameList.count * IRtekFileSDK.NAME_LEN;
                    irDeltaNameList.names = Marshal.AllocHGlobal(bufferSize);
                    result = IRtekFileSDK.ir_file_get_delta_names(HImage, ref irDeltaNameList);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        byte[] buffer = new byte[bufferSize];
                        Marshal.Copy(irDeltaNameList.names, buffer, 0, bufferSize);
                        string[] markerNames = new string[irDeltaNameList.count];
                        for (int i = 0; i < irDeltaNameList.count; i++)
                        {
                            markerNames[i] = BytesToString(buffer, i * IRtekFileSDK.NAME_LEN, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                        }
                        return markerNames;
                    }
                    else
                    {
                        return new string[0];
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irDeltaNameList.names);
                }
            }
            else
            {
                return new string[0];
            }
        }

        private Marker GetMarker(string name)
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
            irMarker.name = StringToBytes(name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_get_marker(HImage, ref irMarker);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    int size = 0;
                    unsafe
                    {
                        size = sizeof(IRtekFileSDK.ir_file_point);
                    }

                    irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                    result = IRtekFileSDK.ir_file_get_marker(HImage, ref irMarker);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        LocalThermalParams thermalParams = new LocalThermalParams();
                        thermalParams.LocalParams = irMarker.localParams == TRUE;
                        thermalParams.Emissivity = irMarker.emissivity;
                        thermalParams.Distance = irMarker.distance;
                        thermalParams.ReflTemp = irMarker.reflTemp;

                        IRtekFileSDK.ir_file_point p;
                        Marker marker;
                        switch (irMarker.type)
                        {
                            case IRtekFileSDK.IR_FILE_SPOT:
                                p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                                Point spot = new Point(p.x, p.y);
                                marker = new MarkerSpot(this, BytesToString(irMarker.name, Encoding.UTF8), thermalParams, spot);
                                break;
                            case IRtekFileSDK.IR_FILE_RECTANGLE:
                                Rectangle rect = new Rectangle();
                                p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                                Point p1 = new Point(p.x, p.y);
                                p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + size, typeof(IRtekFileSDK.ir_file_point));
                                Point p2 = new Point(p.x, p.y);
                                rect.Location = new Point(p1.X <= p2.X ? p1.X : p2.X, p1.Y <= p2.Y ? p1.Y : p2.Y);
                                rect.Size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                                marker = new MarkerRectangle(this, BytesToString(irMarker.name, Encoding.UTF8), thermalParams, rect);
                                break;
                            case IRtekFileSDK.IR_FILE_ELLIPSE:
                                rect = new Rectangle();
                                p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                                p1 = new Point(p.x, p.y);
                                p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + size, typeof(IRtekFileSDK.ir_file_point));
                                p2 = new Point(p.x, p.y);
                                rect.Location = new Point(p1.X <= p2.X ? p1.X : p2.X, p1.Y <= p2.Y ? p1.Y : p2.Y);
                                rect.Size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                                marker = new MarkerEllipse(this, BytesToString(irMarker.name, Encoding.UTF8), thermalParams, rect);
                                break;
                            case IRtekFileSDK.IR_FILE_POLYGON:
                                Point[] points = new Point[irMarker.pointCount];
                                for (int i = 0; i < irMarker.pointCount; i++)
                                {
                                    p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + i * size, typeof(IRtekFileSDK.ir_file_point));
                                    points[i] = new Point(p.x, p.y);
                                }
                                marker = new MarkerPolygon(this, BytesToString(irMarker.name, Encoding.UTF8), thermalParams, points);
                                break;
                            case IRtekFileSDK.IR_FILE_POLYLINE:
                                points = new Point[irMarker.pointCount];
                                for (int i = 0; i < irMarker.pointCount; i++)
                                {
                                    p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + i * size, typeof(IRtekFileSDK.ir_file_point));
                                    points[i] = new Point(p.x, p.y);
                                }
                                marker = new MarkerPolyline(this, BytesToString(irMarker.name, Encoding.UTF8), thermalParams, points);
                                break;
                            default:
                                return null;
                        }

                        float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                        marker.MaxInternal = temp;
                        temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                        marker.MinInternal = temp;
                        temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                        marker.AvgInternal = temp;
                        IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                        marker.HotSpotInternal = new Point(irPoint.x, irPoint.y);
                        irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                        marker.ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                        marker.MeasuredValueInternal = irMarker.measuredValue;

                        return marker;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(irMarker.points);
                }
            }
            else
            {
                return null;
            }
        }

        private Delta GetDelta(string name)
        {
            if (HImage == IntPtr.Zero)
            {
                return null;
            }

            IRtekFileSDK.ir_file_delta irDelta = new IRtekFileSDK.ir_file_delta();
            irDelta.name = StringToBytes(name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_get_delta(HImage, ref irDelta);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                DeltaParams deltaParams = new DeltaParams();
                deltaParams.SrcName = BytesToString(irDelta.srcName, Encoding.UTF8);
                switch (irDelta.srcValueType)
                {
                    case IRtekFileSDK.IR_FILE_MAX:
                    default:
                        deltaParams.SrcValueType = ValueType.Max;
                        break;
                    case IRtekFileSDK.IR_FILE_MIN:
                        deltaParams.SrcValueType = ValueType.Min;
                        break;
                    case IRtekFileSDK.IR_FILE_AVG:
                        deltaParams.SrcValueType = ValueType.Avg;
                        break;
                }
                deltaParams.SrcTemp = irDelta.srcTemp;
                deltaParams.DestName = BytesToString(irDelta.destName, Encoding.UTF8);
                switch (irDelta.destValueType)
                {
                    case IRtekFileSDK.IR_FILE_MAX:
                    default:
                        deltaParams.DestValueType = ValueType.Max;
                        break;
                    case IRtekFileSDK.IR_FILE_MIN:
                        deltaParams.DestValueType = ValueType.Min;
                        break;
                    case IRtekFileSDK.IR_FILE_AVG:
                        deltaParams.DestValueType = ValueType.Avg;
                        break;
                }
                deltaParams.DestTemp = irDelta.destTemp;

                Delta delta = new Delta(this, BytesToString(irDelta.name, Encoding.UTF8), deltaParams);
                delta.Value = irDelta.value;
                return delta;
            }
            else
            {
                return null;
            }
        }

        private bool SetMarker(Marker marker)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
            irMarker.name = StringToBytes(marker.Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            LocalThermalParams thermalParams = marker.ThermalParams;
            irMarker.localParams = thermalParams.LocalParams ? TRUE : FALSE;
            irMarker.emissivity = thermalParams.Emissivity;
            irMarker.reflTemp = thermalParams.ReflTemp;
            irMarker.distance = thermalParams.Distance;
            try
            {
                int size = 0;
                unsafe
                {
                    size = sizeof(IRtekFileSDK.ir_file_point);
                }

                IRtekFileSDK.ir_file_point p;
                switch (marker.Type)
                {
                    case MarkerType.Spot:
                        irMarker.type = IRtekFileSDK.IR_FILE_SPOT;
                        MarkerSpot markerSpot = marker as MarkerSpot;
                        irMarker.pointCount = 1;
                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        p = new IRtekFileSDK.ir_file_point();
                        p.x = (short)markerSpot.Point.X;
                        p.y = (short)markerSpot.Point.Y;
                        Marshal.StructureToPtr(p, irMarker.points, false);
                        break;
                    case MarkerType.Rectangle:
                        irMarker.type = IRtekFileSDK.IR_FILE_RECTANGLE;
                        MarkerRectangle markerRectangle = marker as MarkerRectangle;
                        irMarker.pointCount = 2;
                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        p = new IRtekFileSDK.ir_file_point();
                        p.x = (short)markerRectangle.Rectangle.X;
                        p.y = (short)markerRectangle.Rectangle.Y;
                        Marshal.StructureToPtr(p, irMarker.points, false);
                        p = new IRtekFileSDK.ir_file_point();
                        p.x = (short)markerRectangle.Rectangle.Right;
                        p.y = (short)markerRectangle.Rectangle.Bottom;
                        Marshal.StructureToPtr(p, irMarker.points + size, false);
                        break;
                    case MarkerType.Ellipse:
                        irMarker.type = IRtekFileSDK.IR_FILE_ELLIPSE;
                        MarkerEllipse markerEllipse = marker as MarkerEllipse;
                        irMarker.pointCount = 2;
                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        p = new IRtekFileSDK.ir_file_point();
                        p.x = (short)markerEllipse.Rectangle.X;
                        p.y = (short)markerEllipse.Rectangle.Y;
                        Marshal.StructureToPtr(p, irMarker.points, false);
                        p = new IRtekFileSDK.ir_file_point();
                        p.x = (short)markerEllipse.Rectangle.Right;
                        p.y = (short)markerEllipse.Rectangle.Bottom;
                        Marshal.StructureToPtr(p, irMarker.points + size, false);
                        break;
                    case MarkerType.Polygon:
                        irMarker.type = IRtekFileSDK.IR_FILE_POLYGON;
                        MarkerPolygon markerPolygon = marker as MarkerPolygon;
                        irMarker.pointCount = markerPolygon.Points.Length;
                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        for (int i = 0; i < irMarker.pointCount; i++)
                        {
                            p = new IRtekFileSDK.ir_file_point();
                            p.x = (short)markerPolygon.Points[i].X;
                            p.y = (short)markerPolygon.Points[i].Y;
                            Marshal.StructureToPtr(p, irMarker.points + i * size, false);
                        }
                        break;
                    case MarkerType.Polyline:
                        irMarker.type = IRtekFileSDK.IR_FILE_POLYLINE;
                        MarkerPolyline markerPolyline = marker as MarkerPolyline;
                        irMarker.pointCount = markerPolyline.Points.Length;
                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        for (int i = 0; i < irMarker.pointCount; i++)
                        {
                            p = new IRtekFileSDK.ir_file_point();
                            p.x = (short)markerPolyline.Points[i].X;
                            p.y = (short)markerPolyline.Points[i].Y;
                            Marshal.StructureToPtr(p, irMarker.points + i * size, false);
                        }
                        break;
                }

                int result = IRtekFileSDK.ir_file_set_marker(HImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    IsModified = true;

                    float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                    marker.MaxInternal = temp;
                    temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                    marker.MinInternal = temp;
                    temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                    marker.AvgInternal = temp;
                    IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                    marker.HotSpotInternal = new Point(irPoint.x, irPoint.y);
                    irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                    marker.ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                    marker.MeasuredValueInternal = irMarker.measuredValue;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(irMarker.points);
            }
        }

        private bool SetDelta(Delta delta)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            IRtekFileSDK.ir_file_delta irDelta = new IRtekFileSDK.ir_file_delta();
            irDelta.name = StringToBytes(delta.Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            DeltaParams deltaParams = delta.DeltaParams;
            irDelta.srcName = StringToBytes(deltaParams.SrcName, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            switch (deltaParams.SrcValueType)
            {
                case ValueType.Max:
                default:
                    irDelta.srcValueType = IRtekFileSDK.IR_FILE_MAX;
                    break;
                case ValueType.Min:
                    irDelta.srcValueType = IRtekFileSDK.IR_FILE_MIN;
                    break;
                case ValueType.Avg:
                    irDelta.srcValueType = IRtekFileSDK.IR_FILE_AVG;
                    break;
            }
            irDelta.srcTemp = deltaParams.SrcTemp;
            irDelta.destName = StringToBytes(deltaParams.DestName, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            switch (deltaParams.DestValueType)
            {
                case ValueType.Max:
                default:
                    irDelta.destValueType = IRtekFileSDK.IR_FILE_MAX;
                    break;
                case ValueType.Min:
                    irDelta.destValueType = IRtekFileSDK.IR_FILE_MIN;
                    break;
                case ValueType.Avg:
                    irDelta.destValueType = IRtekFileSDK.IR_FILE_AVG;
                    break;
            }
            irDelta.destTemp = deltaParams.DestTemp;

            int result = IRtekFileSDK.ir_file_set_delta(HImage, ref irDelta);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                IsModified = true;

                delta.Value = irDelta.value;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RemoveMarker(string name)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            byte[] buffer = StringToBytes(name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_remove_marker(HImage, buffer);
            if (result == IRtekFileSDK.IR_FILE_EC_OK || result == IRtekFileSDK.IR_FILE_EC_INVALID_KEY)
            {
                IsModified = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RenameMarker(string fromName, string toName)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            byte[] fromBuffer = StringToBytes(fromName, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            byte[] toBuffer = StringToBytes(toName, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_rename_marker(HImage, fromBuffer, toBuffer);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                IsModified = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RemoveDelta(string name)
        {
            if (HImage == IntPtr.Zero)
            {
                return false;
            }

            byte[] buffer = StringToBytes(name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
            int result = IRtekFileSDK.ir_file_remove_delta(HImage, buffer);
            if (result == IRtekFileSDK.IR_FILE_EC_OK || result == IRtekFileSDK.IR_FILE_EC_INVALID_KEY)
            {
                IsModified = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void HandleAlarmChangedEvent()
        {
            if (AlarmChanged != null)
            {
                AlarmChanged(this, new EventArgs());
            }

            if (PaletteChanged != null)
            {
                PaletteChanged(this, new EventArgs());
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleMappingInfoChangedEvent()
        {
            if (MappingInfoChanged != null)
            {
                MappingInfoChanged(this, new EventArgs());
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleViewInfoChangedEvent()
        {
            if (ViewInfoChanged != null)
            {
                ViewInfoChanged(this, new EventArgs());
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleMirrorTypeChangedEvent()
        {
            if (MirrorTypeChanged != null)
            {
                MirrorTypeChanged(this, new EventArgs());
            }

            for (int i = 0; i < Markers.Count; i++)
            {
                Marker marker = Markers[i];
                marker.Sync();
                if (MarkerChanged != null)
                {
                    MarkerChanged(this, new MarkerEventArgs(marker));
                }
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleRotationTypeChangedEvent()
        {
            if (RotationTypeChanged != null)
            {
                RotationTypeChanged(this, new EventArgs());
            }

            for (int i = 0; i < Markers.Count; i++)
            {
                Marker marker = Markers[i];
                marker.Sync();
                if (MarkerChanged != null)
                {
                    MarkerChanged(this, new MarkerEventArgs(marker));
                }
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleGainChangedEvent()
        {
            if (GainChanged != null)
            {
                GainChanged(this, new EventArgs());
            }

            Alarm alarm = GetAlarm();
            if (alarm.Type != AlarmType.Normal)
            {
                if (PaletteChanged != null)
                {
                    PaletteChanged(this, new EventArgs());
                }
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandlePaletteChangedEvent()
        {
            if (PaletteChanged != null)
            {
                PaletteChanged(this, new EventArgs());
            }

            if (ImageChanged != null)
            {
                ImageChanged(this, new EventArgs());
            }
        }

        private void HandleThermalParamsChangedEvent()
        {
            for (int i = 0; i < Markers.Count; i++)
            {
                Marker marker = Markers[i];
                marker.Sync();
                if (MarkerChanged != null)
                {
                    MarkerChanged(this, new MarkerEventArgs(marker));
                }
            }

            for (int i = 0; i < Deltas.Count; i++)
            {
                Delta delta = Deltas[i];
                delta.Sync();
                if (DeltaChanged != null)
                {
                    DeltaChanged(this, new DeltaEventArgs(delta));
                }
            }
        }

        private void HandleReferenceChangedEvent()
        {
            if (ReferenceChanged != null)
            {
                ReferenceChanged(this, new EventArgs());
            }

            for (int i = 0; i < Markers.Count; i++)
            {
                Marker marker = Markers[i];
                marker.Sync();
                if (MarkerChanged != null)
                {
                    MarkerChanged(this, new MarkerEventArgs(marker));
                }
            }
        }

        private void HandleMarkerAddedEvent(Marker marker)
        {
            if (MarkerAdded != null)
            {
                MarkerAdded(this, new MarkerEventArgs(marker));
            }
        }

        private void HandleMarkerChangedEvent(Marker marker)
        {
            if (MarkerChanged != null)
            {
                MarkerChanged(this, new MarkerEventArgs(marker));
            }

            for (int i = 0; i < Deltas.Count; i++)
            {
                Delta delta = Deltas[i];
                DeltaParams deltaParams = delta.DeltaParams;
                if (string.Equals(deltaParams.SrcName, marker.Name) || string.Equals(deltaParams.DestName, marker.Name))
                {
                    delta.Sync();
                    if (DeltaChanged != null)
                    {
                        DeltaChanged(this, new DeltaEventArgs(delta));
                    }
                }
            }
        }

        private void HandleMarkerRemovedEvent(Marker marker)
        {
            if (MarkerRemoved != null)
            {
                MarkerRemoved(this, new MarkerEventArgs(marker));
            }

            if (string.Equals(ReferenceCache.Name, marker.Name))
            {
                ReferenceCache = GetReference();
                HandleReferenceChangedEvent();
            }

            List<string> deltaNames = new List<string>(GetDeltaNames());
            for (int i = 0; i < Deltas.Count; i++)
            {
                Delta delta = Deltas[i];
                if (!deltaNames.Contains(delta.Name))
                {
                    Deltas.RemoveAt(i);
                }
            }
        }

        private void HandleMarkerRenamedEvent(string oldName, Marker marker)
        {
            if (MarkerRenamed != null)
            {
                MarkerRenamed(this, new MarkerEventArgs(oldName, marker));
            }

            Reference reference = GetReference();
            if (string.Equals(reference.Name, oldName))
            {
                ReferenceCache = reference;
                if (ReferenceChanged != null)
                {
                    ReferenceChanged(this, new EventArgs());
                }
            }

            for (int i = 0; i < Deltas.Count; i++)
            {
                Delta delta = Deltas[i];
                DeltaParams deltaParams = delta.DeltaParams;
                if (string.Equals(deltaParams.SrcName, oldName) || string.Equals(deltaParams.DestName, oldName))
                {
                    delta.Sync();
                    if (DeltaChanged != null)
                    {
                        DeltaChanged(this, new DeltaEventArgs(delta));
                    }
                }
            }
        }

        private void HandleDeltaAddedEvent(Delta delta)
        {
            if (DeltaAdded != null)
            {
                DeltaAdded(this, new DeltaEventArgs(delta));
            }
        }

        private void HandleDeltaChangedEvent(Delta delta)
        {
            if (DeltaChanged != null)
            {
                DeltaChanged(this, new DeltaEventArgs(delta));
            }
        }

        private void HandleDeltaRemovedEvent(Delta delta)
        {
            if (DeltaRemoved != null)
            {
                DeltaRemoved(this, new DeltaEventArgs(delta));
            }
        }

        public static List<PresetPalette> GetPresetPalettes()
        {
            List<PresetPalette> presetPalettes = new List<PresetPalette>();

            IRtekFileSDK.ir_file_preset_palettes irPresetPalettes = new IRtekFileSDK.ir_file_preset_palettes();
            int result = IRtekFileSDK.ir_file_get_preset_palettes(ref irPresetPalettes);
            if (result == IRtekFileSDK.IR_FILE_EC_OK)
            {
                try
                {
                    int size = 1 + IRtekFileSDK.NAME_LEN;
                    irPresetPalettes.presetPalettes = Marshal.AllocHGlobal(irPresetPalettes.count * size);
                    result = IRtekFileSDK.ir_file_get_preset_palettes(ref irPresetPalettes);
                    if (result == IRtekFileSDK.IR_FILE_EC_OK)
                    {
                        for (int i = 0; i < irPresetPalettes.count; i++)
                        {
                            IRtekFileSDK.ir_file_preset_palette irPresetPalette = (IRtekFileSDK.ir_file_preset_palette)Marshal.PtrToStructure(irPresetPalettes.presetPalettes + i * size, typeof(IRtekFileSDK.ir_file_preset_palette));
                            PresetPalette presetPalette = new PresetPalette();
                            presetPalette.RefNo = irPresetPalette.refNo;
                            presetPalette.Name = BytesToString(irPresetPalette.name, Encoding.UTF8);
                            presetPalettes.Add(presetPalette);
                        }
                    }

                    return presetPalettes;
                }
                finally
                {
                    Marshal.FreeHGlobal(irPresetPalettes.presetPalettes);
                }
            }
            else
            {
                return presetPalettes;
            }
        }

        public static Bitmap GetPresetPaletteImage(byte refNo)
        {
            try
            {
                int width = 1;
                int height = IRtekFileSDK.COLORS_LEN;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                IRtekFileSDK.ir_file_image irImage = new IRtekFileSDK.ir_file_image();
                irImage.offset = bmpData.Stride - 3 * width;
                irImage.data = bmpData.Scan0;
                IRtekFileSDK.ir_file_get_preset_palette_image(refNo, ref irImage);
                bitmap.UnlockBits(bmpData);
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public abstract class Marker
        {
            private string name;
            protected LocalThermalParams thermalParams;

            public Marker(ThermalImage owner, string name, LocalThermalParams thermalParams)
            {
                Owner = owner;
                this.name = name;
                this.thermalParams = thermalParams;

                MaxInternal = 313.15F;
                MinInternal = 293.15F;
                AvgInternal = 303.15F;
                HotSpotInternal = Point.Empty;
                ColdSpotInternal = Point.Empty;
                MeasuredValueInternal = 0.0F;
            }

            public ThermalImage Owner { get; private set; }
            public abstract MarkerType Type { get; }

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (string.Equals(name, value))
                    {
                        return;
                    }

                    string oldValue = name;
                    name = value;
                    if (Owner.RenameMarker(oldValue, name))
                    {
                        Owner.HandleMarkerRenamedEvent(oldValue, this);
                    }
                    else
                    {
                        name = oldValue;
                    }
                }
            }

            public LocalThermalParams ThermalParams
            {
                get
                {
                    return thermalParams;
                }
                set
                {
                    LocalThermalParams oldValue = thermalParams;
                    thermalParams = value;
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        thermalParams = oldValue;
                    }
                }
            }

            internal float MaxInternal { get; set; }
            internal float MinInternal { get; set; }
            internal float AvgInternal { get; set; }
            internal Point HotSpotInternal { get; set; }
            internal Point ColdSpotInternal { get; set; }
            internal float MeasuredValueInternal { get; set; }

            public abstract void Sync();
        }

        public class MarkerSpot : Marker
        {
            private Point point;

            public MarkerSpot(ThermalImage owner, string name, LocalThermalParams thermalParams, Point point)
                : base(owner, name, thermalParams)
            {
                Point = point;
            }

            public override MarkerType Type
            {
                get { return MarkerType.Spot; }
            }

            public Point Point
            {
                get
                {
                    return point;
                }
                set
                {
                    if (point == value)
                    {
                        return;
                    }

                    Point oldValue = point;
                    point = value;
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        point = oldValue;
                    }
                }
            }

            public float Value
            {
                get { return MaxInternal; }
            }

            public override void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
                irMarker.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    try
                    {
                        int size = 0;
                        unsafe
                        {
                            size = sizeof(IRtekFileSDK.ir_file_point);
                        }

                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                        if (result == IRtekFileSDK.IR_FILE_EC_OK)
                        {
                            LocalThermalParams thermalParams = new LocalThermalParams();
                            thermalParams.LocalParams = irMarker.localParams == TRUE;
                            thermalParams.Emissivity = irMarker.emissivity;
                            thermalParams.Distance = irMarker.distance;
                            thermalParams.ReflTemp = irMarker.reflTemp;
                            base.thermalParams = thermalParams;

                            IRtekFileSDK.ir_file_point p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                            point = new Point(p.x, p.y);

                            float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                            MaxInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                            MinInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                            AvgInternal = temp;
                            IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                            HotSpotInternal = new Point(irPoint.x, irPoint.y);
                            irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                            ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                            MeasuredValueInternal = irMarker.measuredValue;
                        }
                        else
                        {
                            throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irMarker.points);
                    }
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                }
            }
        }

        public class MarkerRectangle : Marker
        {
            private Rectangle rectangle;

            public MarkerRectangle(ThermalImage owner, string name, LocalThermalParams thermalParams, Rectangle rectangle)
                : base(owner, name, thermalParams)
            {
                Rectangle = rectangle;
            }

            public override MarkerType Type
            {
                get { return MarkerType.Rectangle; }
            }

            public Rectangle Rectangle
            {
                get
                {
                    return rectangle;
                }
                set
                {
                    if (rectangle == value)
                    {
                        return;
                    }

                    Rectangle oldValue = rectangle;
                    rectangle = value;
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        rectangle = oldValue;
                    }
                }
            }

            public float Max
            {
                get { return MaxInternal; }
            }

            public float Min
            {
                get { return MinInternal; }
            }

            public float Avg
            {
                get { return AvgInternal; }
            }

            public Point HotSpot
            {
                get { return HotSpotInternal; }
            }

            public Point ColdSpot
            {
                get { return ColdSpotInternal; }
            }

            public float Area
            {
                get { return MeasuredValueInternal; }
            }

            public override void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
                irMarker.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    try
                    {
                        int size = 0;
                        unsafe
                        {
                            size = sizeof(IRtekFileSDK.ir_file_point);
                        }

                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                        if (result == IRtekFileSDK.IR_FILE_EC_OK)
                        {
                            LocalThermalParams thermalParams = new LocalThermalParams();
                            thermalParams.LocalParams = irMarker.localParams == TRUE;
                            thermalParams.Emissivity = irMarker.emissivity;
                            thermalParams.Distance = irMarker.distance;
                            thermalParams.ReflTemp = irMarker.reflTemp;
                            base.thermalParams = thermalParams;

                            Rectangle rect = new Rectangle();
                            IRtekFileSDK.ir_file_point p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                            Point p1 = new Point(p.x, p.y);
                            p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + size, typeof(IRtekFileSDK.ir_file_point));
                            Point p2 = new Point(p.x, p.y);
                            rect.Location = new Point(p1.X <= p2.X ? p1.X : p2.X, p1.Y <= p2.Y ? p1.Y : p2.Y);
                            rect.Size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                            rectangle = rect;

                            float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                            MaxInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                            MinInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                            AvgInternal = temp;
                            IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                            HotSpotInternal = new Point(irPoint.x, irPoint.y);
                            irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                            ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                            MeasuredValueInternal = irMarker.measuredValue;
                        }
                        else
                        {
                            throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irMarker.points);
                    }
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                }
            }
        }

        public class MarkerEllipse : Marker
        {
            private Rectangle rectangle;

            public MarkerEllipse(ThermalImage owner, string name, LocalThermalParams thermalParams, Rectangle rectangle)
                : base(owner, name, thermalParams)
            {
                Rectangle = rectangle;
            }

            public override MarkerType Type
            {
                get { return MarkerType.Ellipse; }
            }

            public Rectangle Rectangle
            {
                get
                {
                    return rectangle;
                }
                set
                {
                    Rectangle oldValue = rectangle;
                    rectangle = value;
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        rectangle = oldValue;
                    }
                }
            }

            public float Max
            {
                get { return MaxInternal; }
            }

            public float Min
            {
                get { return MinInternal; }
            }

            public float Avg
            {
                get { return AvgInternal; }
            }

            public Point HotSpot
            {
                get { return HotSpotInternal; }
            }

            public Point ColdSpot
            {
                get { return ColdSpotInternal; }
            }

            public float Area
            {
                get { return MeasuredValueInternal; }
            }

            public override void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
                irMarker.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    try
                    {
                        int size = 0;
                        unsafe
                        {
                            size = sizeof(IRtekFileSDK.ir_file_point);
                        }

                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                        if (result == IRtekFileSDK.IR_FILE_EC_OK)
                        {
                            LocalThermalParams thermalParams = new LocalThermalParams();
                            thermalParams.LocalParams = irMarker.localParams == TRUE;
                            thermalParams.Emissivity = irMarker.emissivity;
                            thermalParams.Distance = irMarker.distance;
                            thermalParams.ReflTemp = irMarker.reflTemp;
                            base.thermalParams = thermalParams;

                            Rectangle rect = new Rectangle();
                            IRtekFileSDK.ir_file_point p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points, typeof(IRtekFileSDK.ir_file_point));
                            Point p1 = new Point(p.x, p.y);
                            p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + size, typeof(IRtekFileSDK.ir_file_point));
                            Point p2 = new Point(p.x, p.y);
                            rect.Location = new Point(p1.X <= p2.X ? p1.X : p2.X, p1.Y <= p2.Y ? p1.Y : p2.Y);
                            rect.Size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                            rectangle = rect;

                            float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                            MaxInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                            MinInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                            AvgInternal = temp;
                            IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                            HotSpotInternal = new Point(irPoint.x, irPoint.y);
                            irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                            ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                            MeasuredValueInternal = irMarker.measuredValue;
                        }
                        else
                        {
                            throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irMarker.points);
                    }
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                }
            }
        }

        public class MarkerPolygon : Marker
        {
            private Point[] points;

            public MarkerPolygon(ThermalImage owner, string name, LocalThermalParams thermalParams, Point[] points)
                : base(owner, name, thermalParams)
            {
                Points = points;
            }

            public override MarkerType Type
            {
                get { return MarkerType.Polygon; }
            }

            public Point[] Points
            {
                get
                {
                    return (Point[])points.Clone();
                }
                set
                {
                    Point[] oldValue = points;
                    points = (Point[])value.Clone();
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        points = oldValue;
                    }
                }
            }

            public float Max
            {
                get { return MaxInternal; }
            }

            public float Min
            {
                get { return MinInternal; }
            }

            public float Avg
            {
                get { return AvgInternal; }
            }

            public Point HotSpot
            {
                get { return HotSpotInternal; }
            }

            public Point ColdSpot
            {
                get { return ColdSpotInternal; }
            }

            public float Area
            {
                get { return MeasuredValueInternal; }
            }

            public override void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
                irMarker.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    try
                    {
                        int size = 0;
                        unsafe
                        {
                            size = sizeof(IRtekFileSDK.ir_file_point);
                        }

                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                        if (result == IRtekFileSDK.IR_FILE_EC_OK)
                        {
                            LocalThermalParams thermalParams = new LocalThermalParams();
                            thermalParams.LocalParams = irMarker.localParams == TRUE;
                            thermalParams.Emissivity = irMarker.emissivity;
                            thermalParams.Distance = irMarker.distance;
                            thermalParams.ReflTemp = irMarker.reflTemp;
                            base.thermalParams = thermalParams;

                            Point[] points = new Point[irMarker.pointCount];
                            for (int i = 0; i < irMarker.pointCount; i++)
                            {
                                IRtekFileSDK.ir_file_point p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + i * size, typeof(IRtekFileSDK.ir_file_point));
                                points[i] = new Point(p.x, p.y);
                            }
                            this.points = points;

                            float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                            MaxInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                            MinInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                            AvgInternal = temp;
                            IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                            HotSpotInternal = new Point(irPoint.x, irPoint.y);
                            irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                            ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                            MeasuredValueInternal = irMarker.measuredValue;
                        }
                        else
                        {
                            throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irMarker.points);
                    }
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                }
            }
        }

        public class MarkerPolyline : Marker
        {
            private Point[] points;

            public MarkerPolyline(ThermalImage owner, string name, LocalThermalParams thermalParams, Point[] points)
                : base(owner, name, thermalParams)
            {
                Points = points;
            }

            public override MarkerType Type
            {
                get { return MarkerType.Polyline; }
            }

            public Point[] Points
            {
                get
                {
                    return (Point[])points.Clone();
                }
                set
                {
                    Point[] oldValue = points;
                    points = (Point[])value.Clone();
                    if (Owner.SetMarker(this))
                    {
                        Reference reference = Owner.GetReference();
                        if (string.Equals(reference.Name, Name))
                        {
                            Owner.HandleReferenceChangedEvent();
                        }
                        else
                        {
                            Owner.HandleMarkerChangedEvent(this);
                        }
                    }
                    else
                    {
                        points = oldValue;
                    }
                }
            }

            public float Max
            {
                get { return MaxInternal; }
            }

            public float Min
            {
                get { return MinInternal; }
            }

            public float Avg
            {
                get { return AvgInternal; }
            }

            public Point HotSpot
            {
                get { return HotSpotInternal; }
            }

            public Point ColdSpot
            {
                get { return ColdSpotInternal; }
            }

            public float Length
            {
                get { return MeasuredValueInternal; }
            }

            public override void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_marker irMarker = new IRtekFileSDK.ir_file_marker();
                irMarker.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    try
                    {
                        int size = 0;
                        unsafe
                        {
                            size = sizeof(IRtekFileSDK.ir_file_point);
                        }

                        irMarker.points = Marshal.AllocHGlobal(irMarker.pointCount * size);
                        result = IRtekFileSDK.ir_file_get_marker(hImage, ref irMarker);
                        if (result == IRtekFileSDK.IR_FILE_EC_OK)
                        {
                            LocalThermalParams thermalParams = new LocalThermalParams();
                            thermalParams.LocalParams = irMarker.localParams == TRUE;
                            thermalParams.Emissivity = irMarker.emissivity;
                            thermalParams.Distance = irMarker.distance;
                            thermalParams.ReflTemp = irMarker.reflTemp;
                            base.thermalParams = thermalParams;

                            Point[] points = new Point[irMarker.pointCount];
                            for (int i = 0; i < irMarker.pointCount; i++)
                            {
                                IRtekFileSDK.ir_file_point p = (IRtekFileSDK.ir_file_point)Marshal.PtrToStructure(irMarker.points + i * size, typeof(IRtekFileSDK.ir_file_point));
                                points[i] = new Point(p.x, p.y);
                            }
                            this.points = points;

                            float temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MAX];
                            MaxInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_MIN];
                            MinInternal = temp;
                            temp = irMarker.tempValues[IRtekFileSDK.IR_FILE_TEMP_AVG];
                            AvgInternal = temp;
                            IRtekFileSDK.ir_file_point irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_HOT];
                            HotSpotInternal = new Point(irPoint.x, irPoint.y);
                            irPoint = irMarker.tempSpots[IRtekFileSDK.IR_FILE_SPOT_COLD];
                            ColdSpotInternal = new Point(irPoint.x, irPoint.y);
                            MeasuredValueInternal = irMarker.measuredValue;
                        }
                        else
                        {
                            throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(irMarker.points);
                    }
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_marker error code {0}.", result));
                }
            }
        }

        public class MarkerCollection : IEnumerable
        {
            private readonly List<Marker> Entries = new List<Marker>();

            public MarkerCollection(ThermalImage owner)
            {
                Owner = owner;
            }

            public ThermalImage Owner { get; private set; }

            public int Count
            {
                get { return Entries.Count; }
            }

            public Marker this[int index]
            {
                get
                {
                    return Entries[index];
                }
            }

            public Marker this[string name]
            {
                get
                {
                    for (int i = 0; i < Entries.Count; i++)
                    {
                        if (string.Equals(Entries[i].Name, name))
                        {
                            return Entries[i];
                        }
                    }
                    throw new Exception("name not existed");
                }
            }

            public string NextSpotName
            {
                get
                {
                    return string.Format("{0}{1}", SPOT_PREFIX, GetNextIndex(SPOT_PREFIX));
                }
            }

            public string NextAreaName
            {
                get
                {
                    return string.Format("{0}{1}", AREA_PREFIX, GetNextIndex(AREA_PREFIX));
                }
            }

            public string NextPolylineName
            {
                get
                {
                    return string.Format("{0}{1}", POLYLINE_PREFIX, GetNextIndex(POLYLINE_PREFIX));
                }
            }

            public int IndexOf(string name)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (string.Equals(Entries[i].Name, name))
                    {
                        return i;
                    }
                }
                return -1;
            }

            public void Add(Marker marker)
            {
                if (!Owner.IsMarkersSynced)
                {
                    Entries.Add(marker);
                }
                else if (Owner.SetMarker(marker))
                {
                    Entries.Add(marker);
                    Owner.HandleMarkerAddedEvent(marker);
                }
                else
                {
                    throw new Exception("add failed");
                }
            }

            public bool Remove(string name)
            {
                int index = -1;
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (string.Equals(Entries[i].Name, name))
                    {
                        index = i;
                    }
                }

                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);

                return true;
            }

            public void RemoveAt(int index)
            {
                Marker marker = Entries[index];
                if (!Owner.IsMarkersSynced)
                {
                    Entries.RemoveAt(index);
                }
                else if (Owner.RemoveMarker(marker.Name))
                {
                    Entries.RemoveAt(index);
                    Owner.HandleMarkerRemovedEvent(marker);
                }
                else
                {
                    throw new Exception("remove failed");
                }
            }

            public void Clear(ClearType clearType)
            {
                switch (clearType)
                {
                    case ClearType.UserMarkers:
                        for (int i = Entries.Count - 1; i >= 0; i--)
                        {
                            Marker marker = Entries[i];
                            if (!string.Equals(marker.Name, IRtekFileSDK.GLOBAL_MARKER))
                            {
                                Remove(marker.Name);
                            }
                        }
                        break;
                }
            }

            internal void Clear()
            {
                for (int i = Entries.Count - 1; i >= 0; i--)
                {
                    Marker marker = Entries[i];
                    Remove(marker.Name);
                }
            }

            public IEnumerator GetEnumerator()
            {
                return Entries.GetEnumerator();
            }

            private int GetNextIndex(string prefix)
            {
                int index;
                SortedDictionary<int, int> idMap = new SortedDictionary<int, int>();
                for (int i = 0; i < Entries.Count; i++)
                {
                    string name = Entries[i].Name;
                    if (name.StartsWith(prefix))
                    {
                        if (int.TryParse(name.Substring(prefix.Length), out index))
                        {
                            idMap.Add(index, index);
                        }
                    }
                }

                index = 1;
                foreach (KeyValuePair<int, int> itr in idMap)
                {
                    if (itr.Key != index)
                    {
                        break;
                    }
                    index++;
                }
                return index;
            }

            public bool ContainsKey(string name)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (string.Equals(Entries[i].Name, name))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public delegate void MarkerEventHandler(object sender, MarkerEventArgs e);

        public class MarkerEventArgs : EventArgs
        {
            public MarkerEventArgs(Marker marker)
            {
                OldName = marker.Name;
                Marker = marker;
            }

            public MarkerEventArgs(string oldName, Marker marker)
            {
                OldName = oldName;
                Marker = marker;
            }

            public string OldName { get; private set; }
            public Marker Marker { get; private set; }
        }

        public class Delta
        {
            private DeltaParams deltaParams;

            public Delta(ThermalImage owner, string name, DeltaParams deltaParams)
            {
                Owner = owner;
                Name = name;
                this.deltaParams = deltaParams;
            }

            public ThermalImage Owner { get; private set; }
            public string Name { get; private set; }
            public float Value { get; internal set; }

            public DeltaParams DeltaParams
            {
                get
                {
                    return deltaParams;
                }
                set
                {
                    DeltaParams oldValue = deltaParams;
                    deltaParams = value;
                    if (Owner.SetDelta(this))
                    {
                        Owner.HandleDeltaChangedEvent(this);
                    }
                    else
                    {
                        deltaParams = oldValue;
                    }
                }
            }

            public void Sync()
            {
                IntPtr hImage = Owner.HImage;
                if (hImage == IntPtr.Zero)
                {
                    return;
                }

                IRtekFileSDK.ir_file_delta irDelta = new IRtekFileSDK.ir_file_delta();
                irDelta.name = StringToBytes(Name, IRtekFileSDK.NAME_LEN, Encoding.UTF8);
                int result = IRtekFileSDK.ir_file_get_delta(hImage, ref irDelta);
                if (result == IRtekFileSDK.IR_FILE_EC_OK)
                {
                    DeltaParams deltaParams = new DeltaParams();
                    deltaParams.SrcName = BytesToString(irDelta.srcName, Encoding.UTF8);
                    switch (irDelta.srcValueType)
                    {
                        case IRtekFileSDK.IR_FILE_MAX:
                        default:
                            deltaParams.SrcValueType = ValueType.Max;
                            break;
                        case IRtekFileSDK.IR_FILE_MIN:
                            deltaParams.SrcValueType = ValueType.Min;
                            break;
                        case IRtekFileSDK.IR_FILE_AVG:
                            deltaParams.SrcValueType = ValueType.Avg;
                            break;
                    }
                    deltaParams.SrcTemp = irDelta.srcTemp;
                    deltaParams.DestName = BytesToString(irDelta.destName, Encoding.UTF8);
                    switch (irDelta.destValueType)
                    {
                        case IRtekFileSDK.IR_FILE_MAX:
                        default:
                            deltaParams.DestValueType = ValueType.Max;
                            break;
                        case IRtekFileSDK.IR_FILE_MIN:
                            deltaParams.DestValueType = ValueType.Min;
                            break;
                        case IRtekFileSDK.IR_FILE_AVG:
                            deltaParams.DestValueType = ValueType.Avg;
                            break;
                    }
                    deltaParams.DestTemp = irDelta.destTemp;
                    this.deltaParams = deltaParams;
                    Value = irDelta.value;
                }
                else
                {
                    throw new Exception(string.Format("invoke ir_file_get_delta error code {0}.", result));
                }
            }
        }

        public class DeltaCollection : IEnumerable
        {
            private readonly List<Delta> Entries = new List<Delta>();

            public DeltaCollection(ThermalImage owner)
            {
                Owner = owner;
            }

            public ThermalImage Owner { get; private set; }

            public int Count
            {
                get { return Entries.Count; }
            }

            public Delta this[int index]
            {
                get
                {
                    return Entries[index];
                }
            }

            public Delta this[string name]
            {
                get
                {
                    for (int i = 0; i < Entries.Count; i++)
                    {
                        if (string.Equals(Entries[i].Name, name))
                        {
                            return Entries[i];
                        }
                    }
                    throw new Exception("name not existed");
                }
            }

            public string NextDeltaName
            {
                get
                {
                    return string.Format("{0}{1}", DELTA_PREFIX, GetNextIndex(DELTA_PREFIX));
                }
            }

            public void Add(Delta delta)
            {
                if (!Owner.IsDeltasSynced)
                {
                    Entries.Add(delta);
                }
                else if (Owner.SetDelta(delta))
                {
                    Entries.Add(delta);
                    Owner.HandleDeltaAddedEvent(delta);
                }
                else
                {
                    throw new Exception("add failed");
                }
            }

            public bool Remove(string name)
            {
                int index = -1;
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (string.Equals(Entries[i].Name, name))
                    {
                        index = i;
                    }
                }

                if (index == -1)
                {
                    return false;
                }

                RemoveAt(index);

                return true;
            }

            public void RemoveAt(int index)
            {
                Delta delta = Entries[index];
                if (!Owner.IsDeltasSynced)
                {
                    Entries.RemoveAt(index);
                }
                else if (Owner.RemoveDelta(delta.Name))
                {
                    Entries.RemoveAt(index);
                    Owner.HandleDeltaRemovedEvent(delta);
                }
                else
                {
                    throw new Exception("remove failed");
                }
            }

            public void Clear()
            {
                for (int i = Entries.Count - 1; i >= 0; i--)
                {
                    Delta delta = Entries[i];
                    Remove(delta.Name);
                }
            }

            public IEnumerator GetEnumerator()
            {
                return Entries.GetEnumerator();
            }

            private int GetNextIndex(string prefix)
            {
                int index;
                SortedDictionary<int, int> idMap = new SortedDictionary<int, int>();
                for (int i = 0; i < Entries.Count; i++)
                {
                    string name = Entries[i].Name;
                    if (name.StartsWith(prefix))
                    {
                        if (int.TryParse(name.Substring(prefix.Length), out index))
                        {
                            idMap.Add(index, index);
                        }
                    }
                }

                index = 1;
                foreach (KeyValuePair<int, int> itr in idMap)
                {
                    if (itr.Key != index)
                    {
                        break;
                    }
                    index++;
                }
                return index;
            }
        }

        public delegate void DeltaEventHandler(object sender, DeltaEventArgs e);

        public class DeltaEventArgs : EventArgs
        {
            public DeltaEventArgs(Delta delta)
            {
                OldName = delta.Name;
                Delta = delta;
            }

            public DeltaEventArgs(string oldName, Delta delta)
            {
                OldName = oldName;
                Delta = delta;
            }

            public string OldName { get; private set; }
            public Delta Delta { get; private set; }
        }

        public enum ClearType
        {
            UserMarkers,
        }
    }
}
