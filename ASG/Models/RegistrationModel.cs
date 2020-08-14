using ASG.Common;
using DB_con;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ASG.Models
{
    public class RegistrationModel
    {
       public string Name { get; set; }
       public string Email { get; set; }
       public string UserId { get; set; }
       public string Password { get; set; }
       public string ConfirmPassword { get; set; }
     
    }
    public class RegistrationCtl : IDisposable
    {
        #region "constructors"

        ConnectionCls obj_con = null;
        //Default Constructor
        public RegistrationCtl()
        {
            obj_con = new ConnectionCls();
        }
        #endregion
        public void Dispose()
        {
            obj_con.closeConnection();
            System.GC.SuppressFinalize(this);
        }
        public string insert(RegistrationModel obj)
        {
            try
            {
                obj_con.clearParameter();
                createParameter(obj, DBTrans.Insert);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_Users_Insert", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return obj.UserId = "0";
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_Users_Insert");
            }
        }

        public int IsLogin(RegistrationModel obj)
        {
            int value = 0;
            try
            {
                
                obj_con.clearParameter();
                createParameterLogin(obj, DBTrans.Insert);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_Login", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return value = Convert.ToInt32(obj_con.getValue("@Status"));
            }
            catch (Exception ex)
            {
               return value = 0;
            }
        }
        public int Update(RegistrationModel obj)
        {
            int value = 0;
            try
            {

                obj_con.clearParameter();
                createParameterLogin(obj, DBTrans.Update);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_ForgotPassoword", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return value = Convert.ToInt32(obj_con.getValue("@Status"));
            }
            catch (Exception ex)
            {
                return value = 0;
            }
        }
        public int IsEmailExist(string Email)
        {
            try
            {
                int value = 0;
                obj_con.clearParameter();                
                obj_con.addParameter("@Email", string.IsNullOrEmpty(Email) ? "" : Email);            
                obj_con.addParameter("@Status",value, DB_con.DBTrans.Insert);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_Check_DuplicatedEmail", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return value = Convert.ToInt32(obj_con.getValue("@Status"));
                
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_Check_DuplicatedEmail");
            }
        }        
        public void createParameter(RegistrationModel obj, DB_con.DBTrans trans)
        {
            try
            {
                string password = Security.Encrypt(Convert.ToString(obj.Password));
                obj_con.clearParameter();
                obj_con.addParameter("@Email", string.IsNullOrEmpty(Convert.ToString(obj.Email)) ? "" : obj.Email);
                obj_con.addParameter("@UserName", string.IsNullOrEmpty(Convert.ToString(obj.Name)) ? "" : obj.Name);
                obj_con.addParameter("@Password", string.IsNullOrEmpty(password) ? "" : password);
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void createParameterLogin(RegistrationModel obj, DB_con.DBTrans trans)
        {
            try
            {
                string password = Security.Encrypt(Convert.ToString(obj.Password));
                obj_con.clearParameter();
                obj_con.addParameter("@Email", string.IsNullOrEmpty(Convert.ToString(obj.Email)) ? "" : obj.Email);
                obj_con.addParameter("@Password", string.IsNullOrEmpty(Convert.ToString(password)) ? "" : password);
                obj_con.addParameter("@Status", 0,DB_con.DBTrans.Insert);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}