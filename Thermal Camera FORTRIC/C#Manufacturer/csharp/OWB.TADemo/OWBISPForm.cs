using SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OWB.TADemo
{
    public partial class OWBISPForm : Form
    {
        private const int COLUMN_X = 0;
        private const int COLUMN_Y = 1;

        private static Pen BorderPen = new Pen(Color.Black, 3.0F);
        private static Pen PadPen = new Pen(Color.White, 1.0F);
        private static Brush MaskBursh = new SolidBrush(Color.White);

        private ShapeType _ShapeType;
        private List<GraphicsPoint> _ShapePoints;
        private bool _IsPainting;
        private float _WidthRatio;
        private float _HeightRatio;
        private int _ImageWidth;
        private int _ImageHeight;

        public OWBISPForm()
        {
            InitializeComponent();
            ImageWidth = OWBGlobal.Camera.Width;
            ImageHeight = OWBGlobal.Camera.Height;
            ShapePoints = new List<GraphicsPoint>();

            RefreshForm();
        }

        public ShapeType ShapeType
        {
            get => _ShapeType;
            set
            {
                _ShapeType = value;
                SetShapeType();
            }
        }
        public List<GraphicsPoint> ShapePoints { get => _ShapePoints; set => _ShapePoints = value; }
        public bool IsPainting { get => _IsPainting; set => _IsPainting = value; }

        private float WidthRatio
        {
            get { return _WidthRatio; }
            set { _WidthRatio = value; }
        }

        private float HeightRatio
        {
            get { return _HeightRatio; }
            set { _HeightRatio = value; }
        }

        private int ImageWidth
        {
            get { return _ImageWidth; }
            set
            {
                _ImageWidth = value;
                WidthRatio = (float)tpbTA.Width / _ImageWidth;
            }
        }

        private int ImageHeight
        {
            get { return _ImageHeight; }
            set
            {
                _ImageHeight = value;
                HeightRatio = (float)tpbTA.Height / _ImageHeight;
            }
        }
        private void tpbTA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsPainting = true;
                ShapePoints.Add(new GraphicsPoint { IsTemp = false, Point = e.Location });
                if (ShapeType == ShapeType.Spot)
                {
                    Draw();
                }
            }
        }

        private void tpbTA_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPainting)
            {
                switch (ShapeType)
                {
                    case ShapeType.Polygon:
                    case ShapeType.Line:
                        if (ShapePoints.Count == 0)
                        {
                            return;
                        }
                        GraphicsPoint lastPoint = ShapePoints.Last();

                        if (lastPoint.IsTemp)
                        {
                            ShapePoints.Remove(lastPoint);
                        }
                        ShapePoints.Add(new GraphicsPoint { IsTemp = true, Point = e.Location });
                        Draw();
                        break;
                    case ShapeType.Mask:
                        ShapePoints.Add(new GraphicsPoint { IsTemp = false, Point = e.Location });
                        Draw();
                        break;
                }
            }
        }

        private void tpbTA_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Func<Point, Point> tranFunc = (p) =>
            {
                return new Point((int)(p.X / WidthRatio), (int)(p.Y / HeightRatio));
            };
            if (IsPainting)
            {
                IsPainting = false;
                OWBISPAddForm ispAddForm = new OWBISPAddForm(ShapeType);
                switch (ShapeType)
                {
                    case ShapeType.Spot:
                        ispAddForm.Spot = tranFunc(ShapePoints[0].Point);
                        break;
                    case ShapeType.Polygon:
                    case ShapeType.Line:
                        ispAddForm.PointList = ShapePoints.Where(p => !p.IsTemp).Select(p => tranFunc(p.Point)).ToList();
                        break;
                    case ShapeType.Mask:
                        byte[] mask = new byte[OWBGlobal.Camera.Width * OWBGlobal.Camera.Height];
                        using (var path = new GraphicsPath())
                        {
                            path.AddLines(ShapePoints.Where(p => !p.IsTemp).Select(p => tranFunc(p.Point)).ToArray());
                            Region maskRegion = new Region();
                            maskRegion.MakeEmpty();
                            maskRegion.Union(path);
                            int i = 0;
                            for (int y = 0; y < OWBGlobal.Camera.Height; y++)
                            {
                                for (int x = 0; x < OWBGlobal.Camera.Width; x++)
                                {
                                    Point point = new Point(x, y);
                                    if (maskRegion.IsVisible(point))
                                    {
                                        mask[i++] = 255;
                                    }
                                    else
                                    {
                                        mask[i++] = 0;
                                    }
                                }
                            }
                        }
                        ispAddForm.Mask = mask;
                        break;
                }
                if (ShapeType != ShapeType.None)
                {
                    if (ispAddForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshForm();
                    }
                }
            }
            ShapeType = ShapeType.None;
        }

        private void Draw()
        {
            Action<Graphics> draw = g =>
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;


                if (IsPainting)
                {
                    switch (ShapeType)
                    {
                        case ShapeType.Spot:
                            if (ShapePoints.Count == 0) return;
                            int rx = (int)(ShapePoints[0].Point.X);
                            int ry = (int)(ShapePoints[0].Point.Y);
                            g.DrawLine(BorderPen, rx, ry - 8, rx, ry + 8);
                            g.DrawLine(PadPen, rx, ry - 8, rx, ry + 8);
                            g.DrawLine(BorderPen, rx - 8, ry, rx + 8, ry);
                            g.DrawLine(PadPen, rx - 8, ry, rx + 8, ry);
                            break;
                        case ShapeType.Polygon:
                            if (ShapePoints.Count == 0) return;
                            if (ShapePoints.Count <= 2)
                            {
                                g.DrawLines(BorderPen, ShapePoints.Select(p => p.Point).ToArray());
                                g.DrawLines(PadPen, ShapePoints.Select(p => p.Point).ToArray());
                            }
                            else
                            {
                                g.DrawPolygon(BorderPen, ShapePoints.Select(p => p.Point).ToArray());
                                g.DrawPolygon(PadPen, ShapePoints.Select(p => p.Point).ToArray());
                            }
                            break;
                        case ShapeType.Mask:
                            if (ShapePoints.Count == 0) return;
                            using (var path = new GraphicsPath())
                            {
                                path.AddLines(ShapePoints.Select(p => p.Point).ToArray());
                                if (IsPainting)
                                {
                                    g.FillPath(MaskBursh, path);
                                }
                            }
                            break;
                        case ShapeType.Line:
                            if (ShapePoints.Count == 0) return;
                            g.DrawLines(BorderPen, ShapePoints.Select(p => p.Point).ToArray());
                            g.DrawLines(PadPen, ShapePoints.Select(p => p.Point).ToArray());
                            break;
                    }

                }
            };

            tpbTA.Rander(draw);
        }
        private void tsbArrow_Click(object sender, EventArgs e)
        {
            ShapeType = ShapeType.None;
        }

        private void tsbPoint_Click(object sender, EventArgs e)
        {
            ShapeType = ShapeType.Spot;
        }

        private void tsbPolygon_Click(object sender, EventArgs e)
        {
            ShapeType = ShapeType.Polygon;
        }

        private void tsbLine_Click(object sender, EventArgs e)
        {
            ShapeType = ShapeType.Line;
        }

        private void tsbMask_Click(object sender, EventArgs e)
        {
            ShapeType = ShapeType.Mask;
        }

        private void SetShapeType()
        {
            tsbArrow.Checked = false;
            tsbPoint.Checked = false;
            tsbPolygon.Checked = false;
            tsbLine.Checked = false;
            tsbMask.Checked = false;
            switch (ShapeType)
            {
                case ShapeType.None:
                    tsbArrow.Checked = true;
                    break;
                case ShapeType.Spot:
                    tsbPoint.Checked = true;
                    break;
                case ShapeType.Polygon:
                    tsbPolygon.Checked = true;
                    break;
                case ShapeType.Line:
                    tsbLine.Checked = true;
                    break;
                case ShapeType.Mask:
                    tsbMask.Checked = true;
                    break;
            }
            ShapePoints.Clear();
            Draw();
        }

        private void cmsISP_Opening(object sender, CancelEventArgs e)
        {
            tsmiDel.DropDownItems.Clear();
            List<MarkerItem> markerItems = OWBGlobal.Camera.GetMarkerItems();
            if (markerItems != null)
            {
                foreach (MarkerItem markerItem in markerItems)
                {
                    ToolStripMenuItem tsiDelItem = new ToolStripMenuItem();
                    tsiDelItem.Text = markerItem.ToString();
                    tsiDelItem.Tag = markerItem;
                    tsiDelItem.Click += new EventHandler(tsmiDelItem_Click);
                    tsmiDel.DropDownItems.Add(tsiDelItem);
                }
            }
        }

        private void tsmiDelItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsiDelItem = sender as ToolStripMenuItem;
            MarkerItem markerItem = tsiDelItem.Tag as MarkerItem;
            if (MessageBox.Show("是否删除测温标记\"" + markerItem.MarkerName + "\"", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                switch (markerItem.MarkerType)
                {
                    case MarkerType.Spot:
                        OWBGlobal.Camera.DelPoint(markerItem.MarkerName);
                        break;
                    case MarkerType.Region:
                        OWBGlobal.Camera.DelRegion(markerItem.MarkerName);
                        break;
                    case MarkerType.Line:
                        OWBGlobal.Camera.DelLine(markerItem.MarkerName);
                        break;
                }
            }
            RefreshForm();
        }

        private void RefreshForm()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                byte[] buf = OWBGlobal.Camera.Snapshot();
                if (buf == null)
                {
                    return;
                }
                Bitmap bmp = OWBGlobal.Camera.CreateBitmap(buf);
                tpbTA.Image = bmp;

                cbMarkerName.Items.Clear();
                List<MarkerItem> markerItems = OWBGlobal.Camera.GetMarkerItems();
                if (markerItems != null)
                {
                    foreach (MarkerItem markerItem in markerItems)
                    {
                        cbMarkerName.Items.Add(markerItem);
                    }
                }
            }
        }

        private void cbMarkerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            OWBTypes.Marker marker = new OWBTypes.Marker();
            MarkerItem markerItem = cbMarkerName.SelectedItem as MarkerItem;
            dgvISP.Enabled = true;
            dgvISP.Rows.Clear();
            int index = -1;
            DataGridViewCellCollection cell = null;
            switch (markerItem.MarkerType)
            {
                case MarkerType.Spot:
                    lblLayer.Enabled = false;
                    tbLayer.Enabled = false;
                    marker = OWBGlobal.Camera.GetPoint(markerItem.MarkerName);
                    OWBTypes.SpotMarker spotMarker = marker as OWBTypes.SpotMarker;
                    index = dgvISP.Rows.Add();
                    cell = dgvISP.Rows[index].Cells;
                    cell[COLUMN_X].Value = spotMarker.Point.X;
                    cell[COLUMN_Y].Value = spotMarker.Point.Y;
                    break;
                case MarkerType.Region:
                    lblLayer.Enabled = true;
                    tbLayer.Enabled = true;
                    OWBTypes.MaskMarker maskMarker = OWBGlobal.Camera.GetMask(markerItem.MarkerName);
                    if (maskMarker.MaskPath != null)
                    {
                        dgvISP.Enabled = false;
                        marker = maskMarker;
                        tbLayer.Text = Convert.ToString(maskMarker.Layer);
                    }
                    else
                    {
                        marker = OWBGlobal.Camera.GetRegion(markerItem.MarkerName);
                        OWBTypes.PolygonMarker polygonMarker = marker as OWBTypes.PolygonMarker;
                        for (int i = 0; i < polygonMarker.PointList.Length; i++)
                        {
                            index = dgvISP.Rows.Add();
                            cell = dgvISP.Rows[index].Cells;
                            cell[COLUMN_X].Value = polygonMarker.PointList[i].X;
                            cell[COLUMN_Y].Value = polygonMarker.PointList[i].Y;
                        }
                        tbLayer.Text = Convert.ToString(polygonMarker.Layer);
                        cbMax.Checked = polygonMarker.MaxVisible;
                        cbMin.Checked = polygonMarker.MinVisible;
                        cbAvg.Checked = polygonMarker.AvgVisible;
                    }
                    break;
                case MarkerType.Line:
                    lblLayer.Enabled = false;
                    tbLayer.Enabled = false;
                    marker = OWBGlobal.Camera.GetLine(markerItem.MarkerName);
                    OWBTypes.LineMarker lineMarker = marker as OWBTypes.LineMarker;
                    for (int i = 0; i < lineMarker.PointList.Length; i++)
                    {
                        index = dgvISP.Rows.Add();
                        cell = dgvISP.Rows[index].Cells;
                        cell[COLUMN_X].Value = lineMarker.PointList[i].X;
                        cell[COLUMN_Y].Value = lineMarker.PointList[i].Y;
                    }
                    cbMax.Checked = lineMarker.MaxVisible;
                    cbMin.Checked = lineMarker.MinVisible;
                    cbAvg.Checked = lineMarker.AvgVisible;
                    break;
            }

            OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
            if (marker.Emission == 0)
            {
                nudEmissivityValue.Value = (decimal)instrumentJconfig.Emission;
                marker.EmissionVisible = false;
                cbEmissivityVisible.Checked = false;
            }
            else
            {
                marker.EmissionVisible = true;
                cbEmissivityVisible.Checked = true;
            }
            if(marker.Distance == 0)
            {
                nudDistanceValue.Value = (decimal)instrumentJconfig.Distance;
                marker.DistanceVisible = false;
                cbDistanceVisible.Checked = false;
            }
            else
            {
                marker.DistanceVisible = true;
                cbDistanceVisible.Checked = true;
            }
            nudReflectedTemperatureValue.Value = (decimal)marker.ReflectionTemp;
            nudOffsetValue.Value = (decimal)marker.Offset;
            cbOSD.Checked = marker.Osd_visible;
            cbAPI.Checked = marker.Api_visible;
            if (marker.Mb_slot != null && marker.Mb_slot != "-1")
            {
                cbModbus.Checked = true;
                nudMbSlot.Value = (decimal)Convert.ToInt32(marker.Mb_slot);
            }
            else
            {
                cbModbus.Checked = false;
                nudMbSlot.Value = 0;
            }
            if (marker.Alarm != null)
            {
                cbAlarm.Checked = true;
                tbAlarm.Text = marker.Alarm;
            }
            else
            {
                cbAlarm.Checked = false;
                tbAlarm.Text = string.Empty;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MarkerItem markerItem = cbMarkerName.SelectedItem as MarkerItem;
            if (markerItem == null)
            {
                MessageBox.Show("保存必须选中其中一个标记！");
                return;
            }

            switch (markerItem.MarkerType)
            {
                case MarkerType.Spot:
                    try
                    {
                        OWBTypes.SpotMarker pointMarker = new OWBTypes.SpotMarker();
                        pointMarker.Api_visible = cbAPI.Checked;
                        pointMarker.Osd_visible = cbOSD.Checked;
                        pointMarker.Emission = (float)nudEmissivityValue.Value;
                        pointMarker.EmissionVisible = cbEmissivityVisible.Checked;
                        pointMarker.Distance = (float)nudDistanceValue.Value;
                        pointMarker.DistanceVisible = cbDistanceVisible.Checked;
                        pointMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                        pointMarker.Offset = (float)nudOffsetValue.Value;
                        pointMarker.Point = new OWBTypes.Pos();
                        pointMarker.Point.X = Convert.ToInt32(dgvISP.Rows[0].Cells[COLUMN_X].Value);
                        pointMarker.Point.Y = Convert.ToInt32(dgvISP.Rows[0].Cells[COLUMN_Y].Value);
                        pointMarker.Label = markerItem.MarkerName;
                        if (OWBGlobal.Camera.PutPoint(markerItem.MarkerName, pointMarker))
                        {
                            if (cbModbus.Checked)
                            {
                                OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Spot, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                            }
                            else
                            {
                                OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Spot, new OWBTypes.ModbusSlot { Mb_slot = -1 });
                            }
                            if (cbAlarm.Checked)
                            {
                                OWBGlobal.Camera.PutAlarm(markerItem.MarkerName, MarkerType.Spot, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                            }
                            MessageBox.Show("修改点成功！");
                            RefreshForm();
                        }
                        else
                        {
                            MessageBox.Show("修改点失败！");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("修改点失败！");
                    }
                    break;
                case MarkerType.Region:
                    if (dgvISP.Enabled)
                    {
                        try
                        {
                            OWBTypes.PolygonMarker regionMarker = new OWBTypes.PolygonMarker();
                            regionMarker.Api_visible = cbAPI.Checked;
                            regionMarker.Osd_visible = cbOSD.Checked;
                            regionMarker.Emission = (float)nudEmissivityValue.Value;
                            regionMarker.EmissionVisible = cbEmissivityVisible.Checked;
                            regionMarker.Distance = (float)nudDistanceValue.Value;
                            regionMarker.DistanceVisible = cbDistanceVisible.Checked;
                            regionMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                            regionMarker.Offset = (float)nudOffsetValue.Value;
                            regionMarker.Label = markerItem.MarkerName;
                            regionMarker.Layer = int.Parse(tbLayer.Text.Trim());
                            regionMarker.PointList = new OWBTypes.Pos[dgvISP.Rows.Count - 1];
                            regionMarker.MaxVisible = cbMax.Checked;
                            regionMarker.MinVisible = cbMin.Checked;
                            regionMarker.AvgVisible = cbAvg.Checked;
                            for (int i = 0; i < dgvISP.Rows.Count - 1; i++)
                            {
                                OWBTypes.Pos point = new OWBTypes.Pos();
                                point.X = Convert.ToInt32(dgvISP.Rows[i].Cells[COLUMN_X].Value);
                                point.Y = Convert.ToInt32(dgvISP.Rows[i].Cells[COLUMN_Y].Value);
                                regionMarker.PointList[i] = point;
                            }
                            if (OWBGlobal.Camera.PutRegion(markerItem.MarkerName, regionMarker))
                            {
                                if (cbModbus.Checked)
                                {
                                    OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                                }
                                else
                                {
                                    OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = -1 });
                                }
                                if (cbAlarm.Checked)
                                {
                                    OWBGlobal.Camera.PutAlarm(markerItem.MarkerName, MarkerType.Region, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                                }
                                MessageBox.Show("修改区域成功！");
                                RefreshForm();
                            }
                            else
                            {
                                MessageBox.Show("修改区域失败！");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("修改区域失败！");
                        }
                    }
                    else
                    {
                        try
                        {
                            OWBTypes.MaskMarker maskMarker = new OWBTypes.MaskMarker();
                            maskMarker.Api_visible = cbAPI.Checked;
                            maskMarker.Osd_visible = cbOSD.Checked;
                            maskMarker.Emission = (float)nudEmissivityValue.Value;
                            maskMarker.EmissionVisible = cbEmissivityVisible.Checked;
                            maskMarker.Distance = (float)nudDistanceValue.Value;
                            maskMarker.DistanceVisible = cbDistanceVisible.Checked;
                            maskMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                            maskMarker.Offset = (float)nudOffsetValue.Value;
                            maskMarker.Label = markerItem.MarkerName;
                            maskMarker.Layer = int.Parse(tbLayer.Text.Trim());
                            if (OWBGlobal.Camera.PutMask(markerItem.MarkerName, maskMarker))
                            {
                                if (cbModbus.Checked)
                                {
                                    OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                                }
                                else
                                {
                                    OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Region, new OWBTypes.ModbusSlot { Mb_slot = -1 });
                                }
                                if (cbAlarm.Checked)
                                {
                                    OWBGlobal.Camera.PutAlarm(markerItem.MarkerName, MarkerType.Region, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                                }
                                MessageBox.Show("修改蒙版成功！");
                                RefreshForm();
                            }
                            else
                            {
                                MessageBox.Show("修改蒙版失败！");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("修改蒙版失败！");
                        }
                    }
                    break;
                case MarkerType.Line:
                    try
                    {
                        OWBTypes.LineMarker lineMarker = new OWBTypes.LineMarker();
                        lineMarker.Api_visible = cbAPI.Checked;
                        lineMarker.Osd_visible = cbOSD.Checked;
                        lineMarker.Emission = (float)nudEmissivityValue.Value;
                        lineMarker.EmissionVisible = cbEmissivityVisible.Checked;
                        lineMarker.Distance = (float)nudDistanceValue.Value;
                        lineMarker.DistanceVisible = cbDistanceVisible.Checked;
                        lineMarker.ReflectionTemp = (float)nudReflectedTemperatureValue.Value;
                        lineMarker.Offset = (float)nudOffsetValue.Value;
                        lineMarker.Label = markerItem.MarkerName;
                        lineMarker.PointList = new OWBTypes.Pos[dgvISP.Rows.Count - 1];
                        lineMarker.MaxVisible = cbMax.Checked;
                        lineMarker.MinVisible = cbMin.Checked;
                        lineMarker.AvgVisible = cbAvg.Checked;
                        for (int i = 0; i < dgvISP.Rows.Count - 1; i++)
                        {
                            OWBTypes.Pos point = new OWBTypes.Pos();
                            point.X = Convert.ToInt32(dgvISP.Rows[i].Cells[COLUMN_X].Value);
                            point.Y = Convert.ToInt32(dgvISP.Rows[i].Cells[COLUMN_Y].Value);
                            lineMarker.PointList[i] = point;
                        }
                        if (OWBGlobal.Camera.PutLine(markerItem.MarkerName, lineMarker))
                        {
                            if (cbModbus.Checked)
                            {
                                OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Line, new OWBTypes.ModbusSlot { Mb_slot = (int)nudMbSlot.Value });
                            }
                            else
                            {
                                OWBGlobal.Camera.PutModbus(markerItem.MarkerName, MarkerType.Line, new OWBTypes.ModbusSlot { Mb_slot = -1 });
                            }
                            if (cbAlarm.Checked)
                            {
                                OWBGlobal.Camera.PutAlarm(markerItem.MarkerName, MarkerType.Line, new OWBTypes.Alarm { Value = float.Parse(tbAlarm.Text.Trim()) });
                            }
                            MessageBox.Show("修改线成功！");
                            RefreshForm();
                        }
                        else
                        {
                            MessageBox.Show("修改线失败！");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("修改线失败！");
                    }
                    break;
            }
        }
    }

    public class GraphicsPoint
    {
        public Point Point { get; set; }

        public bool IsTemp { get; set; }
    }
}
