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
        private String vet_ID;
        private String vet_fname;
        private String vet_lname;

        public VetForm(String vet_string)
        {
            
            InitializeComponent(vet_string);
        }
    }
}
