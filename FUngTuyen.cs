﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    public partial class FUngTuyen : Form
    {
        DAO dao = new DAO();
        public event EventHandler ChonButtonXacNhan;
        public FUngTuyen()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UngTuyen_Load(object sender, EventArgs e)
        {
            dao.ApplyCenterAlignment(rtbTenCongTy);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.Close();
            ChonButtonXacNhan?.Invoke(this, EventArgs.Empty);
        }
    }
}
