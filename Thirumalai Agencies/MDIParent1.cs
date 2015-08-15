using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thirumalai_Agencies
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

      

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void cHANGEPASSWORDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changepassword c = new changepassword();
            c.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                c.Show();
            }
            else
            {
                c.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addcompany addcompany1 = new addcompany();
            addcompany1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                addcompany1.Show();
            }
            else
            {
                addcompany1.Show();
            }
        }

        private void editCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifycompany modifycompany1 = new modifycompany();
            modifycompany1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                modifycompany1.Show();
            }
            else
            {
                modifycompany1.Show();
            }
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addproduct addproduct1 = new addproduct();
            addproduct1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                addproduct1.Show();
            }
            else
            {
                addproduct1.Show();
            }
        }

        private void modifyProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifyproduct modifyproduct1 = new modifyproduct();
            modifyproduct1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                modifyproduct1.Show();
            }
            else
            {
                modifyproduct1.Show();
            }
        }

        private void newPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void advancePaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewPurchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void addPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newpurchase newpurchase1 = new newpurchase();
            newpurchase1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                newpurchase1.Show();
            }
            else
            {
                newpurchase1.Show();
            }
       }

        private void duePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewpurchase viewpurchase1 = new viewpurchase();
            viewpurchase1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                viewpurchase1.Show();
            }
            else
            {
                viewpurchase1.Close();
            }

        }

        private void salesBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salesbill salesbill1 = new salesbill();
            salesbill1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                salesbill1.Show();
            }
            else
            {
                salesbill1.Show();
            }

        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            front front1 = new front();
            front1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                front1.Show();
            }
            else
            {
                front1.Show();
            }
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neworder neworder = new neworder();
            neworder.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                neworder.Show();
            }
            else
            {
                neworder.Show();
            }
        }

        private void purchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            advancepayment advancepayment1 = new advancepayment();
            advancepayment1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                advancepayment1.Show();
            }
            else
            {
                advancepayment1.Show();
            }
        }

        private void viewPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vieworders vieworders1 = new vieworders();
            vieworders1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                vieworders1.Show();
            }
            else
            {
                vieworders1.Show();
            }
        }

        private void viewSalesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printbill printbill1 = new printbill();
            printbill1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                printbill1.Show();
            }
            else
            {
                printbill1.Show();
            }
        }

        private void viewSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salescollection salescollection1 = new salescollection();
            salescollection1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                salescollection1.Show();
            }
            else
            {
                salescollection1.Show();
            }
        }

        private void salesCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salescomplete salescomplete1 = new salescomplete();
            salescomplete1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                salescomplete1.Show();
            }
            else
            {
                salescomplete1.Show();
            }
        }

        private void salesTodayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salestoday salestoday1 = new salestoday();
            salestoday1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                salestoday1.Show();
            }
            else
            {
                salestoday1.Show();
            }
        }

        private void stockEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockupdate stockupdate1 = new stockupdate();
            stockupdate1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                stockupdate1.Show();
            }
            else
            {
                stockupdate1.Show();
            }
        }

        private void viewStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockview stockview1 = new stockview();
            stockview1.MdiParent = this;
            if (this.MdiChildren.Length > 1)
            {
                this.MdiChildren[0].Close();
                stockview1.Show();
            }
            else
            {
                stockview1.Show();
            }
        }
    }
}
