using FTMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace FTMS.DAL
{
    public class DataService
    {
        SqlConnection con;
        SqlCommand cmd;
        string conStr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        ///Get Team Details
        public List<Employee> GetTeam()
        {
            List<Employee> EmployeeList = new List<Employee>();
            Employee employees = null;
            try
            { 
                using (con = new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_GetTeamDetails",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        employees = new Employee();
                        employees.empId = Convert.ToInt32(reader["empId"]);
                        employees.empName = Convert.ToString(reader["empName"]);
                        employees.category = Convert.ToString(reader["category"]);
                        employees.tool = Convert.ToString(reader["tool"]);
                        employees.designation = Convert.ToString(reader["designation"]);
                        employees.dob = Convert.ToDateTime(reader["dob"]);
                        EmployeeList.Add(employees);
                    }
                    // Call Close when done reading.
                    reader.Close();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close(); 
            }

            return EmployeeList;

        }

        public int  AddUser(fUser user)
        {
            int returnvalue;
            try
            {
                using (con = new SqlConnection(conStr))
                {

                    StringBuilder SqlInsertQuery = new StringBuilder();
                    SqlInsertQuery.AppendFormat("INSERT INTO USERS");
                    SqlInsertQuery.AppendFormat("(username,[password],name,IsAdmin,tool)");
                    SqlInsertQuery.AppendFormat("VALUES('{0}',", user.uname);
                    SqlInsertQuery.AppendFormat("'{0}',", user.password);
                    SqlInsertQuery.AppendFormat("'{0}',", user.name);
                    if (user.role == "admin")
                        SqlInsertQuery.AppendFormat("{0},", 1);
                    else
                        SqlInsertQuery.AppendFormat("{0},", 0);
                    SqlInsertQuery.AppendFormat("'{0}')",user.tool);
                    
                    cmd = new SqlCommand(SqlInsertQuery.ToString(), con);
                    cmd.CommandType = CommandType.Text;

                    //cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = user.name;
                    //cmd.Parameters.Add("@uName", SqlDbType.VarChar).Value = user.uname;
                    //cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = user.password;
                    //if (user.role == "admin")
                    //    cmd.Parameters.Add("@role", SqlDbType.Bit).Value = 1;
                    //else
                    //    cmd.Parameters.Add("@role", SqlDbType.Bit).Value = 0;
                    //cmd.Parameters.Add("@tool", SqlDbType.VarChar).Value = user.tool;
                    con.Open();
                    returnvalue = cmd.ExecuteNonQuery();
                }
                return returnvalue;
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
        }

        //Get Event Details
        public List<Event> GetEvents()
        {
            List<Event> EventList = new List<Event>();
            Event events = null;
            try
            {
                using (con = new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_EventDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        events = new Event();
                        events.eId = Convert.ToInt16(reader["EventId"]);
                        events.eName = Convert.ToString(reader["EventName"]);
                        events.eDate = Convert.ToDateTime(reader["EventDate"]);
                        events.eBudget = Convert.ToDecimal(reader["Budget"]);
                        events.eStatus = Convert.ToString(reader["Status"]);
                        EventList.Add(events);
                    }
                    // Call Close when done reading.
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }

            return EventList;

        }

        //Autheticate user
        public   bool Login(User user)
        {
            int returnvalue;
            bool result;
            try {

                using (con = new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_Userlogin_check",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("uname", SqlDbType.VarChar).Value = user.username;
                    cmd.Parameters.Add("pass", SqlDbType.VarChar).Value = user.password;
                    con.Open();
                    returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
                    if (returnvalue > 0)
                        result = true;
                    else
                        result = false;
                        
                }
                return result;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                con.Dispose();
                con.Close();
            }
        }

        //Check role of User

        //    public bool IsAdmin(User user)
        //{
        //    int returnvalue;
        //    bool result;
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendFormat("select 1 from users where username = {0}",user.username);
        //        using (con = new SqlConnection(conStr))
        //        {
        //            cmd = new SqlCommand(sb.ToString(), con);
        //            con.Open();
        //            returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
        //            if (returnvalue > 0)
        //                result = true;
        //            else
        //                result = false;

        //        }
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Dispose();
        //        con.Close();
        //    }
        //}


        //Add Member to Team
        public  int AddEmployee(Employee emp)
        {
            int returnvalue;
            try
            {
                using (con=new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_AddEmployee",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@empId", SqlDbType.Int).Value = emp.empId;
                    cmd.Parameters.Add("@empName", SqlDbType.VarChar).Value = emp.empName;
                    cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = emp.category;
                    cmd.Parameters.Add("@tool", SqlDbType.VarChar).Value = emp.tool;
                    cmd.Parameters.Add("@designation", SqlDbType.VarChar).Value = emp.designation;
                    cmd.Parameters.Add("@dob", SqlDbType.Date).Value = emp.dob;
                    con.Open();
                    returnvalue = cmd.ExecuteNonQuery();
                }
                return returnvalue;
            }
            

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
           

        }

        //Delete Team Mmber
        public int  Delete(Employee emp)
        {
            int result;
            try
            {
                using (con=new SqlConnection(conStr))
                {
                    con.Open();
                    cmd = new SqlCommand("sp_DeleteEmployee",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", emp.empId);
                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception("Error occured while deleting member");
            }
            finally
            {
                con.Close();
            }
            
        }

        //Update method for updating Member
                    
        public int Update(Employee emp)
        {
            try
            {
                int returnvalue;
                using(con=new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_UpdateMember", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empId",emp.empId);
                    cmd.Parameters.AddWithValue("@empName", emp.empName);
                    cmd.Parameters.AddWithValue("@category",emp.category);
                    cmd.Parameters.AddWithValue("@tool",emp.tool);
                    con.Open();
                    returnvalue = cmd.ExecuteNonQuery();
                }
                return returnvalue;
            }
            catch(Exception e)
            {
                throw new Exception("Error occured while updating data");
            }
            finally
            {
                con.Close();
            }
        }

        //Add EventMethod
        public int AddEvent(Event events)
        {
            int returnvalue;
            try
            {
                using (con = new SqlConnection(conStr))
                {
                    cmd = new SqlCommand("sp_AddEvent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@eName", SqlDbType.VarChar).Value =events.eName;
                    cmd.Parameters.Add("@eDate", SqlDbType.Date).Value = events.eDate;
                    cmd.Parameters.Add("@eBudget", SqlDbType.Decimal).Value = events.eBudget;
                    cmd.Parameters.Add("@eStatus", SqlDbType.VarChar).Value = "Pending";
                    con.Open();
                    returnvalue = cmd.ExecuteNonQuery();
                }
                return returnvalue;
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }


        }

    }
}