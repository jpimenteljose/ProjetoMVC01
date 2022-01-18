
using ProjetoMVC01_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Interfaces
{
    public interface IUsuarioRepository
    {
        void Inserir(Usuario usuario);
        Usuario Consultar(string email, string senha);
    }
}
