/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2025 DUONG DIEU PHAP
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
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using Cysharp.Text;
using ImageGlass;
using ImageGlass.Base;
using ImageGlass.Base.Update;
using ImageGlass.Settings;

namespace igcmd.Tools;

public partial class FrmUpdate : WebForm
{
    private readonly UpdateService _updater = new();

    public FrmUpdate()
    {
        InitializeComponent();

        // if WebView2 not installed
        if (!Web2.CheckWebview2Installed())
        {
            Opacity = 0;
            _ = ShowNativeUpdateDialogAsync();
        }
    }


    // Protected / override methods
    #region Protected / override methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode) return;

        _ = Config.UpdateFormIcon(this);
        Web2.PageName = "update";
        Text = Config.Language[$"_._CheckForUpdate"];
        CloseFormHotkey = Keys.Escape;

        // center window in screen
        var workingArea = Screen.FromControl(this).WorkingArea;
        var rect = BHelper.CenterRectToRect(Bounds, workingArea);
        Location = rect.Location;
    }


    protected override async Task OnWeb2ReadyAsync()
    {
        await base.OnWeb2ReadyAsync();

        var htmlFilePath = App.StartUpDir(Dir.WebUI, $"{nameof(FrmUpdate)}.html");
        Web2.Source = new Uri(htmlFilePath);
    }


    protected override async Task OnWeb2NavigationCompleted()
    {
        await base.OnWeb2NavigationCompleted();


        // show loading status
        var archInfo = Environment.Is64BitProcess ? "64-bit" : "32-bit";
        await Web2.ExecuteScriptAsync(@$"
            window._page.loadData({{
                CurrentVersion: '{App.Version} ({archInfo})',
            }});

            document.documentElement.setAttribute('app-status', 'checking');
        ");


        // check for update
        var release = await GetUpdateInfoAsync(1000);

        // show result
        var status = _updater.HasNewUpdate ? "outdated" : "updated";

        await Web2.ExecuteScriptAsync(@$"
            window._page.loadData({{
                CurrentVersion: '{App.Version} ({archInfo})',
                LatestVersion: '{release.NewVersion}',
                PublishedDate: '{release.ReleasedDate}',
                ReleaseTitle: '{release.Title}',
                ReleaseLink: '{release.Link}',
                ReleaseDetails: `{release.Details}`,
            }});

            document.documentElement.setAttribute('app-status', '{status}');
        ");
    }


    protected override async Task OnWeb2MessageReceivedAsync(Web2MessageReceivedEventArgs e)
    {
        await base.OnWeb2MessageReceivedAsync(e);

        if (e.Name.Equals("BtnImageGlassStore", StringComparison.InvariantCultureIgnoreCase))
        {
            BHelper.OpenImageGlassMsStore();
        }
        else if (e.Name.Equals("BtnUpdate", StringComparison.InvariantCultureIgnoreCase))
        {
            if (BHelper.IsRunningAsUwp())
            {
                BHelper.OpenImageGlassMsStore();
            }
            else
            {
                _ = BHelper.OpenUrlAsync(_updater.CurrentReleaseInfo?.ChangelogUrl.ToString(), $"from_{Web2.PageName}");
            }
        }
        else if (e.Name.Equals("BtnClose", StringComparison.InvariantCultureIgnoreCase))
        {
            Close();
        }
    }

    #endregion // Protected / override methods


    public async Task<(string NewVersion, string ReleasedDate, string Title, string Link, string Details)> GetUpdateInfoAsync(int delayMs = 0)
    {
        // check for update
        await _updater.GetUpdatesAsync();
        await Task.Delay(delayMs);


        // show results
        var status = _updater.HasNewUpdate ? "outdated" : "updated";
        var newVersion = _updater.CurrentReleaseInfo?.Version ?? "";
        var releasedDate = _updater.CurrentReleaseInfo?.PublishedDate.ToString(Const.DATETIME_FORMAT) ?? "";
        var releaseTitle = _updater.CurrentReleaseInfo?.Title ?? "";
        var releaseLink = _updater.CurrentReleaseInfo?.ChangelogUrl.ToString() ?? "";
        var releaseDetails = _updater.CurrentReleaseInfo?.Description?.Replace("\r\n", "<br/>") ?? "";


        return (newVersion, releasedDate, releaseTitle, releaseLink, releaseDetails);
    }


    private async Task ShowNativeUpdateDialogAsync()
    {
        // get release info
        var release = await GetUpdateInfoAsync();

        var langPath = nameof(FrmUpdate);
        var archInfo = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
        var appVersion = App.Version + $" ({archInfo})";

        // footer buttons
        var btnUpdate = new TaskDialogButton(Config.Language["_._Update"], allowCloseDialog: false);
        var btnIgStore = new TaskDialogButton("Get ImageGlass Store");
        var btnClose = new TaskDialogButton(Config.Language["_._Close"]);

        btnIgStore.Click += (_, _) => BHelper.OpenImageGlassMsStore();
        btnUpdate.Click += async (_, _) =>
        {
            await BHelper.OpenUrlAsync(release.Link, "from_update");
            Close();
        };


        var btns = new TaskDialogButtonCollection()
        {
            btnUpdate, btnClose,
        };
        if (!BHelper.IsRunningAsUwp())
        {
            btns.Insert(1, btnIgStore);
        }

        // WebView2 warning
        var webview2Warning = "";
        if (Web2.Webview2Version == null)
        {
            webview2Warning = Config.Language["_._Webview2._NotFound"];
        }
        else if (Web2.Webview2Version < Web2.MIN_VERSION)
        {
            webview2Warning = ZString.Format(Config.Language["_._Webview2._Outdated"], Web2.MIN_VERSION);
        }

        var details = release.Details
            .Replace("<br/>", "\r\n")
            .Replace("<b>", "").Replace("</b>", "")
            .Replace("<div>", "").Replace("</div>", "\r\n")
            .Replace("<p>", "").Replace("</p>", "\r\n");

        // content
        var page = new TaskDialogPage()
        {
            Icon = _updater.HasNewUpdate ? TaskDialogIcon.Information : TaskDialogIcon.ShieldSuccessGreenBar,
            Buttons = btns,
            SizeToContent = true,
            AllowCancel = true,
            EnableLinks = true,
            Caption = Config.Language["_._CheckForUpdate"],

            Heading = _updater.HasNewUpdate
                ? Config.Language[$"{langPath}._StatusOutdated"]
                : Config.Language[$"{langPath}._StatusUpdated"],

            Text = 
                $"{string.Format(Config.Language[$"{langPath}._CurrentVersion"], appVersion)}\r\n" +
                $"{string.Format(Config.Language[$"{langPath}._LatestVersion"], release.NewVersion)}\r\n" +
                $"{string.Format(Config.Language[$"{langPath}._PublishedDate"], release.ReleasedDate)}\r\n" +
                $"\r\n" +
                $"<a href=\"{release.Link}\">{release.Title}</a>\r\n" +
                $"{details}",

            Footnote = new()
            {
                Icon = TaskDialogIcon.ShieldWarningYellowBar,
                Text = $"{webview2Warning}\r\n\r\n" +
                    $"{Config.Language["FrmAbout._License"]}: <a href=\"https://imageglass.org/license\">https://imageglass.org/license</a>\r\n" +
                    $"Copyright © 2010-{DateTime.Now.Year} by Dương Diệu Pháp. All rights reserved.",
            },
        };

        page.LinkClicked += async (object? sender, TaskDialogLinkClickedEventArgs e) =>
        {
            await BHelper.OpenUrlAsync(e.LinkHref, "from_update");
        };


        // show dialog
        _ = TaskDialog.ShowDialog(this, page);

        Close();
    }

}
