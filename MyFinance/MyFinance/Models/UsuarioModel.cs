﻿using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Informe seu Nome!")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Informe seu Email!")]
        public String Email  { get; set; }

        [Required(ErrorMessage = "Informe sua Senha!")]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Informe sua Data de Nascimento!")]
        public string Data_Nascimento { get; set; }



        public bool ValidarLogin()
        {
            string sql = $"SELECT ID,NOME,DATA_NASCIMENTO FROM USUARIO WHERE EMAIL = '{Email}' AND SENHA = '{Senha}'";
            DAL OBJDAL = new DAL();
            DataTable dt = OBJDAL.RetDataTable(sql);

            if (dt != null){

                if(dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = dt.Rows[0]["NOME"].ToString();
                    Data_Nascimento = dt.Rows[0]["DATA_NASCIMENTO"].ToString();
                    return true;
                }

            }

            return false;
        }

        public void RegistrarUsuario()
        {
            string dataNascimento = DateTime.Parse(Data_Nascimento).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO USUARIO (NOME, EMAIL, SENHA, DATA_NASCIMENTO) VALUE('{Nome}', '{Email}','{Senha}' , '{dataNascimento}' )";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);

        
        }
    }
}
