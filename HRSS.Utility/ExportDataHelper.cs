using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.Utility
{
    public class ExportDataHelper
    {
        #region Constructor
        public ExportDataHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Export to Excel function
        public static Dictionary<string, string> ExportToExcel<T>(string fileName, string worksheetName, string header, List<T> dataFromService, List<string> isCenter = null) where T : class
        {
            Dictionary<string, string> dt = new Dictionary<string, string>();
            string sourcePath = Conf.DocumentFolder + fileName;
            string columnName = "", fldName = "", fileExcel = "", startSheet = "", endSheet = "";
            int iSheetValue = 0;
            #region temp variable
            DateTime tmpDate; int tmpInt; decimal tmpDecimal;
            #endregion

            if (worksheetName == String.Empty)
            {
                worksheetName = "Sheet1";
            }

            var allData = dataFromService;

            Dictionary<string, object> data2 = new Dictionary<string, object>();

            //new workbook
            XLWorkbook workBook = new XLWorkbook();
            //worksheet
            var sheet = workBook.Worksheets.Add(worksheetName);

            var data = Library.GetFieldAndType(typeof(T));
            List<Dictionary<string, string>> col = new List<Dictionary<string, string>>();
            foreach (var items in data)
            {
                Dictionary<string, string> tmpData = new Dictionary<string, string>();
                tmpData.Add("fldName", items["fldName"]);
                tmpData.Add("fldType", items["fldType"]);
                col.Add(tmpData);
            }

            sheet.Cell("A1").Value = header;
            sheet.Cell("A1").Style.Font.Bold = true;
            sheet.Cell("A1").Style.Font.FontSize = 16;

            int i = 1;
            int counter = 0;
            foreach (Dictionary<string, string> x in col)
            {
                columnName = Library.GetColumnExcel(i) + "3";
                fldName = x["fldName"];
                sheet.Cell(columnName).Value = fldName;
                iSheetValue = 4;

                if (counter == 0)
                    startSheet = columnName;
                else if (counter == col.Count - 1)
                    endSheet = columnName;

                int countIndex = 0;
                foreach (var row in allData)
                {
                    columnName = Library.GetColumnExcel(i) + iSheetValue;
                    var rowValue = row.GetType().GetProperty(fldName).GetValue(allData[countIndex]).ToString();
                    sheet.Cell(columnName).Value = rowValue;
                    iSheetValue++;

                    if (DateTime.TryParse(rowValue, out tmpDate))
                    {
                        sheet.Cell(columnName).Style.DateFormat.Format = "dd-MMM-yyyy";
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    else if (decimal.TryParse(rowValue, out tmpDecimal))
                    {
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                    else if (int.TryParse(rowValue, out tmpInt))
                    {
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }

                    if (isCenter != null)
                    {
                        foreach (var item in isCenter)
                        {
                            if (item == fldName)
                            {
                                if (DateTime.TryParse(rowValue, out tmpDate))
                                {
                                    sheet.Cell(columnName).Style.DateFormat.Format = "dd-MMM-yyyy";
                                    sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                                else
                                {
                                    sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                            }
                        }
                    }

                }

                counter++;
                i++;
            }


            var rangeHeader = sheet.Range(startSheet + ":" + endSheet);
            rangeHeader.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rangeHeader.Style.Font.Bold = true;

            string start = startSheet.Remove(startSheet.Length - 1);
            string end = endSheet.Remove(endSheet.Length - 1);
            sheet.Range(start + "1" + ":" + end + "1").Row(1).Merge();

            endSheet = endSheet.Remove(endSheet.Length - 1);
            var rangeBorder = sheet.Range(startSheet + ":" + endSheet + (allData.Count + 3));
            rangeBorder.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rangeBorder.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rangeBorder.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            sheet.Rows().AdjustToContents();
            sheet.Columns().AdjustToContents();

            fileExcel = fileName;
            workBook.SaveAs(sourcePath);

            dt.Add("fileExcel", fileExcel);

            return dt;

        }
        public static Dictionary<string, string> ExportToExcel<T>(string fileName, string worksheetName, List<T> dataFromService, List<string> isCenter = null) where T : class
        {
            string sourcePath = Conf.DocumentFolder + fileName;
            Dictionary<string, string> dt = new Dictionary<string, string>();
            string columnName = "", fldName = "", fileExcel = "", startSheet = "", endSheet = "", cell = "";
            int iSheetValue = 0;
            #region temp variable
            DateTime tmpDate; int tmpInt; decimal tmpDecimal;
            #endregion

            if (worksheetName == String.Empty)
            {
                worksheetName = "Sheet1";
            }

            var allData = dataFromService;
            Dictionary<string, object> data2 = new Dictionary<string, object>();

            //new workbook
            XLWorkbook workBook = new XLWorkbook();
            //worksheet
            IXLWorksheet sheet = workBook.Worksheets.Add(worksheetName);

            var data = Library.GetFieldAndType(typeof(T));
            List<Dictionary<string, string>> col = new List<Dictionary<string, string>>();
            foreach (var items in data)
            {
                Dictionary<string, string> tmpData = new Dictionary<string, string>();
                tmpData.Add("fldName", items["fldName"]);
                tmpData.Add("fldType", items["fldType"]);
                col.Add(tmpData);
            }

            int i = 1;
            int counter = 0;
            foreach (Dictionary<string, string> x in col)
            {
                cell = Library.GetColumnExcel(i);
                columnName = Library.GetColumnExcel(i) + "1";
                fldName = x["fldName"];
                sheet.Cell(columnName).Value = fldName;
                iSheetValue = 2;

                if (counter == 0)
                    startSheet = columnName;
                else if (counter == col.Count - 1)
                    endSheet = columnName;

                int countIndex = 0;
                foreach (var row in allData)
                {
                    columnName = Library.GetColumnExcel(i) + iSheetValue;
                    var rowValue = row.GetType().GetProperty(fldName).GetValue(allData[countIndex]).ToString();
                    sheet.Cell(columnName).Value = rowValue;
                    iSheetValue++;

                    if (DateTime.TryParse(rowValue, out tmpDate))
                    {
                        sheet.Cell(columnName).Style.DateFormat.Format = "dd-MMM-yyyy";
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    else if (decimal.TryParse(rowValue, out tmpDecimal))
                    {
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                    else if (int.TryParse(rowValue, out tmpInt))
                    {
                        sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }

                    if (isCenter != null)
                    {
                        foreach (var item in isCenter)
                        {
                            if (item == fldName)
                            {
                                if (DateTime.TryParse(rowValue, out tmpDate))
                                {
                                    sheet.Cell(columnName).Style.DateFormat.Format = "dd-MMM-yyyy";
                                    sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                                else
                                {
                                    sheet.Cell(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                            }
                        }
                    }
                    countIndex++;
                }

                counter++;
                i++;
            }

            var rangeHeader = sheet.Range(startSheet + ":" + endSheet);
            rangeHeader.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rangeHeader.Style.Font.Bold = true;

            endSheet = endSheet.Remove(endSheet.Length - 1);

            var rangeBorder = sheet.Range(startSheet + ":" + endSheet + (allData.Count + 1));
            rangeBorder.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rangeBorder.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rangeBorder.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            sheet.Rows().AdjustToContents();
            sheet.Columns().AdjustToContents();

            fileExcel = fileName;
            workBook.SaveAs(sourcePath);

            dt.Add("fileExcel", fileExcel);

            return dt;

        }
        #endregion
    }
}
