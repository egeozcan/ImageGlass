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
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using Cysharp.Text;
using ImageGlass.Base;
using ImageGlass.Settings;
using System.Dynamic;

namespace ImageGlass;

public partial class FrmSettings : WebForm
{

    public FrmSettings()
    {
        InitializeComponent();
    }


    // Protected / override methods
    #region Protected / override methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode) return;

        Web2.PageName = "settings";
        Text = Config.Language[$"{nameof(FrmMain)}.{nameof(FrmMain.MnuSettings)}"];
        CloseFormHotkey = Keys.Escape;

        // load window placement from settings
        WindowSettings.LoadFrmSettingsPlacementFromConfig(this);
    }


    protected override void OnRequestUpdatingTheme(RequestUpdatingThemeEventArgs e)
    {
        base.OnRequestUpdatingTheme(e);

        // set app logo on titlebar
        _ = Config.UpdateFormIcon(this);
    }


    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        // save placement setting
        WindowSettings.SaveFrmSettingsPlacementToConfig(this);
    }


    protected override async Task OnWeb2ReadyAsync()
    {
        await base.OnWeb2ReadyAsync();

        var htmlFilePath = App.StartUpDir(Dir.WebUI, $"{nameof(FrmSettings)}.html");
        Web2.Source = new Uri(htmlFilePath);
    }


    protected override async Task OnWeb2NavigationCompleted()
    {
        await base.OnWeb2NavigationCompleted();

        try
        {
            // update the setting data
            await WebUI.UpdateSvgIconsAsync();
            WebUI.UpdateLangListJson();
            WebUI.UpdateThemeListJson();


            // prepare data for app settings UI
            var configJsonObj = Config.PrepareJsonSettingsObject(useAbsoluteFileUrl: true);
            var startupDir = App.StartUpDir();
            var configDir = App.ConfigDir(PathType.Dir);
            var userConfigFilePath = App.ConfigDir(PathType.File, Source.UserFilename);
            var defaultThemeDir = App.ConfigDir(PathType.Dir, Dir.Themes, Const.DEFAULT_THEME);
            var builtInToolbarBtns = Config.ConvertToolbarButtonsToExpandoObjList(Local.BuiltInToolbarItems);


            var pageSettingObj = new ExpandoObject();
            _ = pageSettingObj.TryAdd("startUpDir", startupDir);
            _ = pageSettingObj.TryAdd("configDir", configDir);
            _ = pageSettingObj.TryAdd("userConfigFilePath", userConfigFilePath);
            _ = pageSettingObj.TryAdd("FILE_MACRO", Const.FILE_MACRO);
            _ = pageSettingObj.TryAdd("enums", WebUI.Enums);
            _ = pageSettingObj.TryAdd("defaultThemeDir", defaultThemeDir);
            _ = pageSettingObj.TryAdd("defaultImageInfoTags", Config.DefaultImageInfoTags);
            _ = pageSettingObj.TryAdd("toolList", Config.Tools);
            _ = pageSettingObj.TryAdd("langList", WebUI.LangList);
            _ = pageSettingObj.TryAdd("themeList", WebUI.ThemeList);
            _ = pageSettingObj.TryAdd("icons", WebUI.SvgIcons);
            _ = pageSettingObj.TryAdd("builtInToolbarButtons", builtInToolbarBtns);
            _ = pageSettingObj.TryAdd("config", configJsonObj);
            var pageSettingStr = BHelper.ToJson(pageSettingObj);

            var script = @$"
                window._pageSettings = {pageSettingStr};

                window._page.loadSettings();
                window._page.loadLanguage();
                window._page.setActiveMenu('{Config.LastOpenedSetting}');
            ";

            await Web2.ExecuteScriptAsync(script);
        }
        catch (Exception ex)
        {
            Config.HandleException(ex);
        }
    }


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "<Pending>")]
    protected override async Task OnWeb2MessageReceivedAsync(Web2MessageReceivedEventArgs e)
    {
        await base.OnWeb2MessageReceivedAsync(e);

        // General events
        #region General events
        if (e.Name.Equals("BtnOK", StringComparison.Ordinal))
        {
            ApplySettings(e.Data);
            Close();
        }
        else if (e.Name.Equals("BtnApply", StringComparison.Ordinal))
        {
            ApplySettings(e.Data);
        }
        else if (e.Name.Equals("BtnCancel", StringComparison.Ordinal))
        {
            Close();
        }
        else if (e.Name.Equals("LnkHelp", StringComparison.Ordinal))
        {
            _ = BHelper.OpenUrlAsync("https://imageglass.org/docs", $"from_setting_{Config.LastOpenedSetting}");
        }
        else if (e.Name.Equals("LnkResetSettings", StringComparison.Ordinal))
        {
            FrmMain.IG_OpenQuickSetupDialog();
        }
        #endregion // General events


        // Sidebar
        #region Sidebar
        // sidebar tab changed
        else if (e.Name.Equals("Sidebar_Changed", StringComparison.Ordinal))
        {
            Config.LastOpenedSetting = e.Data;
        }
        #endregion // Sidebar


        // Tab General
        #region Tab General
        else if (e.Name.Equals("Lnk_StartupDir", StringComparison.Ordinal))
        {
            BHelper.OpenFilePath(e.Data);
        }
        else if (e.Name.Equals("Lnk_ConfigDir", StringComparison.Ordinal))
        {
            BHelper.OpenFilePath(e.Data);
        }
        else if (e.Name.Equals("Lnk_UserConfigFile", StringComparison.Ordinal))
        {
            _ = OpenUserConfigFileAsync(e.Data);
        }
        else if (e.Name.Equals("Btn_EnableStartupBoost", StringComparison.Ordinal))
        {
            _ = SetStartupBoostAsync(true);
        }
        else if (e.Name.Equals("Btn_DisableStartupBoost", StringComparison.Ordinal))
        {
            _ = SetStartupBoostAsync(false);
        }
        else if (e.Name.Equals("Lnk_OpenStartupAppsSetting", StringComparison.Ordinal))
        {
            _ = BHelper.OpenUrlAsync("ms-settings:startupapps");
        }
        #endregion // Tab General


        // Tab Image
        #region Tab Image
        else if (e.Name.Equals("Btn_BrowseColorProfile", StringComparison.Ordinal))
        {
            var profileFilePath = SelectColorProfileFile();
            profileFilePath = profileFilePath.Replace("\\", "\\\\");

            if (!String.IsNullOrEmpty(profileFilePath))
            {
                Web2.PostWeb2Message(e.Name, $"\"{profileFilePath}\"");
            }
        }
        else if (e.Name.Equals("Lnk_CustomColorProfile", StringComparison.Ordinal))
        {
            BHelper.OpenFilePath(e.Data);
        }
        #endregion // Tab Image


        // Tab Toolbar
        #region Tab Toolbar
        else if (e.Name.Equals("Btn_ResetToolbarButtons", StringComparison.Ordinal))
        {
            var json = BHelper.ToJson(Local.DefaultToolbarItemIds);
            Web2.PostWeb2Message(e.Name, json);
        }
        else if (e.Name.Equals("Btn_AddCustomToolbarButton_ValidateJson_Create", StringComparison.Ordinal)
            || e.Name.Equals("Btn_AddCustomToolbarButton_ValidateJson_Edit", StringComparison.Ordinal))
        {
            var isCreate = e.Name.Equals("Btn_AddCustomToolbarButton_ValidateJson_Create", StringComparison.Ordinal);
            var isValid = true;

            try
            {
                // try parsing the json
                var btn = BHelper.ParseJson<ToolbarItemModel>(e.Data);

                if (btn.Type == ToolbarItemModelType.Button)
                {
                    var langPath = $"{nameof(FrmSettings)}.Toolbar._Errors";
                    if (string.IsNullOrWhiteSpace(btn.Id))
                    {
                        throw new ArgumentException(Config.Language[$"{langPath}._ButtonIdRequired"], nameof(btn.Id));
                    }

                    if (isCreate
                        && Config.ToolbarButtons.Any(i => i.Id.Equals(btn.Id, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new ArgumentException(ZString.Format(Config.Language[$"{langPath}._ButtonIdDuplicated"], btn.Id), nameof(btn.Id));
                    }

                    if (string.IsNullOrEmpty(btn.OnClick.Executable))
                    {
                        throw new ArgumentException(Config.Language[$"{langPath}._ButtonExecutableRequired"], nameof(btn.OnClick.Executable));
                    }
                }
            }
            catch (Exception ex)
            {
                _ = Config.ShowError(this, title: Config.Language["_._Error"], heading: ex.Message);
                isValid = false;
            }

            Web2.PostWeb2Message(e.Name, BHelper.ToJson(isValid));
        }
        #endregion // Tab Toolbar


        // Tab File type associations
        #region Tab File type associations
        else if (e.Name.Equals("Btn_OpenExtIconFolder", StringComparison.Ordinal))
        {
            var extIconDir = App.ConfigDir(PathType.Dir, Dir.ExtIcons);
            BHelper.OpenFolderPath(extIconDir);
        }
        else if (e.Name.Equals("Btn_MakeDefaultViewer", StringComparison.Ordinal))
        {
            FrmMain.IG_SetDefaultPhotoViewer();
        }
        else if (e.Name.Equals("Btn_RemoveDefaultViewer", StringComparison.Ordinal))
        {
            FrmMain.IG_RemoveDefaultPhotoViewer();
        }
        else if (e.Name.Equals("Lnk_OpenDefaultAppsSetting", StringComparison.Ordinal))
        {
            _ = BHelper.OpenUrlAsync("ms-settings:defaultapps?registeredAppUser=ImageGlass");
        }
        else if (e.Name.Equals("Btn_ResetFileFormats", StringComparison.Ordinal))
        {
            Web2.PostWeb2Message("Btn_ResetFileFormats", $"\"{Const.IMAGE_FORMATS}\"");
        }
        #endregion // Tab File type associations


        // Tab Language
        #region Tab Language
        else if (e.Name.Equals("Btn_RefreshLanguageList", StringComparison.Ordinal))
        {
            WebUI.UpdateLangListJson(true);
            var langListJson = BHelper.ToJson(WebUI.LangList);

            Web2.PostWeb2Message(e.Name, langListJson);
        }
        else if (e.Name.Equals("Lnk_InstallLanguage", StringComparison.Ordinal))
        {
            _ = InstallLanguagePackAsync();
        }
        else if (e.Name.Equals("Lnk_ExportLanguage", StringComparison.Ordinal))
        {
            _ = FrmSettings.ExportLanguagePackAsync(e.Data);
        }
        #endregion // Tab Language


        // Tab Appearance
        #region Tab Appearance
        else if (e.Name.Equals("Btn_BackgroundColor", StringComparison.Ordinal)
            || e.Name.Equals("Btn_SlideshowBackgroundColor", StringComparison.Ordinal))
        {
            var currentColor = BHelper.ColorFromHex(e.Data);
            var newColor = OpenColorPicker(currentColor);
            var hexColor = string.Empty;

            if (newColor != null)
            {
                hexColor = newColor.Value.ToHex();
            }

            Web2.PostWeb2Message(e.Name, $"\"{hexColor}\"");
        }
        else if (e.Name.Equals("Btn_InstallTheme", StringComparison.Ordinal))
        {
            _ = InstallThemeAsync();
        }
        else if (e.Name.Equals("Btn_RefreshThemeList", StringComparison.Ordinal))
        {
            WebUI.UpdateThemeListJson(true);
            var themeListJson = BHelper.ToJson(WebUI.ThemeList);

            Web2.PostWeb2Message("Btn_RefreshThemeList", themeListJson);
        }
        else if (e.Name.Equals("Btn_OpenThemeFolder", StringComparison.Ordinal))
        {
            var themeDir = App.ConfigDir(PathType.Dir, Dir.Themes);
            BHelper.OpenFolderPath(themeDir);
        }
        else if (e.Name.Equals("Delete_Theme_Pack", StringComparison.Ordinal))
        {
            _ = UninstallThemeAsync(e.Data);
        }
        #endregion // Tab Appearance


        // Global
        #region Global
        // open file picker
        else if (e.Name.Equals("OpenFilePicker", StringComparison.Ordinal))
        {
            var filePaths = OpenFilePickerJson(e.Data);
            Web2.PostWeb2Message("OpenFilePicker", filePaths);
        }

        // open hotkey picker
        else if (e.Name.Equals("OpenHotkeyPicker", StringComparison.Ordinal))
        {
            var hotkey = OpenHotkeyPickerJson();
            Web2.PostWeb2Message("OpenHotkeyPicker", $"\"{hotkey}\"");
        }
        #endregion // Global

    }


    #endregion // Protected / override methods


    // Private methods
    #region Private methods

    private void ApplySettings(string dataJson)
    {
        Local.ApplySettingsFromJson(dataJson, (e) =>
        {
            if (e.HasFlag(UpdateRequests.Theme))
            {
                // load the new value of Background color setting when theme is changed
                var bgColorHex = Config.BackgroundColor.ToHex();
                _ = Web2.ExecuteScriptAsync($"""
                    _page.loadBackgroundColorConfig('{bgColorHex}');
                """);
            }
        });
    }


    private async Task InstallLanguagePackAsync()
    {
        using var o = new OpenFileDialog()
        {
            Filter = "ImageGlass language pack (*.iglang.json)|*.iglang.json",
            CheckFileExists = true,
            RestoreDirectory = true,
            Multiselect = true,
        };

        if (o.ShowDialog() != DialogResult.OK)
        {
            Web2.PostWeb2Message("Lnk_InstallLanguage", "null");
            return;
        }

        var filePathsArgs = string.Join(" ", o.FileNames.Select(f => $"\"{f}\""));
        var result = await Config.RunIgcmd(
            $"{IgCommands.INSTALL_LANGUAGES} {IgCommands.SHOW_UI} {filePathsArgs}",
            true);

        if (result == IgExitCode.Done)
        {
            WebUI.UpdateLangListJson(true);
            var langListJson = BHelper.ToJson(WebUI.LangList);

            Web2.PostWeb2Message("Lnk_InstallLanguage", langListJson);
        }
    }


    private static async Task ExportLanguagePackAsync(string langFileName)
    {
        using var o = new SaveFileDialog()
        {
            Filter = "ImageGlass language pack (*.iglang.json)|*.iglang.json",
            AddExtension = true,
            OverwritePrompt = true,
            RestoreDirectory = true,
            FileName = langFileName,
        };

        if (o.ShowDialog() != DialogResult.OK) return;

        var lang = new IgLang(langFileName, App.StartUpDir(Dir.Language));
        await lang.SaveAsFileAsync(o.FileName);
    }


    public static async Task OpenUserConfigFileAsync(string filePath)
    {
        var result = await BHelper.RunExeCmd($"\"{filePath}\"", "", false, false);

        if (result == IgExitCode.Error)
        {
            _ = await BHelper.RunExeCmd("notepad", $"\"{filePath}\"", false, false);
        }
    }


    private static string SelectColorProfileFile()
    {
        using var o = new OpenFileDialog()
        {
            Filter = "Color profile|*.icc;*.icm;|All files|*.*",
            CheckFileExists = true,
            RestoreDirectory = true,
        };

        if (o.ShowDialog() != DialogResult.OK) return string.Empty;

        return o.FileName;
    }


    private async Task InstallThemeAsync()
    {
        using var o = new OpenFileDialog()
        {
            Filter = "ImageGlass theme pack (*.igtheme)|*.igtheme",
            CheckFileExists = true,
            RestoreDirectory = true,
            Multiselect = true,
        };

        if (o.ShowDialog() != DialogResult.OK)
        {
            Web2.PostWeb2Message("Btn_InstallTheme", "null");
            return;
        }

        var filePathsArgs = string.Join(" ", o.FileNames.Select(f => $"\"{f}\""));
        var result = await Config.RunIgcmd(
            $"{IgCommands.INSTALL_THEMES} {IgCommands.SHOW_UI} {filePathsArgs}",
            true);

        if (result == IgExitCode.Done)
        {
            WebUI.UpdateThemeListJson(true);
            var themeListJson = BHelper.ToJson(WebUI.ThemeList);

            Web2.PostWeb2Message("Btn_InstallTheme", themeListJson);
        }
    }


    private async Task UninstallThemeAsync(string themeDirPath)
    {
        var result = await Config.RunIgcmd(
            $"{IgCommands.UNINSTALL_THEME} {IgCommands.SHOW_UI} \"{themeDirPath}\"",
            true);

        if (result == IgExitCode.Done)
        {
            WebUI.UpdateThemeListJson(true);
            var themeListJson = BHelper.ToJson(WebUI.ThemeList);

            Web2.PostWeb2Message("Delete_Theme_Pack", themeListJson);
        }
    }


    private static Color? OpenColorPicker(Color? defaultColor = null)
    {
        using var cd = new ModernColorDialog()
        {
            StartPosition = FormStartPosition.CenterParent,
            ColorValue = defaultColor ?? Color.White,
        };

        if (cd.ShowDialog() == DialogResult.OK)
        {
            return cd.ColorValue;
        }

        return defaultColor;
    }


    /// <summary>
    /// Open file picker.
    /// </summary>
    /// <param name="jsonOptions">Options in JSON: <c>{ filter?: string, multiple?: boolean }</c></param>
    /// <returns>File paths array or null in JSON</returns>
    private static string OpenFilePickerJson(string jsonOptions)
    {
        var options = BHelper.ParseJson<ExpandoObject>(jsonOptions)
                .ToDictionary(i => i.Key, i => i.Value.ToString() ?? string.Empty);

        _ = options.TryGetValue("filter", out var filter);
        _ = options.TryGetValue("multiple", out var multiple);

        filter ??= "All files (*.*)|*.*";
        _ = bool.TryParse(multiple, out var multiSelect);


        using var o = new OpenFileDialog()
        {
            Filter = filter,
            CheckFileExists = true,
            RestoreDirectory = true,
            Multiselect = multiSelect,
        };

        if (o.ShowDialog() == DialogResult.OK)
        {
            return BHelper.ToJson(o.FileNames);
        }

        return "null";
    }


    /// <summary>
    /// Open hotkey picker, returns <c>string.Empty</c> if user cancels the dialog or does not press any key
    /// </summary>
    private string OpenHotkeyPickerJson()
    {
        using var frm = new FrmHotkeyPicker()
        {
            StartPosition = FormStartPosition.CenterParent,
            TopMost = TopMost,
        };

        if (frm.ShowDialog() == DialogResult.OK)
        {
            return frm.HotkeyValue.ToString();
        }

        return string.Empty;
    }


    /// <summary>
    /// Sets Startup Boost setting.
    /// </summary>
    private static async Task SetStartupBoostAsync(bool enable)
    {
        var cmd = enable
            ? IgCommands.SET_STARTUP_BOOST
            : IgCommands.REMOVE_STARTUP_BOOST;

        // run command and show the results
        _ = await Config.RunIgcmd($"{cmd} {IgCommands.SHOW_UI}");
    }

    #endregion // Private methods

}
