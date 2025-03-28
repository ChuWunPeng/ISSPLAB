using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OWB.TADemo
{
    public partial class OWBTAPictureBox : PictureBox
    {
        private Action<Graphics> _Draw;
        public OWBTAPictureBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public OWBTAPictureBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public Action<Graphics> Draw { get => _Draw; set => _Draw = value; }

        public void Rander(Action<Graphics> draw)
        {
            Invalidate();
            Draw = draw;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Draw?.Invoke(pe.Graphics);
        }
    }
}
