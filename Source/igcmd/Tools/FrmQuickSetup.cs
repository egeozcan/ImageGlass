/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2024 DUONG DIEU PHAP
Project homepage: https://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using Cysharp.Text;
using ImageGlass;
using ImageGlass.Base;
using ImageGlass.Settings;

namespace igcmd.Tools;

public partial class FrmQuickSetup : WebForm
{
    public FrmQuickSetup()
    {
        InitializeComponent();
    }



    // Protected / override methods
    #region Protected / override methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode) return;


        // if WebView2 not installed, show the warning
        if (!Web2.CheckWebview2Installed())
        {
            ShowWebView2Warning();
            Close();
            return;
        }


        _ = Config.UpdateFormIcon(this);
        Web2.PageName = "quick-setup";
        Text = Config.Language[$"{nameof(FrmQuickSetup)}._Text"];
        CloseFormHotkey = Keys.Escape;

        // center window in screen
        var workingArea = Screen.FromControl(this).WorkingArea;
        var rect = BHelper.CenterRectToRect(Bounds, workingArea);
        Location = rect.Location;
    }


    protected override void OnRequestUpdatingLanguage()
    {
        base.OnRequestUpdatingLanguage();

        Text = Config.Language[$"{Name}._Text"];
    }


    protected override async Task OnWeb2ReadyAsync()
    {
        await base.OnWeb2ReadyAsync();

        var htmlFilePath = App.StartUpDir(Dir.WebUI, $"{nameof(FrmQuickSetup)}.html");
        Web2.Source = new Uri(htmlFilePath);
    }


    protected override async Task OnWeb2NavigationCompleted()
    {
        await base.OnWeb2NavigationCompleted();

        // load data
        var base64Logo = BHelper.ToBase64Png(Config.Theme.Settings.AppLogo);
        WebUI.UpdateLangListJson();
        var langListJson = BHelper.ToJson(WebUI.LangList);

        await Web2.ExecuteScriptAsync(@$"
            window._page.loadData({{
                appLogo: 'data:image/png;base64,{base64Logo}',
                langList: {langListJson},
                currentLangName: '{Config.Language.FileName}',
            }});
        ");
    }


    protected override async Task OnWeb2MessageReceivedAsync(Web2MessageReceivedEventArgs e)
    {
        await base.OnWeb2MessageReceivedAsync(e);

        if (e.Name.Equals("APPLY_SETTINGS", StringComparison.InvariantCultureIgnoreCase))
        {
            await ApplySettingsAsync(e.Data);
        }
        else if (e.Name.Equals("SKIP_AND_LAUNCH", StringComparison.InvariantCultureIgnoreCase))
        {
            await SkipAndLaunchAsync();
        }
        else if (e.Name.Equals("LOAD_LANGUAGE", StringComparison.InvariantCultureIgnoreCase))
        {
            Config.Language = new IgLang(e.Data, App.StartUpDir(Dir.Language));
            Config.TriggerRequestUpdatingLanguage();
        }
        else if (e.Name.Equals("SET_DEFAULT_VIEWER", StringComparison.InvariantCultureIgnoreCase))
        {
            await Config.SetDefaultPhotoViewerAsync(true);
        }
    }

    #endregion // Protected / override methods


    // Private methods
    #region Private methods

    private void ShowWebView2Warning()
    {
        // footer buttons
        var btnDownload = new TaskDialogCommandLinkButton(Config.Language["_._Download"]);
        var btnSkip = new TaskDialogCommandLinkButton(
            Config.Language[$"{Name}._SkipQuickSetup"],
            allowCloseDialog: false);


        btnDownload.Click += async (_, _) => await BHelper.OpenUrlAsync("https://developer.microsoft.com/microsoft-edge/webview2");
        btnSkip.Click += async (_, _) => await SkipAndLaunchAsync();


        var heading = "";
        if (Web2.Webview2Version == null)
        {
            heading = Config.Language["_._Webview2._NotFound"];
        }
        else if (Web2.Webview2Version < Web2.MIN_VERSION)
        {
            heading = ZString.Format(Config.Language["_._Webview2._Outdated"], Web2.MIN_VERSION);
        }


        // content
        var page = new TaskDialogPage()
        {
            Icon = TaskDialogIcon.ShieldWarningYellowBar,
            Buttons = [btnDownload, btnSkip, TaskDialogButton.Cancel],
            SizeToContent = true,
            AllowCancel = false,
            EnableLinks = true,
            Caption = Config.Language[$"{Name}._Text"],

            Heading = heading,
            Text = "Features require WebView2 Runtime:\r\n" +
                $"- {Config.Language[$"{Name}._Text"]}\r\n" +
                $"- {Config.Language["FrmMain.MnuSettings"]}\r\n" +
                $"- SVG\r\n" +
                "\r\n" +
                "<a href=\"https://developer.microsoft.com/microsoft-edge/webview2\">https://developer.microsoft.com/microsoft-edge/webview2</a>",
        };


        // show dialog
        _ = TaskDialog.ShowDialog(page, TaskDialogStartupLocation.CenterScreen);
    }


    private async Task ApplySettingsAsync(string settingJson)
    {
        // try to kill all ImageGlass processes
        if (!CmdHelper.KillImageGlassProcessesAsync(this, true)) return;


        // don't auto-show Quick Setup again
        Config.QuickSetupVersion = Const.QUICK_SETUP_VERSION;


        // Parse settings JSON
        #region Parse settings JSON
        var dict = BHelper.ParseJson<Dictionary<string, object?>>(settingJson);

        if (dict.TryGetValue(nameof(Config.ColorProfile), out var enableColorProfileObj))
        {
            var enableColorProfile = enableColorProfileObj
                ?.ToString()
                .Equals("true", StringComparison.InvariantCultureIgnoreCase) ?? false;

            Config.ColorProfile = enableColorProfile
                ? nameof(ColorProfileOption.CurrentMonitorProfile)
                : nameof(ColorProfileOption.None);
        }

        if (dict.TryGetValue(nameof(Config.UseEmbeddedThumbnailRawFormats), out var useThumbnailRawFormatsObj))
        {
            Config.UseEmbeddedThumbnailRawFormats = useThumbnailRawFormatsObj
                ?.ToString()
                .Equals("true", StringComparison.InvariantCultureIgnoreCase) ?? true;
        }

        #endregion // Parse settings JSON


        // apply and restart ImageGlass
        await ApplyAndCloseAsync();
    }


    private async Task ApplyAndCloseAsync()
    {
        // write settings
        await Config.WriteAsync();

        CmdHelper.LaunchImageGlass();
        Close();
    }


    private async Task SkipAndLaunchAsync()
    {
        Config.QuickSetupVersion = Const.QUICK_SETUP_VERSION;
        await Config.WriteAsync();

        CmdHelper.LaunchImageGlass();
        Close();
    }

    #endregion // Private methods

}
