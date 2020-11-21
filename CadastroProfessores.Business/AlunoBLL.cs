using CadastroProfessores.Data;
using CadastroProfessores.Model;
using System;
using System.Collections.Generic;

namespace CadastroProfessores.Business
{
    public class AlunoBLL : IDisposable
    {
        public Aluno Get(int Id)
        {
            using (AlunoData aluno = new AlunoData())
            {
                return aluno.Get(Id);
            }
        }

        public List<Aluno> GetAluno(int IdProfesso)
        {
            using (AlunoData alunoData = new AlunoData())
            {
                return alunoData.GetAluno(IdProfesso);
            }
        }

        public Aluno Insert(Aluno aluno)
        {
            using (AlunoData alunoData = new AlunoData())
            {
                return alunoData.Insert(aluno);
            }
        }

        public Aluno Update(Aluno aluno)
        {
            using (AlunoData alunoData = new AlunoData())
            {
                return alunoData.Update(aluno);
            }
        }
        public void Delete(int Id)
        {
            using (AlunoData alunoData = new AlunoData())
            {
                alunoData.Delete(Id);
            }
        }

        public void InsertLog(LogImportacaoArquivo log)
        {
            using (AlunoData alunoData = new AlunoData())
            {
                alunoData.InsertLog(log);
            }
        }

        public LogImportacaoArquivo getLog()
        {
            using (AlunoData alunoData = new AlunoData())
            {
                return alunoData.GetLog();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
