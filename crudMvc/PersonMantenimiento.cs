using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using Services;

namespace crudMvc
{
    public partial class PersonMantenimiento : Form
    {
        public PersonMantenimiento()
        {
            InitializeComponent();
        }

        private void PersonMantenimiento_Load(object sender, EventArgs e)
        {
            InputGender.SelectedIndex = 0;
            ListPerson();
        }

        PersonService service = new PersonService();
        PersonModel model = new PersonModel();
        void ListPerson()
        {            
            DataTable dt = service.ListPerson();
            Datos.DataSource = dt;
        }

        public void Insert()
        {
            string gender ="M";
            string Gender = InputGender.Text.ToString();
            var compare = String.Compare(Gender, "Femenino");
            if (compare == 0)
            {
                gender = "F";
            }
            model.Cui = InputCui.Text;
            model.FirstName = InputFirstName.Text;
            model.LastName = InputLastName.Text;
            model.Gender = gender;
            model.BirthDate = InputBirthDate.Value.Date.ToString("yyyy-MM-dd");
            model.Password = null;
            model.Phone = InputPhone.Text;
            model.CellPhone = InputCellPhone.Text;
            model.Email = InputEmail.Text;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;
            int personId = 0;
            if (textPersonId.Text != null&& textPersonId.Text.Length > 0)
            {
                personId = Int32.Parse(textPersonId.Text);
                model.Id = personId;
            }
            if (ValidForm())
            {
                if (personId == 0)
                {
                    service.InsertPerson(model);
                } else
                {
                    service.UpdatePerson(model);
                }
            } else
            {
                MessageBox.Show("El nombre es requerido");
            }
            // service.InsertPerson(model);
        }
        public Boolean ValidForm ()
        {
            return (InputFirstName.Text.Length > 0) ;
        }
        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            Insert();
            ListPerson();
            ClearForm();
        }
        private void ClearForm ()
        {
            InputFirstName.Text = null;
            InputLastName.Text = null;
            InputCui.Text = null;
            InputBirthDate.Value = DateTime.Now;
            InputPhone.Text = null;
            InputCellPhone.Text = null;
            InputEmail.Text = null;
            InputGender.SelectedIndex = 0;
            textPersonId.Text = null;
            ButtonInsert.Text = "Insertar";
            ButtonClear.Text = "Limpiar";
        }
        private void InputGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Delete (int id)
        {
            service.DeletePerson(id);
            ListPerson();
        }
        private void Datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Datos.Rows[e.RowIndex].Cells["deleteItem"].Selected) {
                int delete = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["id"].Value.ToString());
                DialogResult bottom = MessageBox.Show("Desea eliminar este dato?", "Correcto", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (bottom == DialogResult.OK)
                {
                    Delete(delete);
                }
            } else if (Datos.Rows[e.RowIndex].Cells["update"].Selected)
            {
                int id = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["id"].Value.ToString());
                textPersonId.Text = id + "";
                InputFirstName.Text = Datos.Rows[e.RowIndex].Cells["firstName"].Value.ToString();
                InputLastName.Text = Datos.Rows[e.RowIndex].Cells["lastName"].Value.ToString();
                InputCui.Text = Datos.Rows[e.RowIndex].Cells["cui"].Value.ToString();
                string Gender = InputGender.Text.ToString();
                if (String.Compare(Gender, "F") == 0)
                {
                    InputGender.SelectedIndex = 1;
                } else if (String.Compare(Gender, "M") == 0)
                {
                    InputGender.SelectedIndex = 0;
                }
                InputBirthDate.Text = Datos.Rows[e.RowIndex].Cells["birthDate"].Value.ToString();
                InputPhone.Text = Datos.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                InputCellPhone.Text = Datos.Rows[e.RowIndex].Cells["cellPhone"].Value.ToString();
                InputEmail.Text = Datos.Rows[e.RowIndex].Cells["email"].Value.ToString();
                ButtonInsert.Text = "Actualizar";
                ButtonClear.Text = "Cancelar";
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (InputSearch.Text.Length > 0)
            {
                DataTable dt = service.SearchPerson(InputSearch.Text);
                Datos.DataSource = dt;
                InputSearch.Text = null;
            }
        }

        private void buttonPersonAll_Click(object sender, EventArgs e)
        {
            ListPerson();
        }
    }
}
