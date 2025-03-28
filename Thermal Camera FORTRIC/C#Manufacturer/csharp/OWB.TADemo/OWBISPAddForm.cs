using SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OWB.TADemo
{
    public partial class OWBISPAddForm : Form
    {
        private ShapeType _ShapeType;
        private Point _Spot;
        private List<Point> _PointList;
        private byte[] _Mask;
        public OWBISPAddForm(ShapeType shapeType)
        {
            InitializeComponent();

            ShapeType = shapeType;
            Spot = Point.Empty;
            PointList = null;
            Mask = null;
        }

        public ShapeType ShapeType { get => _ShapeType; set => _ShapeType = value; }
        public Point Spot { get => _Spot; set => _Spot = value; }
        public List<Point> PointList { get => _PointList; set => _PointList = value; }
        public byte[] Mask { get => _Mask; set => _Mask = value; }

        private void OWBISPAddForm_Load(object sender, EventArgs e)
        {
            if (ShapeType == ShapeType.Mask || ShapeType == ShapeType.Polygon)
            {
                lblLayer.Enabled = true;
                tbLayer.Enabled = true;
            }
            else
            {
                lblLayer.Enabled = false;
                tbLayer.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMarkerName.Text.Trim()))
            {
                ttISPAddForm.Show("标记名不能为空！", tbMarkerName, 0, -15, 1000);
                return;
            }
            if (Regex.IsMatch(tbMarkerName.Text.Trim(), @"[\u4e00-\u9fa5]"))
            {
                tbMarkerName.Focus();
                ttISPAddForm.Show("标记名不能为中文！", tbMarkerName, 0, -15, 1000);
                return;
            }
            if (cbAlarm.Checked)
            {
                if (!Regex.IsMatch(tbAlarm.Text.Trim(), @"^([1-9][0-9]*)+(.[0-9]{1,2})?$"))
                {
                    tbAlarm.Focus();
                    ttISPAddForm.Show("温度报警阈值必须为数字！", tbAlarm, 0, -15, 1000);
                    return;
                }
            }
            if (tbLayer.Enabled)
            {
                if (!Regex.IsMatch(tbLayer.Text.Trim(), @"^[0-9]*$"))
                {
                    tbLayer.Focus();
                    ttISPAddForm.Show("区域层级必须为整数！", tbLayer, 0, -15, 1000);
                    return;
                }
            }

            switch (ShapeType)
            {
                case ShapeType.Spot:
                    OWBTypes.SpotMarker pointMarker = new OWBTypes.SpotMarker();
                    pointMarker.Api_visible = cbAPI.Checked;
                    pointMarker.Osd_visible = cbOSD.Checked;
                    pointMarker.Emission = (float)nudEmissivityValue.Value;
                    pointMarker.Distance = (float)nudDistanceValue.Value;
                    pointMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                    pointMarker.Offset = (float)nudOffsetValue.Value;
                    pointMarker.Point = new OWBTypes.Pos();
                    pointMarker.Point.X = Spot.X;
                    pointMarker.Point.Y = Spot.Y;
                    pointMarker.Label = tbMarkerName.Text.Trim();
                    if (OWBGlobal.Camera.PutPoint(tbMarkerName.Text.Trim(), pointMarker))
                    {
                        if (cbModbus.Checked)
                        {
                            OWBGlobal.Camera.PutModbus(tbMarkerName.Text.Trim(), MarkerType.Spot, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                        }
                        if (cbAlarm.Checked)
                        {
                            OWBGlobal.Camera.PutAlarm(tbMarkerName.Text.Trim(), MarkerType.Spot, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("添加点失败！");
                    }
                    break;
                case ShapeType.Polygon:
                    OWBTypes.PolygonMarker regionMarker = new OWBTypes.PolygonMarker();
                    regionMarker.Api_visible = cbAPI.Checked;
                    regionMarker.Osd_visible = cbOSD.Checked;
                    regionMarker.Emission = (float)nudEmissivityValue.Value;
                    regionMarker.Distance = (float)nudDistanceValue.Value;
                    regionMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                    regionMarker.Offset = (float)nudOffsetValue.Value;
                    regionMarker.PointList = new OWBTypes.Pos[PointList.Count];
                    regionMarker.Label = tbMarkerName.Text.Trim();
                    regionMarker.Layer = int.Parse(tbLayer.Text.Trim());
                    regionMarker.MaxVisible = cbMax.Checked;
                    regionMarker.MinVisible = cbMin.Checked;
                    regionMarker.AvgVisible = cbAvg.Checked;
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        OWBTypes.Pos point = new OWBTypes.Pos();
                        point.X = PointList[i].X;
                        point.Y = PointList[i].Y;
                        regionMarker.PointList[i] = point;
                    }
                    if (OWBGlobal.Camera.PutRegion(tbMarkerName.Text.Trim(), regionMarker))
                    {
                        if (cbModbus.Checked)
                        {
                            OWBGlobal.Camera.PutModbus(tbMarkerName.Text.Trim(), MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                        }
                        if (cbAlarm.Checked)
                        {
                            OWBGlobal.Camera.PutAlarm(tbMarkerName.Text.Trim(), MarkerType.Region, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("添加区域失败！");
                    }
                    break;
                case ShapeType.Mask:
                    OWBTypes.MaskMarker maskMarker = new OWBTypes.MaskMarker();
                    maskMarker.Api_visible = cbAPI.Checked;
                    maskMarker.Osd_visible = cbOSD.Checked;
                    maskMarker.Emission = (float)nudEmissivityValue.Value;
                    maskMarker.Distance = (float)nudDistanceValue.Value;
                    maskMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                    maskMarker.Offset = (float)nudOffsetValue.Value;
                    maskMarker.Label = tbMarkerName.Text.Trim();
                    maskMarker.Layer = int.Parse(tbLayer.Text.Trim());
                    string path = string.Empty;
                    if (OWBGlobal.Camera.GetMask(Mask, out path))
                    {
                        maskMarker.MaskPath = path;
                    }
                    if (OWBGlobal.Camera.PutMask(tbMarkerName.Text.Trim(), maskMarker))
                    {
                        if (cbModbus.Checked)
                        {
                            OWBGlobal.Camera.PutModbus(tbMarkerName.Text.Trim(), MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                        }
                        if (cbAlarm.Checked)
                        {
                            OWBGlobal.Camera.PutAlarm(tbMarkerName.Text.Trim(), MarkerType.Region, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("添加蒙版失败！");
                    }
                    break;
                case ShapeType.Line:
                    OWBTypes.LineMarker lineMarker = new OWBTypes.LineMarker();
                    lineMarker.Api_visible = cbAPI.Checked;
                    lineMarker.Osd_visible = cbOSD.Checked;
                    lineMarker.Emission = (float)nudEmissivityValue.Value;
                    lineMarker.Distance = (float)nudDistanceValue.Value;
                    lineMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                    lineMarker.Offset = (float)nudOffsetValue.Value;
                    lineMarker.PointList = new OWBTypes.Pos[PointList.Count];
                    lineMarker.Label = tbMarkerName.Text.Trim();
                    lineMarker.MaxVisible = cbMax.Checked;
                    lineMarker.MinVisible = cbMin.Checked;
                    lineMarker.AvgVisible = cbAvg.Checked;
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        OWBTypes.Pos point = new OWBTypes.Pos();
                        point.X = PointList[i].X;
                        point.Y = PointList[i].Y;
                        lineMarker.PointList[i] = point;
                    }
                    if (OWBGlobal.Camera.PutLine(tbMarkerName.Text.Trim(), lineMarker))
                    {
                        if (cbModbus.Checked)
                        {
                            OWBGlobal.Camera.PutModbus(tbMarkerName.Text.Trim(), MarkerType.Line, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                        }
                        if (cbAlarm.Checked)
                        {
                            OWBGlobal.Camera.PutAlarm(tbMarkerName.Text.Trim(), MarkerType.Line, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("添加线失败！");
                    }
                    break;
            }
        }

        private void cbModbus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbModbus.Checked)
            {
                nudMbSlot.Enabled = true;
            }
            else
            {
                nudMbSlot.Enabled = false;
            }
        }

        private void cbAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAlarm.Checked)
            {
                tbAlarm.Enabled = true;
            }
            else
            {
                tbAlarm.Enabled = false;
            }
        }

        private void cbEmissivityVisible_CheckedChanged(object sender, EventArgs e)
        {
            if(cbEmissivityVisible.Checked)
            {
                nudEmissivityValue.Enabled = true;
            }
            else
            {
                nudEmissivityValue.Enabled = false;
            }
        }

        private void cbDistanceVisible_CheckedChanged(object sender, EventArgs e)
        {
            if(cbDistanceVisible.Checked)
            {
                nudDistanceValue.Enabled = true;
            }
            else
            {
                nudDistanceValue.Enabled = false;
            }
        }
    }
}
