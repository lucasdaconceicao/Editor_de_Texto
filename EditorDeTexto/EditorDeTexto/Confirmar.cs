using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorDeTexto
{
    public partial class Confirmar : Form
    {
        public Confirmar()
        {
            InitializeComponent();
        }

        public bool Salvar { get; set; }
        public bool Cancelar { get; set; }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            Salvar = true;
            this.Dispose();
        }

        private void btn_Nsalvar_Click(object sender, EventArgs e)
        {
            Salvar = false;
            this.Dispose();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Salvar = false;
            Cancelar = true;
            this.Dispose();
        }
    }
}
