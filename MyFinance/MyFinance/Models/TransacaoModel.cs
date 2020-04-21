using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Data!")]
        public string Data { get; set; }

        public string Tipo { get; set; }
        public string Valor { get; set; }

        [Required(ErrorMessage = "Informe a Descrição!")]
        public string Descricao { get; set; }

        public int Conta_Id { get; set; }
        public string NomeConta { get; set; }
        public int Plano_Contas_Id { get; set; }
        public string DescricaoPlanoContas { get; set; }


        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessao
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }


        public List<TransacaoModel> ListaTransacoes()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;

            string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"select *, t.Descricao as historico, c.Nome as conta, p.Descricao as plano_conta from transacao as t " +
                            "inner join conta as c " +
                            "on t.Conta_Id = c.Id " +
                            "inner join plano_contas as p " +
                            "on t.Plano_contas_Id = p.Id " +
                            $" where t.usuario_id = {id_usuarioLogado} order by t.data desc limit 10"; 

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["Data"].ToString()).ToString("dd/MM/yyyy");
                item.Descricao = dt.Rows[i]["historico"].ToString();
                item.Conta_Id = int.Parse(dt.Rows[i]["Conta_Id"].ToString());
                item.NomeConta = dt.Rows[i]["conta"].ToString();
                item.Plano_Contas_Id = int.Parse(dt.Rows[i]["Plano_Contas_Id"].ToString());
                item.DescricaoPlanoContas = dt.Rows[i]["plano_conta"].ToString();
                item.Tipo = dt.Rows[i]["Tipo"].ToString();
                lista.Add(item);
            }

            return lista;
        }

        //public PlanoContaModel CarregarRegistro(int? id)
       // {
            //PlanoContaModel item = new PlanoContaModel();
            //string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            //string sql = $"select id,descricao,tipo,usuario_id from plano_contas where usuario_id  = {id_usuarioLogado} and id = {id}";
            //DAL objDAL = new DAL();
            //DataTable dt = objDAL.RetDataTable(sql);

            //item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            //item.Descricao = dt.Rows[0]["DESCRICAO"].ToString();
            //item.Tipo = dt.Rows[0]["TIPO"].ToString();
            //item.Usuario_Id = int.Parse(dt.Rows[0]["USUARIO_ID"].ToString());

            //return item;
        //}

        public void Insert()
        {
            //string id_usuarioLogado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            //String sql = "";
            //if (Id == 0)
            //{
            //    sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO,TIPO,USUARIO_ID) VALUES ('{Descricao}','{Tipo}','{id_usuarioLogado}')";
            //}
            //else
            //{
            //    sql = $"UPDATE PLANO_CONTAS SET DESCRICAO = '{Descricao}', TIPO = '{Tipo}' WHERE  USUARIO_ID = '{id_usuarioLogado}' AND ID = '{Id}'";
            //}

            //DAL objDAL = new DAL();
            //objDAL.ExecutaComandoSQL(sql);
        }

        public void Excluir(int id_conta)
        {
            //new DAL().ExecutaComandoSQL("DELETE FROM PLANO_CONTAS WHERE ID = " + id_conta);
        }
    }
}
