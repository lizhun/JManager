using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using JiraManager.Entities;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace JiraManager
{
    public class JManager
    {
        private string _username;
        private string _password;
        private bool hasLogin = false;
        private HttpClient webclient;
        public event EventHandler OnAfterGetWebData;

        public JManager(string username, string password)
        {
            this._username = username;
            this._password = password;
            this.webclient = new HttpClient();
            this.webclient.BaseAddress = new Uri(ConfigSettings.JIRAServerUrl);
        }
        public JManager()
        {
            this._username = ConfigSettings.JIRAUserName;
            this._password = ConfigSettings.JIRAPassword;
            this.webclient = new HttpClient();
            this.webclient.BaseAddress = new Uri(ConfigSettings.JIRAServerUrl);
        }

        public async Task<JToken> GetJiraDataAsync(string jiranumber)
        {
            if (!this.hasLogin)
            {
                await this.loginAsync();
            }
            string url = string.Format("/rest/api/2/issue/{0}?expand=changelog&fields=status,summary", jiranumber);
            var datastr = await this.webclient.GetStringAsync(url);
            if (this.OnAfterGetWebData != null)
            {
                this.OnAfterGetWebData(jiranumber, null);
            }
            return JToken.Parse(datastr);
        }

        public async Task<List<JiraPageInfo>> GetJiraDataList(string[] jiranumbers)
        {
            var list = new List<JiraPageInfo>();
            foreach (var jiranumber in jiranumbers)
            {
                list.Add(await this.GetJiraData(jiranumber));
            }
            return list;
        }
        public async Task<JiraPageInfo> GetJiraData(string jiranumber)
        {
            var data = this.GetJiraDataFromDb(jiranumber);
            if (data == null)
            {
                data = await this.GetJiraDataFromWebAsync(jiranumber);
                this.SaveJiraDataToDb(data);
            }
            return data;
        }
        public void ClearDataInDb()
        {
            SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                "delete from JiraPageInfo", CommandType.Text);
            SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                "delete from StatusChangeItem", CommandType.Text);
        }
        public void initDataBase()
        {
            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + ConfigSettings.DBName))
            {
                using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
                {
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
            "CREATE TABLE JiraPageInfo (JiraNumber STRING PRIMARY KEY UNIQUE NOT NULL, CreateTime DATETIME NOT NULL DEFAULT (getdate()), UpdateTime DATETIME DEFAULT (getDate()) NOT NULL, Title STRING, LastestStatus STRING)", CommandType.Text);
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
        "CREATE TABLE StatusChangeItem (ActionDateTime DATETIME NOT NULL, JiraNumber STRING, NewValue STRING, OldValue STRING)", CommandType.Text);
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
"CREATE TABLE TBQuery ( Title   STRING, SqlText TEXT)", CommandType.Text);

                }
            }
        }

        private JiraPageInfo GetJiraDataFromDb(string jiranumbers)
        {
            JiraPageInfo jdata = null;
            using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
            {

                var dr = SQLiteHelper.ExecuteReader(con,
                   "select t1.Title,t1.JiraNumber,t1.CreateTime,t1.UpdateTime," +
                   "t2.OldValue,t2.NewValue,t2.ActionDateTime from JiraPageInfo  t1 " +
                   "left join StatusChangeItem t2 on t1.JiraNumber=t2.JiraNumber  " +
                   "where t1.JiraNumber = '" + jiranumbers + "'", CommandType.Text);
                if (dr.Read())
                {
                    jdata = new JiraPageInfo();
                    jdata.Title = (string)dr["Title"];
                    jdata.JiraNumber = (string)dr["JiraNumber"];
                    jdata.CreateTime = (DateTime)dr["CreateTime"];
                    jdata.UpdateTime = (DateTime)dr["UpdateTime"];
                    var status = new StatusChangeItem();
                    status.OldValue = (string)dr["OldValue"];
                    status.NewValue = (string)dr["NewValue"];
                    status.ActionDateTime = (DateTime)dr["ActionDateTime"];
                    jdata.StatusLogs.Add(status);
                    while (dr.Read())
                    {
                        status = new StatusChangeItem();
                        status.OldValue = (string)dr["OldValue"];
                        status.NewValue = (string)dr["NewValue"];
                        status.ActionDateTime = (DateTime)dr["ActionDateTime"];
                        jdata.StatusLogs.Add(status);
                    }
                }

            }
            return jdata;
        }
        private async Task<JiraPageInfo> GetJiraDataFromWebAsync(string jiranumber)
        {
            var data = await this.GetJiraDataAsync(jiranumber);
            return this.ConvertJiraPage(data);
        }
        private void SaveJiraDataToDb(JiraPageInfo jiraData)
        {
            using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
            {
                this.deleteJira(jiraData.JiraNumber);
                string sqlstr = "insert into JiraPageInfo (JiraNumber,CreateTime,UpdateTime,LastestStatus,Title) values (@JiraNumber,@CreateTime,@UpdateTime,@LastestStatus,@Title)";
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@JiraNumber",jiraData.JiraNumber.ToUpper()),
                    new SQLiteParameter("@CreateTime", DateTime.Now),
                    new SQLiteParameter("@UpdateTime", DateTime.Now),
                    new SQLiteParameter("@LastestStatus", jiraData.LastestStatus),
                    new SQLiteParameter("@Title", jiraData.Title)};
                SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                  sqlstr, CommandType.Text, parameters);
                foreach (var sitem in jiraData.StatusLogs)
                {
                    sqlstr = "insert into StatusChangeItem (JiraNumber,ActionDateTime,NewValue,OldValue) values (@JiraNumber,@ActionDateTime,@NewValue,@OldValue)";
                    SQLiteParameter[] parasitem = {
                    new SQLiteParameter("@JiraNumber", sitem.JiraNumber.ToUpper()),
                    new SQLiteParameter("@ActionDateTime", sitem.ActionDateTime),
                    new SQLiteParameter("@NewValue", sitem.NewValue),
                    new SQLiteParameter("@OldValue", sitem.OldValue)};
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                        sqlstr, CommandType.Text, parasitem);

                }
            }

        }
        public void SaveSqlToDb(string oldTile, string title, string sqltext)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(sqltext))
            {
                using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
                {
                    string sqlstr = "delete from  TBQuery where Title =@title";
                    SQLiteParameter[] paras = {
                    new SQLiteParameter("@title", title)};
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                        sqlstr, CommandType.Text, paras);
                    if (!string.IsNullOrEmpty(oldTile))
                    {

                        string sqlstrold = "delete from  TBQuery where Title =@title";
                        SQLiteParameter[] parasold = {
                    new SQLiteParameter("@title", oldTile)};
                        SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                            sqlstrold, CommandType.Text, parasold);
                    }


                    sqlstr = "insert into TBQuery (title,sqltext) values (@title,@sqltext)";
                    SQLiteParameter[] parasitem = {
                    new SQLiteParameter("@title", title),
                    new SQLiteParameter("@sqltext", sqltext)};
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                        sqlstr, CommandType.Text, parasitem);
                }
            }
        }
        public DataTable GetSqlFromDb()
        {
            using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
            {
                string sqlstr = "select * from  TBQuery";

                return SQLiteHelper.ExecuteDataSet(SQLiteHelper.LocalDbConnectionString,
                      sqlstr, CommandType.Text).Tables[0];
            }

        }

        public DataTable GetSqlResultFromDb(DateTime startTime, DateTime endTime,
            string sqlText, string[] jiranumbers)
        {
            using (var con = new SQLiteConnection(SQLiteHelper.LocalDbConnectionString))
            {
                var jirastr = string.Join(",", jiranumbers.Select(x => "'" + x.ToUpper() + "'"));
                string sqlstr = sqlText.Replace("##jiranumbers##", jirastr);
                SQLiteParameter[] parasitem = {
                    new SQLiteParameter("@startTime", startTime),
                    new SQLiteParameter("@endTime", endTime)};
                return SQLiteHelper.ExecuteDataSet(SQLiteHelper.LocalDbConnectionString,
                      sqlstr, CommandType.Text, parasitem).Tables[0];
            }

        }


        private void deleteJira(string jiranumber)
        {
            if (!string.IsNullOrEmpty(jiranumber))
            {
                SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
                   string.Format("delete from JiraPageInfo where jiranumber='{0}'", jiranumber), CommandType.Text);
                SQLiteHelper.ExecuteNonQuery(SQLiteHelper.LocalDbConnectionString,
            string.Format("delete from StatusChangeItem where jiranumber='{0}'", jiranumber), CommandType.Text);
            }
        }

        private JiraPageInfo ConvertJiraPage(JToken jiradata)
        {
            var data = new JiraPageInfo();
            data.JiraNumber = jiradata["key"].ToString();
            data.Title = jiradata["fields"]["summary"].ToString();
            data.LastestStatus = jiradata["fields"]["status"]["name"].ToString();
            //data.CreateTime = DateTime.Now;
            //data.UpdateTime = DateTime.Now;
            data.StatusLogs = jiradata["changelog"]["histories"]
                 .SelectMany(x => x["items"].Where(xx => xx["field"].ToString() == "status")
                 .Select(item => new StatusChangeItem()
                 {
                     JiraNumber = jiradata["key"].ToString(),
                     ActionDateTime = DateTime.Parse(x["created"].ToString()),
                     OldValue = item["fromString"].ToString(),
                     NewValue = item["toString"].ToString()
                 })).ToList();


            return data;
        }

        private async Task loginAsync()
        {
            var jsonParam = new JObject();
            jsonParam.Add("username", this._username);
            jsonParam.Add("password", this._password);
            var response = await this.webclient.PostAsJsonAsync("rest/auth/1/session", jsonParam);
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var oAuthTokenJsonString = await response.Content.ReadAsStringAsync();
                dynamic oAuthToken = JToken.Parse(oAuthTokenJsonString);
                string token = oAuthToken.session.value;
                string tokenname = oAuthToken.session.name;
                this.webclient.DefaultRequestHeaders.Add("cookie", tokenname + "=" + token);
                this.hasLogin = true;
            }

        }

    }
}
