using DesktopToast;
using HealthDeclaration.Helpers;
using HealthDeclaration.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthDeclaration
{
    public partial class Main : Form
    {
        static WebModel _webModel;
        static string _pDebug = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        static string _pScript = _pDebug + "\\scripts\\";
        static string _url = "COVID";
        string _pCurrentScript = null;
        bool[] Days;
        TimerHelper _timerHelper;
        IniHelper _initHelper;
        static DateTime _morning = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            10, 0, 0);
        static DateTime _afternoon = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            16, 0, 0);

        public Main()
        {
            InitializeComponent();
            _ = Init();
            InitLoadData();
            InitSystemTray();
            CheckInternet();
        }

        #region Init
        async Task Init()
        {
            this.Text = "Health Declaration" + $" - v{System.Reflection.Assembly.GetEntryAssembly().GetName().Version}";
            _initHelper = new IniHelper();
            Days = new bool[7];
            lbMessage.Text = "";
            _webModel = new WebModel();
            wbSamsung.ScriptErrorsSuppressed = true;
            wbSamsung.DocumentCompleted += async (s, e) => await IsSubmitForm(wbSamsung.Document);
            wbSamsung.Navigating += async (s, e) => await LoadScript();

            if (CommonHelper.CheckForInternetConnection())
            {
                InitSourceComboBox();
                InitWeb();
            }
            else
            {
                await ShowToastAsync("info", "TimDev", new string[] { "Can't connect internet!" });
                lbMessage.Text = "Can't connect internet!";
            }

            _timerHelper = new TimerHelper(60 * 30, async () =>
              {
                  //check internet
                  if (!CommonHelper.CheckForInternetConnection())
                  {
                      await ShowToastAsync("info", "TimDev", new string[] { "Can't connect internet!" });
                      lbMessage.Text = "Can't connect internet!";
                      return;
                  }

                  var nscript = _initHelper.Read("Script");
                  if (string.IsNullOrEmpty(nscript)
                  || !File.Exists(_pScript + nscript + ".json"))
                  {
                      await ShowToastAsync("error", "TimDev", new string[] { "Script does not exist!" });
                      lbMessage.Text = "Script does not exist!";
                      return;
                  }

                  var day = (int)DateTime.Now.DayOfWeek;
                  if (Days[day - 1] && DateTime.Now > _morning && DateTime.Now < _morning.AddHours(1))
                  {
                      await FillScript(nscript, "MorningTime");
                  }
                  if (Days[day - 1] && DateTime.Now > _afternoon && DateTime.Now < _afternoon.AddHours(1))
                  {
                      await FillScript(nscript, "AfternoonTime");
                  }
              });
            _timerHelper.Start();
        }

        void InitWeb()
        {
            _webModel = new WebModel();
            if (cbSource.SelectedItem != null)
                wbSamsung.Navigate("https://203.254.249.240/" + $"{_url}{cbSource.SelectedItem.ToString().ToUpper()}");
        }

        async void InitSourceComboBox()
        {
            var web = new WebBrowser();
            web.Navigate("https://203.254.249.240/");
            await Task.Delay(1000);
            var listCombo = GetComboBoxs(web.Document.Body.InnerHtml);

            if (listCombo.Count == 1)
            {
                var lSource = new List<string>();
                var options = GetOptionComboBox(listCombo[0]);
                for (int i = 1; i < options.Count(); i++)
                {
                    var comp = GetCompany(options[i]);
                    lSource.Add(comp);
                }
                cbSource.DataSource = lSource;
            }
        }

        void InitSystemTray()
        {
            this.SystemTrayIcon.Visible = true;
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Exit", ContextMenuExit);
            menu.MenuItems.Add("Show", ContextMenuShow);
            this.SystemTrayIcon.ContextMenu = menu;
            this.WindowState = FormWindowState.Minimized;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;
        }

        void InitLoadData()
        {
            if (_initHelper.KeyExists("Auto"))
                cbAuto.Checked = bool.Parse(_initHelper.Read("Auto"));

            cbScripts.DataSource = GetListScript(_pCurrentScript);

            if (_initHelper.KeyExists("Script"))
                cbScripts.SelectedItem = _initHelper.Read("Script");

            if (_initHelper.KeyExists("MorningTime"))
            {
                var tMorning = DateTime.ParseExact(_initHelper.Read("MorningTime"), "HH:mm:ss dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
                lbMessage.Text = $"Tool has automatically submitted\nThe form on Morning time: {tMorning}";
            }
            if (_initHelper.KeyExists("AfternoonTime"))
            {
                var tAfternoon = DateTime.ParseExact(_initHelper.Read("AfternoonTime"), "HH:mm:ss dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
                lbMessage.Text = $"Tool has automatically submitted\nThe form on Afternoon time: {tAfternoon}";
            }

            if (_initHelper.KeyExists("Days"))
            {
                var list = _initHelper.Read("Days");
                foreach (var day in list.Trim().Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(day))
                    {
                        Days[int.Parse(day) - 1] = true;
                        CheckBoxDay(int.Parse(day), true);
                    }
                }
            }
        }

        void CheckBoxDay(int day, bool state)
        {
            if (day == 1) cb2.Checked = state;
            else if (day == 2) cb3.Checked = state;
            else if (day == 3) cb4.Checked = state;
            else if (day == 4) cb5.Checked = state;
            else if (day == 5) cb6.Checked = state;
            else if (day == 6) cb7.Checked = state;
            else if (day == 7) cbCn.Checked = state;
        }

        void CheckInternet()
        {
            var timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                var state = CommonHelper.CheckForInternetConnection();
                var localstate = bool.Parse(_initHelper.Read("Internet") == "" ? "false" : _initHelper.Read("Internet"));

                if (state)
                {
                    if (!localstate)
                    {
                        lbMessage.Text = $"Internet connected!";
                    }
                }
                else
                {
                    lbMessage.Text = $"No internet!";
                }
                _initHelper.Write("Internet", state.ToString());
            };
            timer.Start();
        }
        #endregion

        #region Check & Filter

        List<string> GetListScript(string path)
        {
            var result = new List<string>();
            foreach (var item in Directory.GetFiles(_pScript))
            {
                result.Add(Path.GetFileNameWithoutExtension(item));
            }

            return result;
        }

        async Task IsSubmitForm(HtmlDocument document)
        {
            var mess = GetMessage(document);
            if (mess != null)
            {
                if (mess.ToLower().Trim().Contains("thành công".Trim()))
                {
                    #region Save
                    if (_pCurrentScript != null)
                    {
                        #region Radio
                        var html = wbSamsung.DocumentText;
                        var listRadio = GetRadios(html);
                        _webModel.Radios = new List<RadioItem>();
                        int tempindex = 0;
                        string tempid = "";
                        foreach (var radio in listRadio)
                        {
                            var id = GetId(radio).Trim().Replace("\"", "");
                            var isCheck = GetChecked(radio)?.Trim().Replace("\"", "");

                            if (tempid != id)
                            {
                                tempindex = 0;
                                tempid = id;
                            }
                            else tempindex++;

                            if (!string.IsNullOrWhiteSpace(isCheck) && isCheck == "checked")
                            {
                                _webModel.Radios.Add(new RadioItem
                                {
                                    Id = id,
                                    Index = tempindex,
                                });
                            }
                        }
                        #endregion

                        cbScripts.DataSource = GetListScript(_pCurrentScript);
                        var result = await JsonHelper<WebModel>.WriteItemAsync(_pCurrentScript, _webModel);
                        if (result)
                        {
                            lbMessage.Text = "Save script success";
                            _initHelper.Write("UserTime", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                            _initHelper.Write("Script", Path.GetFileNameWithoutExtension(_pCurrentScript));
                            await ShowToastAsync("ok", "TimDev", new string[] { "Save script success!" });
                        }
                    }
                    #endregion
                }
                else lbMessage.Text = mess;
            }
        }

        string GetMessage(HtmlDocument document)
        {
            try
            {
                var ul = document.GetElementsByTagName("ul");
                var li = ul[0].GetElementsByTagName("li");
                return li[0].InnerText;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region Common
        string GetId(string source)
        {
            var list = source.GetElementByRegex("id=", " ");
            return list.Count != 0 ? list[0] : null;
        }

        string GetCompany(string source)
        {
            var list = source.GetElementByRegex(">", "<");
            return list.Count != 0 ? list[0] : null;
        }

        string GetChecked(string source)
        {
            var list = source.GetElementByRegex("checked=", " ");
            return list.Count != 0 ? list[0] : null;
        }
        #endregion

        #region ComboBox
        List<string> GetComboBoxs(string html)
        {
            return html.GetElementByRegex("<select", "</select>");
        }
        List<string> GetOptionComboBox(string select)
        {
            return select.GetElementByRegex("<option", "/option>");
        }
        object[] GetSelectComboBox(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var option = options[i];
                if (option.Contains("selected"))
                    return new object[] { i, GetCompany(option) };
            }
            return null;
        }

        private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            wbSamsung.Navigate("https://203.254.249.240/" + $"{_url}{cbSource.SelectedItem.ToString().ToUpper()}");
        }
        #endregion

        #region Text
        List<string> GetTexts(string html)
        {
            return html.GetElementByRegex("<input", ">").Where(x => x.Contains("type=\"text\"") && x.Contains("value")).ToList();
        }
        #endregion

        #region Radio
        List<string> GetRadios(string html)
        {
            return html.GetElementByRegex("<input", ">").Where(x => x.Contains("type=\"radio\"") && x.Contains("value")).ToList();
        }

        #endregion

        #region CheckBox
        List<string> GetCheckBoxs(string html)
        {
            return html.GetElementByRegex("<input", "<br>").Where(x => x.Contains("type=\"checkbox\"") && x.Contains("value")).ToList();
        }
        #endregion


        #region GetScript
        async Task LoadScript()
        {
            var html = wbSamsung.DocumentText;
            if (string.IsNullOrEmpty(html)) return;
            _webModel = new WebModel();

            #region combobox
            _webModel.ComboBoxs = new List<ComboBoxItem>();
            var isExistNameScript = false;
            var listCombo = GetComboBoxs(wbSamsung.Document.Body.InnerHtml);
            foreach (var combo in listCombo)
            {
                var objs = GetSelectComboBox(GetOptionComboBox(combo));
                var index = int.Parse(objs?[0].ToString());

                if (index != 0)
                {
                    var id = GetId(combo);
                    if (_webModel.ComboBoxs.Exists(x => x.Id == id)) continue;
                    if (!isExistNameScript && combo.Contains("onchange"))
                    {
                        isExistNameScript = true;
                        _pCurrentScript = _pScript + objs?[1].ToString() + ".json";
                    }

                    _webModel.ComboBoxs.Add(new ComboBoxItem
                    {
                        Id = id,
                        Index = index.ToString(),
                        IsOnChange = combo.Contains("onchange")
                    });
                }
            }
            #endregion

            #region Text
            var listText = GetTexts(html);
            _webModel.Texts = new List<TextItem>();
            foreach (var text in listText)
            {
                var id = GetId(text).Trim().Replace("\"", "");
                var value = wbSamsung.Document.GetElementById(id).GetAttribute("value");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    _webModel.Texts.Add(new TextItem
                    {
                        Id = id,
                        Text = value,
                    });
                }
            }
            #endregion

            #region CheckBox
            await Task.Delay(1500);
            var listCheckbox = GetCheckBoxs(html);
            //_webModel.CheckBoxs = new List<CheckBoxItem>();
            //tempindex = 0;
            //tempid = "";
            //foreach (var checkbox in listCheckbox)
            //{
            //    var id = GetId(checkbox).Trim().Replace("\"", "");
            //    var isCheck = GetChecked(checkbox)?.Trim().Replace("\"", "");

            //    if (tempid != id)
            //    {
            //        tempindex = 0;
            //        tempid = id;
            //    }
            //    else tempindex++;

            //    if (!string.IsNullOrWhiteSpace(isCheck) && isCheck == "checked")
            //    {
            //        //_webModel.Radios.Add(new RadioItem
            //        //{
            //        //    Id = id,
            //        //    Index = tempindex.ToString(),
            //        //});
            //    }
            //}
            #endregion
        }
        #endregion

        #region LoadScript
        async Task FillScript(string nScript, string type)
        {
            if (nScript == null) return;
            var pCurrentScript = _pScript + nScript + ".json";
            if (!File.Exists(pCurrentScript)) return;

            var webmodel = await JsonHelper<WebModel>.ReadItemAsync(pCurrentScript);
            var web = new WebBrowser();
            web.Navigate("https://203.254.249.240/" + $"{_url}{nScript.ToUpper()}");
            await Task.Delay(2000);

            #region Fill

            var document = web.Document;

            //ComboBoxs
            foreach (var combo in webmodel.ComboBoxs)
            {
                try
                {
                    var cb = document.GetElementById(combo.Id);
                    cb?.SetAttribute("selectedIndex", combo.Index);
                    if (combo.IsOnChange)
                    {
                        cb.InvokeMember("onchange");
                        await Task.Delay(1000);
                    }
                }
                catch (Exception)
                {
                    lbMessage.Text = $"Send form error!";
                }
            }

            //Texts
            foreach (var text in webmodel.Texts)
            {
                try
                {
                    document.GetElementById(text.Id).SetAttribute("value", text.Text);
                }
                catch (Exception)
                {
                    lbMessage.Text = $"Send form error!";
                }
            }

            //Radios
            foreach (var radio in webmodel.Radios)
            {
                try
                {
                    document.All.GetElementsByName(radio.Id)[radio.Index].InvokeMember("click");
                }
                catch (Exception)
                {
                    lbMessage.Text = $"Send form error!";
                }
            }

            //CheckBoxs
            if (webmodel.CheckBoxs != null)
            {
                foreach (var text in webmodel.CheckBoxs)
                {
                }
            }

            #endregion

            //submit
            ActionButtonSubmit(document, () => { });
            web.DocumentCompleted += async (s, e) =>
            {
                var mess = GetMessage(web.Document);
                if (!string.IsNullOrEmpty(mess))
                {
                    if (mess.ToLower().Trim().Contains("thành công".Trim()))
                    {
                        _initHelper.Write(type, DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                        _initHelper.Write("Script", Path.GetFileNameWithoutExtension(pCurrentScript));
                        lbMessage.Text = $"{type} submit form success!\nTime:{DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")}";
                        await ShowToastAsync("ok", "TimDev", new string[] { "Submit form success!", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                    }
                    else
                    {
                        lbMessage.Text = mess;
                        await ShowToastAsync("error", "TimDev", new string[] { mess });
                    }
                    wbSamsung.DocumentText = web.DocumentText;
                }
                else
                {
                    lbMessage.Text = $"Fail to send Form!";
                    await ShowToastAsync("error", "TimDev", new string[] { "Fail to send Form!" });
                }
            };
        }
        #endregion

        #region Helper
        public void ActionButtonSubmit(HtmlDocument document, Action @callback)
        {
            HtmlElementCollection elc = document.GetElementsByTagName("input");
            foreach (HtmlElement el in elc)
            {
                if (el.GetAttribute("type").Equals("submit"))
                {
                    el.InvokeMember("Click");
                    @callback.Invoke();
                }
            }
        }
        #endregion

        #region Event
        private void SystemTrayIconDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.SystemTrayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void ContextMenuShow(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void WindowResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            _initHelper.Write("Auto", cbAuto.Checked.ToString());
            SetRegistry(cbAuto.Checked);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            var nscript = _initHelper.Read("Script");
            if (string.IsNullOrEmpty(nscript)
            || !File.Exists(_pScript + nscript + ".json"))
            {
                await ShowToastAsync("error", "TimDev", new string[] { "Script does not exist!" });
                return;
            }
            await FillScript(nscript, "UserTime");
        }

        private void cbScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = cbScripts.SelectedItem.ToString();
            _initHelper.Write("Script", name);
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            if (CommonHelper.CheckForInternetConnection())
            {
                lbMessage.Text = "Reload website success!";
                InitSourceComboBox();
                InitWeb();
            }
            else
            {
                await ShowToastAsync("info", "TimDev", new string[] { "Can't connect internet!" });
                lbMessage.Text = "Can't connect internet!";
            }
        }
        #endregion

        #region Another
        void SetRegistry(bool state)
        {
            using (var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (state)
                    key.SetValue(Application.ProductName, "\"" + Application.ExecutablePath + "\"");
                else
                    key.DeleteValue(Application.ProductName);
            }
        }
        private async Task<string> ShowToastAsync(string type, string title, IList<string> message)
        {
            var request = new ToastRequest
            {
                ToastTitle = title,
                ToastBodyList = message,
                ToastAudio = DesktopToast.ToastAudio.SMS,
                ShortcutFileName = "HealthDeclaration.lnk",
                ShortcutTargetFilePath = Assembly.GetExecutingAssembly().Location,
                AppId = "HealthDeclaration",
                ToastLogoFilePath = _pDebug + $"\\Assets\\{type}.png",
                ShortcutIconFilePath = _pDebug + @"\Assets\automation.ico",
            };

            var result = await ToastManager.ShowAsync(request);
            return result.ToString();
        }
        #endregion

        #endregion

        #region Days
        void WriteList(bool[] list)
        {
            var outer = "";
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i])
                    outer += (i + 1) + ",";
            }
            _initHelper.Write("Days", outer);
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            Days[0] = cb2.Checked;
            WriteList(Days);

        }

        private void cb3_CheckedChanged(object sender, EventArgs e)
        {
            Days[1] = cb3.Checked;
            WriteList(Days);
        }

        private void cb4_CheckedChanged(object sender, EventArgs e)
        {
            Days[2] = cb4.Checked;
            WriteList(Days);
        }

        private void cb5_CheckedChanged(object sender, EventArgs e)
        {
            Days[3] = cb5.Checked;
            WriteList(Days);
        }

        private void cb6_CheckedChanged(object sender, EventArgs e)
        {
            Days[4] = cb6.Checked;
            WriteList(Days);
        }

        private void cb7_CheckedChanged(object sender, EventArgs e)
        {
            Days[5] = cb7.Checked;
            WriteList(Days);
        }

        private void cbCn_CheckedChanged(object sender, EventArgs e)
        {
            Days[6] = cbCn.Checked;
            WriteList(Days);
        }
        #endregion


    }
}
