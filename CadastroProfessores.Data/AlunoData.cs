using CadastroProfessores.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CadastroProfessores.Data
{
    public class AlunoData : IDisposable
    {
        public Aluno Get(int Id)
        {
            try
            {
                string strSql = "Select * from tblAluno Where Id=@Id";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Aluno>(strSql, new { Id = Id }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Aluno> GetAluno(int IdProfessor)
        {
            try
            {
                string strSql = "Select a.*,p.Nome NomeProfessor from tblAluno a inner join tblProfessor p on p.Id=a.IdProfessor Where IdProfessor=@IdProfessor";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Aluno>(strSql, new { IdProfessor = IdProfessor }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Aluno Insert(Aluno aluno)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Insert into tblAluno values (@Nome,@VlrMensalidade,@DtVencimento,@IdProfessor)");
                strSql.Append("select * from tblAluno where Id=scope_identity()");

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<Aluno>(strSql.ToString(), aluno).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Aluno Update(Aluno aluno)
        {
            try
            {
                string strSql = "update tblAluno set Nome=@Nome,VlrMensalidade=@VlrMensalidade,DtVencimento=@DtVencimento,IdProfessor=@IdProfessor where Id=@Id";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    conexaoBD.Execute(strSql.ToString(), aluno);
                    return aluno;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertLog(LogImportacaoArquivo log)
        {
            try
            {
                string strSql = "Insert into tblLogImportacaoArquivo values (@DataImportacao,@NomeArquivo)";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    conexaoBD.Execute (strSql.ToString(), log);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LogImportacaoArquivo GetLog()
        {
            try
            {
                string strSql = "Select top 1 * from tblLogImportacaoArquivo Order by DataImportacao Desc";

                using (SqlConnection conexaoBD = new SqlConnection(Config.GetConnectionString()))
                {
                    return conexaoBD.Query<LogImportacaoArquivo>(strSql).SingleOrDefault();
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
                string strSql = "Delete from tblAluno Where Id=@Id";

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
