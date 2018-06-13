using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models
{
    public enum LoginState
    {
        EMAIL_NOTFOUND, WRONG_PASSWORD, CONNECTION_FAILED, CONNECTED
    }

    static class LoginStateMethods
    {

        /// <summary>
        /// Retorna uma mensagem associada ao resultado(erro) da execução da autenticação.
        /// </summary>
        /// <param name="s">Estado do Login</param>
        /// <returns>Mensagem descritiva do erro da operação.</returns>
        public static string GetMessage(this LoginState s)
        {
            switch (s)
            {
                case LoginState.EMAIL_NOTFOUND:
                    return "Email not registered";
                case LoginState.WRONG_PASSWORD:
                    return "Wrong password";
                case LoginState.CONNECTION_FAILED:
                    return "Connection failed";
                default:
                    return "";
            }
        }
    }
}
