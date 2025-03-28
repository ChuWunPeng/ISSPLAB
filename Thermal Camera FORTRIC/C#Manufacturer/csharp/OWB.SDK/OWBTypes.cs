using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SDK
{
    public class OWBTypes
    {
        public const int OK = 200;
        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int InternalServerError = 500;
        public const int BadGateway = 502;

        public static bool CheckStatus(int statusCode)
        {
            if (statusCode - OK < 100 && statusCode - OK >= 0)
            {
                return true;
            }
            return false;
        }

        [DataContract]
        public class OWBStatusCode
        {
            [DataMember(Name = "sc", IsRequired = true)]
            public int SC;

            public OWBStatusCode()
            {
                SC = OK;
            }
        }

        [DataContract]
        public class Device_FW
        {
            [DataMember(Name = "name", IsRequired = true)]
            public string Name;
            [DataMember(Name = "version", IsRequired = true)]
            public int[] Version;
            [DataMember(Name = "build", IsRequired = true)]
            public string Build;
            [DataMember(Name = "tag")]
            public string Tag;
        }

        [DataContract]
        public class AdminInfo
        {
            [DataMember(Name = "create-date")]
            public string Create_Date;
            [DataMember(Name = "device-fw", IsRequired = true)]
            public Device_FW Device_FW;
            [DataMember(Name = "device-id")]
            public string Device_ID;
            [DataMember(Name = "device-model")]
            public string Device_Model;
            [DataMember(Name = "device-sn0")]
            public string Device_SN0;
            [DataMember(Name = "device-sn1")]
            public string Device_SN1;
            [DataMember(Name = "device-tag0")]
            public string Device_Tag0;
            [DataMember(Name = "device-tag1")]
            public string Device_Tag1;
            [DataMember(Name = "device-tag2")]
            public string Device_Tag2;
            [DataMember(Name = "device-version")]
            public string Device_Version;
            [DataMember(Name = "fpga")]
            public string FPGA;
            [DataMember(Name = "fpga-version")]
            public string FPGA_Version;
            [DataMember(Name = "hardware")]
            public string Hardware;
            [DataMember(Name = "hardware-version")]
            public string Hardware_Version;
            [DataMember(Name = "modify-date")]
            public string Modify_Date;
            [DataMember(Name = "seal-date")]
            public string Seal_Date;
            [DataMember(Name = "sensor")]
            public string Sensor;
            [DataMember(Name = "sensor-sn")]
            public string Sensor_SN;
            [DataMember(Name = "update-counter")]
            public int Update_Counter;
        }

        [DataContract]
        public class SensorDimension
        {
            [DataMember(Name = "h", IsRequired = true)]
            public int H;
            [DataMember(Name = "w", IsRequired = true)]
            public int W;
        }

        [DataContract]
        public class StreamVideoRaw
        {
            [DataMember(Name = "width", IsRequired = true)]
            public int Width;
            [DataMember(Name = "height", IsRequired = true)]
            public int Height;
            [DataMember(Name = "pixel-format", IsRequired = true)]
            public string Pixel_Format;
            [DataMember(Name = "max-packet-size", IsRequired = true)]
            public int Max_Packet_Size;
            [DataMember(Name = "max-fps")]
            public int Max_FPS;
            [DataMember(Name = "max-connection-num", IsRequired = true)]
            public int Max_Connection_Num;
        }

        [DataContract]
        public class StreamVideoPri
        {
            [DataMember(Name = "bitrate")]
            public int Bitrate;
            [DataMember(Name = "connections", IsRequired = true)]
            public int Connections;
            [DataMember(Name = "width", IsRequired = true)]
            public int Width;
            [DataMember(Name = "height", IsRequired = true)]
            public int Height;
            [DataMember(Name = "pixel-format", IsRequired = true)]
            public string Pixel_Format;
            [DataMember(Name = "fps")]
            public float FPS;
            [DataMember(Name = "max-bitrate")]
            public int Max_BitRate;
            [DataMember(Name = "max-packet-size", IsRequired = true)]
            public int Max_Packet_Size;
            [DataMember(Name = "max-connection-num", IsRequired = true)]
            public int Max_Connection_Num;
        }

        [DataContract]
        public class StreamVideoPri_BitRate
        {
            [DataMember(Name = "bitrate", IsRequired = true)]
            public int BitRate;
        }

        [DataContract]
        public class StreamVideoSub
        {
            [DataMember(Name = "bitrate")]
            public int Bitrate;
            [DataMember(Name = "connections", IsRequired = true)]
            public int Connections;
            [DataMember(Name = "width", IsRequired = true)]
            public int Width;
            [DataMember(Name = "height", IsRequired = true)]
            public int Height;
            [DataMember(Name = "pixel-format", IsRequired = true)]
            public string Pixel_Format;
            [DataMember(Name = "max-bitrate")]
            public int Max_BitRate;
            [DataMember(Name = "max-packet-size", IsRequired = true)]
            public int Max_Packet_Size;
            [DataMember(Name = "max-connection-num", IsRequired = true)]
            public int Max_Connection_Num;
        }

        [DataContract]
        public class StreamVideoSub_BitRate
        {
            [DataMember(Name = "bitrate", IsRequired = true)]
            public int BitRate;
        }

        [DataContract]
        public class OsdTrayPlt
        {
            [DataMember(Name = "inverse", IsRequired = true)]
            public bool Inverse;
            [DataMember(Name = "name", IsRequired = true)]
            public string Name;
        }

        [DataContract]
        public class IspTItem
        {
            [DataMember(Name = "r", IsRequired = true)]
            public ushort R;
            [DataMember(Name = "t", IsRequired = true)]
            public float T;
        }

        [DataContract]
        public class FileCache
        {
            [DataMember(Name = "path", IsRequired = true)]
            public string Path;
        }

        [DataContract]
        public class TMotionItem
        {
            [DataMember(Name = "r", IsRequired = true)]
            public ushort R;
            [DataMember(Name = "t", IsRequired = true)]
            public float T;
            [DataMember(Name = "x", IsRequired = true)]
            public ushort X;
            [DataMember(Name = "y", IsRequired = true)]
            public ushort Y;
        }

        [DataContract]
        public class TMotion
        {
            [DataMember(Name = "max")]
            public TMotionItem Max;
            [DataMember(Name = "min")]
            public TMotionItem Min;
            [DataMember(Name = "avg")]
            public TMotionItem Avg;
        }

        [DataContract]
        public class MaxPackageSize
        {
            [DataMember(Name = "max-packet-size", IsRequired = true)]
            public int Max_Packet_Size;
        }

        [DataContract]
        public class InstrumentJconfig
        {
            [DataMember(Name = "ambient-t")]
            public double Atmosphere_t;
            [DataMember(Name = "distance")]
            public double Distance;
            [DataMember(Name = "emissivity")]
            public double Emission;
            [DataMember(Name = "reflect-t")]
            public double Reflection_t;
            [DataMember(Name = "rh")]
            public double RH;
            [DataMember(Name = "lens-t")]
            public double Lens_t;
            [DataMember(Name = "lens-transmissivity")]
            public double Lens_transmission;
            [DataMember(Name = "offset")]
            public double Offset;
        }

        [DataContract]
        public class Pos
        {
            [DataMember(Name = "x", IsRequired = true)]
            public int X;
            [DataMember(Name = "y", IsRequired = true)]
            public int Y;
        }

        [DataContract]
        public class ModbusSlot
        {
            [DataMember(Name = "mb-slot")]
            public int Mb_slot;
        }

        [DataContract]
        public class Alarm
        {
            [DataMember(Name = "alarm-t")]
            public float Value;
        }

        [DataContract]
        public class Marker
        {
            [DataMember(Name = "label")]
            [JsonProperty(PropertyName = "label")]
            public string Label;
            [DataMember(Name = "emissivity")]
            [JsonProperty(PropertyName = "emissivity", NullValueHandling = NullValueHandling.Ignore)]
            public float Emission;
            [JsonIgnore]
            public bool EmissionVisible;
            [DataMember(Name = "reflect-t")]
            [JsonProperty(PropertyName = "reflect-t")]
            public float ReflectionTemp;
            [DataMember(Name = "distance")]
            [JsonProperty(PropertyName = "distance", NullValueHandling = NullValueHandling.Ignore)]
            public float Distance;
            [JsonIgnore]
            public bool DistanceVisible;
            [DataMember(Name = "offset")]
            [JsonProperty(PropertyName = "offset")]
            public float Offset;
            [DataMember(Name = "osd-visible")]
            [JsonProperty(PropertyName = "osd-visible")]
            public bool Osd_visible;
            [DataMember(Name = "api-visible")]
            [JsonProperty(PropertyName = "api-visible")]
            public bool Api_visible;
            [DataMember(Name = "mb-slot")]
            [JsonProperty(PropertyName = "mb-slot")]
            public string Mb_slot;
            [DataMember(Name = "alarm-t")]
            [JsonProperty(PropertyName = "alarm-t")]
            public string Alarm;
        }

        [DataContract]
        public class SpotMarker : Marker
        {
            [DataMember(Name = "pos", IsRequired = true)]
            [JsonProperty(PropertyName = "pos")]
            public Pos Point;
        }

        [DataContract]
        public class PolygonMarker : Marker
        {
            [DataMember(Name = "polygon", IsRequired = true)]
            [JsonProperty(PropertyName = "polygon")]
            public Pos[] PointList;
            [DataMember(Name = "layer", IsRequired = true)]
            [JsonProperty(PropertyName = "layer")]
            public int Layer;
            [DataMember(Name = "max")]
            [JsonProperty(PropertyName = "max")]
            public bool MaxVisible;
            [DataMember(Name = "min")]
            [JsonProperty(PropertyName = "min")]
            public bool MinVisible;
            [DataMember(Name = "avg")]
            [JsonProperty(PropertyName = "avg")]
            public bool AvgVisible;
        }

        [DataContract]
        public class LineMarker : Marker
        {
            [DataMember(Name = "polyline", IsRequired = true)]
            [JsonProperty(PropertyName = "polyline")]
            public Pos[] PointList;
            [DataMember(Name = "max")]
            [JsonProperty(PropertyName = "max")]
            public bool MaxVisible;
            [DataMember(Name = "min")]
            [JsonProperty(PropertyName = "min")]
            public bool MinVisible;
            [DataMember(Name = "avg")]
            [JsonProperty(PropertyName = "avg")]
            public bool AvgVisible;
        }

        [DataContract]
        public class MaskMarker : Marker
        {
            [DataMember(Name = "mask", IsRequired = true)]
            [JsonProperty(PropertyName = "mask")]
            public string MaskPath;
            [DataMember(Name = "layer", IsRequired = true)]
            [JsonProperty(PropertyName = "layer")]
            public int Layer;
        }

        [DataContract]
        public class OSDProperty
        {
            [DataMember(Name = "align")]
            public string Align;
            [DataMember(Name = "pos")]
            public Pos Point;
            [DataMember(Name = "show")]
            public bool visible;
        }

        [DataContract]
        public class UserProperty
        {
            [DataMember(Name = "pw")]
            public string Password;
            [DataMember(Name = "group")]
            public string UserGroup;
        }

        [DataContract]
        public class ISPJconfig
        {
            [DataMember(Name = "black-level")]
            public int BlackLevel;
            [DataMember(Name = "contrast")]
            public float Contrast;
            [DataMember(Name = "method")]
            public string Method;
            [DataMember(Name = "roi")]
            public string Roi;
        }

        [DataContract]
        public class SensorLens
        {
            [DataMember(Name = "model", IsRequired = true)]
            public string Model;
        }

        [DataContract]
        public class SensorT_Range
        {
            [DataMember(Name = "high", IsRequired = true)]
            public float High;
            [DataMember(Name = "low", IsRequired = true)]
            public float Low;
        }

        [DataContract]
        public class SensorLUT
        {
            [DataMember(Name = "lens", IsRequired = true)]
            public int Lens;
            [DataMember(Name = "t-range", IsRequired = true)]
            public int T_Range;
        }

        [DataContract]
        public class SensorLUTItem
        {
            [DataMember(Name = "r", IsRequired = true)]
            public ushort R;
            [DataMember(Name = "t", IsRequired = true)]
            public float T;
        }

        [DataContract]
        public class SensorJConfig
        {
            [DataMember(Name = "selected-lens", IsRequired = true)]
            public int Selected_Lens;
            [DataMember(Name = "selected-t-range", IsRequired = true)]
            public int Selected_T_Range;
        }

        [DataContract]
        public class SensorJConfig_Selected_Lens
        {
            [DataMember(Name = "selected-lens", IsRequired = true)]
            public int Selected_Lens;
        }

        [DataContract]
        public class SensorJConfig_Selected_T_Range
        {
            [DataMember(Name = "selected-t-range", IsRequired = true)]
            public int Selected_T_Range;
        }

        [DataContract]
        public class SerialPort
        {
            [DataMember(Name = "baudrate", IsRequired = true)]
            public int Baudrate;
            [DataMember(Name = "csize", IsRequired = true)]
            public int CSize;
            [DataMember(Name = "parity", IsRequired = true)]
            public string Parity;
            [DataMember(Name = "stopbit", IsRequired = true)]
            public string Stopbit;
        }

        [DataContract]
        public class Template
        {
            private string _Name;
            private string _Method;
            private string _URL;
            private object _Data;

            public Template(string name, string method, string url, string data)
            {
                Name = name;
                Method = method;
                URL = url;
                Data = data;
            }

            [DataMember(Name = "name", IsRequired = true)]
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            [DataMember(Name = "method", IsRequired = true)]
            public string Method
            {
                get { return _Method; }
                set { _Method = value; }
            }
            [DataMember(Name = "url", IsRequired = true)]
            public string URL
            {
                get { return _URL; }
                set { _URL = value; }
            }
            [DataMember(Name = "data", IsRequired = true)]
            public object Data
            {
                get { return _Data; }
                set { _Data = value; }
            }
            public override string ToString()
            {
                return string.Format("{0}", Name);
            }
        }
    }

    [DataContract]
    public class SensorVersionInfo
    {
        [DataMember(Name = "id", IsRequired = true)]
        public string ID;
        [DataMember(Name = "model", IsRequired = true)]
        public string Model;
        [DataMember(Name = "version", IsRequired = true)]
        public string Version;
    }

    [DataContract]
    public class SensorCommand
    {
        [DataMember(Name = "cmd0", IsRequired = true)]
        public string CMD0;
        [DataMember(Name = "cmd1", IsRequired = true)]
        public string CMD1;
        [DataMember(Name = "data", IsRequired = true)]
        public string Data;
    }

    [DataContract]
    public class SensorData
    {
        [DataMember(Name = "data")]
        public string Data;
        [DataMember(Name = "status", IsRequired = true)]
        public string Status;
    }
}
