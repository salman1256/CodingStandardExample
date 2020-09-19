using System;
using System.Data.SqlClient;
using System.Text;

namespace SalaryMangament
{
    class Program
    {
        static void Main(string[] args)
        {
            try
             {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "DESKTOP-6C7NE1Q\\SQL2012",
                    InitialCatalog = "day57db",
                    IntegratedSecurity = true
                };
                using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                 { const double GRADE_TDS = 0.10;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("select Grade,Basic,HRA,TA,DA");
                    stringBuilder.Append(",(HRA+TA+DA) as NetSalary ");
                    stringBuilder.Append("from Salary order by Grade ");
                    Console.WriteLine("Grade \t Basic \t HRA \t TA \t DA \t  Net Salary \t InHandSalary");
                   
                    string cmdText = stringBuilder.ToString();
                    con.Open();
                    using (SqlCommand cmd=new SqlCommand (cmdText,con))
                     {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                         {
                            while (sqlDataReader.Read())
                             {
                                Console.Write(sqlDataReader["Grade"] + "\t");
                                Console.Write(sqlDataReader["Basic"] + "\t");
                                Console.Write(sqlDataReader["HRA"] + "\t");
                                Console.Write(sqlDataReader["TA"] + "\t");
                                Console.Write(sqlDataReader["DA"]+"\t \t");
                              
                                double netSal = Convert.ToDouble(sqlDataReader["NetSalary"].ToString());
                                Console.Write(netSal+ "\t \t");
                                double inSal = netSal - (netSal * GRADE_TDS);
                                Console.Write(inSal);

                                Console.WriteLine("\n");
                             }
                        }
                     }
                }

             }
            catch (Exception ex)
             { Console.WriteLine("Erro"+ex.Message); }
             finally
             {
                Console.WriteLine("Bye!!!");
                Console.ReadKey(); 
             }
        }
    }
}
