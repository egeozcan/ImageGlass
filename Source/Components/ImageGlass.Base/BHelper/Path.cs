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
using ImageGlass.Base.FileSystem;
using ImageGlass.Base.Photoing.Codecs;
using ImageGlass.Base.WinApi;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Web;
using Windows.System;

namespace ImageGlass.Base;


public partial class BHelper
{
    /// <summary>
    /// Check if the given path (file or directory) is writable. 
    /// </summary>
    /// <param name="type">Indicates if the given path is either file or directory</param>
    /// <param name="path">Full path of file or directory</param>
    public static bool CheckPathWritable(PathType type, string path)
    {
        try
        {
            // If path is file
            if (type == PathType.File)
            {
                using (File.OpenWrite(path)) { }
            }

            // if path is directory
            else
            {
                var isDirExist = Directory.Exists(path);

                if (!isDirExist)
                {
                    Directory.CreateDirectory(path);
                }

                var sampleFile = Path.Combine(path, "test_write_file.temp");

                using (File.Create(sampleFile)) { }
                File.Delete(sampleFile);

                if (!isDirExist)
                {
                    Directory.Delete(path, true);
                }
            }


            return true;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Get distinct directories list from paths list.
    /// </summary>
    public static List<string> GetDistinctDirsFromPaths(IEnumerable<string> pathList)
    {
        if (!pathList.Any()) return [];

        var hashedDirsList = new HashSet<string>();

        foreach (var path in pathList)
        {
            var pathType = CheckPath(path);
            if (pathType == PathType.Unknown) continue;

            if (pathType == PathType.Dir)
            {
                hashedDirsList.Add(path);
            }
            else
            {
                string dir;
                if (string.Equals(Path.GetExtension(path), ".lnk", StringComparison.CurrentCultureIgnoreCase))
                {
                    var shortcutPath = FileShortcutApi.GetTargetPathFromShortcut(path);
                    var shortcutPathType = CheckPath(shortcutPath);
                    if (shortcutPathType == PathType.Unknown) continue;

                    // get the DIR path of shortcut target
                    if (shortcutPathType == PathType.Dir)
                    {
                        dir = shortcutPath;
                    }
                    else
                    {
                        dir = Path.GetDirectoryName(shortcutPath) ?? "";
                    }
                }
                else
                {
                    dir = Path.GetDirectoryName(path) ?? "";
                }

                hashedDirsList.Add(dir);
            }
        }

        return [.. hashedDirsList];
    }



    /// <summary>
    /// Checks type of the path.
    /// </summary>
    public static PathType CheckPath(string path)
    {
        try
        {
            var attrs = File.GetAttributes(path);

            if (attrs.HasFlag(FileAttributes.Directory))
            {
                return PathType.Dir;
            }

            return PathType.File;
        }
        catch { }

        return PathType.Unknown;
    }


    /// <summary>
    /// Resolves a relative/protocol/link path to absolute path
    /// </summary>
    /// <param name="inputPath">A path</param>
    /// <returns></returns>
    public static string ResolvePath(string? inputPath)
    {
        if (string.IsNullOrEmpty(inputPath))
            return inputPath ?? "";

        var path = inputPath;
        const string protocol = Const.APP_PROTOCOL + ":";

        // If inputPath is URI Scheme
        if (path.StartsWith(protocol))
        {
            // Retrieve the real path
            path = Uri.UnescapeDataString(path).Remove(0, protocol.Length);
        }

        // Parse environment vars to absolute path
        path = Environment.ExpandEnvironmentVariables(path);

        if (string.Equals(Path.GetExtension(inputPath), ".lnk", StringComparison.CurrentCultureIgnoreCase))
        {
            path = FileShortcutApi.GetTargetPathFromShortcut(path);
        }

        return path;
    }


    /// <summary>
    /// Open URL in the default browser.
    /// </summary>
    public static async Task OpenUrlAsync(string? url, string campaign = "from_unknown")
    {
        if (string.IsNullOrWhiteSpace(url)) return;

        try
        {
            var ub = new UriBuilder(url);
            var queries = HttpUtility.ParseQueryString(ub.Query);
            queries["utm_source"] = "app_" + App.Version;
            queries["utm_medium"] = "app_click";
            queries["utm_campaign"] = campaign;

            ub.Query = queries.ToString();

            _ = await Launcher.LaunchUriAsync(ub.Uri);
        }
        catch { }
    }


    /// <summary>
    /// Opens file path in Explorer and selects it.
    /// </summary>
    public static void OpenFilePath(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return;

        try
        {
            ExplorerApi.OpenFolderAndSelectItem(filePath);
        }
        catch
        {
            using var proc = Process.Start("explorer.exe", $"/select,\"{filePath}\"");
        }
    }


    /// <summary>
    /// Opens the folder path in Explorer, creates the fodler path if not existed.
    /// </summary>
    public static void OpenFolderPath(string? dirPath)
    {
        if (string.IsNullOrWhiteSpace(dirPath)) return;

        try
        {
            Directory.CreateDirectory(dirPath);
        }
        catch { }

        try
        {
            using var proc = Process.Start("explorer.exe", $"\"{dirPath}\"");
        }
        catch { }
    }


    /// <summary>
    /// Delete a file
    /// </summary>
    /// <param name="filePath">Full file path to delete</param>
    /// <param name="moveToRecycleBin"><c>true</c>: Move to Recycle bin; <c>false</c>: Delete permanently</param>
    public static void DeleteFile(string filePath, bool moveToRecycleBin = true)
    {
        var option = moveToRecycleBin ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;

        try
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, option);
        }
        catch (OperationCanceledException) { }
    }



