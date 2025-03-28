namespace OWB.SnipDemo.SDK
{
    public class DeviceSDKType
    {
        public enum LoginType
        {
            H264 = 0,
            RAW = 1,
        }

        public enum StreamType
        {
            PRI = 0,
            SUB = 1,
        }

        public enum MarkerType
        {
            Spot,
            Region,
            Line,
        }

        public class MarkerTypeItem
        {
            private MarkerType _MarkerType;

            public static string Point = "点";
            public static string Region = "区域";
            public static string Line = "线";

            public MarkerTypeItem(MarkerType makerType)
            {
                MarkerType = makerType;
            }

            public MarkerTypeItem(string markerTypeItem)
            {
                if (markerTypeItem == Point)
                {
                    MarkerType = MarkerType.Spot;
                }
                else if (markerTypeItem == Region)
                {
                    MarkerType = MarkerType.Region;
                }
                else if (markerTypeItem == Line)
                {
                    MarkerType = MarkerType.Line;
                }
            }

            public MarkerType MarkerType
            {
                get { return _MarkerType; }
                private set { _MarkerType = value; }
            }

            public override int GetHashCode()
            {
                return (int)MarkerType;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is MarkerTypeItem))
                {
                    return false;
                }

                return MarkerType == (obj as MarkerTypeItem).MarkerType;
            }

            public override string ToString()
            {
                switch (MarkerType)
                {
                    case MarkerType.Spot:
                        return Point;
                    case MarkerType.Region:
                        return Region;
                    case MarkerType.Line:
                        return Line;
                    default:
                        return string.Empty;
                }
            }
        }

        public enum ShapeType
        {
            None,
            Spot,
            Polygon,
            Line,
            Mask,
        }
    }
}
