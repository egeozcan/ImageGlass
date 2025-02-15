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
using D2Phap;

namespace ImageGlass.Base.DirectoryComparer;


public class FileFinder
{
    /// <summary>
    /// Occurs when the host is being panned.
    /// </summary>
    public event EventHandler<FilesEnumeratedEventArgs>? FilesEnumerated;


    /// <summary>
    /// Starts finding files.
    /// <para>
    ///   If <paramref name="foregroundShell"/> is not null,
    ///   it will get files from the foreground window,
    ///   otherwise from the given directories <paramref name="dirs"/>.
    /// </para>
    /// <para>Use the <see cref="FilesEnumerated"/> event to get results.</para>
    /// </summary>
    /// <param name="foregroundShell">The Shell object</param>
    /// <param name="dirs">List of directories to search for files</param>
    /// <param name="searchSubDirectories">Option to search in sub-directories</param>
    /// <param name="includeHidden">Option to include the hidden files</param>
    /// <param name="filterFn">Function to apply path filter</param>
    /// <param name="nonShellSortFn">Function to apply path sorting when Explorer file order is not detected</param>
    /// <remarks>🔴 NOTE: Must run on UI thread.</remarks>
    public void StartFindingFiles(
        ExplorerView? foregroundShell,
        IEnumerable<string> dirs,
        bool searchSubDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? nonShellSortFn = null)
    {
        // 1. get files from the foreground window
        if (foregroundShell != null)
        {
            StartFindingFiles(foregroundShell, searchSubDirectories, includeHidden, filterFn, nonShellSortFn);
            return;
        }


        // 2. get files from the given directories
        StartFindingFiles(dirs, searchSubDirectories, includeHidden, filterFn, nonShellSortFn);
    }


    // Private Methods
    #region Private Methods

    /// <summary>
    /// Finds files in the given directories.
    /// </summary>
    /// <remarks>🔴 NOTE: Must run on UI thread.</remarks>
    private void StartFindingFiles(
        IEnumerable<string> dirs,
        bool searchSubDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? nonShellSortFn = null)
    {
        // get files from the given directories
        foreach (var dirPath in dirs)
        {
            var folderShell = GetShellFolderView(dirPath, null);

            // with shell
            if (folderShell.View != null)
            {
                StartFindingFiles_WithShell(folderShell.View,
                    dirPath,
                    searchSubDirectories,
                    includeHidden,
                    filterFn,
                    nonShellSortFn);

                // dispose shell object
                folderShell.View.Dispose();
            }

            // without shell
            else
            {
                StartFindingFiles_WithDotNet(dirPath, searchSubDirectories, includeHidden, filterFn, nonShellSortFn);
            }
        }
    }


    /// <summary>
    /// Finds files from the given foreground shell object.
    /// </summary>
    /// <remarks>🔴 NOTE: Must run on UI thread.</remarks>
    private void StartFindingFiles(
        ExplorerView? foregroundShell,
        bool searchSubDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? nonShellSortFn = null)
    {
        if (foregroundShell == null) return;

        var folderShell = GetShellFolderView(null, foregroundShell);

        StartFindingFiles_WithShell(folderShell.View,
            folderShell.DirPath,
            searchSubDirectories,
            includeHidden,
            filterFn,
            nonShellSortFn);
    }


