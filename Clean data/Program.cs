using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using cExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace ConsoleApp
{
    class Program : Library
    {
        // lấy dữ liệu được clean 1 nửa từ excel
        static void GetDataExcel(string path, int select)
        {
            if (File.Exists(path))
            {
                cExcel.Application xlApp = new cExcel.Application();
                cExcel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                cExcel.Worksheet xlWorksheet = (cExcel.Worksheet)xlWorkbook.Sheets.get_Item(1);
                cExcel.Range xlRange = xlWorksheet.UsedRange;
                object[,] valueArray = (object[,])xlRange.get_Value(cExcel.XlRangeValueDataType.xlRangeValueDefault);

                for (int row = 1; row <= xlWorksheet.UsedRange.Rows.Count; ++row)
                {
                    for (int colum = 2; colum <= xlWorksheet.UsedRange.Columns.Count; ++colum)
                    {
                        if (valueArray[row, colum].ToString() != null)
                        {
                            List<string> listValue = new List<string>();
                            listValue.Add(valueArray[row, colum].ToString());

                            if (select == 1)
                            {
                                DateTime date = DateTime.Parse(valueArray[row, 1].ToString());
                                UpdateDataToDB(listValue, select, date);
                            }

                            if (select == 2)
                            {
                                DateTime date = DateTime.Parse(valueArray[row, 1].ToString());
                                UpdateDataToDB(listValue, select, date);
                            }

                            if (select == 3)
                            {
                                DateTime date = DateTime.Parse(valueArray[row, 1].ToString());
                                UpdateDataToDB(listValue, select, date);
                            }
                        }
                    }
                }

                xlWorkbook.Close(false);
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            }
            else
                Console.WriteLine("Err parth");
        }

        static void LogData(DateTime date, string local, int countPatient, int select)
        {
            if (InsertData(date, local, countPatient, select) == true)
                Console.WriteLine(date + " " + local + " " + countPatient);
            else
                Console.WriteLine("Err " + date + " " + local + " " + countPatient);
        }

        static void UpdateDataToDB(List<string> listValue, int select, DateTime date)
        {
            foreach (DictionaryEntry item in HashData(listValue))
            {
                string local = RemoveUnicode(item.Key.ToString().Replace("  ", " "));
                int countPatient = int.Parse(item.Value.ToString());

                LogData(date, local, countPatient, select);
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePathExcel = "D:/_project/COVID-19/ConvertData/COVID-19/ConsoleApp/Source/test.xlsx";
            try
            {
                GetDataExcel(filePathExcel, Table_CaBenhTheoNgay);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
