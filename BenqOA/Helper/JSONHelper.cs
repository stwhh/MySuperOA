using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BenqOA.Helper
{
    public static class JSONHelper
    {
        public static string DatatableToJSON(this DataTable dt)
        {
            if (dt != null)
            {
                decimal zero = 0;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = new ArrayList();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        var strvalue = dataRow[dataColumn.ColumnName] + "";
                        if (decimal.TryParse(strvalue, out zero) && strvalue.Substring(0, 1) != "0")
                        {
                            dictionary.Add(dataColumn.ColumnName, decimal.Parse(strvalue));
                        }
                        else
                        {
                            dictionary.Add(dataColumn.ColumnName, strvalue);
                        }
                    }
                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
            }
            else
            {
                return "";
            }
        }

        public static string DatatableToJSON_Format(this DataTable dt, params string[] columnname)
        {
            if (dt != null)
            {
                decimal zero = 0;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = new ArrayList();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        var strvalue = dataRow[dataColumn.ColumnName] + "";
                        if (decimal.TryParse(strvalue, out zero) && strvalue.Substring(0, 1) != "0")
                        {
                            if (columnname.Contains(dataColumn.ColumnName))
                            {
                                dictionary.Add(dataColumn.ColumnName, strvalue);
                            }
                            else
                            {
                                dictionary.Add(dataColumn.ColumnName, decimal.Parse(strvalue));
                            }
                        }
                        else
                        {
                            dictionary.Add(dataColumn.ColumnName, strvalue);
                        }
                    }
                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 父子表关联 返回 JSON
        /// </summary>
        /// <param name="dt">主表</param>
        /// <param name="relationName">关系名</param>
        /// <param name="dt2">子表</param>
        /// <returns></returns>
        public static string DatatableToJSON_Two(this DataTable dt, string relationName, DataTable dt2)
        {
            if (dt != null)
            {
                decimal zero = 0;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = new ArrayList();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        var strvalue = dataRow[dataColumn.ColumnName] + "";
                        if (decimal.TryParse(strvalue, out zero) && strvalue.Substring(0, 1) != "0")
                        {
                            dictionary.Add(dataColumn.ColumnName, decimal.Parse(strvalue));
                        }
                        else
                        {
                            dictionary.Add(dataColumn.ColumnName, strvalue);
                        }
                    }
                    //子表数据
                    var childRows = dataRow.GetChildRows(relationName);
                    ArrayList carrayList = new ArrayList();
                    foreach (DataRow cRow in childRows)
                    {
                        Dictionary<string, object> cdictionary = new Dictionary<string, object>();  //实例化一个参数集合
                        foreach (DataColumn cdataColumn in dt2.Columns)
                        {
                            var strvalue = cRow[cdataColumn.ColumnName] + "";
                            if (decimal.TryParse(strvalue, out zero) && strvalue.Substring(0, 1) != "0")
                            {
                                cdictionary.Add(cdataColumn.ColumnName, decimal.Parse(strvalue));
                            }
                            else
                            {
                                cdictionary.Add(cdataColumn.ColumnName, strvalue);
                            }

                        }
                        carrayList.Add(cdictionary);
                    }

                    dictionary.Add("ChildList", carrayList);

                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
            }
            else
            {
                return "";
            }
        }

    }
}