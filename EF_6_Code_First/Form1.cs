using EF_6_Code_First.DAO;
using EF_6_Code_First.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;


namespace EF_6_Code_First
{
    public partial class Form1 : Form
    {
        string nome, cpf, endereco, data_nasc, telefone;
        string id;
        bool modificaPessoa = false;
        public Form1()
        {
            InitializeComponent();
            
            PreencheGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indice = this.dataGridView1.CurrentRow.Index;
            DataGridViewRow rowData = dataGridView1.Rows[indice];
            id = rowData.Cells[5].Value.ToString();
            nome = rowData.Cells[0].Value.ToString();
            cpf = rowData.Cells[1].Value.ToString();
            endereco = rowData.Cells[2].Value.ToString();
            data_nasc = rowData.Cells[3].Value.ToString();

            telefone = rowData.Cells[4].Value.ToString();



            //MessageBox.Show(id + " " + nome + " " + cpf + " " + endereco + " " + data_nasc + " " + telefone, "Alerta");


            tabControl1.SelectedTab = tabPage2;
            tbNome.Text = nome;
            tbEndereco.Text = endereco;
            tbCpf.Text = cpf;
            tbData_Nasc.Text = data_nasc;
            tbTelefone.Text = telefone;

            modificaPessoa = true;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if(modificaPessoa == false)
            {
                string nome = tbNome.Text;
                string endereco = tbEndereco.Text;
                string cpf = tbCpf.Text;
                string telefone = tbTelefone.Text;
                DateTime data_nasc = DateTime.Parse(tbData_Nasc.Text);
                Cliente pf = new Cliente();

                pf.Nome = nome;
                pf.Endereco = endereco;
                pf.Cpf = cpf;
                pf.Telefone = telefone;
                pf.Data_Nasc = data_nasc;

                var context = new PFContext();
                context.PessoaFisicas.Add(pf);

                context.SaveChanges();
                MessageBox.Show("Cliente Inserido com sucesso.", "Alerta");
                LimpaCamposPessoa();
                PreencheGrid();
            }
            else
            {
                var context = new PFContext();
                int aux = Convert.ToInt32(id);
                Cliente pf = context.PessoaFisicas.Single(x => x.ClienteID == aux);

                pf.Nome = tbNome.Text.ToString();
                pf.Endereco = tbEndereco.Text.ToString();
                pf.Cpf = tbCpf.Text.ToString();
                pf.Data_Nasc = DateTime.Parse(tbData_Nasc.Text);
                pf.Telefone = tbTelefone.Text.ToString();

                context.PessoaFisicas.Attach(pf);
                context.Entry(pf).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                LimpaCamposPessoa();

                MessageBox.Show("Cliente Alterado com sucesso.", "Alerta");
                modificaPessoa = false;
                PreencheGrid();
            }

        }

        private void bDeletar_Click(object sender, EventArgs e)
        {
            PFContext context = new PFContext();
            int indice = this.dataGridView1.CurrentRow.Index;
            DataGridViewRow rowData = dataGridView1.Rows[indice];
            String aux = rowData.Cells[5].Value.ToString().Trim();
            int clienteIdAux = Convert.ToInt32(aux);
            Cliente pf = context.PessoaFisicas.Single(x => x.ClienteID == clienteIdAux);

            string message = "Deseja realmente remover este Cliente?";
            string caption = "Error";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                context.PessoaFisicas.Attach(pf);
                context.PessoaFisicas.Remove(pf);
                context.SaveChanges();
                MessageBox.Show("Cliente removido com sucesso.", "Alerta");
                this.PreencheGrid();

            }
        }

        private void btNovo_Carro_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();

            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tbData_Nasc_TextChanged(object sender, EventArgs e)
        {

        }


        public void LimpaCamposPessoa()
        {
            this.tbNome.Clear();
            this.tbEndereco.Clear();
            this.tbCpf.Clear();
            this.tbData_Nasc.Clear();
            this.tbTelefone.Clear();

        }

        public void PreencheGrid()
        {
            PFContext context = new PFContext();
            var query = context.PessoaFisicas
                .Select(x => new { x.Nome, x.Cpf, x.Endereco,x.Data_Nasc,x.Telefone,x.ClienteID }).ToList();

            dataGridView1.DataSource = query;
        }
    }
}
