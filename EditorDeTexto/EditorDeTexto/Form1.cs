using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EditorDeTexto
{
    public partial class Form1 : Form
    {
        string diretorio;
        bool arquivoSalvo = false;
        string texto = "";

        public Form1()
        {
            InitializeComponent();
            this.Text = "Sem título - Bloco de notas";
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvarSimples();
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvarComo();
        }

        private void abrirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opd = new OpenFileDialog();
                opd.Filter = "Documentos de Texto (*.txt)|*.txt|Formato Rich Text (*.rtf)|*.rtf|Documento do word 97-2003 (*.doc)|*.doc|Documento do word (*.docx)|*.docx"; ;
                opd.Title = "Selecionar Arquivo";

                if (opd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(opd.FileName);
                    Rtb.Text = sr.ReadToEnd();
                    this.texto = Rtb.Text;
                    this.diretorio = opd.FileName;
                    this.arquivoSalvo = true;
                    sr.Dispose();
                    this.Text = Path.GetFileName(this.diretorio) + " - Bloco de notas";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            novoDocumento();
        }

        public void salvarComo()
        {
            try
            {
                //verifica se o texto esta vazio
                if (String.IsNullOrEmpty(Rtb.Text))
                {
                    return;
                }

                SaveFileDialog sfdlg = new SaveFileDialog();
                //Define as extensões permitidas
                sfdlg.Filter = "Documentos de Texto (*.txt)|*.txt|Formato Rich Text (*.rtf)|*.rtf|Documento do word 97-2003 (*.doc)|*.doc|Documento do word (*.docx)|*.docx";
                sfdlg.FileName = "Documento";
                if (sfdlg.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfdlg.FileName);
                    sw.Write(Rtb.Text);
                    this.texto = Rtb.Text;
                    this.diretorio = sfdlg.FileName;
                    this.arquivoSalvo = true;
                    sw.Dispose();
                    this.Text = Path.GetFileName(this.diretorio) + " - Bloco de notas";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void salvarSimples()
        {
            try
            {
                //se o arquivo ja foi criado
                if (this.arquivoSalvo)
                {
                    StreamWriter sw = new StreamWriter(this.diretorio);
                    sw.Write(Rtb.Text);
                    this.texto = Rtb.Text;
                    sw.Dispose();
                }
                else
                {
                    salvarComo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void novoDocumento()
        {
            if (!string.IsNullOrEmpty(Rtb.Text))
            {
                if (MessageBox.Show("Deseja salvar as alterações em Sem titulo?", "Editor de texto", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    salvarSimples();
                    this.arquivoSalvo = false;
                    this.diretorio = "";
                    Rtb.Clear();
                    Rtb.Focus();
                }
                else
                {
                    this.arquivoSalvo = false;
                    this.diretorio = "";
                    this.Text = "Sem título - Bloco de notas";
                    Rtb.Clear();
                    Rtb.Focus();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Rtb.Text != this.texto)
                {
                    Confirmar chamaTela = new Confirmar();
                    chamaTela.ShowDialog();
                    if (chamaTela.Salvar)
                    {
                        salvarSimples();
                        e.Cancel = true;
                    }
                    if (chamaTela.Cancelar)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