    /// <summary>
    /// Finds files in the given <see cref="ExplorerFolderView"/>.
    /// Use the <see cref="FilesEnumerated"/> event to get results.
    /// </summary>
    /// <remarks>🔴 NOTE: Must run on UI thread.</remarks>
    private void StartFindingFiles_WithShell(ExplorerFolderView? fv,
        string? rootDir,
        bool searchSubDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? nonShellSortFn = null)
    {
        // no folder view
        if (fv is null)
        {
            if (!string.IsNullOrWhiteSpace(rootDir))
            {
                StartFindingFiles_WithDotNet(rootDir, searchSubDirectories, includeHidden, filterFn, nonShellSortFn);
            }
            return;
        }


        // has folder view
        var filePaths = fv.GetItems(FolderItemViewOptions.SVGIO_FLAG_VIEWORDER)
            .Where(path =>
            {
                // ignore special folders
                if (path.StartsWith("::{", StringComparison.InvariantCultureIgnoreCase)) return false;

                // get path attributes
                var attrs = File.GetAttributes(path);

                // path is dir
                if (attrs.HasFlag(FileAttributes.Directory)) return false;

                // path is hidden
                if (!includeHidden && attrs.HasFlag(FileAttributes.Hidden)) return false;

                // custom filter
                if (filterFn != null) return filterFn(path);

                return true;
            });


        FilesEnumerated?.Invoke(this, new FilesEnumeratedEventArgs(filePaths));


        // search all sub-directories if root dir is not empty
        if (searchSubDirectories && string.IsNullOrWhiteSpace(rootDir))
        {
            // search files for the sub dirs
            // get sub folders
            var subDirList = Directory.EnumerateDirectories(rootDir, "*", new EnumerationOptions()
            {
                IgnoreInaccessible = true,
                AttributesToSkip = includeHidden
                    ? FileAttributes.System
                    : FileAttributes.System | FileAttributes.Hidden,
                RecurseSubdirectories = false,
            });

            // find files in sub folders
            StartFindingFiles(subDirList, searchSubDirectories, includeHidden, filterFn, nonShellSortFn);
        }
    }


    /// <summary>
    /// Gets the <see cref="ExplorerFolderView"/> from the given dir path.
    /// </summary>
    /// <remarks>🔴 NOTE: Must run on UI thread.</remarks>
    private static (ExplorerFolderView? View, string DirPath) GetShellFolderView(string? rootDir, ExplorerView? foregroundShell)
    {
        var folderPath = "";
        ExplorerFolderView? folderView = null;
        using var shell = new EggShell();


        // if no dir path, get the explorer's folder view where the application opened from
        if (string.IsNullOrWhiteSpace(rootDir))
        {
            if (foregroundShell?.GetTabFolderView() is ExplorerFolderView fv)
            {
                folderPath = foregroundShell.GetTabViewPath();
                folderView = fv;
            }
        }
        else if (!Path.EndsInDirectorySeparator(rootDir))
        {
            rootDir += Path.DirectorySeparatorChar;
        }


        rootDir ??= "";

        // find the folder view from the opening explorer windows
        if (folderView == null)
        {
            // find the explorer's folder view for each directory
            shell.WithOpeningWindows(ev =>
            {
                var windowPath = ev.GetTabViewPath();
                if (!Path.EndsInDirectorySeparator(windowPath))
                {
                    windowPath += Path.DirectorySeparatorChar;
                }

                // get the folder view for the input dir
                if (rootDir.Equals(windowPath, StringComparison.InvariantCultureIgnoreCase)
                    && ev.GetTabFolderView() is ExplorerFolderView fv)
                {
                    folderPath = windowPath;
                    folderView = fv;
                    return true;
                }

                return false;
            }, true);
        }


        return (folderView, folderPath);
    }


    /// <summary>
    /// Finds files in the given directory with .NET support.
    /// Use the <see cref="FilesEnumerated"/> event to get results.
    /// </summary>
    private void StartFindingFiles_WithDotNet(string rootDir,
        bool searchSubDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? sortFn = null)
    {
        // check attributes to skip
        var skipAttrs = FileAttributes.System;
        if (!includeHidden) skipAttrs |= FileAttributes.Hidden;

        var filePaths = Directory.EnumerateFiles(rootDir, "*", new EnumerationOptions()
        {
            IgnoreInaccessible = true,
            AttributesToSkip = skipAttrs,
            RecurseSubdirectories = searchSubDirectories,
        }).Where(path => filterFn == null || filterFn(path));


        // sort list
        if (sortFn != null) filePaths = sortFn(filePaths);


        FilesEnumerated?.Invoke(this, new FilesEnumeratedEventArgs(filePaths));
    }


    #endregion // Private Methods

}
