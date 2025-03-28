using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace OWB.TADemo
{
    public static class OWBGlobal
    {
        public static OWBCamera Camera = null;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        public static int[] StreamSDKVersion;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        public static int[] DetectSDKVersion;
    }

    public enum FormType
    {
        Add,
        Update,
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

    public class MarkerItem
    {
        private string _MarkerName;
        private MarkerType _MarkerType;

        public MarkerItem(string markerName, MarkerType markerType)
        {
            MarkerName = markerName;
            MarkerType = markerType;
        }
        public string MarkerName { get => _MarkerName; set => _MarkerName = value; }
        public MarkerType MarkerType { get => _MarkerType; set => _MarkerType = value; }

        public override string ToString()
        {
            return MarkerName;
        }
    }

}
