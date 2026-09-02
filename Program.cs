using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Data;
using B2B.UI;
using B2B.Domain;
using B2B.Infrastructure;
using B2B.Service;
using static B2B.UI.ConsoleIO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

class Programa 
{

    static void Main()
    {
        var DB = new DB();
        var Auth = new AuthService();
        var menu = new Menus();

        Menus.MenuEntrada();

        if(Auth.Login())
        {
            menu.MenuPrincipal();
        }
        
    }
}
