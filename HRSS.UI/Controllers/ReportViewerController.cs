using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using KNSSService;
using KNSSService.Contract;
using KNSSUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AppMockup.Controllers
{
    public class ReportViewerController : Controller
    {
        private readonly IReportViewerService _reportService;
        public ReportViewerController()
        {
            _reportService = new ReportViewerService();
        }
        //GET: Home
        public ActionResult ReportViewer()
        {
            var rptDoc = new ReportDocument();
            string sessionId = this.Request.QueryString["sessionId"];
            var tes = System.Web.HttpContext.Current.Session[sessionId + "_reportId"];
            string instanDb = System.Configuration.ConfigurationManager.AppSettings["InstanDB"].ToString();
            string serverDb = System.Configuration.ConfigurationManager.AppSettings["ServerDb"].ToString();
            string userDB = System.Configuration.ConfigurationManager.AppSettings["userDB"].ToString();
            string paswwordDb = System.Configuration.ConfigurationManager.AppSettings["paswwordDb"].ToString();
            //hdnSession.Value = sessionId;

            #region Test
            if (System.Web.HttpContext.Current.Session[sessionId + "_reportId"] != null)
            {
                string reportId = System.Web.HttpContext.Current.Session[sessionId + "_reportId"].ToString();
                var param = System.Web.HttpContext.Current.Session[sessionId + "_param"];

                //Func.sessionReport(reportId);

                //string sSql = HttpContext.Current.Session[sessionId + "_sql"].ToString();
                //string tabel = HttpContext.Current.Session[sessionId + "_tabel"].ToString();
                //string mode = HttpContext.Current.Session[sessionId + "_mode"].ToString();
                //string con = HttpContext.Current.Session[sessionId + "_con"].ToString();

                //if (con == null)
                //{
                //    con = "0";
                //}

                GetDataReportViewerByIdRequest request = new GetDataReportViewerByIdRequest();
                request.ReportViewerId = reportId;

                GetDataReportViewerByIdResponse response = _reportService.GetDataReportViewerById(request);

                var report = response.ReportViewer;

                string reportFile = "../Reports/" + report.ReportFile.ToString();
                string query = report.SqlQuery.ToString();
                string useSp = report.UseSp.ToString();
                bool isSp = false;
                if (!(useSp == "" || useSp == "0"))
                {
                    isSp = true;
                }

                int count = 0;
                var listParam = new List<Dictionary<string, string>>();

                if (System.Web.HttpContext.Current.Session[sessionId + "_param"] != null)
                {
                    foreach (Dictionary<string, string> x in (List<Dictionary<string, string>>)param)
                    {
                        var tmp = new Dictionary<string, string>();

                        string fldName = x["fldName"];
                        string fldOpr = x["fldOpr"];
                        string fldValue1 = x["fldValue1"];
                        string fldValue2 = x["fldValue2"];
                        string isParam = x["isParameter"];

                        string conditionExp = "";
                        if (isParam == "0")
                        {
                            switch (fldOpr)
                            {
                                case "eq":
                                    conditionExp = fldName + " = " + "'" + fldValue1 + "'";
                                    break;
                                case "ne":
                                    conditionExp = fldName + " <> " + "'" + fldValue1 + "'";
                                    break;
                                case "bw":
                                    conditionExp = fldName + " LIKE " + "'" + fldValue1 + "%'";
                                    break;
                                case "bn":
                                    conditionExp = fldName + " NOT LIKE " + "'" + fldValue1 + "%'";
                                    break;
                                case "ew":
                                    conditionExp = fldName + " LIKE " + "'" + "%" + fldValue1 + "'";
                                    break;
                                case "en":
                                    conditionExp = fldName + " NOT LIKE " + "'" + "%" + fldValue1 + "'";
                                    break;
                                case "cn":
                                    conditionExp = " CONTAINS " + "(" + fldName + "," + fldValue1 + ")";
                                    break;
                                case "nc":
                                    conditionExp = " NOT CONTAINS " + "(" + fldName + "," + fldValue1 + ")";
                                    break;
                                case "lt":
                                    conditionExp = fldName + " < " + "'" + fldValue1 + "'";
                                    break;
                                case "le":
                                    conditionExp = fldName + " <= " + "'" + fldValue1 + "'";
                                    break;
                                case "gt":
                                    conditionExp = fldName + " > " + "'" + fldValue1 + "'";
                                    break;
                                case "ge":
                                    conditionExp = fldName + " >= " + "'" + fldValue1 + "'";
                                    break;
                                case "bt":
                                    conditionExp = fldName + " BETWEEN " + "'" + fldValue1 + "'" + " AND " + "'" + fldValue2 + "'";
                                    break;
                            }
                            string whereExp = count == 0 ? " WHERE " : " AND ";
                            query += whereExp + conditionExp;
                            count++;
                        }
                        else
                        {
                            // Untuk menampung paramater
                            tmp.Add("fldName", fldName);
                            tmp.Add("fldOpr", fldOpr);
                            tmp.Add("fldValue1", fldValue1);
                            tmp.Add("fldValue2", fldValue2);
                            tmp.Add("isParamater", isParam);
                            listParam.Add(tmp);
                        }
                    }
                }

                DataSet ds = new DataSet();
                ds.ReadXmlSchema(Server.MapPath("~/DatasetReport") + "/DataSet1.xsd");
                ds.EnforceConstraints = false;

                DataTable dt = new DataTable();
                dt.TableName = reportId;

                if (isSp)
                {
                    var paramSp = new Dictionary<string, string>();
                    if (listParam.Count > 0)
                    {
                        foreach (var x in listParam)
                        {
                            string fldName = x["fldName"];
                            string value = x["fldValue1"];
                            paramSp.Add(fldName, value);
                        }
                    }
                    //throw new Exception("ok");
                    dt = Library.ExecGetMultiDataBySPtoDatatable(query, paramSp);
                }
                else
                {
                    dt = Library.QueryToDataTable(query);
                }

                rptDoc.Load(Server.MapPath(reportFile));
                ds.Tables[rptDoc.Database.Tables[0].Name].Merge(dt);

                rptDoc.SetDatabaseLogon(userDB, paswwordDb, instanDb, serverDb);
                rptDoc.SetDataSource(ds);
                rptDoc.Refresh();
                rptDoc.VerifyDatabase();

                SubreportObject subreport = null;
                foreach (Section section in rptDoc.ReportDefinition.Sections)
                {
                    foreach (object item in section.ReportObjects)
                    {
                        subreport = item as SubreportObject;
                        if (subreport != null)
                        {
                            if (subreport.SubreportName != "")
                            {
                                dt = new DataTable();
                                String tblname =
                                ds.Tables[rptDoc.Subreports[subreport.SubreportName].Database.Tables[0].Name].TableName;
                                dt.TableName = subreport.Name;
                                dt = Library.QueryToDataTable("select * from " + tblname);
                                ds.Tables[tblname].Merge(dt);
                                rptDoc.Subreports[subreport.SubreportName].SetDataSource(ds);
                                rptDoc.Subreports[subreport.SubreportName].VerifyDatabase();
                            }
                        }
                    }

                    //Jika Terdapat Field Paramter (Otomatis digunakan untuk menyimpan info parameter filter)
                    CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions crParameterdef;
                    crParameterdef = rptDoc.DataDefinition.ParameterFields;
                    int i = 0;
                    foreach (CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition def in crParameterdef)
                    {
                        var test = def.ReportName;
                        if (def.ReportName == "Company.rpt")
                        {
                            if (def.Name == "baseURL")
                            {
                                rptDoc.SetParameterValue(def.Name, Conf.Folder);
                            }
                        }
                        else if (def.ReportName == "")
                        {
                            //var d = listParam[i]["fldValue1"];
                            if (def.Name == "basePath")
                            {
                                rptDoc.SetParameterValue(def.Name, Conf.EmployeeFolder);
                            }
                            else if (def.Name == "baseURL")
                            {
                                rptDoc.SetParameterValue(def.Name, Conf.Folder);
                            }
                            else
                            {
                                foreach (var x in listParam)
                                {
                                    string fldName = x["fldName"];
                                    string value = x["fldValue1"];
                                    if (String.Equals(def.Name, fldName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        rptDoc.SetParameterValue(def.Name, value);
                                    }
                                }
                            }

                            //rptDoc.SetParameterValue(def.Name, listParam[i]["fldValue1"]);                                           
                        }
                        i++;
                    }

                    CrystalReportViewer crReport = new CrystalReportViewer();
                    crReport.ReportSource = rptDoc;
                    crReport.HasToggleGroupTreeButton = false;
                    crReport.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    crReport.HasToggleParameterPanelButton = false;
                    crReport.HasPrintButton = true;
                    crReport.DocumentView = DocumentViewType.WebLayout;

                    //Func.AuditFunction();
                }
            }
            #endregion

            return View();
        }
    }
}