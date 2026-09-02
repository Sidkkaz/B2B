using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using B2B.UI;
using B2B.Domain;
using B2B.Infrastructure;
using B2B.Service;


class Programa 
{

    static void Main()
    {
        var DB = new DB();
        var Auth = new AuthService();
        var menu = new Menus(Auth);

        menu.EntradaAnim();
        Menus.MenuEntrada();

        if(Auth.Login())
        {
            menu.MenuPrincipal();
        }
        
    }
}
