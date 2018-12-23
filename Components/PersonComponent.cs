using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;
using System.Configuration;

using System.Data.SqlClient;
using System.Data;

// import itextsharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Components
{
    public class PersonComponent
    {
        public void GeneratePdf (PersonModel person) {
            try
            {
                string firstName = person.FirstName;
                string lastName = person.LastName;
                string cui = person.Cui;
                string birthDate = person.BirthDate;
                string phone = person.Phone;
                string cellPhone = person.CellPhone;
                string gender = "Masculino";
                var timeStamp = DateTime.Now.ToFileTime();
                if (String.Compare(person.Gender, "F") == 0) {
                    gender = "Femenino";
               }
                string email = person.Email;
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream("C:/code/" + timeStamp + ".pdf", FileMode.Create));
                document.Open();
                Paragraph t = new Paragraph("Datos de " + firstName);
                t.Font.SetStyle(Font.BOLD);
                t.Alignment = 1;
                document.Add(t);

                Paragraph n1 = new Paragraph("Nombres: " + firstName);
                document.Add(n1);
                Paragraph n2 = new Paragraph("Apellidos: " + lastName);
                document.Add(n2);
                Paragraph c = new Paragraph("Indentificación: " + cui);
                document.Add(c);
                Paragraph g = new Paragraph("Genero: " + gender);
                document.Add(g);
                Paragraph bD = new Paragraph("Fecha de nacimiento: " + birthDate);
                document.Add(bD);
                Paragraph e = new Paragraph("Correo: " + email);
                document.Add(e);
                Paragraph p = new Paragraph("Teléfono: " + phone);
                document.Add(p);
                Paragraph cP = new Paragraph("Celular: " + cellPhone);
                document.Add(cP);

                document.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
        public DataTable PersonList()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("person_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            } catch (InvalidCastException e)
            {
                Console.WriteLine(e);
            }
            return dt;
        }

        public void PersonDelete(int personId)
        {
            try
            {
                Console.WriteLine(personId);
                SqlCommand command = new SqlCommand("person_delete", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", personId);
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void PersonUpdate(PersonModel emp)
        {
            try
            {
                SqlCommand comand = new SqlCommand("person_update", con);
                comand.CommandType = CommandType.StoredProcedure;
                comand.Parameters.AddWithValue("@id", emp.Id);
                comand.Parameters.AddWithValue("@cui", emp.Cui);
                comand.Parameters.AddWithValue("@firstName", emp.FirstName);
                comand.Parameters.AddWithValue("@lastName", emp.LastName);
                comand.Parameters.AddWithValue("@gender", emp.Gender);
                comand.Parameters.AddWithValue("@birthDate", emp.BirthDate);
                comand.Parameters.AddWithValue("@pass", "secret");
                comand.Parameters.AddWithValue("@phone", emp.Phone);
                comand.Parameters.AddWithValue("@cellPhone", emp.CellPhone);
                comand.Parameters.AddWithValue("@email", emp.Email);
                comand.Parameters.AddWithValue("@updatedAt", emp.UpdatedAt);
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception o)
            {
                Console.WriteLine(o);
            }
        }

        public DataTable PersonSearch(String search)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand comand = new SqlCommand("person_search", con);
                comand.CommandType = CommandType.StoredProcedure;
                comand.Parameters.AddWithValue("@search", search);
                comand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(comand);
                da.Fill(dt);
            }
            catch (Exception o)
            {
                Console.WriteLine(o);
            }
            return dt;
        }
        public void PersonInsert(PersonModel emp)
        {
            try
            {
                SqlCommand comand = new SqlCommand("person_insert", con);
                comand.CommandType = CommandType.StoredProcedure;
                comand.Parameters.AddWithValue("@cui", emp.Cui);
                comand.Parameters.AddWithValue("@firstName", emp.FirstName);
                comand.Parameters.AddWithValue("@lastName", emp.LastName);
                comand.Parameters.AddWithValue("@gender", emp.Gender);
                comand.Parameters.AddWithValue("@birthDate", emp.BirthDate);
                comand.Parameters.AddWithValue("@pass", "secret");
                comand.Parameters.AddWithValue("@phone", emp.Phone);
                comand.Parameters.AddWithValue("@cellPhone", emp.CellPhone);
                comand.Parameters.AddWithValue("@email", emp.Email);
                comand.Parameters.AddWithValue("@createdAt", emp.CreatedAt);
                comand.Parameters.AddWithValue("@updatedAt", emp.UpdatedAt);
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                comand.ExecuteNonQuery();
                con.Close();
            } catch (Exception o)
            {
                Console.WriteLine(o);
            }
        }
    }
}