    /// <summary>
    /// Finds all files in the given directories.
    /// </summary>
    public static IEnumerable<string> FindFiles(IEnumerable<string> rootDirs,
        bool searchAllDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? postProcessFn = null)
    {
        var results = Enumerable.Empty<string>();

        Parallel.For(0, rootDirs.Count(), i =>
        {
            var filePaths = FindFiles(rootDirs.ElementAt(i),
                searchAllDirectories,
                includeHidden,
                filterFn); // skip postProcessFn

            results = Enumerable.Concat(results, filePaths);
        });


        // post processing
        if (postProcessFn != null) results = postProcessFn(results);

        return results;
    }


    /// <summary>
    /// Finds all files in the given directory.
    /// </summary>
    public static IEnumerable<string> FindFiles(string rootDir,
        bool searchAllDirectories,
        bool includeHidden,
        Predicate<string>? filterFn = null,
        Func<IEnumerable<string>, IEnumerable<string>>? postProcessFn = null)
    {
        // check attributes to skip
        var skipAttrs = FileAttributes.System;
        if (!includeHidden) skipAttrs |= FileAttributes.Hidden;


        var filePaths = Directory.EnumerateFiles(rootDir, "*", new EnumerationOptions()
        {
            IgnoreInaccessible = true,
            AttributesToSkip = skipAttrs,
            RecurseSubdirectories = searchAllDirectories,
        }).Where(path => filterFn == null ? true : filterFn(path));


        // post processing
        if (postProcessFn != null) filePaths = postProcessFn(filePaths);

        return filePaths;
    }


    /// <summary>
    /// Sort image list.
    /// </summary>
    public static IEnumerable<string> SortFilePathList(IEnumerable<string> fileList,
        ImageOrderBy orderBy, ImageOrderType orderType, bool groupByDir)
    {
        // KBR 20190605
        // Fix observed limitation: to more closely match the Windows Explorer's sort
        // order, we must sort by the target column, then by name.
        var filePathComparer = new StringNaturalComparer(orderType == ImageOrderType.Asc, true);

        // initiate directory sorter to a comparer that does nothing
        // if user wants to group by directory, we initiate the real comparer
        var identityComparer = (IComparer<string?>)Comparer<string>.Create((a, b) => 0);
        var dirPathComparer = groupByDir
            ? new StringNaturalComparer(orderType == ImageOrderType.Asc, true)
            : identityComparer;

        // KBR 20190605 Fix observed discrepancy: using UTC for create,
        // but not for write/access times

        // Sort image file
        if (orderBy == ImageOrderBy.FileSize)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => new FileInfo(f).Length)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
            else
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => new FileInfo(f).Length)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by CreationTime
        if (orderBy == ImageOrderBy.DateCreated)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => new FileInfo(f).CreationTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
            else
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => new FileInfo(f).CreationTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by Extension
        if (orderBy == ImageOrderBy.Extension)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => new FileInfo(f).Extension.ToLowerInvariant())
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
            else
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => new FileInfo(f).Extension.ToLowerInvariant())
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by LastAccessTime
        if (orderBy == ImageOrderBy.DateAccessed)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => new FileInfo(f).LastAccessTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
            else
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => new FileInfo(f).LastAccessTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by LastWriteTime
        if (orderBy == ImageOrderBy.DateModified)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => new FileInfo(f).LastWriteTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
            else
            {
                return fileList.AsParallel()
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => new FileInfo(f).LastWriteTimeUtc)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by Random
        if (orderBy == ImageOrderBy.Random)
        {
            // NOTE: ignoring the 'descending order' setting
            return fileList.AsParallel()
                .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                .ThenBy(_ => Guid.NewGuid());
        }

        // sort by DateTaken
        if (orderBy == ImageOrderBy.ExifDateTaken)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => PhotoCodec.LoadMetadata(f).ExifDateTimeOriginal)
                    .ThenBy(f => Path.GetFileName(f), new StringNaturalComparer()); // always by ASC
            }
            else
            {
                return fileList
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => PhotoCodec.LoadMetadata(f).ExifDateTimeOriginal)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }

        // sort by Rating
        if (orderBy == ImageOrderBy.ExifRating)
        {
            if (orderType == ImageOrderType.Desc)
            {
                return fileList
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenByDescending(f => PhotoCodec.LoadMetadata(f).ExifRatingPercent)
                    .ThenBy(f => Path.GetFileName(f), new StringNaturalComparer()); // always by ASC
            }
            else
            {
                return fileList
                    .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
                    .ThenBy(f => PhotoCodec.LoadMetadata(f).ExifRatingPercent)
                    .ThenBy(f => Path.GetFileName(f), filePathComparer);
            }
        }


        // sort by Name (default)
        return fileList.AsParallel()
            .OrderBy(f => Path.GetDirectoryName(f), dirPathComparer)
            .ThenBy(f => Path.GetFileName(f), filePathComparer);
    }

}
