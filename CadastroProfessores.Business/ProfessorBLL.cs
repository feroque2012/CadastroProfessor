using CadastroProfessores.Data;
using CadastroProfessores.Model;
using System;
using System.Collections.Generic;

namespace CadastroProfessores.Business
{
    public class ProfessorBLL : IDisposable
    {
        public Professor Get(int Id)
        {
            using (ProfessorData professorData = new ProfessorData())
            {
                return professorData.Get(Id);
            }
        }

        public IEnumerable<Professor> Get()
        {
            using (ProfessorData professorData = new ProfessorData())
            {
                return professorData.Get();
            }
        }

        public Professor Insert(Professor Professor)
        {
            using (ProfessorData professorData = new ProfessorData())
            {
                return professorData.Insert(Professor);
            }
        }

        public Professor Update(Professor Professor)
        {
            using (ProfessorData professorData = new ProfessorData())
            {
                return professorData.Update(Professor);
            }
        }

        public void Delete(int Id)
        {
            using (ProfessorData professorData = new ProfessorData())
            {
                professorData.Delete(Id);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
