using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe a Descrição!")]
        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaModel()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessao
        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }


        public List<PlanoContaModel> ListaPlanoContas()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();
            PlanoContaModel item;

            string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"select id,descricao,tipo,usuario_id from plano_contas where usuario_id  = {id_usuarioLogado}";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PlanoContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Descricao = dt.Rows[i]["DESCRICAO"].ToString();
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());
                lista.Add(item);
            }

            return lista;
        }

        public void Insert()
        {
            string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            String sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO,TIPO,USUARIO_ID) VALUES ('{Descricao}','{Tipo}','{id_usuarioLogado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);
        }

        public void Excluir(int id_conta)
        {
            new DAL().ExecutaComandoSQL("DELETE FROM PLANO_CONTAS WHERE ID = " + id_conta);
        }







    }
}
