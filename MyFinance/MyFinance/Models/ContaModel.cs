using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Informe o Nome da Conta!")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Informe o Saldo da Conta!")]
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }


        public ContaModel()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessao
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }


        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"select * from conta where usuario_id  = {id_usuarioLogado}";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i =0;i<dt.Rows.Count;i ++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Nome = dt.Rows[i]["NOME"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["SALDO"].ToString());
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());
                lista.Add(item);
            }

            return lista;
        }

        public void Insert()
        {
            string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            String sql = $"INSERT INTO CONTA(NOME,SALDO,USUARIO_ID) VALUES ('{Nome}','{Saldo}','{id_usuarioLogado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);
        }

        public void Excluir(int id_conta)
        {
            new DAL().ExecutaComandoSQL("DELETE FROM CONTA WHERE ID = " + id_conta);
        }

    }
}
