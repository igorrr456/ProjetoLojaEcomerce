using LojaEcomerce.Models;

namespace LojaEcomerce.Interfaces
{
    public interface IUsuarioRepositorio
    {
        // A INTERFACE NAO CONTEM CODIGO APENAS A PROMESSA DO QUE DEVE SER FEITO 
        LoginViewModel Validar(string Email, string Senha);
    }
}
