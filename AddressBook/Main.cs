using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //click event for business menu item
        private void businessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessContacts form = new BusinessContacts(); //make new business contacts form
            form.MdiParent = this; // set the main from as parent of each business form
            form.Show(); //show the new form
        }

        //click event for cascade menu item
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);//cascade the child forms inside the main form
        }

        //click event for vertical menu item
        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);//apply tile vertical the child forms inside the main form
        }

        //click event for horizontal menu item
        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);//apply tile horizontal the child forms inside the main form
        }
    }
}
