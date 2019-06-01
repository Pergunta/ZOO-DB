using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZOO_db
{
    public partial class VetForm : Form
    {
        private String vet_ID = "";

        public VetForm(String vet_string)
        {
            SplitLine(vet_string);
            InitializeComponent();

        }

        private void SplitLine(String line)
        {
            string[] split = line.Split(':');
            vet_ID = split[0];
        }
    }
}
