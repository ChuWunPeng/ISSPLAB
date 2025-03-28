using System.Drawing;

namespace OWB.SnipDemo.SDK
{
    public class FileSDKType
    {
        public enum IRMarkerType
        {
            Spot = 1,
            Rectangle = 2,
            Ellipse = 3,
            Polyline = 4,
            Polygon = 5,
            Delta = 6,
        }

        public enum IRFileType
        {
            Invalid = 0,
            IR_IRS = 1,
            IR_JPG = 2,
            IR_FLIR_SEQ = 3,
            IR_FLIR_JPG_IMG = 4,
            IR_JPG2 = 5,
            IR_IR = 6,
            IR_FJPG = 7,
        }

        public abstract class OWBFileMarker
        {
            private string _Name;
            private bool _IndependentEmissivity;
            private float _Emissivity;

            public OWBFileMarker(string name, bool independentEmissivity, float emissivity, float reflTemp, float distance, float offsetTemp)
            {
                Name = name;
                IndependentEmissivity = independentEmissivity;
                Emissivity = emissivity;
                ReflectedTemperature = reflTemp;
                Distance = distance;
                OffsetTemp = offsetTemp;

                Max = 313.15F;
                Min = 293.15F;
                Avg = 303.15F;
            }

            internal void SetTemp(float max, float min, float avg)
            {
                Max = max;
                Min = min;
                Avg = avg;
            }
            public abstract IRMarkerType MarkerType { get; }

            public string Name
            {
                get { return _Name; }
                private set { _Name = value; }
            }

            public bool IndependentEmissivity
            {
                get { return _IndependentEmissivity; }
                private set { _IndependentEmissivity = value; }
            }

            public float Emissivity
            {
                get { return _Emissivity; }
                private set { _Emissivity = value; }
            }

            public float ReflectedTemperature { get; private set; }

            public float Distance { get; private set; }

            public float OffsetTemp { get; private set; }

            public float Max { get; private set; }

            public float Min { get; private set; }

            public float Avg { get; private set; }
        }

        public class OWBFileSpotMarker : OWBFileMarker
        {
            private Point _Point;

            public OWBFileSpotMarker(string name, bool independentEmissivity, float emissivity, Point point, float reflTemp, float distance, float offsetTemp)
                    : base(name, independentEmissivity, emissivity, reflTemp, distance, offsetTemp)
            {
                Point = point;
            }

            public override IRMarkerType MarkerType
            {
                get { return IRMarkerType.Spot; }
            }

            public Point Point
            {
                get { return _Point; }
                private set { _Point = value; }
            }
        }

        public class OWBFilePolylineMarker : OWBFileMarker
        {
            private Point[] _Points;

            public OWBFilePolylineMarker(string name, bool independentEmissivity, float emissivity, Point[] points, float reflTemp, float distance, float offsetTemp)
                    : base(name, independentEmissivity, emissivity, reflTemp, distance, offsetTemp)
            {
                Points = points;
            }

            public override IRMarkerType MarkerType
            {
                get { return IRMarkerType.Polyline; }
            }

            public Point[] Points
            {
                get { return _Points; }
                private set { _Points = value; }
            }
        }

        public class OWBFilePolygonMarker : OWBFileMarker
        {
            private Point[] _Points;

            public OWBFilePolygonMarker(string name, bool independentEmissivity, float emissivity, Point[] points, float reflTemp, float distance, float offsetTemp)
                : base(name, independentEmissivity, emissivity, reflTemp, distance, offsetTemp)
            {
                Points = points;
            }

            public override IRMarkerType MarkerType
            {
                get { return IRMarkerType.Polygon; }
            }

            public Point[] Points
            {
                get { return _Points; }
                private set { _Points = value; }
            }
        }

        public enum IRDeltaMarkerType
        {
            None = 0,
            Marker = 1,
            Temperature = 2,
        }

        public enum IRTemperatureType
        {
            None = 0,
            Max = 1,
            Min = 2,
            Avg = 3,
        }

        public class OWBRefMarker
        {
            private IRDeltaMarkerType _DeltaMarkerType;
            private IRTemperatureType _TemperatureType;
            private string _Name;
            private float _Temperature;

            public OWBRefMarker(IRDeltaMarkerType deltaType, IRTemperatureType temperatureType, string name, float temperature)
            {
                DeltaMarkerType = deltaType;
                TemperatureType = temperatureType;
                Name = name;
                Temperature = temperature;
            }

            public IRDeltaMarkerType DeltaMarkerType
            {
                get { return _DeltaMarkerType; }
                private set { _DeltaMarkerType = value; }
            }

            public IRTemperatureType TemperatureType
            {
                get { return _TemperatureType; }
                private set { _TemperatureType = value; }
            }

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public float Temperature
            {
                get { return _Temperature; }
                private set { _Temperature = value; }
            }
        }
    }
}
