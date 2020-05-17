using System.Collections.Generic;
using UnityEngine;
using  MoonSharp.Interpreter;

/// <summary>
/// Класс проверяющий правильность ввода пароля
/// </summary>
public class PasswordChecker : MonoBehaviour
{
    /// <summary>
    /// Действующий пароль
    /// </summary>
    public List<int> password;

    /// <summary>
    /// Проверить правильность пароля
    /// </summary>
    /// <param name="enteredPassword"></param>
    /// <returns></returns>
    public bool CheckPassword(List<int> enteredPassword)
    {        
        string scriptCode = @"
           function checkPassword(password,enteredPassword)    
               if #password!=#enteredPassword then
                   return false
               end
               for i=1, #password do
                   if password[i]!=enteredPassword[i] then
                      return false
                   end
               end
               return true
           end";


        Script script = new Script();
        script.DoString(scriptCode);
        Table _password = ConvertListIntToTable(password,ref script);
        Table _enteredPassword = ConvertListIntToTable(enteredPassword, ref script);
        DynValue checkPassword = script.Globals.Get("checkPassword");
        DynValue value = script.Call(checkPassword, _password, _enteredPassword);
        return value.Boolean;
    }

    /// <summary>
    /// Преобразовать List<int> в Table
    /// </summary>
    /// <param name="list"></param>
    /// <param name="script"></param>
    /// <returns></returns>
    private Table ConvertListIntToTable(List<int> list, ref Script script)
    {
        Table table = new Table(script);
        for (int i = 0; i <list.Count ; i++)
        {
            table.Append(DynValue.NewNumber(list[i]));
        }

        return table;
    }
}

