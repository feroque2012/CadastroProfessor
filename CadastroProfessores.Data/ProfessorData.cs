using CadastroProfessores.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CadastroProfessores.Data
{
    public class ProfessorData : IDisposable
    {
        public Professor Get(int Id)
        {
            try
            {
                string strSql = "Select * from tblProfessor Where Id=@Id";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Professor>(strSql, new { Id = Id }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Professor> Get()
        {
            try
            {
                string strSql = "Select * from tblProfessor";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Professor>(strSql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Professor Insert(Professor professor)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine("Insert into tblProfessor values (@Nome)");
                strSql.AppendLine("select * from tblProfessor where Id=scope_identity()");

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Professor>(strSql.ToString(), professor).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Professor Update(Professor professor)
        {
            try
            {
                string strSql = "update tblProfessor set Nome=@Nome where Id=@Id";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    conexaoBD.Execute(strSql.ToString(), professor);
                    return professor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int Id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("Delete from tblAluno Where IdProfessor=@Id");
                strSql.AppendLine("Delete from tblProfessor Where Id=@Id");

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    conexaoBD.Execute(strSql.ToString(), new { Id = Id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
